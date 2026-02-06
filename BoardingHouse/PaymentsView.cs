using System;
using System.Data;
using System.Globalization;
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

        private string _receiptToPrint = "";
        private int _printLineIndex = 0;
        private readonly Font _receiptFont = new Font("Consolas", 10f);

        public PaymentsView()
        {
            InitializeComponent();

            if (cbSearchBy.Items.Count > 0 && cbSearchBy.SelectedIndex < 0) cbSearchBy.SelectedIndex = 0;
            if (cbPaymentMethod.Items.Count > 0 && cbPaymentMethod.SelectedIndex < 0) cbPaymentMethod.SelectedIndex = 0;

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
            var dt = dpBilling_Month.Value;
            return new DateTime(dt.Year, dt.Month, 1);
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
            where.Append(" WHERE r.status='ACTIVE' AND bh.id=@bhId ");

            if (!string.IsNullOrWhiteSpace(q))
            {
                if (searchBy == "Room")
                {
                    where.Append(" AND rm.room_no LIKE @q ");
                }
                else if (searchBy == "Contact No")
                {
                    where.Append(" AND t.contact_no LIKE @q ");
                }
                else // Tenant Name
                {
                    where.Append(" AND (t.full_name LIKE @q OR t.firstname LIKE @q OR t.lastname LIKE @q) ");
                }
            }

            using var conn = DbConnectionFactory.CreateConnection();
            using var cmd = new MySqlCommand($@"
                SELECT
                    r.id AS RentalId,
                    t.id AS TenantId,
                    t.full_name AS Tenant,
                    t.contact_no AS ContactNo,
                    rm.room_no AS Room,
                    rm.room_type AS RoomType,
                    r.monthly_rate AS MonthlyRate
                FROM rentals r
                INNER JOIN tenants t ON t.id = r.tenant_id
                INNER JOIN rooms rm ON rm.id = r.room_id
                INNER JOIN boarding_houses bh ON bh.id = rm.boarding_house_id
                {where}
                ORDER BY t.full_name;", conn);

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
            if (dgvDataSource.Columns.Contains("TenantId")) dgvDataSource.Columns["TenantId"].Visible = false;
        }

        private decimal GetTotalPaidForMonth(int rentalId, DateTime billMonth)
        {
            using var conn = DbConnectionFactory.CreateConnection();
            using var cmd = new MySqlCommand(@"
                SELECT COALESCE(SUM(amount),0)
                FROM payments
                WHERE rental_id=@rentalId
                  AND bill_month=@billMonth
                  AND status='POSTED';", conn);

            cmd.Parameters.AddWithValue("@rentalId", rentalId);
            cmd.Parameters.AddWithValue("@billMonth", billMonth.ToString("yyyy-MM-dd"));

            return Convert.ToDecimal(cmd.ExecuteScalar());
        }

        private void LoadBillingSummary(int rentalId)
        {
            lvChargeList.Items.Clear();

            var billMonth = GetSelectedBillMonth();

            using var conn = DbConnectionFactory.CreateConnection();
            using var cmd = new MySqlCommand(@"
                SELECT id, charge_type, description, amount, status
                FROM billing_items
                WHERE rental_id=@rentalId AND bill_month=@billMonth
                ORDER BY FIELD(charge_type,'RENT','ELECTRIC','WATER','INTERNET','PENALTY','OTHER'), id;", conn);

            cmd.Parameters.AddWithValue("@rentalId", rentalId);
            cmd.Parameters.AddWithValue("@billMonth", billMonth.ToString("yyyy-MM-dd"));

            decimal totalCharges = 0m;

            using (var r = cmd.ExecuteReader())
            {
                while (r.Read())
                {
                    var chargeType = r["charge_type"]?.ToString() ?? "";
                    var desc = r["description"]?.ToString() ?? "";
                    var amt = Convert.ToDecimal(r["amount"]);
                    var status = r["status"]?.ToString() ?? "";

                    if (status != "VOID") totalCharges += amt;

                    var li = new ListViewItem(chargeType);
                    li.SubItems.Add(desc);
                    li.SubItems.Add(amt.ToString("N2"));
                    li.SubItems.Add(status);
                    li.Tag = Convert.ToInt32(r["id"]);
                    lvChargeList.Items.Add(li);
                }
            }

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
                using var conn = DbConnectionFactory.CreateConnection();
                using var tx = conn.BeginTransaction();

                // 1) Insert payment
                int paymentId;
                using (var cmd = new MySqlCommand(@"
                    INSERT INTO payments
                    (rental_id, payment_date, bill_month, amount, method, reference_no, remarks, status, received_by, created_at)
                    VALUES
                    (@rentalId, @payDate, @billMonth, @amount, @method, @refNo, @remarks, 'POSTED', 1, NOW());
                    SELECT LAST_INSERT_ID();", conn, tx))
                {
                    cmd.Parameters.AddWithValue("@rentalId", _selectedRentalId.Value);
                    cmd.Parameters.AddWithValue("@payDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@billMonth", billMonth.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@method", method);
                    cmd.Parameters.AddWithValue("@refNo", string.IsNullOrWhiteSpace(refNo) ? (object)DBNull.Value : refNo);
                    cmd.Parameters.AddWithValue("@remarks", string.IsNullOrWhiteSpace(remarks) ? (object)DBNull.Value : remarks);

                    paymentId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // 2) Apply payment to UNPAID billing_items (simple full-item coverage only)
                decimal remaining = amount;

                using (var cmdGet = new MySqlCommand(@"
                    SELECT id, amount
                    FROM billing_items
                    WHERE rental_id=@rentalId AND bill_month=@billMonth AND status='UNPAID'
                    ORDER BY FIELD(charge_type,'RENT','ELECTRIC','WATER','INTERNET','PENALTY','OTHER'), id;", conn, tx))
                {
                    cmdGet.Parameters.AddWithValue("@rentalId", _selectedRentalId.Value);
                    cmdGet.Parameters.AddWithValue("@billMonth", billMonth.ToString("yyyy-MM-dd"));

                    using var r = cmdGet.ExecuteReader();
                    var unpaid = new System.Collections.Generic.List<(int id, decimal amt)>();
                    while (r.Read())
                        unpaid.Add((Convert.ToInt32(r["id"]), Convert.ToDecimal(r["amount"])));
                    r.Close();

                    foreach (var item in unpaid)
                    {
                        if (remaining <= 0) break;

                        if (remaining >= item.amt)
                        {
                            using var cmdUpd = new MySqlCommand(@"
                                UPDATE billing_items
                                SET status='PAID'
                                WHERE id=@id;", conn, tx);
                            cmdUpd.Parameters.AddWithValue("@id", item.id);
                            cmdUpd.ExecuteNonQuery();

                            remaining -= item.amt;
                        }
                    }
                }

                tx.Commit();

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

        private void lvChargeList_Click(object sender, EventArgs e)
        {
            // intentionally empty
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

            doc.DefaultPageSettings.PaperSize = new PaperSize("Receipt80mm", 315, 800);
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
            int left = e.MarginBounds.Left;
            int top = e.MarginBounds.Top;

            string[] lines = _receiptToPrint.Replace("\r\n", "\n").Split('\n');
            float lineHeight = _receiptFont.GetHeight(e.Graphics) + 2;
            float y = top;

            while (_printLineIndex < lines.Length)
            {
                e.Graphics.DrawString(lines[_printLineIndex], _receiptFont, Brushes.Black, left, y);
                y += lineHeight;

                if (y + lineHeight > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }

                _printLineIndex++;
            }

            e.HasMorePages = false;
            _printLineIndex = 0;
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
                        t.full_name AS tenant_name,
                        rm.room_no,
                        bh.name AS bh_name
                    FROM payments p
                    INNER JOIN rentals r ON r.id = p.rental_id
                    INNER JOIN tenants t ON t.id = r.tenant_id
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

                // Simple revert: all PAID for that month -> UNPAID
                using (var cmdRevert = new MySqlCommand(@"
                    UPDATE billing_items
                    SET status='UNPAID'
                    WHERE rental_id=@rentalId AND bill_month=@billMonth AND status='PAID';", conn, tx))
                {
                    cmdRevert.Parameters.AddWithValue("@rentalId", _selectedRentalId.Value);
                    cmdRevert.Parameters.AddWithValue("@billMonth", billMonth.ToString("yyyy-MM-dd"));
                    cmdRevert.ExecuteNonQuery();
                }

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
    }
}
