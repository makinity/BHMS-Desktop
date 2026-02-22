using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BoardingHouse
{
    public partial class RentalsView : UserControl
    {
        // TODO: set this from your login/session
        private int CurrentUserId = 1;

        private int? SelectedRentalId = null;
        private int? SelectedRentalRoomId = null;
        private int? SelectedRentalOccupantId = null;

        public RentalsView()
        {
            InitializeComponent();
            WireUiDefaults();
            Load += RentalsView_Load;
        }

        private void RentalsView_Load(object sender, EventArgs e)
        {
            try
            {
                LoadFilterDropdowns();
                LoadModalDropdowns();

                // Defaults
                if (cbStatus.Items.Count > 0) cbStatus.SelectedIndex = 0; // All / ACTIVE
                if (cbBoardingHouse.Items.Count > 0) cbBoardingHouse.SelectedIndex = 0;
                if (cbRoom.Items.Count > 0) cbRoom.SelectedIndex = 0;

                dtFrom.Value = DateTime.Today.AddMonths(-2);
                dtTo.Value = DateTime.Today;

                LoadRentalsGrid();
                ClearSummary();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load RentalsView.\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void WireUiDefaults()
        {
            // Modal hidden by default (you already do this in designer too)
            if (modalNewRental != null) modalNewRental.Visible = false;
            if (modalTransfer != null) modalTransfer.Visible = false;

            // Make Notes summary read-only (usually)
            if (txtNotes != null) txtNotes.ReadOnly = true;

            // Optional: set date pickers format
            dtFrom.Format = DateTimePickerFormat.Short;
            dtTo.Format = DateTimePickerFormat.Short;
            dtModalStart.Format = DateTimePickerFormat.Short;
            dtModalTransferDate.Format = DateTimePickerFormat.Short;

            // Status filter options (if you didn’t add in designer)
            cbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBoardingHouse.DropDownStyle = ComboBoxStyle.DropDownList;
            cbRoom.DropDownStyle = ComboBoxStyle.DropDownList;

            cbModalOccupant.DropDownStyle = ComboBoxStyle.DropDownList;
            cbModalBh.DropDownStyle = ComboBoxStyle.DropDownList;
            cbModalRoom.DropDownStyle = ComboBoxStyle.DropDownList;
            cbModalTransferBh.DropDownStyle = ComboBoxStyle.DropDownList;
            cbModalTransferRoom.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        // ============================================================
        // Dropdown loaders
        // ============================================================

        private void LoadFilterDropdowns()
        {
            using var conn = DbConnectionFactory.CreateConnection();

            // Boarding houses
            var dtBh = new DataTable();
            using (var cmd = new MySqlCommand(@"
                SELECT id, name
                FROM boarding_houses
                WHERE status = 'ACTIVE' OR status IS NULL
                ORDER BY name;", conn))
            using (var ad = new MySqlDataAdapter(cmd))
            {
                ad.Fill(dtBh);
            }

            cbBoardingHouse.DataSource = null;
            cbBoardingHouse.DisplayMember = "name";
            cbBoardingHouse.ValueMember = "id";

            // Insert "All"
            var dtBhWithAll = dtBh.Copy();
            var rowAll = dtBhWithAll.NewRow();
            rowAll["id"] = 0;
            rowAll["name"] = "All boarding houses";
            dtBhWithAll.Rows.InsertAt(rowAll, 0);

            cbBoardingHouse.DataSource = dtBhWithAll;

            // Rooms (depends on BH; load initially all)
            LoadRoomsForFilter(null);

            // Status
            cbStatus.Items.Clear();
            cbStatus.Items.Add("All statuses");
            cbStatus.Items.Add("ACTIVE");
            cbStatus.Items.Add("ENDED");
            cbStatus.SelectedIndex = 0;
        }

        private void LoadRoomsForFilter(int? boardingHouseId)
        {
            using var conn = DbConnectionFactory.CreateConnection();

            string sql = @"
                SELECT rm.id, CONCAT(bh.name, ' - ', rm.room_no) AS room_label
                FROM rooms rm
                JOIN boarding_houses bh ON bh.id = rm.boarding_house_id
                WHERE 1=1 ";

            if (boardingHouseId.HasValue && boardingHouseId.Value > 0)
                sql += " AND bh.id = @bhId ";

            sql += " ORDER BY bh.name, rm.room_no;";

            var dtRooms = new DataTable();
            using (var cmd = new MySqlCommand(sql, conn))
            {
                if (boardingHouseId.HasValue && boardingHouseId.Value > 0)
                    cmd.Parameters.AddWithValue("@bhId", boardingHouseId.Value);

                using var ad = new MySqlDataAdapter(cmd);
                ad.Fill(dtRooms);
            }

            var dtRoomsWithAll = dtRooms.Copy();
            var rowAll = dtRoomsWithAll.NewRow();
            rowAll["id"] = 0;
            rowAll["room_label"] = "All rooms";
            dtRoomsWithAll.Rows.InsertAt(rowAll, 0);

            cbRoom.DataSource = null;
            cbRoom.DisplayMember = "room_label";
            cbRoom.ValueMember = "id";
            cbRoom.DataSource = dtRoomsWithAll;
        }

        private void LoadModalDropdowns()
        {
            using var conn = DbConnectionFactory.CreateConnection();

            // Occupants
            var dtOcc = new DataTable();
            using (var cmd = new MySqlCommand(@"
                SELECT id, CONCAT(full_name, ' (', occupant_type, ')') AS occ_label
                FROM occupants
                WHERE status = 'ACTIVE' OR status IS NULL
                ORDER BY full_name;", conn))
            using (var ad = new MySqlDataAdapter(cmd))
            {
                ad.Fill(dtOcc);
            }

            var dtOccWithSelect = dtOcc.Copy();
            var rowSel = dtOccWithSelect.NewRow();
            rowSel["id"] = 0;
            rowSel["occ_label"] = "Select occupant";
            dtOccWithSelect.Rows.InsertAt(rowSel, 0);

            cbModalOccupant.DataSource = null;
            cbModalOccupant.DisplayMember = "occ_label";
            cbModalOccupant.ValueMember = "id";
            cbModalOccupant.DataSource = dtOccWithSelect;

            // Boarding houses for modal
            var dtBh = new DataTable();
            using (var cmd = new MySqlCommand(@"
                SELECT id, name
                FROM boarding_houses
                WHERE status = 'ACTIVE' OR status IS NULL
                ORDER BY name;", conn))
            using (var ad = new MySqlDataAdapter(cmd))
            {
                ad.Fill(dtBh);
            }

            var dtBhWithSelect = dtBh.Copy();
            var rowSelBh = dtBhWithSelect.NewRow();
            rowSelBh["id"] = 0;
            rowSelBh["name"] = "Select boarding house";
            dtBhWithSelect.Rows.InsertAt(rowSelBh, 0);

            cbModalBh.DataSource = null;
            cbModalBh.DisplayMember = "name";
            cbModalBh.ValueMember = "id";
            cbModalBh.DataSource = dtBhWithSelect;

            cbModalTransferBh.DataSource = null;
            cbModalTransferBh.DisplayMember = "name";
            cbModalTransferBh.ValueMember = "id";
            cbModalTransferBh.DataSource = dtBhWithSelect.Copy();

            // Rooms for modal start empty until BH selected
            cbModalRoom.DataSource = null;
            cbModalRoom.Items.Clear();
            cbModalRoom.Items.Add("Select room");
            cbModalRoom.SelectedIndex = 0;

            cbModalTransferRoom.DataSource = null;
            cbModalTransferRoom.Items.Clear();
            cbModalTransferRoom.Items.Add("Select room");
            cbModalTransferRoom.SelectedIndex = 0;
        }

        private void LoadRoomsForModal(ComboBox roomCombo, int boardingHouseId, bool onlyAvailable)
        {
            using var conn = DbConnectionFactory.CreateConnection();

            string sql = @"
                SELECT id, room_no
                FROM rooms
                WHERE boarding_house_id = @bhId ";

            if (onlyAvailable)
                sql += " AND status = 'AVAILABLE' ";

            sql += " ORDER BY room_no;";

            var dt = new DataTable();
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@bhId", boardingHouseId);
                using var ad = new MySqlDataAdapter(cmd);
                ad.Fill(dt);
            }

            var dtWithSelect = dt.Copy();
            var rowSel = dtWithSelect.NewRow();
            rowSel["id"] = 0;
            rowSel["room_no"] = "Select room";
            dtWithSelect.Rows.InsertAt(rowSel, 0);

            roomCombo.DataSource = null;
            roomCombo.DisplayMember = "room_no";
            roomCombo.ValueMember = "id";
            roomCombo.DataSource = dtWithSelect;
        }

        // ============================================================
        // Grid load (filters + search)
        // ============================================================

        private void LoadRentalsGrid()
        {
            using var conn = DbConnectionFactory.CreateConnection();

            int bhId = GetComboIntValue(cbBoardingHouse);
            int roomId = GetComboIntValue(cbRoom);
            string status = cbStatus.SelectedItem?.ToString() ?? "All statuses";
            DateTime from = dtFrom.Value.Date;
            DateTime to = dtTo.Value.Date;

            string search = (txtSearch.Text ?? "").Trim();

            string sql = @"
SELECT
  r.id,
  bh.name AS boarding_house,
  rm.room_no,
  o.full_name AS occupant,
  r.start_date,
  r.end_date,
  r.status
FROM rentals r
JOIN rooms rm ON rm.id = r.room_id
JOIN boarding_houses bh ON bh.id = rm.boarding_house_id
JOIN occupants o ON o.id = r.occupant_id
WHERE 1=1
";

            if (bhId > 0) sql += " AND bh.id = @bhId ";
            if (roomId > 0) sql += " AND rm.id = @roomId ";
            if (!string.IsNullOrWhiteSpace(status) && status != "All statuses")
                sql += " AND r.status = @status ";

            // Overlap filter: rental overlaps [from,to]
            sql += " AND r.start_date <= @to AND (r.end_date IS NULL OR r.end_date >= @from) ";

            if (!string.IsNullOrWhiteSpace(search))
            {
                // If numeric, allow exact id match too
                sql += @"
AND (
  CAST(r.id AS CHAR) = @qExact
  OR o.full_name LIKE @qLike
  OR rm.room_no LIKE @qLike
  OR bh.name LIKE @qLike
)
";
            }

            sql += " ORDER BY r.id DESC;";

            var dt = new DataTable();
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);

                if (bhId > 0) cmd.Parameters.AddWithValue("@bhId", bhId);
                if (roomId > 0) cmd.Parameters.AddWithValue("@roomId", roomId);
                if (!string.IsNullOrWhiteSpace(status) && status != "All statuses")
                    cmd.Parameters.AddWithValue("@status", status);

                if (!string.IsNullOrWhiteSpace(search))
                {
                    cmd.Parameters.AddWithValue("@qExact", search);
                    cmd.Parameters.AddWithValue("@qLike", "%" + search + "%");
                }

                using var ad = new MySqlDataAdapter(cmd);
                ad.Fill(dt);
            }

            // Bind to grid (keep your columns; easiest is to set DataSource then map)
            dgvRentals.AutoGenerateColumns = false;

            // Map columns if you used manual columns in designer:
            colId.DataPropertyName = "id";
            colBoardingHouse.DataPropertyName = "boarding_house";
            colRoomNo.DataPropertyName = "room_no";
            colOccupant.DataPropertyName = "occupant";
            colStart.DataPropertyName = "start_date";
            colEnd.DataPropertyName = "end_date";
            colStatus.DataPropertyName = "status";

            dgvRentals.DataSource = dt;
        }

        // ============================================================
        // Summary / right panel
        // ============================================================

        private void LoadRentalSummary(int rentalId)
        {
            using var conn = DbConnectionFactory.CreateConnection();

            // Summary details
            using (var cmd = new MySqlCommand(@"
SELECT
  r.id,
  r.room_id,
  r.occupant_id,
  o.full_name,
  o.contact_no,
  bh.name AS boarding_house,
  rm.room_no,
  r.monthly_rate,
  r.notes,
  r.status
FROM rentals r
JOIN rooms rm ON rm.id = r.room_id
JOIN boarding_houses bh ON bh.id = rm.boarding_house_id
JOIN occupants o ON o.id = r.occupant_id
WHERE r.id = @rentalId;", conn))
            {
                cmd.Parameters.AddWithValue("@rentalId", rentalId);

                using var rd = cmd.ExecuteReader();
                if (!rd.Read())
                {
                    ClearSummary();
                    return;
                }

                SelectedRentalId = rentalId;
                SelectedRentalRoomId = rd.GetInt32("room_id");
                SelectedRentalOccupantId = rd.GetInt32("occupant_id");

                lblSumRentalNoVal.Text = rd["id"].ToString();
                lblSumOccupantVal.Text = rd["full_name"].ToString();
                lblSumContactVal.Text = rd["contact_no"].ToString();
                lblSumBoardingHouseVal.Text = rd["boarding_house"].ToString();
                lblSumRoomVal.Text = rd["room_no"].ToString();

                decimal rate = rd.IsDBNull(rd.GetOrdinal("monthly_rate")) ? 0m : rd.GetDecimal("monthly_rate");
                lblSumRateVal.Text = "₱ " + rate.ToString("N2", CultureInfo.InvariantCulture);

                txtNotes.Text = rd["notes"]?.ToString() ?? "";
            }

            LoadAlerts(rentalId);
            LoadAuditLogs(rentalId);
        }

        private void ClearSummary()
        {
            SelectedRentalId = null;
            SelectedRentalRoomId = null;
            SelectedRentalOccupantId = null;

            lblSumRentalNoVal.Text = "-";
            lblSumOccupantVal.Text = "-";
            lblSumContactVal.Text = "-";
            lblSumRoomVal.Text = "-";
            lblSumBoardingHouseVal.Text = "-";
            lblSumRateVal.Text = "-";
            txtNotes.Text = "";
            txtAlerts.Text = "";
            dgvAudit.DataSource = null;
        }

        private void LoadAlerts(int rentalId)
        {
            using var conn = DbConnectionFactory.CreateConnection();

            // Simple alert: unpaid billing items
            var dt = new DataTable();
            using (var cmd = new MySqlCommand(@"
SELECT bill_month, charge_type, description, amount, status
FROM billing_items
WHERE rental_id = @rentalId
  AND status <> 'PAID'
ORDER BY bill_month DESC, id DESC;", conn))
            {
                cmd.Parameters.AddWithValue("@rentalId", rentalId);
                using var ad = new MySqlDataAdapter(cmd);
                ad.Fill(dt);
            }

            if (dt.Rows.Count == 0)
            {
                txtAlerts.Text = "No alerts.";
                return;
            }

            var sb = new System.Text.StringBuilder();
            foreach (DataRow r in dt.Rows)
            {
                string month = r["bill_month"]?.ToString() ?? "-";
                string type = r["charge_type"]?.ToString() ?? "-";
                string desc = r["description"]?.ToString() ?? "-";
                decimal amt = 0m;
                decimal.TryParse(r["amount"]?.ToString(), out amt);
                string st = r["status"]?.ToString() ?? "-";

                sb.AppendLine($"{month} | {type} | ₱ {amt:N2} | {st} | {desc}");
            }

            txtAlerts.Text = sb.ToString();
        }

        private void LoadAuditLogs(int rentalId)
        {
            using var conn = DbConnectionFactory.CreateConnection();

            var dt = new DataTable();
            using (var cmd = new MySqlCommand(@"
SELECT created_at, action, user_id, details
FROM audit_logs
WHERE entity = 'rental' AND entity_id = @id
ORDER BY created_at DESC;", conn))
            {
                cmd.Parameters.AddWithValue("@id", rentalId);
                using var ad = new MySqlDataAdapter(cmd);
                ad.Fill(dt);
            }

            dgvAudit.AutoGenerateColumns = false;
            colAuditDate.DataPropertyName = "created_at";
            colAuditAction.DataPropertyName = "action";
            colAuditUser.DataPropertyName = "user_id";
            colAuditDetails.DataPropertyName = "details";

            dgvAudit.DataSource = dt;
        }

        // ============================================================
        // Modals: New Rental
        // ============================================================

        private void btnModalNewSave_Click(object sender, EventArgs e)
        {
            try
            {
                int occupantId = GetComboIntValue(cbModalOccupant);
                int bhId = GetComboIntValue(cbModalBh);
                int roomId = GetComboIntValue(cbModalRoom);

                if (occupantId <= 0) { MessageBox.Show("Please select an occupant."); return; }
                if (bhId <= 0) { MessageBox.Show("Please select a boarding house."); return; }
                if (roomId <= 0) { MessageBox.Show("Please select a room."); return; }

                if (!decimal.TryParse(txtModalRate.Text.Trim(), out decimal monthlyRate) || monthlyRate <= 0)
                {
                    MessageBox.Show("Please enter a valid monthly rate.");
                    return;
                }

                string notes = txtModalNotes.Text ?? "";
                DateTime startDate = dtModalStart.Value.Date;

                using var conn = DbConnectionFactory.CreateConnection();
                using var tx = conn.BeginTransaction();

                // Ensure room still available
                using (var chk = new MySqlCommand("SELECT status FROM rooms WHERE id=@id FOR UPDATE;", conn, tx))
                {
                    chk.Parameters.AddWithValue("@id", roomId);
                    var status = (chk.ExecuteScalar() ?? "").ToString();
                    if (!string.Equals(status, "AVAILABLE", StringComparison.OrdinalIgnoreCase))
                    {
                        tx.Rollback();
                        MessageBox.Show("Selected room is not available anymore.");
                        return;
                    }
                }

                // Prevent duplicate ACTIVE rentals for same occupant
                using (var dup = new MySqlCommand(@"
                SELECT COUNT(*)
                FROM rentals
                WHERE occupant_id=@occ AND status='ACTIVE' AND end_date IS NULL;", conn, tx))
                {
                    dup.Parameters.AddWithValue("@occ", occupantId);
                    int c = Convert.ToInt32(dup.ExecuteScalar());
                    if (c > 0)
                    {
                        tx.Rollback();
                        MessageBox.Show("This occupant already has an ACTIVE rental.");
                        return;
                    }
                }

                int newRentalId;
                using (var cmd = new MySqlCommand(@"
INSERT INTO rentals
(room_id, start_date, end_date, monthly_rate, deposit_amount, status, notes, created_by, created_at, updated_at, occupant_id)
VALUES
(@roomId, @start, NULL, @rate, @deposit, 'ACTIVE', @notes, @user, NOW(), NOW(), @occupant);
SELECT LAST_INSERT_ID();", conn, tx))
                {
                    cmd.Parameters.AddWithValue("@roomId", roomId);
                    cmd.Parameters.AddWithValue("@start", startDate);
                    cmd.Parameters.AddWithValue("@rate", monthlyRate);
                    cmd.Parameters.AddWithValue("@deposit", monthlyRate); // you used equal in sample; adjust if needed
                    cmd.Parameters.AddWithValue("@notes", notes);
                    cmd.Parameters.AddWithValue("@user", CurrentUserId);
                    cmd.Parameters.AddWithValue("@occupant", occupantId);

                    newRentalId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                using (var cmdRoom = new MySqlCommand("UPDATE rooms SET status='OCCUPIED' WHERE id=@id;", conn, tx))
                {
                    cmdRoom.Parameters.AddWithValue("@id", roomId);
                    cmdRoom.ExecuteNonQuery();
                }

                InsertAudit(conn, tx, "CREATE", "rental", newRentalId, $"Created rental. RoomId={roomId}, OccupantId={occupantId}, Rate={monthlyRate:N2}");

                tx.Commit();

                // Close modal & refresh
                btnModalNewCancel_Click(sender, e);
                LoadRentalsGrid();
                LoadRentalSummary(newRentalId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Save rental failed.\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModalNewCancel_Click(object sender, EventArgs e)
        {
            modalNewRental.Visible = false;
            txtModalRate.Clear();
            txtModalNotes.Clear();

            if (cbModalOccupant.Items.Count > 0) cbModalOccupant.SelectedIndex = 0;
            if (cbModalBh.Items.Count > 0) cbModalBh.SelectedIndex = 0;

            cbModalRoom.DataSource = null;
            cbModalRoom.Items.Clear();
            cbModalRoom.Items.Add("Select room");
            cbModalRoom.SelectedIndex = 0;
        }

        private void cbModalRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            int roomId = GetComboIntValue(cbModalRoom);
            if (roomId <= 0)
            {
                txtModalRate.Clear();
                return;
            }

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                using var cmd = new MySqlCommand("SELECT monthly_rate FROM rooms WHERE id=@id;", conn);
                cmd.Parameters.AddWithValue("@id", roomId);

                var val = cmd.ExecuteScalar();
                if (val == null || val == DBNull.Value)
                {
                    txtModalRate.Clear();
                    return;
                }

                decimal rate = Convert.ToDecimal(val);
                txtModalRate.Text = rate.ToString("0.00");
            }
            catch
            {
                // keep quiet; don't block UI
            }
        }

        private void cbModalTransferRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            int roomId = GetComboIntValue(cbModalTransferRoom);
            if (roomId <= 0)
            {
                if (!chkModalKeepRate.Checked) txtModalTransferRate.Clear();
                return;
            }

            if (chkModalKeepRate.Checked)
            {
                // KeepRate overrides any auto-fill
                return;
            }

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                using var cmd = new MySqlCommand("SELECT monthly_rate FROM rooms WHERE id=@id;", conn);
                cmd.Parameters.AddWithValue("@id", roomId);

                var val = cmd.ExecuteScalar();
                if (val == null || val == DBNull.Value)
                {
                    txtModalTransferRate.Clear();
                    return;
                }

                decimal rate = Convert.ToDecimal(val);
                txtModalTransferRate.Text = rate.ToString("0.00");
            }
            catch { }
        }

        private void chkModalKeepRate_CheckedChanged(object sender, EventArgs e)
        {
            txtModalTransferRate.ReadOnly = chkModalKeepRate.Checked;

            if (chkModalKeepRate.Checked)
            {
                txtModalTransferRate.Clear(); // optional
            }
            else
            {
                // optional: if a room is selected, auto-fill from room monthly_rate
                cbModalTransferRoom_SelectedIndexChanged(sender, e);
            }
        }

        private void cbModalBh_SelectedIndexChanged(object sender, EventArgs e)
        {
            int bhId = GetComboIntValue(cbModalBh);
            if (bhId <= 0)
            {
                cbModalRoom.DataSource = null;
                cbModalRoom.Items.Clear();
                cbModalRoom.Items.Add("Select room");
                cbModalRoom.SelectedIndex = 0;
                return;
            }

            // For NEW rental, show only available rooms
            LoadRoomsForModal(cbModalRoom, bhId, onlyAvailable: true);
        }

        // ============================================================
        // Modals: Transfer Room
        // ============================================================

        private void btnModalTransferConfirm_Click(object sender, EventArgs e)
        {
            if (SelectedRentalId == null || SelectedRentalRoomId == null || SelectedRentalOccupantId == null)
            {
                MessageBox.Show("Please select a rental first.");
                return;
            }

            try
            {
                int newBhId = GetComboIntValue(cbModalTransferBh);
                int newRoomId = GetComboIntValue(cbModalTransferRoom);

                if (newBhId <= 0) { MessageBox.Show("Please select a boarding house."); return; }
                if (newRoomId <= 0) { MessageBox.Show("Please select a new room."); return; }

                DateTime transferDate = dtModalTransferDate.Value.Date;
                bool keepRate = chkModalKeepRate.Checked;

                decimal newRate = 0m;
                if (!keepRate)
                {
                    if (!decimal.TryParse(txtModalTransferRate.Text.Trim(), out newRate) || newRate <= 0)
                    {
                        MessageBox.Show("Enter a valid new rate or check 'Keep same rate'.");
                        return;
                    }
                }

                string notes = txtModalTransferNotes.Text ?? "";

                int oldRentalId = SelectedRentalId.Value;
                int oldRoomId = SelectedRentalRoomId.Value;
                int occId = SelectedRentalOccupantId.Value;

                using var conn = DbConnectionFactory.CreateConnection();
                using var tx = conn.BeginTransaction();

                // Lock old rental row
                decimal oldRate;
                using (var cmdGet = new MySqlCommand("SELECT monthly_rate FROM rentals WHERE id=@id FOR UPDATE;", conn, tx))
                {
                    cmdGet.Parameters.AddWithValue("@id", oldRentalId);
                    oldRate = Convert.ToDecimal(cmdGet.ExecuteScalar());
                }

                decimal finalRate = keepRate ? oldRate : newRate;

                // Ensure new room available
                using (var chk = new MySqlCommand("SELECT status FROM rooms WHERE id=@id FOR UPDATE;", conn, tx))
                {
                    chk.Parameters.AddWithValue("@id", newRoomId);
                    var status = (chk.ExecuteScalar() ?? "").ToString();
                    if (!string.Equals(status, "AVAILABLE", StringComparison.OrdinalIgnoreCase))
                    {
                        tx.Rollback();
                        MessageBox.Show("Selected new room is not available.");
                        return;
                    }
                }

                // 1) End old rental
                using (var cmdEnd = new MySqlCommand(@"
UPDATE rentals
SET end_date = @endDate, status='ENDED', updated_at = NOW()
WHERE id=@id;", conn, tx))
                {
                    cmdEnd.Parameters.AddWithValue("@endDate", transferDate);
                    cmdEnd.Parameters.AddWithValue("@id", oldRentalId);
                    cmdEnd.ExecuteNonQuery();
                }

                // 2) Free old room
                using (var cmdFree = new MySqlCommand("UPDATE rooms SET status='AVAILABLE' WHERE id=@id;", conn, tx))
                {
                    cmdFree.Parameters.AddWithValue("@id", oldRoomId);
                    cmdFree.ExecuteNonQuery();
                }

                // 3) Create new rental
                int newRentalId;
                using (var cmdNew = new MySqlCommand(@"
INSERT INTO rentals
(room_id, start_date, end_date, monthly_rate, deposit_amount, status, notes, created_by, created_at, updated_at, occupant_id)
VALUES
(@roomId, @start, NULL, @rate, @deposit, 'ACTIVE', @notes, @user, NOW(), NOW(), @occupant);
SELECT LAST_INSERT_ID();", conn, tx))
                {
                    cmdNew.Parameters.AddWithValue("@roomId", newRoomId);
                    cmdNew.Parameters.AddWithValue("@start", transferDate);
                    cmdNew.Parameters.AddWithValue("@rate", finalRate);
                    cmdNew.Parameters.AddWithValue("@deposit", finalRate);
                    cmdNew.Parameters.AddWithValue("@notes", notes);
                    cmdNew.Parameters.AddWithValue("@user", CurrentUserId);
                    cmdNew.Parameters.AddWithValue("@occupant", occId);

                    newRentalId = Convert.ToInt32(cmdNew.ExecuteScalar());
                }

                // 4) Occupy new room
                using (var cmdOcc = new MySqlCommand("UPDATE rooms SET status='OCCUPIED' WHERE id=@id;", conn, tx))
                {
                    cmdOcc.Parameters.AddWithValue("@id", newRoomId);
                    cmdOcc.ExecuteNonQuery();
                }

                InsertAudit(conn, tx, "TRANSFER_END", "rental", oldRentalId, $"Ended rental due to transfer. NewRentalId={newRentalId}, TransferDate={transferDate:yyyy-MM-dd}");
                InsertAudit(conn, tx, "TRANSFER_CREATE", "rental", newRentalId, $"Created rental from transfer. OldRentalId={oldRentalId}, NewRoomId={newRoomId}, Rate={finalRate:N2}");

                tx.Commit();

                // Close modal & refresh
                btnModalTransferCancel_Click(sender, e);
                LoadRentalsGrid();
                LoadRentalSummary(newRentalId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Transfer failed.\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModalTransferCancel_Click(object sender, EventArgs e)
        {
            modalTransfer.Visible = false;
            lblModalCurrentVal.Text = "-";
            chkModalKeepRate.Checked = false;
            txtModalTransferRate.Clear();
            txtModalTransferNotes.Clear();

            if (cbModalTransferBh.Items.Count > 0) cbModalTransferBh.SelectedIndex = 0;

            cbModalTransferRoom.DataSource = null;
            cbModalTransferRoom.Items.Clear();
            cbModalTransferRoom.Items.Add("Select room");
            cbModalTransferRoom.SelectedIndex = 0;
        }

        private void cbModalTransferBh_SelectedIndexChanged(object sender, EventArgs e)
        {
            int bhId = GetComboIntValue(cbModalTransferBh);
            if (bhId <= 0)
            {
                cbModalTransferRoom.DataSource = null;
                cbModalTransferRoom.Items.Clear();
                cbModalTransferRoom.Items.Add("Select room");
                cbModalTransferRoom.SelectedIndex = 0;
                return;
            }

            // Transfer: also show only available rooms
            LoadRoomsForModal(cbModalTransferRoom, bhId, onlyAvailable: true);
        }

        // ============================================================
        // Events you already have
        // ============================================================

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadRentalsGrid();
                if (SelectedRentalId.HasValue)
                    LoadRentalSummary(SelectedRentalId.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Refresh failed.\n{ex.Message}");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try { LoadRentalsGrid(); }
            catch (Exception ex) { MessageBox.Show($"Search failed.\n{ex.Message}"); }
        }

        private void cbBoardingHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int bhId = GetComboIntValue(cbBoardingHouse);
                LoadRoomsForFilter(bhId <= 0 ? null : bhId);
            }
            catch { /* ignore */ }
        }

        private void dgvRentals_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                var row = dgvRentals.Rows[e.RowIndex];
                if (row == null) return;

                int rentalId = Convert.ToInt32(row.Cells[colId.Index].Value);
                LoadRentalSummary(rentalId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load rental.\n{ex.Message}");
            }
        }

        private void btnNewRental_Click(object sender, EventArgs e)
        {
            modalNewRental.Visible = true;
            dtModalStart.Value = DateTime.Today;
        }

        // ============================================================
        // Helpers
        // ============================================================

        private int GetComboIntValue(ComboBox cb)
        {
            if (cb == null) return 0;
            if (cb.SelectedValue == null) return 0;

            if (int.TryParse(cb.SelectedValue.ToString(), out int id))
                return id;

            return 0;
        }

        private void InsertAudit(MySqlConnection conn, MySqlTransaction tx, string action, string entity, int entityId, string details)
        {
            using var cmd = new MySqlCommand(@"
INSERT INTO audit_logs (user_id, action, entity, entity_id, details, created_at)
VALUES (@user, @action, @entity, @entityId, @details, NOW());", conn, tx);

            cmd.Parameters.AddWithValue("@user", CurrentUserId);
            cmd.Parameters.AddWithValue("@action", action);
            cmd.Parameters.AddWithValue("@entity", entity);
            cmd.Parameters.AddWithValue("@entityId", entityId);
            cmd.Parameters.AddWithValue("@details", details);
            cmd.ExecuteNonQuery();
        }

        // ============================================================
        // Optional: open Transfer modal (call this from a Transfer button you add later)
        // ============================================================
        private void OpenTransferModal()
        {
            if (SelectedRentalId == null || SelectedRentalRoomId == null)
            {
                MessageBox.Show("Select a rental first.");
                return;
            }

            modalTransfer.Visible = true;
            dtModalTransferDate.Value = DateTime.Today;

            // Current display
            lblModalCurrentVal.Text = $"{lblSumBoardingHouseVal.Text} - Room {lblSumRoomVal.Text}";

            // Reset transfer fields
            if (cbModalTransferBh.Items.Count > 0) cbModalTransferBh.SelectedIndex = 0;

            cbModalTransferRoom.DataSource = null;
            cbModalTransferRoom.Items.Clear();
            cbModalTransferRoom.Items.Add("Select room");
            cbModalTransferRoom.SelectedIndex = 0;

            chkModalKeepRate.Checked = true; // default safer
            txtModalTransferNotes.Clear();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvRentals.DataSource == null || dgvRentals.Rows.Count == 0)
                {
                    MessageBox.Show("No rentals to export.");
                    return;
                }

                using var sfd = new SaveFileDialog
                {
                    Filter = "CSV Files (*.csv)|*.csv",
                    FileName = $"rentals_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                };

                if (sfd.ShowDialog() != DialogResult.OK) return;

                var sb = new System.Text.StringBuilder();

                // headers
                for (int i = 0; i < dgvRentals.Columns.Count; i++)
                {
                    sb.Append(dgvRentals.Columns[i].HeaderText);
                    sb.Append(i == dgvRentals.Columns.Count - 1 ? "\n" : ",");
                }

                // rows
                foreach (DataGridViewRow row in dgvRentals.Rows)
                {
                    if (row.IsNewRow) continue;

                    for (int i = 0; i < dgvRentals.Columns.Count; i++)
                    {
                        var val = row.Cells[i].Value?.ToString() ?? "";
                        val = val.Replace("\"", "\"\"");
                        sb.Append($"\"{val}\"");
                        sb.Append(i == dgvRentals.Columns.Count - 1 ? "\n" : ",");
                    }
                }

                System.IO.File.WriteAllText(sfd.FileName, sb.ToString(), System.Text.Encoding.UTF8);

                MessageBox.Show("Export complete.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Export failed.\n{ex.Message}");
            }
        }

        private void modalNewRental_Paint(object sender, PaintEventArgs e) { }

        private void cbModalOccupant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int occId = GetComboIntValue(cbModalOccupant);
                if (occId <= 0) return;

                using var conn = DbConnectionFactory.CreateConnection();
                using var cmd = new MySqlCommand(@"
            SELECT COUNT(*)
            FROM rentals
            WHERE occupant_id = @occ
              AND status = 'ACTIVE'
              AND end_date IS NULL;", conn);
                cmd.Parameters.AddWithValue("@occ", occId);

                int c = Convert.ToInt32(cmd.ExecuteScalar());
                if (c > 0)
                {
                    MessageBox.Show("This occupant already has an ACTIVE rental.", "Not allowed",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    cbModalOccupant.SelectedIndex = 0;
                }
            }
            catch
            {
            }
        }

        private void cbRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadRentalsGrid();
            }
            catch
            {
                // ignore minor UI errors
            }
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
        }

        private void btnTransferRoom_Click(object sender, EventArgs e)
        {
            OpenTransferModal();
        }
    }
}