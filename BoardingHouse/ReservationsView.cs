using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BoardingHouse
{
    public partial class ReservationsView : UserControl
    {
        private MySqlConnection Conn() => DbConnectionFactory.CreateConnection();

        private uint? _selectedReservationId = null;

        private bool _editorIsEditMode = false;
        private uint? _editorReservationId = null;
        private bool _loadingEditorCombos = false;
        public int CurrentUserId { get; set; }

        public ReservationsView()
        {
            InitializeComponent();

            // Missing in your designer snippet: reservation grid click handler.
            dgvReservations.CellClick += dgvReservations_CellClick;

            // Close editor panel (button exists in designer but no handler assigned there)
            btnCloseEditor.Click += (s, e) => HideEditor();

            // default filter ranges
            dtFilterFrom.Value = DateTime.Today.AddDays(-30);
            dtFilterTo.Value = DateTime.Today.AddDays(30);
        }

        // ---------------------------
        // LOAD
        // ---------------------------
        private void ReservationsView_Load_1(object sender, EventArgs e)
        {
            LoadStatusCombos();
            LoadBoardingHouses();
            MakeComboSearchable(cbEditorOccupant);

            cbEditorRoom.DataSource = null;
            cbEditorOccupant.DataSource = null;
            cbEditorRoom.Enabled = false;
            cbEditorOccupant.Enabled = false;

            RefreshReservations();
            ClearSummary();
            HideEditor();
        }

        private void LoadStatusCombos()
        {
            if (cbStatusFilter.Items.Count == 0)
                cbStatusFilter.Items.AddRange(new object[] { "ALL", "PENDING", "APPROVED", "REJECTED", "CANCELLED", "EXPIRED", "CHECKED_IN" });
            cbStatusFilter.SelectedIndex = 0;

            if (cbEditorStatus.Items.Count == 0)
                cbEditorStatus.Items.AddRange(new object[] { "PENDING", "APPROVED", "REJECTED", "CANCELLED", "EXPIRED", "CHECKED_IN" });
            cbEditorStatus.SelectedIndex = 0;
        }

        private void LoadBoardingHouses()
        {
            // TODO: adjust if your table name/columns differ
            using var con = Conn();

            var dt = new DataTable();
            using var da = new MySqlDataAdapter(
                "SELECT id, name FROM boarding_houses WHERE status='ACTIVE' ORDER BY name;",
                con
            );
            da.Fill(dt);

            cbBoardingHouse.DisplayMember = "name";
            cbBoardingHouse.ValueMember = "id";
            cbBoardingHouse.DataSource = dt;

            cbEditorBoardingHouse.DisplayMember = "name";
            cbEditorBoardingHouse.ValueMember = "id";
            cbEditorBoardingHouse.DataSource = dt.Copy();

            if (cbEditorBoardingHouse.Items.Count > 0)
                cbEditorBoardingHouse.SelectedIndex = -1;

            if (TryGetComboSelectedUInt(cbBoardingHouse, out var bhId))
                LoadRoomsByBoardingHouse(cbRoom, bhId);
            else
                cbRoom.DataSource = null;
        }

        private static void MakeComboSearchable(ComboBox cb)
        {
            if (cb == null) return;
            cb.DropDownStyle = ComboBoxStyle.DropDown;
            cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cb.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private static bool TryGetComboSelectedUInt(ComboBox cb, out uint value)
        {
            value = 0;
            if (cb == null) return false;

            object? raw = cb.SelectedValue;
            if (raw == null || raw is DataRowView)
            {
                raw = cb.SelectedItem;
                if (raw == null || raw is DataRowView) return false;
            }

            return raw switch
            {
                uint u => (value = u) > 0,
                int i when i > 0 => (value = (uint)i) > 0,
                long l when l > 0 => (value = (uint)l) > 0,
                decimal d when d > 0 => (value = (uint)d) > 0,
                _ => uint.TryParse(raw.ToString(), out value) && value > 0
            };
        }

        private void LoadRoomsByBoardingHouse(ComboBox cbRooms, uint boardingHouseId)
        {
            using var con = Conn();

            var dt = new DataTable();
            using var da = new MySqlDataAdapter(
                "SELECT id, room_no FROM rooms WHERE boarding_house_id=@bh AND status <> 'INACTIVE' ORDER BY room_no;",
                con
            );
            da.SelectCommand.Parameters.AddWithValue("@bh", boardingHouseId);
            da.Fill(dt);

            cbRooms.DisplayMember = "room_no";
            cbRooms.ValueMember = "id";
            cbRooms.DataSource = dt;
        }

        private void LoadOccupantsForEditorByBoardingHouse(uint boardingHouseId)
        {
            using var con = Conn();

            var dt = new DataTable();
            const string sql = @"
                SELECT d.occupant_id, d.full_name
                FROM (
                    SELECT
                        o.id AS occupant_id,
                        COALESCE(
                            NULLIF(s.full_name, ''),
                            CONCAT(s.lastname, ', ', s.firstname, IFNULL(CONCAT(' ', s.middlename), ''))
                        ) AS full_name
                    FROM occupants o
                    INNER JOIN student_occupant_map som ON som.occupant_id = o.id
                    INNER JOIN students s ON s.id = som.student_id
                    WHERE s.boarding_house_id = @bh
                      AND s.status = 'ACTIVE'
                      AND o.status = 'ACTIVE'

                    UNION

                    SELECT
                        o.id AS occupant_id,
                        COALESCE(
                            NULLIF(t.full_name, ''),
                            CONCAT(t.lastname, ', ', t.firstname, IFNULL(CONCAT(' ', t.middlename), ''))
                        ) AS full_name
                    FROM occupants o
                    INNER JOIN tenant_occupant_map tom ON tom.occupant_id = o.id
                    INNER JOIN tenants t ON t.id = tom.tenant_id
                    WHERE t.boarding_house_id = @bh
                      AND t.status = 'ACTIVE'
                      AND o.status = 'ACTIVE'
                ) d
                ORDER BY d.full_name;";

            using var da = new MySqlDataAdapter(sql, con);
            da.SelectCommand.Parameters.AddWithValue("@bh", boardingHouseId);
            da.Fill(dt);

            cbEditorOccupant.DisplayMember = "full_name";
            cbEditorOccupant.ValueMember = "occupant_id";
            cbEditorOccupant.DataSource = dt;
        }

        private void LoadOccupantsForEditor()
        {
            using var con = Conn();

            // occupant display name via students/tenants mapping
            var sql = @"
                SELECT o.id AS occupant_id,
                       COALESCE(s.full_name, t.full_name, CONCAT('Occupant #', o.id)) AS full_name
                FROM occupants o
                LEFT JOIN student_occupant_map som ON som.occupant_id = o.id
                LEFT JOIN students s ON s.id = som.student_id
                LEFT JOIN tenant_occupant_map tom ON tom.occupant_id = o.id
                LEFT JOIN tenants t ON t.id = tom.tenant_id
                WHERE o.status='ACTIVE'
                ORDER BY full_name;";

            var dt = new DataTable();
            using var da = new MySqlDataAdapter(sql, con);
            da.Fill(dt);

            cbEditorOccupant.DisplayMember = "full_name";
            cbEditorOccupant.ValueMember = "occupant_id";
            cbEditorOccupant.DataSource = dt;
        }

        // ---------------------------
        // FILTERS / SEARCH
        // ---------------------------
        private void cbBoardingHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!TryGetComboSelectedUInt(cbBoardingHouse, out var bhId))
            {
                cbRoom.DataSource = null;
                RefreshReservations();
                return;
            }

            LoadRoomsByBoardingHouse(cbRoom, bhId);
            RefreshReservations();
        }

        private void cbRoom_SelectedIndexChanged(object sender, EventArgs e) => RefreshReservations();
        private void cbStatusFilter_SelectedIndexChanged(object sender, EventArgs e) => RefreshReservations();
        private void dtFilterFrom_ValueChanged(object sender, EventArgs e) => RefreshReservations();
        private void dtFilterTo_ValueChanged(object sender, EventArgs e) => RefreshReservations();

        private void btnSearch_Click(object sender, EventArgs e) => RefreshReservations();

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            cbStatusFilter.SelectedIndex = 0;
            dtFilterFrom.Value = DateTime.Today.AddDays(-30);
            dtFilterTo.Value = DateTime.Today.AddDays(30);
            RefreshReservations();
        }

        private void btnRefresh_Click(object sender, EventArgs e) => RefreshReservations();

        private void RefreshReservations()
        {
            using var con = Conn();

            string status = cbStatusFilter.SelectedItem?.ToString() ?? "ALL";
            string search = (txtSearch.Text ?? "").Trim();

            uint? bhId = null;
            if (TryGetComboSelectedUInt(cbBoardingHouse, out var b))
                bhId = b;

            uint? roomId = null;
            if (TryGetComboSelectedUInt(cbRoom, out var r))
                roomId = r;

            var from = dtFilterFrom.Value;
            var to = dtFilterTo.Value;

            var sql = @"
                SELECT 
                    res.id,
                    bh.name AS boarding_house,
                    rm.room_no,
                    COALESCE(st.full_name, tn.full_name, CONCAT('Occupant #', o.id)) AS occupant_name,
                    res.reserved_from,
                    res.reserved_to,
                    res.status
                FROM reservations res
                INNER JOIN rooms rm ON rm.id = res.room_id
                INNER JOIN boarding_houses bh ON bh.id = rm.boarding_house_id
                INNER JOIN occupants o ON o.id = res.occupant_id
                LEFT JOIN student_occupant_map som ON som.occupant_id = o.id
                LEFT JOIN students st ON st.id = som.student_id
                LEFT JOIN tenant_occupant_map tom ON tom.occupant_id = o.id
                LEFT JOIN tenants tn ON tn.id = tom.tenant_id
                WHERE res.reserved_from <= @to
                  AND res.reserved_to   >= @from
            ";

            if (bhId.HasValue) sql += " AND rm.boarding_house_id = @bhId";
            if (roomId.HasValue) sql += " AND res.room_id = @roomId";
            if (!string.Equals(status, "ALL", StringComparison.OrdinalIgnoreCase)) sql += " AND res.status = @status";

            if (!string.IsNullOrWhiteSpace(search))
            {
                sql += @"
                  AND (
                        CAST(res.id AS CHAR) LIKE CONCAT('%', @search, '%')
                     OR rm.room_no LIKE CONCAT('%', @search, '%')
                     OR COALESCE(st.full_name, tn.full_name, '') LIKE CONCAT('%', @search, '%')
                  )";
            }

            sql += " ORDER BY res.reserved_from DESC;";

            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@from", from);
            cmd.Parameters.AddWithValue("@to", to);

            if (bhId.HasValue) cmd.Parameters.AddWithValue("@bhId", bhId.Value);
            if (roomId.HasValue) cmd.Parameters.AddWithValue("@roomId", roomId.Value);
            if (!string.Equals(status, "ALL", StringComparison.OrdinalIgnoreCase)) cmd.Parameters.AddWithValue("@status", status);
            if (!string.IsNullOrWhiteSpace(search)) cmd.Parameters.AddWithValue("@search", search);

            var dt = new DataTable();
            using var da = new MySqlDataAdapter(cmd);
            da.Fill(dt);

            dgvReservations.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                dgvReservations.Rows.Add(
                    row["id"].ToString(),
                    row["boarding_house"].ToString(),
                    row["room_no"].ToString(),
                    row["occupant_name"].ToString(),
                    Convert.ToDateTime(row["reserved_from"]).ToString("yyyy-MM-dd HH:mm"),
                    Convert.ToDateTime(row["reserved_to"]).ToString("yyyy-MM-dd HH:mm"),
                    row["status"].ToString()
                );
            }

            // keep selection if still exists
            if (_selectedReservationId.HasValue)
            {
                foreach (DataGridViewRow gr in dgvReservations.Rows)
                {
                    if (uint.TryParse(gr.Cells[0].Value?.ToString(), out var id) && id == _selectedReservationId.Value)
                    {
                        gr.Selected = true;
                        dgvReservations.FirstDisplayedScrollingRowIndex = gr.Index;
                        break;
                    }
                }
            }
            else
            {
                ClearSummary();
                dgvAuditLogs.Rows.Clear();
            }

            UpdateActionButtons();
        }

        // ---------------------------
        // GRID SELECTION -> SUMMARY + LOGS
        // ---------------------------
        private void dgvReservations_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvReservations.Rows[e.RowIndex];
            if (row.Cells[0].Value == null) return;

            if (!uint.TryParse(row.Cells[0].Value.ToString(), out var id)) return;

            _selectedReservationId = id;
            LoadReservationSummary(id);
            LoadAuditLogs(id);
            UpdateActionButtons();
        }

        private void LoadReservationSummary(uint reservationId)
        {
            using var con = Conn();

            var sql = @"
                SELECT 
                    res.id, res.status, res.notes, res.reserved_from, res.reserved_to,
                    rm.room_no,
                    bh.name AS boarding_house,
                    COALESCE(st.full_name, tn.full_name, CONCAT('Occupant #', o.id)) AS occupant_name,
                    COALESCE(st.contact_no, tn.contact_no, '') AS contact_no
                FROM reservations res
                INNER JOIN rooms rm ON rm.id = res.room_id
                INNER JOIN boarding_houses bh ON bh.id = rm.boarding_house_id
                INNER JOIN occupants o ON o.id = res.occupant_id
                LEFT JOIN student_occupant_map som ON som.occupant_id = o.id
                LEFT JOIN students st ON st.id = som.student_id
                LEFT JOIN tenant_occupant_map tom ON tom.occupant_id = o.id
                LEFT JOIN tenants tn ON tn.id = tom.tenant_id
                WHERE res.id=@id
                LIMIT 1;";

            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", reservationId);

            using var rd = cmd.ExecuteReader();
            if (!rd.Read())
            {
                ClearSummary();
                return;
            }

            var status = rd["status"]?.ToString() ?? "-";
            var from = Convert.ToDateTime(rd["reserved_from"]);
            var to = Convert.ToDateTime(rd["reserved_to"]);

            lblSelectedId.Text = $"Reservation # {rd["id"]}";
            lblSelectedStatusBadge.Text = status;
            PaintStatusBadge(status);

            lblOccName.Text = $"Occupant: {rd["occupant_name"]}";
            lblOccContact.Text = $"Contact: {rd["contact_no"]}";
            lblRoomNo.Text = $"Room: {rd["room_no"]}";
            lblBHName.Text = $"Boarding House: {rd["boarding_house"]}";
            txtSelectedNotes.Text = rd["notes"]?.ToString() ?? "";

            txtAlerts.Text = BuildAlerts(status, from, to);
        }

        private void ClearSummary()
        {
            lblSelectedId.Text = "Reservation # -";
            lblSelectedStatusBadge.Text = "-";
            lblSelectedStatusBadge.BackColor = Color.SlateGray;

            lblOccName.Text = "Occupant: -";
            lblOccContact.Text = "Contact: -";
            lblRoomNo.Text = "Room: -";
            lblBHName.Text = "Boarding House: -";
            txtSelectedNotes.Text = "";
            txtAlerts.Text = "";
        }

        private string BuildAlerts(string status, DateTime from, DateTime to)
        {
            var sb = new StringBuilder();

            if (string.Equals(status, "PENDING", StringComparison.OrdinalIgnoreCase) && from < DateTime.Now)
                sb.AppendLine("• Start time already passed. Consider EXPIRED or CANCELLED.");

            if (string.Equals(status, "APPROVED", StringComparison.OrdinalIgnoreCase) && DateTime.Now > to)
                sb.AppendLine("• Approved but end time passed. Consider EXPIRED.");

            if (to <= from)
                sb.AppendLine("• Invalid range: 'To' must be after 'From'.");

            return sb.ToString().Trim();
        }

        private void PaintStatusBadge(string status)
        {
            status = (status ?? "").ToUpperInvariant();
            lblSelectedStatusBadge.ForeColor = Color.White;

            switch (status)
            {
                case "PENDING": lblSelectedStatusBadge.BackColor = Color.DarkOrange; break;
                case "APPROVED": lblSelectedStatusBadge.BackColor = Color.SeaGreen; break;
                case "REJECTED": lblSelectedStatusBadge.BackColor = Color.Firebrick; break;
                case "CANCELLED": lblSelectedStatusBadge.BackColor = Color.DimGray; break;
                case "EXPIRED": lblSelectedStatusBadge.BackColor = Color.SlateGray; break;
                case "CHECKED_IN": lblSelectedStatusBadge.BackColor = Color.SteelBlue; break;
                case "CONVERTED": lblSelectedStatusBadge.BackColor = Color.DarkSlateGray; break;
                default: lblSelectedStatusBadge.BackColor = Color.SlateGray; break;
            }
        }

        private void LoadAuditLogs(uint reservationId)
        {
            using var con = Conn();

            var dt = new DataTable();
            using var da = new MySqlDataAdapter(@"
                SELECT
                    a.created_at,
                    a.action,
                    COALESCE(NULLIF(u.name, ''), 'System') AS user_name,
                    a.details
                FROM audit_logs a
                LEFT JOIN users u ON u.idusers = a.user_id
                WHERE a.entity = 'reservations'
                  AND a.entity_id = @id
                ORDER BY a.created_at DESC
                LIMIT 200;", con);

            da.SelectCommand.Parameters.AddWithValue("@id", reservationId);
            da.Fill(dt);

            dgvAuditLogs.Rows.Clear();
            foreach (DataRow r in dt.Rows)
            {
                dgvAuditLogs.Rows.Add(
                    Convert.ToDateTime(r["created_at"]).ToString("yyyy-MM-dd HH:mm"),
                    r["action"]?.ToString(),
                    r["user_name"]?.ToString(),
                    r["details"]?.ToString()
                );
            }
        }

        private void UpdateActionButtons()
        {
            bool hasSelection = _selectedReservationId.HasValue;

            btnApprove.Visible = false;
            btnReject.Visible = false;
            btnCancelReservation.Visible = false;
            btnCheckIn.Visible = false;
            btnConvertToRental.Visible = false;
            btnEditReservation.Visible = hasSelection;

            btnApprove.Enabled = hasSelection;
            btnReject.Enabled = hasSelection;
            btnCancelReservation.Enabled = hasSelection;
            btnCheckIn.Enabled = hasSelection;
            btnConvertToRental.Enabled = hasSelection;
            btnEditReservation.Enabled = hasSelection;

            if (!hasSelection) return;

            var status = (lblSelectedStatusBadge.Text ?? "").ToUpperInvariant();

            if (status == "CONVERTED")
            {
                btnEditReservation.Visible = false;
                btnEditReservation.Enabled = false;
                return;
            }

            if (status == "PENDING")
            {
                btnApprove.Visible = true;
                btnReject.Visible = true;
                btnCancelReservation.Visible = true;
                return;
            }

            if (status == "APPROVED")
            {
                btnCancelReservation.Visible = true;
                btnCheckIn.Visible = true;
                btnConvertToRental.Visible = true;
                return;
            }

            if (status == "CHECKED_IN")
            {
                btnConvertToRental.Visible = true;
                return;
            }
        }

        // ---------------------------
        // HEADER ACTIONS
        // ---------------------------
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvReservations.Rows.Count == 0)
            {
                MessageBox.Show("No reservations to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "CSV File (*.csv)|*.csv",
                FileName = $"reservations_{DateTime.Now:yyyyMMdd_HHmm}.csv"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            var sb = new StringBuilder();
            sb.AppendLine("ID,BoardingHouse,RoomNo,Occupant,From,To,Status");

            foreach (DataGridViewRow row in dgvReservations.Rows)
            {
                if (row.IsNewRow) continue;
                sb.AppendLine(string.Join(",",
                    Csv(row.Cells[0].Value),
                    Csv(row.Cells[1].Value),
                    Csv(row.Cells[2].Value),
                    Csv(row.Cells[3].Value),
                    Csv(row.Cells[4].Value),
                    Csv(row.Cells[5].Value),
                    Csv(row.Cells[6].Value)
                ));
            }

            File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
            MessageBox.Show("Exported successfully.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static string Csv(object v)
        {
            var s = (v?.ToString() ?? "").Replace("\"", "\"\"");
            if (s.Contains(",") || s.Contains("\n") || s.Contains("\r")) s = $"\"{s}\"";
            return s;
        }

        // ---------------------------
        // NEW / EDIT (EDITOR PANEL)
        // ---------------------------
        private void btnNewReservation_Click(object sender, EventArgs e)
        {
            _editorIsEditMode = false;
            _editorReservationId = null;
            lblEditorTitle.Text = "Reservation Editor (New)";
            btnAddUpdate.Text = "Add";

            ClearEditorFields();
            _loadingEditorCombos = true;
            try
            {
                cbEditorBoardingHouse.SelectedIndex = -1;
                cbEditorRoom.DataSource = null;
                cbEditorOccupant.DataSource = null;
                cbEditorRoom.Enabled = false;
                cbEditorOccupant.Enabled = false;
            }
            finally
            {
                _loadingEditorCombos = false;
            }

            pnlEditor.Visible = true;
            lblConflictWarning.Visible = false;
        }

        private void btnEditReservation_Click(object sender, EventArgs e)
        {
            if (!_selectedReservationId.HasValue) return;

            _editorIsEditMode = true;
            _editorReservationId = _selectedReservationId.Value;

            lblEditorTitle.Text = $"Reservation Editor (Edit #{_editorReservationId})";
            btnAddUpdate.Text = "Update";
            pnlEditor.Visible = true;

            LoadReservationToEditor(_editorReservationId.Value);
            lblConflictWarning.Visible = false;
        }

        private void HideEditor()
        {
            pnlEditor.Visible = false;
            _editorIsEditMode = false;
            _editorReservationId = null;
        }

        private void btnEditorClear_Click(object sender, EventArgs e)
        {
            ClearEditorFields();
            lblConflictWarning.Visible = false;
        }

        private void ClearEditorFields()
        {
            txtEditorNotes.Text = "";
            cbEditorStatus.SelectedIndex = 0;
            dtFrom.Value = DateTime.Now.AddHours(1);
            dtTo.Value = DateTime.Now.AddHours(2);
        }

        private void LoadReservationToEditor(uint reservationId)
        {
            using var con = Conn();

            using var cmd = new MySqlCommand(@"
                SELECT occupant_id, room_id, reserved_from, reserved_to, status, notes
                FROM reservations
                WHERE id=@id
                LIMIT 1;", con);

            cmd.Parameters.AddWithValue("@id", reservationId);

            using var rd = cmd.ExecuteReader();
            if (!rd.Read()) return;

            var occupantId = Convert.ToUInt32(rd["occupant_id"]);
            var roomId = Convert.ToUInt32(rd["room_id"]);
            var reservedFrom = Convert.ToDateTime(rd["reserved_from"]);
            var reservedTo = Convert.ToDateTime(rd["reserved_to"]);
            var status = rd["status"]?.ToString() ?? "PENDING";
            var notes = rd["notes"]?.ToString() ?? "";

            rd.Close();

            // get bh of the room
            using var cmd2 = new MySqlCommand("SELECT boarding_house_id FROM rooms WHERE id=@rid LIMIT 1;", con);
            cmd2.Parameters.AddWithValue("@rid", roomId);
            var bhIdObj = cmd2.ExecuteScalar();
            if (bhIdObj != null && uint.TryParse(bhIdObj.ToString(), out var bhId))
            {
                _loadingEditorCombos = true;
                cbEditorBoardingHouse.SelectedValue = bhId;
                _loadingEditorCombos = false;

                cbEditorRoom.Enabled = true;
                cbEditorOccupant.Enabled = true;
                LoadRoomsByBoardingHouse(cbEditorRoom, bhId);
                LoadOccupantsForEditorByBoardingHouse(bhId);
                cbEditorRoom.SelectedValue = roomId;
                cbEditorOccupant.SelectedValue = occupantId;
            }

            dtFrom.Value = reservedFrom;
            dtTo.Value = reservedTo;
            cbEditorStatus.SelectedItem = status;
            txtEditorNotes.Text = notes;
        }

        private void cbEditorBoardingHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loadingEditorCombos) return;
            if (!TryGetComboSelectedUInt(cbEditorBoardingHouse, out var bhId))
            {
                cbEditorRoom.DataSource = null;
                cbEditorOccupant.DataSource = null;
                cbEditorRoom.Enabled = false;
                cbEditorOccupant.Enabled = false;
                return;
            }

            cbEditorRoom.Enabled = true;
            cbEditorOccupant.Enabled = true;
            LoadRoomsByBoardingHouse(cbEditorRoom, bhId);
            LoadOccupantsForEditorByBoardingHouse(bhId);
            CheckEditorConflict();
        }

        private void cbEditorRoom_SelectedIndexChanged(object sender, EventArgs e) => CheckEditorConflict();
        private void cbEditorOccupant_SelectedIndexChanged(object sender, EventArgs e) { /* optional */ }
        private void cbEditorStatus_SelectedIndexChanged(object sender, EventArgs e) { /* optional */ }

        private void dtFrom_ValueChanged(object sender, EventArgs e) => CheckEditorConflict();
        private void dtTo_ValueChanged(object sender, EventArgs e) => CheckEditorConflict();

        private void btnAddUpdate_Click(object sender, EventArgs e)
        {
            if (cbEditorOccupant.SelectedValue == null || cbEditorRoom.SelectedValue == null)
            {
                MessageBox.Show("Please select occupant and room.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TryGetComboSelectedUInt(cbEditorOccupant, out var occupantId) ||
                !TryGetComboSelectedUInt(cbEditorRoom, out var roomId))
            {
                MessageBox.Show("Invalid occupant/room selection.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var from = dtFrom.Value;
            var to = dtTo.Value;

            if (to <= from)
            {
                MessageBox.Show("'To' must be after 'From'.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (HasConflict(roomId, from, to, _editorIsEditMode ? _editorReservationId : null))
            {
                lblConflictWarning.Visible = true;
                MessageBox.Show("Conflict detected: room already reserved for selected time.", "Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lblConflictWarning.Visible = false;

            var status = cbEditorStatus.SelectedItem?.ToString() ?? "PENDING";
            var notes = (txtEditorNotes.Text ?? "").Trim();

            using var con = Conn();

            if (!_editorIsEditMode)
            {
                using var cmd = new MySqlCommand(@"
                    INSERT INTO reservations (occupant_id, room_id, reserved_from, reserved_to, status, notes, created_at, updated_at)
                    VALUES (@occ, @room, @from, @to, @status, @notes, NOW(), NOW());", con);

                cmd.Parameters.AddWithValue("@occ", occupantId);
                cmd.Parameters.AddWithValue("@room", roomId);
                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@notes", string.IsNullOrWhiteSpace(notes) ? (object)DBNull.Value : notes);

                cmd.ExecuteNonQuery();

                _selectedReservationId = (uint)cmd.LastInsertedId;
                WriteAuditLog(_selectedReservationId.Value, "CREATE", "system", $"Created reservation ({status})");
            }
            else
            {
                if (!_editorReservationId.HasValue)
                {
                    MessageBox.Show("No reservation selected for update.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var cmd = new MySqlCommand(@"
                    UPDATE reservations
                    SET occupant_id=@occ,
                        room_id=@room,
                        reserved_from=@from,
                        reserved_to=@to,
                        status=@status,
                        notes=@notes,
                        updated_at=NOW()
                    WHERE id=@id;", con);

                cmd.Parameters.AddWithValue("@id", _editorReservationId.Value);
                cmd.Parameters.AddWithValue("@occ", occupantId);
                cmd.Parameters.AddWithValue("@room", roomId);
                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@notes", string.IsNullOrWhiteSpace(notes) ? (object)DBNull.Value : notes);

                cmd.ExecuteNonQuery();

                _selectedReservationId = _editorReservationId.Value;
                WriteAuditLog(_selectedReservationId.Value, "UPDATE", "system", $"Updated reservation ({status})");
            }

            HideEditor();
            RefreshReservations();

            if (_selectedReservationId.HasValue)
            {
                LoadReservationSummary(_selectedReservationId.Value);
                LoadAuditLogs(_selectedReservationId.Value);
            }
            UpdateActionButtons();
        }

        private void btnVoidSelected_Click(object sender, EventArgs e)
        {
            // cancel selected from editor panel
            if (!_selectedReservationId.HasValue)
            {
                MessageBox.Show("Select a reservation first.", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CancelReservation(_selectedReservationId.Value, "Cancelled from editor panel.");
        }

        private void CheckEditorConflict()
        {
            if (cbEditorRoom.SelectedValue == null) return;
            if (!TryGetComboSelectedUInt(cbEditorRoom, out var roomId)) return;

            var from = dtFrom.Value;
            var to = dtTo.Value;
            if (to <= from)
            {
                lblConflictWarning.Visible = true;
                lblConflictWarning.Text = "Invalid range: 'To' must be after 'From'.";
                return;
            }

            bool has = HasConflict(roomId, from, to, _editorIsEditMode ? _editorReservationId : null);
            lblConflictWarning.Visible = has;
            lblConflictWarning.Text = "Conflict detected: room already reserved for selected time.";
        }

        private bool HasConflict(uint roomId, DateTime from, DateTime to, uint? ignoreReservationId)
        {
            using var con = Conn();

            // conflict with other reservations not cancelled/rejected/expired
            // overlap: existing_from < new_to AND existing_to > new_from
            var sql = @"
                SELECT COUNT(*)
                FROM reservations
                WHERE room_id=@room
                  AND status IN ('PENDING','APPROVED','CHECKED_IN')
                  AND reserved_from < @to
                  AND reserved_to   > @from
            ";

            if (ignoreReservationId.HasValue) sql += " AND id <> @ignoreId";

            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@room", roomId);
            cmd.Parameters.AddWithValue("@from", from);
            cmd.Parameters.AddWithValue("@to", to);
            if (ignoreReservationId.HasValue) cmd.Parameters.AddWithValue("@ignoreId", ignoreReservationId.Value);

            var count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }

        private static bool StatusEquals(string? a, string b)
        {
            return string.Equals((a ?? string.Empty).Trim(), b, StringComparison.OrdinalIgnoreCase);
        }

        private static bool ReservationIsConvertible(string? status)
        {
            return StatusEquals(status, "APPROVED") || StatusEquals(status, "CHECKED_IN");
        }

        private static bool IsInvalidReservationStatusValue(MySqlException ex)
        {
            if (ex == null) return false;
            if (ex.Number == 1265 || ex.Number == 1366) return true;

            var msg = ex.Message ?? string.Empty;
            return msg.IndexOf("Data truncated for column", StringComparison.OrdinalIgnoreCase) >= 0
                || msg.IndexOf("Incorrect enum value", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private bool HasActiveRentalForSameRoomOccupant(uint roomId, uint occupantId, MySqlConnection con, MySqlTransaction tx)
        {
            using var cmd = new MySqlCommand(@"
                SELECT COUNT(*)
                FROM rentals
                WHERE room_id = @room
                  AND occupant_id = @occ
                  AND status = 'ACTIVE';", con, tx);
            cmd.Parameters.AddWithValue("@room", roomId);
            cmd.Parameters.AddWithValue("@occ", occupantId);
            var count = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
            return count > 0;
        }

        // ---------------------------
        // STATUS ACTIONS
        // ---------------------------
        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (!_selectedReservationId.HasValue) return;
            SetReservationStatus(_selectedReservationId.Value, "APPROVED", "Approved reservation.");
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            if (!_selectedReservationId.HasValue) return;
            SetReservationStatus(_selectedReservationId.Value, "REJECTED", "Rejected reservation.");
        }

        private void btnCancelReservation_Click(object sender, EventArgs e)
        {
            if (!_selectedReservationId.HasValue) return;
            CancelReservation(_selectedReservationId.Value, "Cancelled reservation.");
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            if (!_selectedReservationId.HasValue) return;
            SetReservationStatus(_selectedReservationId.Value, "CHECKED_IN", "Checked in reservation.");
        }

        private void btnConvertToRental_Click(object sender, EventArgs e)
        {
            if (!_selectedReservationId.HasValue) return;

            var id = _selectedReservationId.Value;
            var confirm = MessageBox.Show(
                "Convert this reservation to an ACTIVE rental?",
                "Confirm Convert",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            using var con = Conn();
            using var tx = con.BeginTransaction();

            try
            {
                // lock and load reservation
                using var cmd = new MySqlCommand(@"
                    SELECT occupant_id, room_id, reserved_from, reserved_to, status
                    FROM reservations
                    WHERE id=@id
                    LIMIT 1
                    FOR UPDATE;", con, tx);
                cmd.Parameters.AddWithValue("@id", id);

                uint occupantId, roomId;
                DateTime from;
                DateTime? to = null;
                string reservationStatus;
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read())
                    {
                        tx.Rollback();
                        MessageBox.Show("Reservation not found.", "Convert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    occupantId = Convert.ToUInt32(rd["occupant_id"]);
                    roomId = Convert.ToUInt32(rd["room_id"]);
                    from = Convert.ToDateTime(rd["reserved_from"]);
                    to = rd["reserved_to"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rd["reserved_to"]);
                    reservationStatus = rd["status"]?.ToString() ?? string.Empty;
                }

                if (StatusEquals(reservationStatus, "CONVERTED"))
                {
                    tx.Rollback();
                    MessageBox.Show("This reservation is already converted.", "Convert",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!ReservationIsConvertible(reservationStatus))
                {
                    tx.Rollback();
                    MessageBox.Show("Only APPROVED or CHECKED_IN reservations can be converted.", "Convert",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (HasActiveRentalForSameRoomOccupant(roomId, occupantId, con, tx))
                {
                    tx.Rollback();
                    MessageBox.Show("An ACTIVE rental already exists for this room and occupant.", "Convert",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // get room monthly rate
                using var cmdRate = new MySqlCommand("SELECT monthly_rate FROM rooms WHERE id=@rid LIMIT 1;", con, tx);
                cmdRate.Parameters.AddWithValue("@rid", roomId);
                var rateObj = cmdRate.ExecuteScalar();
                decimal monthlyRate = rateObj != null ? Convert.ToDecimal(rateObj) : 0m;

                // insert rental
                using var cmdIns = new MySqlCommand(@"
                    INSERT INTO rentals (room_id, occupant_id, start_date, end_date, monthly_rate, deposit_amount, status, notes, created_by, created_at, updated_at)
                    VALUES (@room, @occ, @start, @end, @rate, 0.00, 'ACTIVE', @notes, @createdBy, NOW(), NOW());", con, tx);
                cmdIns.Parameters.AddWithValue("@room", roomId);
                cmdIns.Parameters.AddWithValue("@occ", occupantId);
                cmdIns.Parameters.AddWithValue("@start", from.Date);
                cmdIns.Parameters.AddWithValue("@end", to.HasValue ? (object)to.Value.Date : DBNull.Value);
                cmdIns.Parameters.AddWithValue("@rate", monthlyRate);
                cmdIns.Parameters.AddWithValue("@notes", $"Converted from reservation #{id}");
                cmdIns.Parameters.AddWithValue("@createdBy", CurrentUserId > 0 ? (object)CurrentUserId : DBNull.Value);
                cmdIns.ExecuteNonQuery();
                long rentalId = cmdIns.LastInsertedId;

                bool usedFallbackStatus = false;
                string conversionNote = $"Converted to rental #{rentalId} on {DateTime.Now:yyyy-MM-dd HH:mm}.";

                try
                {
                    using var cmdUpd = new MySqlCommand(@"
                        UPDATE reservations
                        SET status = 'CONVERTED',
                            notes = CASE
                                WHEN notes IS NULL OR TRIM(notes) = '' THEN @note
                                ELSE CONCAT(notes, CHAR(10), @note)
                            END,
                            updated_at = NOW()
                        WHERE id = @id;", con, tx);
                    cmdUpd.Parameters.AddWithValue("@id", id);
                    cmdUpd.Parameters.AddWithValue("@note", conversionNote);
                    cmdUpd.ExecuteNonQuery();
                }
                catch (MySqlException ex) when (IsInvalidReservationStatusValue(ex))
                {
                    usedFallbackStatus = true;
                    using var cmdFallback = new MySqlCommand(@"
                        UPDATE reservations
                        SET status = 'CHECKED_IN',
                            notes = CASE
                                WHEN notes IS NULL OR TRIM(notes) = '' THEN @note
                                ELSE CONCAT(notes, CHAR(10), @note)
                            END,
                            updated_at = NOW()
                        WHERE id = @id;", con, tx);
                    cmdFallback.Parameters.AddWithValue("@id", id);
                    cmdFallback.Parameters.AddWithValue("@note", conversionNote + " (fallback status)");
                    cmdFallback.ExecuteNonQuery();
                }

                tx.Commit();

                string terminalStatus = usedFallbackStatus ? "CHECKED_IN" : "CONVERTED";
                WriteAuditLog(id, "CONVERT", "system",
                    $"Converted reservation to rental #{rentalId}. terminal_status={terminalStatus}");
                MessageBox.Show("Converted to rental successfully.", "Convert", MessageBoxButtons.OK, MessageBoxIcon.Information);

                RefreshReservations();
                LoadReservationSummary(id);
                LoadAuditLogs(id);
                UpdateActionButtons();
            }
            catch (Exception ex)
            {
                try { tx.Rollback(); } catch { }
                MessageBox.Show($"Failed to convert: {ex.Message}", "Convert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetReservationStatus(uint reservationId, string newStatus, string logDetails)
        {
            using var con = Conn();

            using var cmd = new MySqlCommand("UPDATE reservations SET status=@s, updated_at=NOW() WHERE id=@id;", con);
            cmd.Parameters.AddWithValue("@id", reservationId);
            cmd.Parameters.AddWithValue("@s", newStatus);

            cmd.ExecuteNonQuery();

            WriteAuditLog(reservationId, $"STATUS:{newStatus}", "system", logDetails);

            RefreshReservations();
            LoadReservationSummary(reservationId);
            LoadAuditLogs(reservationId);
            UpdateActionButtons();
        }

        private void CancelReservation(uint reservationId, string details)
        {
            var confirm = MessageBox.Show("Cancel this reservation?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            SetReservationStatus(reservationId, "CANCELLED", details);
        }

        // ---------------------------
        // AUDIT LOG WRITER
        // ---------------------------
        private void WriteAuditLog(uint reservationId, string action, string userName, string details)
        {
            try
            {
                using var con = Conn();
                using var cmd = new MySqlCommand(@"
                    INSERT INTO audit_logs (user_id, action, entity, entity_id, details, created_at)
                    VALUES (@uid, @act, 'reservations', @rid, @det, NOW());", con);

                cmd.Parameters.AddWithValue("@rid", reservationId);
                if (CurrentUserId > 0)
                    cmd.Parameters.AddWithValue("@uid", CurrentUserId);
                else
                    cmd.Parameters.AddWithValue("@uid", DBNull.Value);
                cmd.Parameters.AddWithValue("@act", action);
                cmd.Parameters.AddWithValue("@det", details);

                cmd.ExecuteNonQuery();
            }
            catch
            {
            }
        }

        private void dgvAuditLogs_CellClick(object sender, DataGridViewCellEventArgs e) { }
    }
}
