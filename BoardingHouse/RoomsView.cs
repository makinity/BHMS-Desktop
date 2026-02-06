using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace BoardingHouse
{
    public partial class RoomsView : UserControl
    {
        private int _selectedBhId = 0;
        private int _selectedRoomId = 0;
        private int _selectedTenantId = 0;
        private int _selectedRentalId = 0;

        public int CurrentUserId { get; set; }

        public RoomsView()
        {
            InitializeComponent();
        }

        private void RoomsView_Load(object sender, EventArgs e)
        {
            SetupStatusFilter();
            SetupDetailsStatusButtons();

            LoadBoardingHouseDropdown();

            // default load
            if (cbBoardingHouses.Items.Count > 0)
                cbBoardingHouses.SelectedIndex = 0;
        }

        private void SetupStatusFilter()
        {
            cbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStatusFilter.Items.Clear();
            cbStatusFilter.Items.Add("ALL");
            cbStatusFilter.Items.Add("AVAILABLE");
            cbStatusFilter.Items.Add("OCCUPIED");
            cbStatusFilter.Items.Add("MAINTENANCE");
            cbStatusFilter.Items.Add("INACTIVE");
            cbStatusFilter.SelectedIndex = 0;
        }

        private void SetupDetailsStatusButtons()
        {
            btnMarkAvailable.Click += (s, e) => TrySetRoomStatus("AVAILABLE");
            btnMarkOccupied.Click += (s, e) => TrySetRoomStatus("OCCUPIED");
            btnMarkMaintenance.Click += (s, e) => TrySetRoomStatus("MAINTENANCE");
            btnMarkInactive.Click += (s, e) => TrySetRoomStatus("INACTIVE");

            btnCloseDetails.Click += (s, e) =>
            {
                detailsModal.Visible = false;
                _selectedRoomId = 0;
            };
        }

        private void LoadBoardingHouseDropdown()
        {
            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = conn.CreateCommand())
                {
                    EnsureOpen(conn);

                    cmd.CommandText = @"
                        SELECT id, name
                        FROM boarding_houses
                        ORDER BY name ASC;
                    ";

                    var dt = new DataTable();
                    using (var ad = new MySqlDataAdapter(cmd))
                        ad.Fill(dt);

                    BindBoardingHouseDropdowns(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load boarding houses.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindBoardingHouseDropdowns(DataTable dt)
        {
            var mainSource = new BindingSource { DataSource = dt };
            cbBoardingHouses.DisplayMember = "name";
            cbBoardingHouses.ValueMember = "id";
            cbBoardingHouses.DataSource = mainSource;

            var modalSource = new BindingSource { DataSource = dt.Copy() };
            addRoomBhCb.DisplayMember = "name";
            addRoomBhCb.ValueMember = "id";
            addRoomBhCb.DataSource = modalSource;
        }

        private void cbBoardingHouses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBoardingHouses.SelectedValue == null) return;

            if (int.TryParse(cbBoardingHouses.SelectedValue.ToString(), out int bhId))
            {
                _selectedBhId = bhId;
                LoadRoomsTiles();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            LoadRoomsTiles();
        }

        private void cbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRoomsTiles();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            txtSearch.Text = "";
            cbStatusFilter.SelectedIndex = 0;
            LoadRoomsTiles();
        }

        private void addRoomBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            addRoomModal.Visible = true;
            CenterAddRoomModal();
            addRoomModal.BringToFront();
        }

        private void addRoomCancelBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            HideAddRoomModal();
        }

        private void addRoomCloseBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            HideAddRoomModal();
        }

        private void addRoomSaveBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            if (!ValidateAddRoomInputs(out string error))
            {
                MessageBox.Show(error, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SaveRoom();
        }

        private void LoadRoomsTiles()
        {
            flpRooms.SuspendLayout();
            flpRooms.Controls.Clear();

            if (_selectedBhId <= 0)
            {
                flpRooms.ResumeLayout();
                return;
            }

            string keyword = (txtSearch.Text ?? "").Trim();
            string statusFilter = cbStatusFilter.SelectedItem?.ToString() ?? "ALL";

            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = conn.CreateCommand())
                {
                    EnsureOpen(conn);

                    cmd.CommandText = @"
                    SELECT
                        r.id,
                        r.room_no,
                        r.room_type,
                        r.capacity,
                        r.monthly_rate,
                        r.status,
                        (
                            SELECT COUNT(*)
                            FROM rentals ren
                            WHERE ren.room_id = r.id
                              AND ren.status = 'ACTIVE'
                              AND (ren.end_date IS NULL OR ren.end_date >= CURDATE())
                        ) AS active_tenants_count
                    FROM rooms r
                    WHERE r.boarding_house_id = @bhId
                      AND (
                            @kw = '' OR
                            r.room_no LIKE @kwLike OR
                            r.room_type LIKE @kwLike
                          )
                      AND (@st = 'ALL' OR r.status = @st)
                    ORDER BY r.room_no ASC;
                ";


                    cmd.Parameters.AddWithValue("@bhId", _selectedBhId);
                    cmd.Parameters.AddWithValue("@kw", keyword);
                    cmd.Parameters.AddWithValue("@kwLike", "%" + keyword + "%");
                    cmd.Parameters.AddWithValue("@st", statusFilter);

                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            int roomId = Convert.ToInt32(r["id"]);
                            string roomNo = r["room_no"]?.ToString() ?? "";
                            string type = r["room_type"]?.ToString() ?? "";
                            int cap = r["capacity"] == DBNull.Value ? 0 : Convert.ToInt32(r["capacity"]);
                            string status = r["status"]?.ToString() ?? "";
                            int activeTenantCount = r["active_tenants_count"] == DBNull.Value ? 0 : Convert.ToInt32(r["active_tenants_count"]);

                            decimal rate = 0m;
                            if (r["monthly_rate"] != DBNull.Value)
                                rate = Convert.ToDecimal(r["monthly_rate"], CultureInfo.InvariantCulture);

                            var card = BuildRoomCard(roomId, roomNo, type, cap, rate, status, activeTenantCount);
                            flpRooms.Controls.Add(card);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load rooms.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                flpRooms.ResumeLayout();
                UpdateCardHighlights();
            }
        }

        private void UpdateCardHighlights()
        {
            foreach (Control control in flpRooms.Controls)
            {
                if (control is Panel panel && panel.Tag is RoomCardMeta meta)
                {
                    ApplyCardStyling(panel, meta);
                    panel.Invalidate();
                }
            }
        }

        private void ApplyCardStyling(Panel card, RoomCardMeta meta)
        {
            bool isSelected = meta.RoomId == _selectedRoomId;
            card.Padding = new Padding(10);
            card.Margin = new Padding(10);
            card.BorderStyle = BorderStyle.FixedSingle;
            card.BackColor = isSelected ? LightenColor(meta.BaseColor, 0.06f) : meta.BaseColor;
            if (isSelected)
                card.BorderStyle = BorderStyle.Fixed3D;
        }

        private static Color LightenColor(Color color, double factor)
        {
            int r = color.R + (int)((255 - color.R) * factor);
            int g = color.G + (int)((255 - color.G) * factor);
            int b = color.B + (int)((255 - color.B) * factor);
            return Color.FromArgb(Math.Min(255, r), Math.Min(255, g), Math.Min(255, b));
        }

        private static Color DarkenColor(Color color, double factor)
        {
            int r = (int)(color.R * (1 - factor));
            int g = (int)(color.G * (1 - factor));
            int b = (int)(color.B * (1 - factor));
            return Color.FromArgb(Math.Max(0, r), Math.Max(0, g), Math.Max(0, b));
        }

        private void RoomCard_Paint(object sender, PaintEventArgs e)
        {
            if (sender is Panel card && card.Tag is RoomCardMeta meta && meta.RoomId == _selectedRoomId)
            {
                using var brush = new SolidBrush(Color.FromArgb(0, 120, 215));
                e.Graphics.FillRectangle(brush, 0, 0, 6, card.Height);
            }
        }

        private Control BuildRoomCard(int roomId, string roomNo, string type, int cap, decimal rate, string status, int activeTenantCount)
        {
            Color baseColor = GetStatusColor(status);
            var meta = new RoomCardMeta(roomId, baseColor);

            var chip = new Label
            {
                AutoSize = true,
                BackColor = DarkenColor(baseColor, 0.25),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Padding = new Padding(8, 2, 8, 2),
                Text = status ?? "",
                TextAlign = ContentAlignment.MiddleCenter
            };
            chip.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            var preferredChipSize = chip.GetPreferredSize(Size.Empty);
            chip.Size = preferredChipSize;

            var panel = new Panel
            {
                Width = 245,
                Height = 130,
                Margin = new Padding(10),
                BackColor = baseColor,
                Cursor = Cursors.Hand,
                Tag = meta
            };
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Paint += RoomCard_Paint;
            panel.MouseEnter += Card_MouseEnter;
            panel.MouseLeave += Card_MouseLeave;

            var lblTitle = new Label
            {
                Text = $"Room: {roomNo}",
                AutoSize = false,
                Dock = DockStyle.Top,
                Height = 28,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(5, 0, 5, 0),
                BackColor = Color.Transparent
            };

            var lblTypeCap = new Label
            {
                Text = $"Type: {type}  |  Cap: {cap}",
                AutoSize = false,
                Dock = DockStyle.Top,
                Height = 22,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(5, 0, 5, 0),
                BackColor = Color.Transparent
            };

            var lblRate = new Label
            {
                Text = $"Rate: {rate:N2}",
                AutoSize = false,
                Dock = DockStyle.Top,
                Height = 20,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(5, 0, 5, 0),
                BackColor = Color.Transparent
            };

            var lblTenant = new Label
            {
                Text = activeTenantCount > 0 ? $"Tenants: {activeTenantCount}" : "Tenants: (none)",
                AutoSize = false,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = activeTenantCount > 0 ? Color.Black : Color.DimGray,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(5, 0, 5, 0),
                BackColor = Color.Transparent
            };

            chip.Tag = meta;

            panel.Controls.Add(lblTenant);
            panel.Controls.Add(lblRate);
            panel.Controls.Add(lblTypeCap);
            panel.Controls.Add(lblTitle);
            panel.Controls.Add(chip);
            chip.BringToFront();

            foreach (Control c in panel.Controls)
                c.Click += RoomCard_Click;

            panel.Click += RoomCard_Click;

            PositionChip(panel, chip);
            panel.SizeChanged += (s, e) => PositionChip(panel, chip);

            ApplyCardStyling(panel, meta);

            return panel;
        }

        private Color GetStatusColor(string status)
        {
            switch ((status ?? "").Trim().ToUpperInvariant())
            {
                case "AVAILABLE": return Color.FromArgb(210, 255, 210);
                case "OCCUPIED": return Color.FromArgb(255, 230, 180);
                case "MAINTENANCE": return Color.FromArgb(220, 220, 220);
                case "INACTIVE": return Color.FromArgb(240, 200, 200);
                default: return Color.White;
            }
        }

        private void RoomCard_Click(object sender, EventArgs e)
        {
            SoundClicked.itemClicked();
            Control clicked = sender as Control;
            if (clicked == null) return;

            int roomId = 0;
            if (clicked.Tag is RoomCardMeta directMeta)
                roomId = directMeta.RoomId;
            else if (clicked.Tag is int directId)
                roomId = directId;
            else if (clicked.Parent != null)
            {
                if (clicked.Parent.Tag is RoomCardMeta parentMeta)
                    roomId = parentMeta.RoomId;
                else if (clicked.Parent.Tag is int parentId)
                    roomId = parentId;
            }

            if (roomId <= 0) return;

            _selectedRoomId = roomId;
            UpdateCardHighlights();
            LoadRoomDetails(roomId);

            detailsModal.Visible = true;
            detailsModal.BringToFront();
        }

        private void LoadRoomDetails(int roomId)
        {
            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = conn.CreateCommand())
                {
                    EnsureOpen(conn);

                    cmd.CommandText = @"
                        SELECT
                            r.id,
                            r.room_no,
                            r.room_type,
                            r.capacity,
                            r.monthly_rate,
                            r.status,
                            r.notes
                        FROM rooms r
                        WHERE r.id = @id
                        LIMIT 1;
                    ";
                    cmd.Parameters.AddWithValue("@id", roomId);

                    using (var r = cmd.ExecuteReader())
                    {
                        if (!r.Read())
                        {
                            MessageBox.Show("Room not found.", "Info",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            detailsModal.Visible = false;
                            _selectedRoomId = 0;
                            return;
                        }

                        lblRoomTitle.Text = $"Room Details (ID: {roomId})";

                        detailsRoomNo.Text = r["room_no"]?.ToString() ?? "";
                        detailsRoomType.Text = r["room_type"]?.ToString() ?? "";
                        detailsCapacity.Text = r["capacity"] == DBNull.Value ? "0" : r["capacity"].ToString();

                        decimal rate = 0m;
                        if (r["monthly_rate"] != DBNull.Value)
                            rate = Convert.ToDecimal(r["monthly_rate"], CultureInfo.InvariantCulture);
                        detailsRate.Text = rate.ToString("N2");

                        detailsStatus.Text = r["status"]?.ToString() ?? "";
                        detailsNotes.Text = r["notes"] == DBNull.Value ? "" : r["notes"]?.ToString();
                        LoadRoomTenants(roomId);
                        int activeTenantCount = GetActiveTenantCount(roomId);
                        int capacity = r["capacity"] == DBNull.Value
                        ? 0
                        : Convert.ToInt32(r["capacity"]);
                        lblOccupancy.Text = $"Occupancy: {activeTenantCount} / {capacity}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load room details.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TrySetRoomStatus(string newStatus)
        {
            if (_selectedRoomId <= 0) return;

            bool hasActiveRental = HasActiveRental(_selectedRoomId);

            // Relationship rules:
            // - If active rental exists => room is occupied by business logic; block other statuses
            if (hasActiveRental && newStatus != "OCCUPIED")
            {
                MessageBox.Show(
                    "This room has an ACTIVE rental.\n" +
                    "You cannot set it to AVAILABLE/MAINTENANCE/INACTIVE while occupied.\n\n" +
                    "End the rental first.",
                    "Action Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // - If no active rental => should not be set to OCCUPIED directly
            if (!hasActiveRental && newStatus == "OCCUPIED")
            {
                MessageBox.Show(
                    "This room has NO active rental.\n" +
                    "To mark as OCCUPIED, create an ACTIVE rental for this room.",
                    "Action Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = conn.CreateCommand())
                {
                    EnsureOpen(conn);

                    cmd.CommandText = @"
                        UPDATE rooms
                        SET status = @st, updated_at = NOW()
                        WHERE id = @id
                        LIMIT 1;
                    ";
                    cmd.Parameters.AddWithValue("@st", newStatus);
                    cmd.Parameters.AddWithValue("@id", _selectedRoomId);

                    int affected = cmd.ExecuteNonQuery();
                    if (affected <= 0)
                    {
                        MessageBox.Show("No changes saved.", "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                // refresh UI
                LoadRoomDetails(_selectedRoomId);
                LoadRoomsTiles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update status.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetActiveTenantCount(int roomId)
        {
            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                using var cmd = conn.CreateCommand();
                EnsureOpen(conn);

                cmd.CommandText = @"
                        SELECT COUNT(*)
                        FROM rentals
                        WHERE room_id = @roomId
                          AND status = 'ACTIVE'
                          AND (end_date IS NULL OR end_date >= CURDATE());
                    ";
                cmd.Parameters.AddWithValue("@roomId", roomId);

                var v = cmd.ExecuteScalar();
                return (v == null || v == DBNull.Value) ? 0 : Convert.ToInt32(v);
            }
            catch
            {
                return 0;
            }
        }

        private void LoadRoomTenants(int roomId)
        {
            detailsTenantsList.Items.Clear();
            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                using var cmd = conn.CreateCommand();
                EnsureOpen(conn);

                cmd.CommandText = @"
                        SELECT t.lastname, t.firstname, t.middlename
                        FROM rentals r
                        JOIN tenants t ON t.id = r.tenant_id
                        WHERE r.room_id = @roomId
                          AND r.status = 'ACTIVE'
                          AND (r.end_date IS NULL OR r.end_date >= CURDATE())
                        ORDER BY t.lastname, t.firstname;
                    ";
                cmd.Parameters.AddWithValue("@roomId", roomId);

                bool found = false;
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string last = (reader["lastname"]?.ToString() ?? "").Trim();
                    string first = (reader["firstname"]?.ToString() ?? "").Trim();
                    string middle = (reader["middlename"]?.ToString() ?? "").Trim();
                    string full = $"{last}, {first}";
                    if (!string.IsNullOrWhiteSpace(middle))
                        full += $" {middle}";
                    detailsTenantsList.Items.Add(full.Trim(' ', ','));
                    found = true;
                }

                if (!found)
                    detailsTenantsList.Items.Add("(none)");
            }
            catch
            {
                detailsTenantsList.Items.Add("(none)");
            }
        }

        private bool HasActiveRental(int roomId)
        {
            return GetActiveTenantCount(roomId) > 0;
        }

        private static void EnsureOpen(MySqlConnection conn)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
        }

        private void CenterAddRoomModal()
        {
            addRoomModal.Left = (ClientSize.Width - addRoomModal.Width) / 2;
            addRoomModal.Top = (ClientSize.Height - addRoomModal.Height) / 2;
        }

        private void RoomsView_Resize(object sender, EventArgs e)
        {
            if (addRoomModal.Visible)
                CenterAddRoomModal();
        }

        private void HideAddRoomModal()
        {
            addRoomModal.Visible = false;
            ClearAddRoomModalInputs();
        }

        private void ClearAddRoomModalInputs()
        {
            if (addRoomBhCb.Items.Count > 0)
                addRoomBhCb.SelectedIndex = -1;
            addRoomRoomNoTxt.Text = "";
            addRoomTypeTxt.Text = "";
            addRoomCapNum.Value = addRoomCapNum.Minimum;
            addRoomRateNum.Value = addRoomRateNum.Minimum;
            if (addRoomStatusCb.Items.Count > 0)
            {
                int availableIndex = addRoomStatusCb.Items.IndexOf("AVAILABLE");
                addRoomStatusCb.SelectedIndex = availableIndex >= 0 ? availableIndex : 0;
            }
            addRoomNotesTxt.Text = "";
        }

        private bool ValidateAddRoomInputs(out string error)
        {
            if (addRoomBhCb.SelectedValue == null)
            {
                error = "Select a boarding house.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(addRoomRoomNoTxt.Text))
            {
                error = "Room number is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(addRoomTypeTxt.Text))
            {
                error = "Room type is required.";
                return false;
            }

            if (addRoomCapNum.Value < 1)
            {
                error = "Capacity must be at least 1.";
                return false;
            }

            if (addRoomRateNum.Value <= 0)
            {
                error = "Monthly rate must be greater than 0.";
                return false;
            }

            if (addRoomStatusCb.SelectedItem == null)
            {
                error = "Select a room status.";
                return false;
            }

            error = null;
            return true;
        }

        private void Card_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Panel card && card.Tag is RoomCardMeta meta)
            {
                if (meta.RoomId == _selectedRoomId) return;
                card.BackColor = LightenColor(meta.BaseColor, 0.08);
                card.BorderStyle = BorderStyle.Fixed3D;
            }
        }

        private void Card_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Panel card && card.Tag is RoomCardMeta meta)
            {
                var cursorPos = card.PointToClient(Cursor.Position);
                if (card.ClientRectangle.Contains(cursorPos)) return;
                if (meta.RoomId == _selectedRoomId) return;
                card.BackColor = meta.BaseColor;
                card.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void PositionChip(Panel card, Label chip)
        {
            chip.Location = new Point(card.ClientSize.Width - chip.Width - 12, 8);
        }

        private void detailsModal_Paint(object sender, PaintEventArgs e)
        {

        }

        private class TenantListItem
        {
            public int TenantId { get; }
            public string Display { get; }

            public TenantListItem(int tenantId, string display)
            {
                TenantId = tenantId;
                Display = display;
            }

            public override string ToString() => Display;
        }

        private class CurrentTenantItem
        {
            public int RentalId { get; }
            public int TenantId { get; }
            public string Display { get; }

            public CurrentTenantItem(int rentalId, int tenantId, string display)
            {
                RentalId = rentalId;
                TenantId = tenantId;
                Display = display;
            }

            public override string ToString() => Display;
        }


        private class RoomCardMeta
        {
            public int RoomId { get; }
            public Color BaseColor { get; }

            public RoomCardMeta(int roomId, Color baseColor)
            {
                RoomId = roomId;
                BaseColor = baseColor;
            }
        }

        private void grpDetails_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            detailsModal.Visible = false;
        }

        private void topBar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void addRoomModal_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SaveRoom()
        {
            int bhId = Convert.ToInt32(addRoomBhCb.SelectedValue);
            string roomNo = (addRoomRoomNoTxt.Text ?? "").Trim();
            string type = (addRoomTypeTxt.Text ?? "").Trim();
            int capacity = Convert.ToInt32(addRoomCapNum.Value);
            decimal rate = addRoomRateNum.Value;
            string status = addRoomStatusCb.SelectedItem?.ToString() ?? "AVAILABLE";
            string notes = (addRoomNotesTxt.Text ?? "").Trim();

            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = conn.CreateCommand())
                {
                    EnsureOpen(conn);

                    // optional: prevent duplicates in same boarding house
                    cmd.CommandText = @"
                SELECT COUNT(*)
                FROM rooms
                WHERE boarding_house_id = @bhId
                  AND room_no = @roomNo
                LIMIT 1;
            ";
                    cmd.Parameters.AddWithValue("@bhId", bhId);
                    cmd.Parameters.AddWithValue("@roomNo", roomNo);

                    int exists = Convert.ToInt32(cmd.ExecuteScalar());
                    if (exists > 0)
                    {
                        MessageBox.Show("Room number already exists in this boarding house.",
                            "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    cmd.Parameters.Clear();
                    cmd.CommandText = @"
                INSERT INTO rooms
                    (boarding_house_id, room_no, room_type, capacity, monthly_rate, status, notes, created_at, updated_at)
                VALUES
                    (@bhId, @roomNo, @type, @cap, @rate, @st, @notes, NOW(), NOW());
            ";
                    cmd.Parameters.AddWithValue("@bhId", bhId);
                    cmd.Parameters.AddWithValue("@roomNo", roomNo);
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@cap", capacity);
                    cmd.Parameters.AddWithValue("@rate", rate);
                    cmd.Parameters.AddWithValue("@st", status);
                    cmd.Parameters.AddWithValue("@notes", string.IsNullOrWhiteSpace(notes) ? (object)DBNull.Value : notes);

                    cmd.ExecuteNonQuery();
                }

                HideAddRoomModal();

                // refresh list (and keep current BH selection consistent)
                _selectedBhId = bhId;
                cbBoardingHouses.SelectedValue = bhId;
                LoadRoomsTiles();

                MessageBox.Show("Room added successfully.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save room.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMarkAvailable_Click(object sender, EventArgs e)
        {

        }

        private void btnMarkOccupied_Click(object sender, EventArgs e)
        {

        }

        private void btnMarkMaintenance_Click(object sender, EventArgs e)
        {

        }

        private void btnMarkInactive_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void flpRooms_Paint(object sender, PaintEventArgs e)
        {

        }

        private void manageTenantsModal_Paint(object sender, PaintEventArgs e)
        {

        }


        private void assignTenantBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            if (_selectedRoomId <= 0)
            {
                MessageBox.Show("Please select a room first.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_selectedTenantId <= 0)
            {
                MessageBox.Show("Save/select a tenant first before assigning.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = conn.CreateCommand())
                {
                    EnsureOpen(conn);

                    // Get capacity + monthly_rate from room
                    cmd.CommandText = @"SELECT capacity, monthly_rate FROM rooms WHERE id = @id LIMIT 1;";
                    cmd.Parameters.AddWithValue("@id", _selectedRoomId);

                    int capacity = 0;
                    decimal roomRate = 0m;

                    using (var r = cmd.ExecuteReader())
                    {
                        if (!r.Read())
                        {
                            MessageBox.Show("Room not found.", "Info",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        capacity = r["capacity"] == DBNull.Value ? 0 : Convert.ToInt32(r["capacity"]);
                        roomRate = r["monthly_rate"] == DBNull.Value ? 0m : Convert.ToDecimal(r["monthly_rate"]);
                    }

                    if (roomRate <= 0m)
                    {
                        MessageBox.Show("This room has an invalid monthly rate. Please update the room rate first.",
                            "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Active tenants count (ACTIVE rentals)
                    cmd.Parameters.Clear();
                    cmd.CommandText = @"
                SELECT COUNT(*)
                FROM rentals
                WHERE room_id = @roomId
                  AND status = 'ACTIVE'
                  AND (end_date IS NULL OR end_date >= CURDATE());
            ";
                    cmd.Parameters.AddWithValue("@roomId", _selectedRoomId);

                    int activeCount = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);

                    if (capacity > 0 && activeCount >= capacity)
                    {
                        MessageBox.Show("Room is already full based on its capacity.", "Action Denied",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Tenant already has ACTIVE rental anywhere?
                    cmd.Parameters.Clear();
                    cmd.CommandText = @"
                SELECT COUNT(*)
                FROM rentals
                WHERE tenant_id = @tenantId
                  AND status = 'ACTIVE'
                  AND (end_date IS NULL OR end_date >= CURDATE());
            ";
                    cmd.Parameters.AddWithValue("@tenantId", _selectedTenantId);

                    int tenantActive = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                    if (tenantActive > 0)
                    {
                        MessageBox.Show("This tenant already has an ACTIVE room.\nEnd their current stay first.", "Action Denied",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    cmd.Parameters.Clear();
                    cmd.CommandText = @"
                        INSERT INTO rentals
                            (tenant_id, room_id, start_date, end_date, monthly_rate, deposit_amount,
                             status, notes, created_by, created_at, updated_at)
                        VALUES
                            (@tenantId, @roomId, CURDATE(), NULL, @rate, @deposit,
                             'ACTIVE', NULL, @createdBy, NOW(), NOW());
                    ";
                    cmd.Parameters.AddWithValue("@tenantId", _selectedTenantId);
                    cmd.Parameters.AddWithValue("@roomId", _selectedRoomId);
                    cmd.Parameters.AddWithValue("@rate", roomRate);     
                    cmd.Parameters.AddWithValue("@deposit", 0.00m);     
                    cmd.Parameters.AddWithValue("@createdBy", CurrentUserId > 0 ? (object)CurrentUserId : DBNull.Value);

                    cmd.ExecuteNonQuery();

                }

                LoadRoomDetails(_selectedRoomId);
                LoadRoomsTiles();

                MessageBox.Show("Tenant assigned successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to assign tenant.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string BuildFullName(string ln, string fn, string mn)
        {
            ln = (ln ?? "").Trim();
            fn = (fn ?? "").Trim();
            mn = (mn ?? "").Trim();

            string full = $"{ln}, {fn}";
            if (!string.IsNullOrWhiteSpace(mn))
                full += $" {mn}";

            return full.Trim();
        }

        private void flpRooms_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void lblOccupancy_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            if (_selectedRoomId <= 0)
            {
                MessageBox.Show("Please select a room first.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Block delete if ACTIVE rental exists
            if (HasActiveRental(_selectedRoomId))
            {
                MessageBox.Show(
                    "This room has an ACTIVE rental.\n\nEnd the rental first before deleting the room.",
                    "Action Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            var confirm = MessageBox.Show(
                "Are you sure you want to delete this room?\nThis action cannot be undone.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                int bhId = _selectedBhId;

                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = conn.CreateCommand())
                {
                    EnsureOpen(conn);

                    // get bhId for refresh safety
                    cmd.CommandText = @"
                SELECT boarding_house_id
                FROM rooms
                WHERE id = @id
                LIMIT 1;
            ";
                    cmd.Parameters.AddWithValue("@id", _selectedRoomId);

                    var bhObj = cmd.ExecuteScalar();
                    if (bhObj != null && bhObj != DBNull.Value)
                        bhId = Convert.ToInt32(bhObj);

                    cmd.Parameters.Clear();
                    cmd.CommandText = @"
                DELETE FROM rooms
                WHERE id = @id
                LIMIT 1;
            ";
                    cmd.Parameters.AddWithValue("@id", _selectedRoomId);

                    int affected = cmd.ExecuteNonQuery();
                    if (affected <= 0)
                    {
                        MessageBox.Show("Room not found or already deleted.", "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                _selectedRoomId = 0;
                detailsModal.Visible = false;

                _selectedBhId = bhId;
                LoadRoomsTiles();

                MessageBox.Show("Room deleted successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to delete room.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            if (_selectedRoomId <= 0)
            {
                MessageBox.Show("Please select a room first.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string roomNo = (detailsRoomNo.Text ?? "").Trim();
            string type = (detailsRoomType.Text ?? "").Trim();
            string notes = (detailsNotes.Text ?? "").Trim();

            if (string.IsNullOrWhiteSpace(roomNo))
            {
                MessageBox.Show("Room number is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                detailsRoomNo.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(type))
            {
                MessageBox.Show("Room type is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                detailsRoomType.Focus();
                return;
            }

            // capacity (int)
            if (!int.TryParse((detailsCapacity.Text ?? "").Trim(), out int capacity) || capacity < 1)
            {
                MessageBox.Show("Capacity must be at least 1.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                detailsCapacity.Focus();
                return;
            }

            // rate (decimal) - handle commas
            string rateRaw = (detailsRate.Text ?? "").Trim().Replace(",", "");
            if (!decimal.TryParse(rateRaw, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal rate) || rate <= 0)
            {
                MessageBox.Show("Monthly rate must be greater than 0.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                detailsRate.Focus();
                return;
            }

            try
            {
                int bhId = 0;

                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = conn.CreateCommand())
                {
                    EnsureOpen(conn);

                    // Get BH context of this room
                    cmd.CommandText = @"
                SELECT boarding_house_id
                FROM rooms
                WHERE id = @id
                LIMIT 1;
            ";
                    cmd.Parameters.AddWithValue("@id", _selectedRoomId);

                    var bhObj = cmd.ExecuteScalar();
                    if (bhObj == null || bhObj == DBNull.Value)
                    {
                        MessageBox.Show("Room not found.", "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    bhId = Convert.ToInt32(bhObj);

                    // Prevent duplicate room_no in same BH (excluding this room)
                    cmd.Parameters.Clear();
                    cmd.CommandText = @"
                SELECT COUNT(*)
                FROM rooms
                WHERE boarding_house_id = @bhId
                  AND room_no = @roomNo
                  AND id <> @id
                LIMIT 1;
            ";
                    cmd.Parameters.AddWithValue("@bhId", bhId);
                    cmd.Parameters.AddWithValue("@roomNo", roomNo);
                    cmd.Parameters.AddWithValue("@id", _selectedRoomId);

                    int exists = Convert.ToInt32(cmd.ExecuteScalar());
                    if (exists > 0)
                    {
                        MessageBox.Show("Room number already exists in this boarding house.",
                            "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // If ACTIVE rental exists, do NOT allow capacity/rate/type changes (optional but safe)
                    // You can remove this block if you want to allow edits while occupied.
                    cmd.Parameters.Clear();
                    cmd.CommandText = @"
                SELECT COUNT(*)
                FROM rentals
                WHERE room_id = @roomId
                  AND status = 'ACTIVE'
                  AND (end_date IS NULL OR end_date >= CURDATE())
                LIMIT 1;
            ";
                    cmd.Parameters.AddWithValue("@roomId", _selectedRoomId);
                    int activeRentalCount = Convert.ToInt32(cmd.ExecuteScalar());

                    if (activeRentalCount > 0)
                    {
                        // allow notes only, or block entirely (choose one)
                        // Here we BLOCK structural changes:
                        MessageBox.Show(
                            "This room has an ACTIVE rental.\n" +
                            "You cannot update room number/type/capacity/rate while occupied.\n\n" +
                            "End the rental first.",
                            "Action Denied",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    // Update editable fields (status stays handled by status buttons)
                    cmd.Parameters.Clear();
                    cmd.CommandText = @"
                UPDATE rooms
                SET
                    room_no = @roomNo,
                    room_type = @type,
                    capacity = @cap,
                    monthly_rate = @rate,
                    notes = @notes,
                    updated_at = NOW()
                WHERE id = @id
                LIMIT 1;
            ";
                    cmd.Parameters.AddWithValue("@roomNo", roomNo);
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@cap", capacity);
                    cmd.Parameters.AddWithValue("@rate", rate);
                    cmd.Parameters.AddWithValue("@notes", string.IsNullOrWhiteSpace(notes) ? (object)DBNull.Value : notes);
                    cmd.Parameters.AddWithValue("@id", _selectedRoomId);

                    int affected = cmd.ExecuteNonQuery();
                    if (affected <= 0)
                    {
                        MessageBox.Show("No changes saved.", "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    _selectedBhId = bhId; // keep list aligned
                }

                LoadRoomDetails(_selectedRoomId);
                LoadRoomsTiles();

                MessageBox.Show("Room updated successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update room.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
