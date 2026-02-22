using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Drawing.Printing;


namespace BoardingHouse
{
    public partial class PaymentsView : UserControl
    {
        private int? _selectedRentalId = null;
        private int? _selectedPaymentId = null;
        private int? _currentRentalId = null;
        private DateTime _currentBillMonth = DateTime.MinValue;
        private int? _selectedBillingItemId = null;
        private string? _billingAmountColumnName = null;
        private string? _billingStatusEnumColumnType = null;
        private bool? _billingHasUpdatedAtColumn = null;
        public int CurrentUserId { get; set; }

        private string _receiptToPrint = "";
        private int _printLineIndex = 0;
        private readonly Font _receiptFont = new Font("Consolas", 10f);

        public PaymentsView()
        {
            InitializeComponent();
            SetupChargeListView();

            if (cbSearchBy.Items.Count > 0 && cbSearchBy.SelectedIndex < 0) cbSearchBy.SelectedIndex = 0;
            if (cbPaymentMethod.Items.Count > 0 && cbPaymentMethod.SelectedIndex < 0) cbPaymentMethod.SelectedIndex = 0;
            if (cbChargeType.Items.Count > 0 && cbChargeType.SelectedIndex < 0) cbChargeType.SelectedIndex = 0;

            if (dpBilling_Month != null)
            {
                var now = DateTime.Now;
                dpBilling_Month.Value = new DateTime(now.Year, now.Month, 1);
            }

            if (modalDialog != null) modalDialog.Visible = false;

            SetPaymentControlsEnabled(false, "Select a tenant to start.");
        }

        private int? GetSelectedBoardingHouseId()
        {
            if (cbBoardingHouses.SelectedItem is ComboBoxItem item) return item.Id;
            return null;
        }

        private DateTime GetSelectedBillMonth()
        {
            return NormalizeBillMonth(dpBilling_Month.Value);
        }

        private DateTime NormalizeBillMonth(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        // rentals.rental_id is the single source of truth for payments and billing in this screen.
        private string GetBillingAmountColumn(MySqlConnection conn, MySqlTransaction? tx = null)
        {
            if (!string.IsNullOrWhiteSpace(_billingAmountColumnName))
                return _billingAmountColumnName!;

            using var cmd = new MySqlCommand(@"
                SELECT COLUMN_NAME
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_SCHEMA = DATABASE()
                  AND TABLE_NAME = 'billing_items'
                  AND COLUMN_NAME IN ('amount_due','amount')
                ORDER BY CASE COLUMN_NAME WHEN 'amount_due' THEN 0 ELSE 1 END
                LIMIT 1;", conn, tx);

            var col = cmd.ExecuteScalar()?.ToString();
            _billingAmountColumnName = string.Equals(col, "amount_due", StringComparison.OrdinalIgnoreCase)
                ? "amount_due"
                : "amount";
            return _billingAmountColumnName!;
        }

        private bool BillingSupportsStatus(MySqlConnection conn, MySqlTransaction tx, string statusValue)
        {
            if (string.IsNullOrWhiteSpace(_billingStatusEnumColumnType))
            {
                using var cmd = new MySqlCommand(@"
                    SELECT COLUMN_TYPE
                    FROM INFORMATION_SCHEMA.COLUMNS
                    WHERE TABLE_SCHEMA = DATABASE()
                      AND TABLE_NAME = 'billing_items'
                      AND COLUMN_NAME = 'status'
                    LIMIT 1;", conn, tx);
                _billingStatusEnumColumnType = cmd.ExecuteScalar()?.ToString() ?? "";
            }

            return _billingStatusEnumColumnType
                .IndexOf("'" + statusValue + "'", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private bool BillingHasUpdatedAtColumn(MySqlConnection conn, MySqlTransaction tx)
        {
            if (_billingHasUpdatedAtColumn.HasValue)
                return _billingHasUpdatedAtColumn.Value;

            using var cmd = new MySqlCommand(@"
                SELECT COUNT(*)
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_SCHEMA = DATABASE()
                  AND TABLE_NAME = 'billing_items'
                  AND COLUMN_NAME = 'updated_at';", conn, tx);

            _billingHasUpdatedAtColumn = Convert.ToInt32(cmd.ExecuteScalar() ?? 0) > 0;
            return _billingHasUpdatedAtColumn.Value;
        }

        private bool IsRentalActive(MySqlConnection conn, MySqlTransaction tx, int rentalId)
        {
            using var cmd = new MySqlCommand(@"
                SELECT COUNT(*)
                FROM rentals
                WHERE id=@rentalId
                  AND status='ACTIVE'
                  AND (end_date IS NULL OR end_date>=CURDATE());", conn, tx);
            cmd.Parameters.AddWithValue("@rentalId", rentalId);
            return Convert.ToInt32(cmd.ExecuteScalar() ?? 0) > 0;
        }

        private bool BillingItemExistsForRentalMonth(MySqlConnection conn, MySqlTransaction tx, int rentalId, DateTime billMonth)
        {
            var normalized = NormalizeBillMonth(billMonth);
            using var cmd = new MySqlCommand(@"
                SELECT COUNT(*)
                FROM billing_items
                WHERE rental_id=@rentalId
                  AND bill_month=@billMonth
                  AND status<>'VOID';", conn, tx);
            cmd.Parameters.AddWithValue("@rentalId", rentalId);
            cmd.Parameters.AddWithValue("@billMonth", normalized.ToString("yyyy-MM-dd"));
            return Convert.ToInt32(cmd.ExecuteScalar() ?? 0) > 0;
        }

        // Recomputes billing month status from posted payments. Kept in the same transaction as posting/voiding.
        private void UpdateBillingItemStatus(MySqlConnection conn, MySqlTransaction tx, int rentalId, DateTime billMonth)
        {
            var normalized = NormalizeBillMonth(billMonth);
            string amountCol = GetBillingAmountColumn(conn, tx);

            decimal due;
            using (var cmdDue = new MySqlCommand($@"
                SELECT COALESCE(SUM({amountCol}),0)
                FROM billing_items
                WHERE rental_id=@rentalId
                  AND bill_month=@billMonth
                  AND status<>'VOID';", conn, tx))
            {
                cmdDue.Parameters.AddWithValue("@rentalId", rentalId);
                cmdDue.Parameters.AddWithValue("@billMonth", normalized.ToString("yyyy-MM-dd"));
                due = Convert.ToDecimal(cmdDue.ExecuteScalar() ?? 0m);
            }

            decimal paid;
            using (var cmdPaid = new MySqlCommand(@"
                SELECT COALESCE(SUM(amount),0)
                FROM payments
                WHERE rental_id=@rentalId
                  AND bill_month=@billMonth
                  AND status='POSTED';", conn, tx))
            {
                cmdPaid.Parameters.AddWithValue("@rentalId", rentalId);
                cmdPaid.Parameters.AddWithValue("@billMonth", normalized.ToString("yyyy-MM-dd"));
                paid = Convert.ToDecimal(cmdPaid.ExecuteScalar() ?? 0m);
            }

            string nextStatus;
            if (due > 0m && paid + 0.01m >= due)
            {
                nextStatus = "PAID";
            }
            else if (paid > 0m && BillingSupportsStatus(conn, tx, "PARTIAL"))
            {
                nextStatus = "PARTIAL";
            }
            else
            {
                nextStatus = "UNPAID";
            }

            string updateSql = BillingHasUpdatedAtColumn(conn, tx)
                ? @"
                    UPDATE billing_items
                    SET status=@newStatus, updated_at=NOW()
                    WHERE rental_id=@rentalId
                      AND bill_month=@billMonth
                      AND status<>'VOID';"
                : @"
                    UPDATE billing_items
                    SET status=@newStatus
                    WHERE rental_id=@rentalId
                      AND bill_month=@billMonth
                      AND status<>'VOID';";

            using var cmdUpdate = new MySqlCommand(updateSql, conn, tx);
            cmdUpdate.Parameters.AddWithValue("@newStatus", nextStatus);
            cmdUpdate.Parameters.AddWithValue("@rentalId", rentalId);
            cmdUpdate.Parameters.AddWithValue("@billMonth", normalized.ToString("yyyy-MM-dd"));
            cmdUpdate.ExecuteNonQuery();
        }

        private int PostPaymentToRental(int rentalId, DateTime billMonth, decimal amount, string method, string referenceNo, string remarks, DateTime paymentDate)
        {
            if (rentalId <= 0)
                throw new InvalidOperationException("Select an ACTIVE rental first.");
            if (amount <= 0m)
                throw new InvalidOperationException("Amount must be greater than zero.");

            var normalizedBillMonth = NormalizeBillMonth(billMonth);

            using (var connCheck = DbConnectionFactory.CreateConnection())
            using (var cmdActive = new MySqlCommand(@"
                SELECT COUNT(*)
                FROM rentals
                WHERE id=@rentalId
                  AND status='ACTIVE'
                  AND (end_date IS NULL OR end_date>=CURDATE());", connCheck))
            {
                cmdActive.Parameters.AddWithValue("@rentalId", rentalId);
                if (Convert.ToInt32(cmdActive.ExecuteScalar() ?? 0) <= 0)
                    throw new InvalidOperationException("Selected rental is not ACTIVE.");
            }

            // Existing app flow expects monthly rent billing row to exist; keep this behavior without duplicates.
            EnsureRentBillingItem(rentalId, normalizedBillMonth);

            using var conn = DbConnectionFactory.CreateConnection();
            using var tx = conn.BeginTransaction();

            if (!IsRentalActive(conn, tx, rentalId))
                throw new InvalidOperationException("Selected rental is not ACTIVE.");

            if (!BillingItemExistsForRentalMonth(conn, tx, rentalId, normalizedBillMonth))
                throw new InvalidOperationException("No billing item found for this month.");

            int paymentId;
            using (var cmd = new MySqlCommand(@"
                INSERT INTO payments
                (rental_id, payment_date, bill_month, amount, method, reference_no, remarks, status, received_by, created_at)
                VALUES
                (@rentalId, @payDate, @billMonth, @amount, @method, @refNo, @remarks, 'POSTED', @receivedBy, NOW());
                SELECT LAST_INSERT_ID();", conn, tx))
            {
                cmd.Parameters.AddWithValue("@rentalId", rentalId);
                cmd.Parameters.AddWithValue("@payDate", paymentDate);
                cmd.Parameters.AddWithValue("@billMonth", normalizedBillMonth.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@method", method);
                cmd.Parameters.AddWithValue("@refNo", string.IsNullOrWhiteSpace(referenceNo) ? (object)DBNull.Value : referenceNo.Trim());
                cmd.Parameters.AddWithValue("@remarks", string.IsNullOrWhiteSpace(remarks) ? (object)DBNull.Value : remarks.Trim());
                cmd.Parameters.AddWithValue("@receivedBy", CurrentUserId > 0 ? (object)CurrentUserId : 1);
                paymentId = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
            }

            UpdateBillingItemStatus(conn, tx, rentalId, normalizedBillMonth);
            tx.Commit();
            return paymentId;
        }

        private static decimal ParseMoney(string? input)
        {
            if (string.IsNullOrWhiteSpace(input)) return 0m;
            input = input.Replace("₱", "").Replace(",", "").Trim();

            if (decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out var v)) return v;
            if (decimal.TryParse(input, NumberStyles.Any, CultureInfo.CurrentCulture, out v)) return v;

            return 0m;
        }

        private void ShowInfo(string msg)
        {
            if (alerLbl != null) alerLbl.Text = msg;
        }

        private void ClearRightSide()
        {
            lvChargeList.Items.Clear();
            lvPaymentHistory.Items.Clear();

            lblTotalCharges.Text = "(N)";
            lblTotalPaid.Text = "(N)";
            lblRmngBal.Text = "(N)";
        }

        private void ClearForm(bool keepSelection = true)
        {
            txtAmount.Text = "";
            if (cbPaymentMethod.Items.Count > 0) cbPaymentMethod.SelectedIndex = 0;
            txtRefNum.Text = "";
            richtxtRemarks.Text = "";

            modalDialog.Visible = false;
            richTxtVoid.Text = "";
            lblVoidInfo.Text = "Selected Payment: (none)";
            _selectedPaymentId = null;

            if (!keepSelection)
            {
                _selectedRentalId = null;
                txtTenantName.Text = "(tenant name)";
                txtRoomName.Text = "(Room)";
                txtBoardingHouseName.Text = "(Boardinghouse)";
                ClearRightSide();
            }
        }

        private void SetupChargeListView()
        {
            lvChargeList.View = View.Details;
            lvChargeList.FullRowSelect = true;
            lvChargeList.GridLines = true;
            lvChargeList.MultiSelect = false;
            if (lvChargeList.Columns.Count == 0)
            {
                lvChargeList.Columns.Add("Charge Type", 95);
                lvChargeList.Columns.Add("Description", 200);
                lvChargeList.Columns.Add("Amount", 80);
                lvChargeList.Columns.Add("Status", 90);
            }
        }

        // ✅ NEW: enable/disable payment UI as a “lock” when fully paid
        private void SetPaymentControlsEnabled(bool enabled, string? infoMessage = null)
        {
            txtAmount.Enabled = enabled;
            cbPaymentMethod.Enabled = enabled;
            txtRefNum.Enabled = enabled;
            richtxtRemarks.Enabled = enabled;

            btnPayFullBalance.Enabled = enabled;
            paymentBtn.Enabled = enabled;
            clearFormBtn.Enabled = enabled;

            if (!string.IsNullOrWhiteSpace(infoMessage))
                ShowInfo(infoMessage);

            // If disabled, clear input to avoid accidental posting
            if (!enabled)
            {
                txtAmount.Text = "";
                txtRefNum.Text = "";
                richtxtRemarks.Text = "";
            }
        }

        // ✅ NEW: called after Billing Summary refresh to keep screen “correct”
        private void ApplyPaymentGuardAfterTotals()
        {
            if (_selectedRentalId == null)
            {
                SetPaymentControlsEnabled(false, "Select a tenant to start.");
                return;
            }

            decimal remaining = ParseMoney(lblRmngBal.Text);

            if (remaining <= 0m)
            {
                // Fully paid -> lock payment actions
                SetPaymentControlsEnabled(false, "This billing month is fully paid. (Payments locked)");
            }
            else
            {
                // Still has balance -> allow payment
                SetPaymentControlsEnabled(true, "Ready. Enter payment details then click Confirm Payment.");
            }
        }

        // =========================
        // Data Loading
        // =========================
        private void LoadBoardingHouses()
        {
            cbBoardingHouses.Items.Clear();

            using var conn = DbConnectionFactory.CreateConnection();
            using var cmd = new MySqlCommand(@"
                SELECT id, name
                FROM boarding_houses
                WHERE status='ACTIVE'
                ORDER BY name;", conn);

            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                cbBoardingHouses.Items.Add(new ComboBoxItem(
                    Convert.ToInt32(r["id"]),
                    r["name"]?.ToString() ?? "(Unnamed)"
                ));
            }

            if (cbBoardingHouses.Items.Count > 0)
                cbBoardingHouses.SelectedIndex = 0;
        }

        private void RefreshTenantGrid()
        {
            var bhId = GetSelectedBoardingHouseId();
            if (bhId == null)
            {
                dgvDataSource.DataSource = null;
                return;
            }

            var q = (txtSearch.Text ?? "").Trim();
            var searchBy = cbSearchBy.SelectedItem?.ToString() ?? "Tenant Name";

            var where = new StringBuilder();
            where.Append(" WHERE r.status='ACTIVE' AND (r.end_date IS NULL OR r.end_date >= CURDATE()) AND bh.id=@bhId ");

            if (!string.IsNullOrWhiteSpace(q))
            {
                if (searchBy == "Room")
                {
                    where.Append(" AND rm.room_no LIKE @q ");
                }
                else if (searchBy == "Contact No")
                {
                    where.Append(" AND o.contact_no LIKE @q ");
                }
                else // Tenant Name
                {
                    where.Append(" AND (o.full_name LIKE @q OR o.firstname LIKE @q OR o.lastname LIKE @q) ");
                }
            }

            using var conn = DbConnectionFactory.CreateConnection();
            using var cmd = new MySqlCommand($@"
                SELECT
                    r.id AS RentalId,
                    o.id AS OccupantId,
                    COALESCE(NULLIF(TRIM(o.full_name),''), TRIM(CONCAT(
                        COALESCE(o.lastname,''), ', ', COALESCE(o.firstname,''),
                        CASE WHEN o.middlename IS NULL OR o.middlename='' THEN '' ELSE CONCAT(' ', o.middlename) END
                    ))) AS Tenant,
                    o.contact_no AS ContactNo,
                    rm.room_no AS Room,
                    rm.room_type AS RoomType,
                    r.monthly_rate AS MonthlyRate
                FROM rentals r
                INNER JOIN occupants o ON o.id = r.occupant_id
                INNER JOIN rooms rm ON rm.id = r.room_id
                INNER JOIN boarding_houses bh ON bh.id = rm.boarding_house_id
                {where}
                ORDER BY Tenant;", conn);

            cmd.Parameters.AddWithValue("@bhId", bhId.Value);
            if (!string.IsNullOrWhiteSpace(q))
                cmd.Parameters.AddWithValue("@q", "%" + q + "%");

            var dt = new DataTable();
            using (var ad = new MySqlDataAdapter(cmd))
            {
                ad.Fill(dt);
            }

            dgvDataSource.DataSource = dt;

            // Hide internal keys
            if (dgvDataSource.Columns.Contains("RentalId")) dgvDataSource.Columns["RentalId"].Visible = false;
            if (dgvDataSource.Columns.Contains("OccupantId")) dgvDataSource.Columns["OccupantId"].Visible = false;
        }

        private decimal GetTotalPaidForMonth(int rentalId, DateTime billMonth)
        {
            var normalized = NormalizeBillMonth(billMonth);

            using var conn = DbConnectionFactory.CreateConnection();
            using var cmd = new MySqlCommand(@"
                SELECT COALESCE(SUM(amount),0)
                FROM payments
                WHERE rental_id=@rentalId
                  AND bill_month=@billMonth
                  AND status='POSTED';", conn);

            cmd.Parameters.AddWithValue("@rentalId", rentalId);
            cmd.Parameters.AddWithValue("@billMonth", normalized.ToString("yyyy-MM-dd"));

            return Convert.ToDecimal(cmd.ExecuteScalar());
        }

        private void LoadBillingSummary(int rentalId)
        {
            var billMonth = GetSelectedBillMonth();
            EnsureRentBillingItem(rentalId, billMonth);
            var items = LoadBillingItems(rentalId, billMonth);
            decimal totalCharges = items.Sum(item => item.Amount);
            var totalPaid = GetTotalPaidForMonth(rentalId, billMonth);
            var remaining = Math.Max(0m, totalCharges - totalPaid);

            lblTotalCharges.Text = "₱ " + totalCharges.ToString("N2");
            lblTotalPaid.Text = "₱ " + totalPaid.ToString("N2");
            lblRmngBal.Text = "₱ " + remaining.ToString("N2");

            // nice default
            if (remaining > 0) txtAmount.Text = remaining.ToString("N2");

            // ✅ NEW: lock/unlock payment controls after totals are computed
            ApplyPaymentGuardAfterTotals();
        }

        private List<BillingItem> LoadBillingItems(int rentalId, DateTime billMonth, bool skipVoids = true)
        {
            var normalized = NormalizeBillMonth(billMonth);
            lvChargeList.Items.Clear();

            using var conn = DbConnectionFactory.CreateConnection();
            string amountCol = GetBillingAmountColumn(conn);
            using var cmd = new MySqlCommand($@"
                SELECT id, charge_type, description, {amountCol} AS amount, status
                FROM billing_items
                WHERE rental_id=@rentalId AND bill_month=@billMonth
                ORDER BY FIELD(charge_type,'RENT','ELECTRIC','WATER','INTERNET','PENALTY','OTHER'), id;", conn);

            cmd.Parameters.AddWithValue("@rentalId", rentalId);
            cmd.Parameters.AddWithValue("@billMonth", normalized.ToString("yyyy-MM-dd"));

            var items = new List<BillingItem>();

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var chargeType = reader["charge_type"]?.ToString() ?? "";
                var desc = reader["description"]?.ToString() ?? "";
                var amt = Convert.ToDecimal(reader["amount"]);
                var status = reader["status"]?.ToString() ?? "";

                if (skipVoids && string.Equals(status, "VOID", StringComparison.OrdinalIgnoreCase))
                    continue;

                var item = new BillingItem(Convert.ToInt32(reader["id"]), chargeType, desc, amt, status);

                var li = new ListViewItem(item.ChargeType);
                li.SubItems.Add(item.Description);
                li.SubItems.Add(item.Amount.ToString("N2"));
                li.SubItems.Add(item.Status);
                li.Tag = item.Id;
                lvChargeList.Items.Add(li);

                items.Add(item);
            }

            return items;
        }

        private void EnsureRentBillingItem(int rentalId, DateTime billMonth)
        {
            var normalized = NormalizeBillMonth(billMonth);

            using var conn = DbConnectionFactory.CreateConnection();
            using var tx = conn.BeginTransaction();
            string amountCol = GetBillingAmountColumn(conn, tx);

            using (var cmdCheck = new MySqlCommand(@"
                SELECT id
                FROM billing_items
                WHERE rental_id=@rentalId AND bill_month=@billMonth AND charge_type='RENT'
                LIMIT 1 FOR UPDATE;", conn, tx))
            {
                cmdCheck.Parameters.AddWithValue("@rentalId", rentalId);
                cmdCheck.Parameters.AddWithValue("@billMonth", normalized.ToString("yyyy-MM-dd"));

                using var reader = cmdCheck.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                    tx.Commit();
                    return;
                }
            }

            decimal monthlyRate = 0m;
            using (var cmdRate = new MySqlCommand(@"SELECT monthly_rate FROM rentals WHERE id=@rentalId LIMIT 1;", conn, tx))
            {
                cmdRate.Parameters.AddWithValue("@rentalId", rentalId);
                var rateObj = cmdRate.ExecuteScalar();
                if (rateObj != null && rateObj != DBNull.Value)
                    monthlyRate = Convert.ToDecimal(rateObj);
            }

            if (monthlyRate < 0) monthlyRate = 0m;

            using (var cmdInsert = new MySqlCommand($@"
                INSERT INTO billing_items
                (rental_id, bill_month, charge_type, description, {amountCol}, status, created_at)
                VALUES
                (@rentalId, @billMonth, 'RENT', @description, @amount, 'UNPAID', NOW());", conn, tx))
            {
                cmdInsert.Parameters.AddWithValue("@rentalId", rentalId);
                cmdInsert.Parameters.AddWithValue("@billMonth", normalized.ToString("yyyy-MM-dd"));
                cmdInsert.Parameters.AddWithValue("@description", "Monthly Rent");
                cmdInsert.Parameters.AddWithValue("@amount", monthlyRate);
                cmdInsert.ExecuteNonQuery();
            }

            tx.Commit();
        }

        private void ResetBillingItemForm()
        {
            _selectedBillingItemId = null;
            if (cbChargeType.Items.Count > 0)
                cbChargeType.SelectedIndex = 0;
            txtChargeDescription.Text = "";
            nudChargeAmount.Value = nudChargeAmount.Minimum;
            lvChargeList.SelectedItems.Clear();
        }

        private void LoadPaymentHistory(int rentalId)
        {
            lvPaymentHistory.Items.Clear();
            _selectedPaymentId = null;
            lblVoidInfo.Text = "Selected Payment: (none)";

            using var conn = DbConnectionFactory.CreateConnection();
            using var cmd = new MySqlCommand(@"
                SELECT id, payment_date, bill_month, amount, method, reference_no, status
                FROM payments
                WHERE rental_id=@rentalId
                ORDER BY payment_date DESC;", conn);

            cmd.Parameters.AddWithValue("@rentalId", rentalId);

            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                var id = Convert.ToInt32(r["id"]);
                var payDate = Convert.ToDateTime(r["payment_date"]);
                var bm = Convert.ToDateTime(r["bill_month"]);
                var amt = Convert.ToDecimal(r["amount"]);
                var method = r["method"]?.ToString() ?? "";
                var refNo = r["reference_no"]?.ToString() ?? "";
                var status = r["status"]?.ToString() ?? "";

                var li = new ListViewItem(payDate.ToString("yyyy-MM-dd"));
                li.SubItems.Add(bm.ToString("MMM yyyy"));
                li.SubItems.Add(amt.ToString("N2"));
                li.SubItems.Add(method);
                li.SubItems.Add(refNo);
                li.SubItems.Add(status);
                li.Tag = id;

                lvPaymentHistory.Items.Add(li);
            }
        }

        // =========================
        // Event handlers (KEEP your empty ones)
        // =========================

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // intentionally empty (accidental double-click)
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // intentionally empty
        }

        private void label9_Click(object sender, EventArgs e)
        {
            // intentionally empty
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            // intentionally empty
        }

        private void label10_Click(object sender, EventArgs e)
        {
            // intentionally empty
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            // intentionally empty
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            // intentionally empty
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            modalDialog.Visible = false;
            richTxtVoid.Text = "";
        }

        private void richtxtRemarks_TextChanged(object sender, EventArgs e)
        {
            // intentionally empty
        }

        private void txtRefNum_TextChanged(object sender, EventArgs e)
        {
            // intentionally empty
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // optional: comment out if you don't want live search
            // RefreshTenantGrid();
        }

        private void topBar_Paint(object sender, PaintEventArgs e)
        {
            // intentionally empty
        }

        private void PaymentsView_Load(object sender, EventArgs e)
        {
            try
            {
                // set defaults if needed
                if (cbSearchBy.Items.Count > 0 && cbSearchBy.SelectedIndex < 0) cbSearchBy.SelectedIndex = 0;
                if (cbPaymentMethod.Items.Count > 0 && cbPaymentMethod.SelectedIndex < 0) cbPaymentMethod.SelectedIndex = 0;

                LoadBoardingHouses();
                RefreshTenantGrid();
                ClearForm(keepSelection: false);

                SetPaymentControlsEnabled(false, "Select a tenant to view billing summary and payment history.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modalDialog_Paint(object sender, PaintEventArgs e)
        {
            // intentionally empty
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            try
            {
                RefreshTenantGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clearSearchBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            txtSearch.Text = "";
            if (cbSearchBy.Items.Count > 0) cbSearchBy.SelectedIndex = 0;

            try { RefreshTenantGrid(); }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Clear Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbBoardingHouses_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RefreshTenantGrid();
                ClearForm(keepSelection: false);
                SetPaymentControlsEnabled(false, "Boarding house changed. Select a tenant.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Boarding House Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dpBilling_Month_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                // normalize to first day of month
                var bm = GetSelectedBillMonth();
                if (dpBilling_Month.Value.Day != 1) dpBilling_Month.Value = bm;

                if (_selectedRentalId != null)
                {
                    EnsureRentBillingItem(_selectedRentalId.Value, bm);
                    LoadBillingSummary(_selectedRentalId.Value);
                    LoadPaymentHistory(_selectedRentalId.Value);
                    // ApplyPaymentGuardAfterTotals() is already called inside LoadBillingSummary
                }
                else
                {
                    SetPaymentControlsEnabled(false, "Select a tenant to start.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Billing Month Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            // intentionally empty / optional
        }

        private void dgvDataSource_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // intentionally empty
        }

        private void dgvDataSource_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SoundClicked.itemClicked();
            if (e.RowIndex < 0) return;

            try
            {
                var row = dgvDataSource.Rows[e.RowIndex];
                if (row == null) return;

                _selectedRentalId = Convert.ToInt32(row.Cells["RentalId"].Value);

                txtTenantName.Text = row.Cells["Tenant"].Value?.ToString() ?? "(tenant name)";
                txtRoomName.Text = row.Cells["Room"].Value?.ToString() ?? "(Room)";
                txtBoardingHouseName.Text = (cbBoardingHouses.SelectedItem as ComboBoxItem)?.Text ?? "(Boardinghouse)";

                ClearForm(keepSelection: true);
                EnsureRentBillingItem(_selectedRentalId.Value, GetSelectedBillMonth());
                LoadBillingSummary(_selectedRentalId.Value);
                LoadPaymentHistory(_selectedRentalId.Value);

                // ApplyPaymentGuardAfterTotals() is called inside LoadBillingSummary
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Select Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPayFullBalance_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            // ✅ Respect the guard: only fill when remaining > 0
            var remaining = ParseMoney(lblRmngBal.Text);
            if (remaining <= 0m)
            {
                ShowInfo("This billing month is already fully paid.");
                return;
            }

            txtAmount.Text = remaining.ToString("N2");
        }

        private void cbPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            // intentionally small / optional
        }

        private void clearFormBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            ClearForm(keepSelection: true);

            if (_selectedRentalId != null)
                LoadBillingSummary(_selectedRentalId.Value); // will apply guard again
            else
                SetPaymentControlsEnabled(false, "Select a tenant to start.");
        }

        private void paymentBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            if (_selectedRentalId == null)
            {
                MessageBox.Show("Please select a tenant first.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ NEW: hard guard - block continuous/over payments
            var currentRemaining = ParseMoney(lblRmngBal.Text);
            if (currentRemaining <= 0m)
            {
                MessageBox.Show("This billing month is already fully paid. Payment is locked.", "Fully Paid",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ApplyPaymentGuardAfterTotals();
                return;
            }

            var amount = ParseMoney(txtAmount.Text);
            if (amount <= 0)
            {
                MessageBox.Show("Enter a valid amount.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ NEW: prevent overpayment
            if (amount > currentRemaining)
            {
                MessageBox.Show(
                    $"Overpayment not allowed.\n\nRemaining Balance: ₱ {currentRemaining:N2}\nEntered Amount: ₱ {amount:N2}",
                    "Overpayment",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtAmount.Text = currentRemaining.ToString("N2");
                return;
            }

            var method = cbPaymentMethod.SelectedItem?.ToString() ?? "CASH";
            var refNo = (txtRefNum.Text ?? "").Trim();
            var remarks = (richtxtRemarks.Text ?? "").Trim();

            if (method != "CASH" && string.IsNullOrWhiteSpace(refNo))
            {
                MessageBox.Show("Reference No. is required for non-cash payments.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var billMonth = GetSelectedBillMonth();

            try
            {
                int paymentId = PostPaymentToRental(
                    _selectedRentalId.Value,
                    billMonth,
                    amount,
                    method,
                    refNo,
                    remarks,
                    DateTime.Now);

                MessageBox.Show($"Payment posted. (Payment ID: {paymentId})", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadBillingSummary(_selectedRentalId.Value);
                LoadPaymentHistory(_selectedRentalId.Value);
                ClearForm(keepSelection: true);

                // After refresh, guard is applied inside LoadBillingSummary
                ShowInfo("Payment posted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Payment Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvChargeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvChargeList.SelectedItems.Count == 0)
            {
                _selectedBillingItemId = null;
                return;
            }

            var selected = lvChargeList.SelectedItems[0];
            _selectedBillingItemId = selected.Tag is int id ? id : null;

            var chargeType = selected.Text;
            if (cbChargeType.Items.Contains(chargeType))
                cbChargeType.SelectedItem = chargeType;
            else if (cbChargeType.Items.Count > 0)
                cbChargeType.SelectedIndex = 0;

            txtChargeDescription.Text = selected.SubItems[1].Text;

            var amountText = selected.SubItems[2].Text;
            if (!decimal.TryParse(amountText, NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out var parsed) &&
                !decimal.TryParse(amountText, NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out parsed))
            {
                parsed = nudChargeAmount.Minimum;
            }

            parsed = Math.Min(nudChargeAmount.Maximum, Math.Max(nudChargeAmount.Minimum, parsed));
            nudChargeAmount.Value = parsed;
        }

        private void btnAddOrUpdateCharge_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            if (_currentRentalId == null)
            {
                MessageBox.Show("Open the billing items editor after selecting a tenant.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var chargeType = cbChargeType.SelectedItem?.ToString() ?? "";
            var description = (txtChargeDescription.Text ?? "").Trim();
            var amount = nudChargeAmount.Value;

            if (amount <= 0)
            {
                MessageBox.Show("Amount must be greater than zero.", "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if ((chargeType == "OTHER" || chargeType == "PENALTY") && string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Description is required for PENALTY and OTHER charges.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var billMonth = _currentBillMonth == DateTime.MinValue ? NormalizeBillMonth(dpBilling_Month.Value) : _currentBillMonth;
            var normalizedString = NormalizeBillMonth(billMonth).ToString("yyyy-MM-dd");

            bool saved = false;

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                using var tx = conn.BeginTransaction();
                string amountCol = GetBillingAmountColumn(conn, tx);

                int? existingId = null;
                string existingStatus = "";

                using (var cmdCheck = new MySqlCommand(@"
                    SELECT id, status
                    FROM billing_items
                    WHERE rental_id=@rentalId AND bill_month=@billMonth AND charge_type=@chargeType
                    LIMIT 1 FOR UPDATE;", conn, tx))
                {
                    cmdCheck.Parameters.AddWithValue("@rentalId", _currentRentalId.Value);
                    cmdCheck.Parameters.AddWithValue("@billMonth", normalizedString);
                    cmdCheck.Parameters.AddWithValue("@chargeType", chargeType);

                    using var reader = cmdCheck.ExecuteReader();
                    if (reader.Read())
                    {
                        existingId = Convert.ToInt32(reader["id"]);
                        existingStatus = reader["status"]?.ToString() ?? "";
                    }
                }

                if (string.Equals(existingStatus, "PAID", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Paid charges cannot be edited.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tx.Rollback();
                    return;
                }

                var descriptionParam = string.IsNullOrWhiteSpace(description) ? (object)DBNull.Value : description;

                if (existingId == null)
                {
                    using var cmdInsert = new MySqlCommand($@"
                        INSERT INTO billing_items
                        (rental_id, bill_month, charge_type, description, {amountCol}, status, created_at)
                        VALUES
                        (@rentalId, @billMonth, @chargeType, @description, @amount, 'UNPAID', NOW());", conn, tx);
                    cmdInsert.Parameters.AddWithValue("@rentalId", _currentRentalId.Value);
                    cmdInsert.Parameters.AddWithValue("@billMonth", normalizedString);
                    cmdInsert.Parameters.AddWithValue("@chargeType", chargeType);
                    cmdInsert.Parameters.AddWithValue("@description", descriptionParam);
                    cmdInsert.Parameters.AddWithValue("@amount", amount);
                    cmdInsert.ExecuteNonQuery();
                }
                else
                {
                    using var cmdUpdate = new MySqlCommand($@"
                        UPDATE billing_items
                        SET description=@description, {amountCol}=@amount
                        WHERE id=@id AND status!='PAID';", conn, tx);
                    cmdUpdate.Parameters.AddWithValue("@id", existingId.Value);
                    cmdUpdate.Parameters.AddWithValue("@description", descriptionParam);
                    cmdUpdate.Parameters.AddWithValue("@amount", amount);

                    var updated = cmdUpdate.ExecuteNonQuery();
                    if (updated == 0)
                    {
                        MessageBox.Show("Unable to update the selected billing item. It might already be paid.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tx.Rollback();
                        return;
                    }
                }

                tx.Commit();
                saved = true;
                ShowInfo("Billing item saved.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Billing Item Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                if (saved && _currentRentalId != null)
                {
                    LoadBillingSummary(_currentRentalId.Value);
                    ResetBillingItemForm();
                }
            }
        }

        private void btnVoidCharge_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            if (_currentRentalId == null || _selectedBillingItemId == null)
            {
                MessageBox.Show("Select a billing item to void.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lvChargeList.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select a billing item to void.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selected = lvChargeList.SelectedItems[0];
            var status = selected.SubItems[3].Text;
            var chargeType = selected.Text;

            if (string.Equals(status, "PAID", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Paid charges cannot be voided.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.Equals(chargeType, "RENT", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Rent charges cannot be voided.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool voided = false;
            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                using var cmd = new MySqlCommand(@"
                    UPDATE billing_items
                    SET status='VOID'
                    WHERE id=@id AND status!='PAID';", conn);
                cmd.Parameters.AddWithValue("@id", _selectedBillingItemId.Value);

                var affected = cmd.ExecuteNonQuery();
                if (affected == 0)
                {
                    MessageBox.Show("Unable to void the selected billing item.", "Void Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                voided = true;
                ShowInfo("Billing item voided.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Billing Item Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                if (voided && _currentRentalId != null)
                {
                    LoadBillingSummary(_currentRentalId.Value);
                    ResetBillingItemForm();
                }
            }
        }

        private void lvPaymentHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvPaymentHistory.SelectedItems.Count == 0)
            {
                _selectedPaymentId = null;
                lblVoidInfo.Text = "Selected Payment: (none)";
                return;
            }

            _selectedPaymentId = (int)lvPaymentHistory.SelectedItems[0].Tag;
            lblVoidInfo.Text = $"Selected Payment: #{_selectedPaymentId}";
        }

        private void btnVoidSelected_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            if (_selectedRentalId == null)
            {
                MessageBox.Show("Select a tenant first.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_selectedPaymentId == null)
            {
                MessageBox.Show("Select a payment from Payment History first.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            modalDialog.Visible = true;
            lblVoidInfo.Text = $"Selected Payment: #{_selectedPaymentId}";
            richTxtVoid.Focus();
        }

        private void PreviewReceipt(string receiptText)
        {
            _receiptToPrint = receiptText;
            _printLineIndex = 0;

            var doc = new PrintDocument();
            doc.DocumentName = "Payment Receipt";

            int lineCount = receiptText.Split('\n').Length;
            int lineHeight = 20;
            int padding = 40;

            int paperHeight = (lineCount * lineHeight) + padding;


            doc.DefaultPageSettings.PaperSize = new PaperSize("Receipt80mm", 315, paperHeight);
            doc.DefaultPageSettings.Margins = new Margins(10, 10, 10, 10);

            doc.PrintPage += Receipt_PrintPage;

            using (var preview = new PrintPreviewDialog())
            {
                preview.Document = doc;
                preview.Width = 600;
                preview.Height = 700;

                preview.ShowDialog();
            }

            doc.PrintPage -= Receipt_PrintPage;
            doc.Dispose();
        }


        private void PrintReceipt(string receiptText)
        {
            _receiptToPrint = receiptText;
            _printLineIndex = 0;

            using (var doc = new PrintDocument())
            {
                doc.DocumentName = "Payment Receipt";
                doc.DefaultPageSettings.PaperSize = new PaperSize("Receipt80mm", 315, 800);
                doc.DefaultPageSettings.Margins = new Margins(10, 10, 10, 10);
                doc.PrintPage += Receipt_PrintPage;

                using (var dlg = new PrintDialog())
                {
                    dlg.Document = doc;

                    dlg.AllowSomePages = false;
                    dlg.UseEXDialog = true;

                    if (dlg.ShowDialog() == DialogResult.OK)
                        doc.Print();
                }

                doc.PrintPage -= Receipt_PrintPage;
            }
        }


        private void Receipt_PrintPage(object sender, PrintPageEventArgs e)
        {
            using var font = new Font("Consolas", 10f);
            using var brush = Brushes.Black;

            var rect = new RectangleF(
                e.MarginBounds.Left,
                e.MarginBounds.Top + 20,
                e.MarginBounds.Width,
                e.MarginBounds.Height - 20
            );

            var fmt = new StringFormat
            {
                Alignment = StringAlignment.Center,   
                LineAlignment = StringAlignment.Near   
            };

            e.Graphics.DrawString(_receiptToPrint, font, brush, rect, fmt);

            e.HasMorePages = false;
        }


        private void btnViewReceipt_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            if (_selectedPaymentId == null)
            {
                MessageBox.Show("Select a payment first.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                using var cmd = new MySqlCommand(@"
                    SELECT
                        p.id, p.payment_date, p.bill_month, p.amount, p.method, p.reference_no, p.remarks, p.status,
                        COALESCE(NULLIF(TRIM(o.full_name),''), TRIM(CONCAT(
                            COALESCE(o.lastname,''), ', ', COALESCE(o.firstname,''),
                            CASE WHEN o.middlename IS NULL OR o.middlename='' THEN '' ELSE CONCAT(' ', o.middlename) END
                        ))) AS tenant_name,
                        rm.room_no,
                        bh.name AS bh_name
                    FROM payments p
                    INNER JOIN rentals r ON r.id = p.rental_id
                    INNER JOIN occupants o ON o.id = r.occupant_id
                    INNER JOIN rooms rm ON rm.id = r.room_id
                    INNER JOIN boarding_houses bh ON bh.id = rm.boarding_house_id
                    WHERE p.id=@id;", conn);

                cmd.Parameters.AddWithValue("@id", _selectedPaymentId.Value);

                using var r = cmd.ExecuteReader();
                if (!r.Read())
                {
                    MessageBox.Show("Payment not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var sb = new StringBuilder();
                sb.AppendLine("=== PAYMENT RECEIPT ===");
                sb.AppendLine($"Receipt #: {Convert.ToInt32(r["id"])}");
                sb.AppendLine($"Status   : {r["status"]}");
                sb.AppendLine($"Date     : {Convert.ToDateTime(r["payment_date"]):yyyy-MM-dd HH:mm}");
                sb.AppendLine($"Bill Month: {Convert.ToDateTime(r["bill_month"]):MMMM yyyy}");
                sb.AppendLine();
                sb.AppendLine($"Boarding House: {r["bh_name"]}");
                sb.AppendLine($"Tenant       : {r["tenant_name"]}");
                sb.AppendLine($"Room         : {r["room_no"]}");
                sb.AppendLine();
                sb.AppendLine($"Amount : ₱ {Convert.ToDecimal(r["amount"]):N2}");
                sb.AppendLine($"Method : {r["method"]}");
                sb.AppendLine($"Ref No : {((r["reference_no"] == DBNull.Value) ? "-" : r["reference_no"])}");
                sb.AppendLine($"Remarks: {((r["remarks"] == DBNull.Value) ? "-" : r["remarks"])}");

                PreviewReceipt(sb.ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Receipt Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void voidConfirmBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            if (_selectedRentalId == null || _selectedPaymentId == null) return;

            var reason = (richTxtVoid.Text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(reason))
            {
                MessageBox.Show("Please enter a void reason.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                using var tx = conn.BeginTransaction();

                // Lock & read payment
                DateTime billMonth;
                string status;

                using (var cmdRead = new MySqlCommand(@"
                    SELECT bill_month, status
                    FROM payments
                    WHERE id=@id FOR UPDATE;", conn, tx))
                {
                    cmdRead.Parameters.AddWithValue("@id", _selectedPaymentId.Value);

                    using var r = cmdRead.ExecuteReader();
                    if (!r.Read())
                    {
                        MessageBox.Show("Payment not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    billMonth = Convert.ToDateTime(r["bill_month"]);
                    status = r["status"]?.ToString() ?? "";
                    r.Close();
                }
                billMonth = NormalizeBillMonth(billMonth);

                if (status == "VOID")
                {
                    MessageBox.Show("This payment is already VOID.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tx.Rollback();
                    modalDialog.Visible = false;
                    return;
                }

                // Void payment
                using (var cmdVoid = new MySqlCommand(@"
                    UPDATE payments
                    SET status='VOID',
                        remarks = CONCAT(IFNULL(remarks,''), ' | VOID: ', @reason)
                    WHERE id=@id;", conn, tx))
                {
                    cmdVoid.Parameters.AddWithValue("@id", _selectedPaymentId.Value);
                    cmdVoid.Parameters.AddWithValue("@reason", reason);
                    cmdVoid.ExecuteNonQuery();
                }

                // Recompute month status from remaining POSTED payments after void.
                UpdateBillingItemStatus(conn, tx, _selectedRentalId.Value, billMonth);

                tx.Commit();

                MessageBox.Show("Payment voided.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                modalDialog.Visible = false;
                richTxtVoid.Text = "";
                _selectedPaymentId = null;

                LoadBillingSummary(_selectedRentalId.Value);
                LoadPaymentHistory(_selectedRentalId.Value);

                ShowInfo("Payment voided.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Void Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================
        // ComboBox item helper
        // =========================
        private sealed class ComboBoxItem
        {
            public int Id { get; }
            public string Text { get; }

            public ComboBoxItem(int id, string text)
            {
                Id = id;
                Text = text;
            }

            public override string ToString() => Text;
        }

        private sealed record BillingItem(int Id, string ChargeType, string Description, decimal Amount, string Status);

        private void manageBillItemsBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            if (_selectedRentalId == null)
            {
                MessageBox.Show("Select a tenant first to manage billing items.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _currentRentalId = _selectedRentalId;
            _currentBillMonth = NormalizeBillMonth(dpBilling_Month.Value);

            try
            {
                EnsureRentBillingItem(_currentRentalId.Value, _currentBillMonth);
                LoadBillingSummary(_currentRentalId.Value);
                ResetBillingItemForm();
                billingItemsModal.Visible = true;
                billingItemsModal.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Billing Items Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void closeBillingItemsBtn_Click(object sender, EventArgs e)
        {
            billingItemsModal.Visible = false;
            ResetBillingItemForm();
            _currentRentalId = null;
            _currentBillMonth = DateTime.MinValue;
        }

        public void OpenPaymentsFromRental(int rentalId, bool autoSelectFirstRoom)
        {
            if (rentalId <= 0) return;

            try
            {
                if (cbBoardingHouses.Items.Count == 0)
                    LoadBoardingHouses();
                var bhId = GetBoardingHouseIdByRental(rentalId);
                if (bhId != null)
                {
                    SelectBoardingHouseInCombo(bhId.Value);
                }

                RefreshTenantGrid();

                SelectRentalRowInGrid(rentalId, triggerCellClickLogic: true);

                if (_selectedRentalId != rentalId)
                    ShowInfo("Rental not found in the current boarding house list.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Open Payments Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private int? GetBoardingHouseIdByRental(int rentalId)
        {
            using var conn = DbConnectionFactory.CreateConnection();
            using var cmd = new MySqlCommand(@"
        SELECT bh.id
        FROM rentals r
        INNER JOIN rooms rm ON rm.id = r.room_id
        INNER JOIN boarding_houses bh ON bh.id = rm.boarding_house_id
        WHERE r.id = @rentalId
        LIMIT 1;", conn);

            cmd.Parameters.AddWithValue("@rentalId", rentalId);

            var result = cmd.ExecuteScalar();
            if (result == null || result == DBNull.Value) return null;

            return Convert.ToInt32(result);
        }

        private void SelectBoardingHouseInCombo(int bhId)
        {
            for (int i = 0; i < cbBoardingHouses.Items.Count; i++)
            {
                if (cbBoardingHouses.Items[i] is ComboBoxItem item && item.Id == bhId)
                {
                    cbBoardingHouses.SelectedIndex = i;
                    return;
                }
            }
        }

        private void SelectRentalRowInGrid(int rentalId, bool triggerCellClickLogic)
        {
            if (dgvDataSource.DataSource == null || dgvDataSource.Rows.Count == 0)
                return;

            foreach (DataGridViewRow row in dgvDataSource.Rows)
            {
                if (row.IsNewRow) continue;

                if (row.Cells["RentalId"]?.Value == null) continue;

                if (Convert.ToInt32(row.Cells["RentalId"].Value) == rentalId)
                {
                    dgvDataSource.ClearSelection();
                    row.Selected = true;

                    dgvDataSource.FirstDisplayedScrollingRowIndex = row.Index;

                    if (triggerCellClickLogic)
                    {
                        dgvDataSource_CellClick(dgvDataSource, new DataGridViewCellEventArgs(0, row.Index));
                    }
                    else
                    {
                        _selectedRentalId = rentalId;
                        EnsureRentBillingItem(rentalId, GetSelectedBillMonth());
                        LoadBillingSummary(rentalId);
                        LoadPaymentHistory(rentalId);
                    }

                    return;
                }
            }
        }



    }
}
