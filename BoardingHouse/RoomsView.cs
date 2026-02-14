using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BoardingHouse
{
    public partial class RoomsView : UserControl
    {
        private sealed record TenantCardData(
            int OccupantId,
            int RentalId,
            string FullName,
            string Contact,
            string Address,
            string Email,
            string ProfilePath
        );

        private sealed class OccupantCardMeta
        {
            public int OccupantId { get; }
            public OccupantCardMeta(int occupantId) => OccupantId = occupantId;
        }

        private int _selectedBhId = 0;
        private int _selectedRoomId = 0;
        private int _selectedTenantId = 0;
        private int _selectedRentalId = 0;
        private int _pendingOpenBhId = 0;
        private bool _pendingAutoSelectFirstRoom = true;
        private int _pendingOpenRoomId = 0;

        private FlowLayoutPanel flpViewTenants;
        private readonly Image _tenantPlaceholderImage;

        public int CurrentUserId { get; set; }

        public RoomsView()
        {
            InitializeComponent();
            EnsureViewTenantsContainer();
            _tenantPlaceholderImage = CreateTenantPlaceholderImage();
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
            grpQuickActions.Visible = true;
            updateRoomBtn.Visible = true;
            deleteRoomBtn.Visible = true;

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

                        lblRoomTitle.Text = "Room Details";

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

            int uid = CurrentUserId > 0 ? CurrentUserId : 1;
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
                int roomId = _selectedRoomId;
                object beforeDetails;
                int beforeBhId = 0;
                string beforeRoomNo = "";
                string beforeRoomType = "";
                int? beforeCapacity = null;
                decimal? beforeRate = null;
                string beforeNotes = "";
                string beforeStatus = "";

                using (var conn = DbConnectionFactory.CreateConnection())
                {
                    EnsureOpen(conn);

                    using (var selectCmd = conn.CreateCommand())
                    {
                        selectCmd.CommandText = @"
                        SELECT id, boarding_house_id, room_no, room_type, capacity, monthly_rate, status, notes
                        FROM rooms
                        WHERE id = @id
                        LIMIT 1;
                    ";
                        selectCmd.Parameters.AddWithValue("@id", roomId);

                        using (var reader = selectCmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show("Room not found.", "Info",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            beforeDetails = BuildRoomAuditDetailsFromReader(reader);
                            beforeBhId = reader["boarding_house_id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["boarding_house_id"]);
                            beforeRoomNo = reader["room_no"]?.ToString() ?? "";
                            beforeRoomType = reader["room_type"]?.ToString() ?? "";
                            beforeCapacity = reader["capacity"] == DBNull.Value ? null : Convert.ToInt32(reader["capacity"]);
                            beforeRate = reader["monthly_rate"] == DBNull.Value ? null : Convert.ToDecimal(reader["monthly_rate"], CultureInfo.InvariantCulture);
                            beforeStatus = reader["status"]?.ToString() ?? "";
                            beforeNotes = reader["notes"] == DBNull.Value ? "" : reader["notes"]?.ToString() ?? "";
                        }
                    }

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                        UPDATE rooms
                        SET status = @st, updated_at = NOW()
                        WHERE id = @id
                        LIMIT 1;
                    ";

                        cmd.Parameters.AddWithValue("@st", newStatus);
                        cmd.Parameters.AddWithValue("@id", roomId);

                        int affected = cmd.ExecuteNonQuery();
                        if (affected <= 0)
                        {
                            MessageBox.Show("No changes saved.", "Info",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    var afterDetails = BuildRoomAuditDetails(
                        roomId,
                        beforeBhId,
                        beforeRoomNo,
                        beforeRoomType,
                        beforeCapacity,
                        beforeRate,
                        newStatus,
                        beforeNotes);
                    AuditLogger.Log(uid, "UPDATE", "rooms", roomId, new
                    {
                        before = beforeDetails,
                        after = afterDetails
                    });
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
                        SELECT o.full_name, o.lastname, o.firstname, o.middlename
                        FROM rentals r
                        JOIN occupants o ON o.id = r.occupant_id
                        WHERE r.room_id = @roomId
                          AND r.status = 'ACTIVE'
                          AND (r.end_date IS NULL OR r.end_date >= CURDATE())
                        ORDER BY o.lastname, o.firstname;
                    ";
                cmd.Parameters.AddWithValue("@roomId", roomId);

                bool found = false;
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string fullName = (reader["full_name"]?.ToString() ?? "").Trim();
                    if (string.IsNullOrWhiteSpace(fullName))
                    {
                        string last = (reader["lastname"]?.ToString() ?? "").Trim();
                        string first = (reader["firstname"]?.ToString() ?? "").Trim();
                        string middle = (reader["middlename"]?.ToString() ?? "").Trim();
                        fullName = $"{last}, {first}";
                        if (!string.IsNullOrWhiteSpace(middle))
                            fullName += $" {middle}";
                    }

                    detailsTenantsList.Items.Add(fullName.Trim(' ', ','));
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

        private int GetBoardingHouseIdForRoom(int roomId)
        {
            if (roomId <= 0) return 0;

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                using var cmd = conn.CreateCommand();
                EnsureOpen(conn);

                cmd.CommandText = @"
                    SELECT boarding_house_id
                    FROM rooms
                    WHERE id = @roomId
                    LIMIT 1;
                ";
                cmd.Parameters.AddWithValue("@roomId", roomId);

                var result = cmd.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                    return 0;

                return Convert.ToInt32(result);
            }
            catch
            {
                return 0;
            }
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
            int uid = CurrentUserId > 0 ? CurrentUserId : 1;
            var roomDetails = new
            {
                boarding_house_id = bhId,
                room_no = roomNo,
                room_type = type,
                capacity,
                monthly_rate = rate,
                status,
                notes = string.IsNullOrWhiteSpace(notes) ? null : notes
            };

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

                    using (var idCmd = conn.CreateCommand())
                    {
                        idCmd.CommandText = "SELECT LAST_INSERT_ID();";
                        long newRoomId = Convert.ToInt64(idCmd.ExecuteScalar());
                        AuditLogger.Log(uid, "CREATE", "rooms", newRoomId, roomDetails);
                    }
                }

                HideAddRoomModal();

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

        private void flpRooms_Paint(object sender, PaintEventArgs e)
        {

        }

        private void manageTenantsModal_Paint(object sender, PaintEventArgs e)
        {

        }


        private void addTenantBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            int bhId = _selectedBhId;

            if (bhId <= 0 && _selectedRoomId > 0)
                bhId = GetBoardingHouseIdForRoom(_selectedRoomId);

            if (bhId <= 0 && cbBoardingHouses.SelectedValue != null &&
                int.TryParse(cbBoardingHouses.SelectedValue.ToString(), out int currentBh))
            {
                bhId = currentBh;
            }

            if (FindForm() is not MainLayout main)
                return;

            main.OpenAddTenantModalFromRooms(bhId);
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

            int uid = CurrentUserId > 0 ? CurrentUserId : 1;

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

                    // Resolve occupant_id from selected tenant_id
                    cmd.Parameters.Clear();
                    cmd.CommandText = @"
                SELECT occupant_id
                FROM tenant_occupant_map
                WHERE tenant_id = @tenantId
                LIMIT 1;
            ";
                    cmd.Parameters.AddWithValue("@tenantId", _selectedTenantId);

                    var occObj = cmd.ExecuteScalar();
                    int occupantId = (occObj == null || occObj == DBNull.Value) ? 0 : Convert.ToInt32(occObj);
                    if (occupantId <= 0)
                    {
                        MessageBox.Show("This tenant has no linked occupant record.", "Action Denied",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                WHERE occupant_id = @occupantId
                  AND status = 'ACTIVE'
                  AND (end_date IS NULL OR end_date >= CURDATE());
            ";
                    cmd.Parameters.AddWithValue("@occupantId", occupantId);

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
                            (occupant_id, room_id, start_date, end_date, monthly_rate, deposit_amount,
                             status, notes, created_by, created_at, updated_at)
                        VALUES
                            (@occupantId, @roomId, CURDATE(), NULL, @rate, @deposit,
                             'ACTIVE', NULL, @createdBy, NOW(), NOW());
                    ";
                    cmd.Parameters.AddWithValue("@occupantId", occupantId);
                    cmd.Parameters.AddWithValue("@roomId", _selectedRoomId);
                    cmd.Parameters.AddWithValue("@rate", roomRate);
                    cmd.Parameters.AddWithValue("@deposit", 0.00m);
                    cmd.Parameters.AddWithValue("@createdBy", CurrentUserId > 0 ? (object)CurrentUserId : DBNull.Value);

                    cmd.ExecuteNonQuery();

                    using (var idCmd = conn.CreateCommand())
                    {
                        idCmd.CommandText = "SELECT LAST_INSERT_ID();";
                        long rentalId = Convert.ToInt64(idCmd.ExecuteScalar());
                        var rentalDetails = new
                        {
                            occupant_id = occupantId,
                            tenant_id = _selectedTenantId,
                            room_id = _selectedRoomId,
                            start_date = DateTime.Today,
                            end_date = (DateTime?)null,
                            monthly_rate = roomRate,
                            deposit_amount = 0.00m,
                            status = "ACTIVE",
                            created_by = uid
                        };
                        AuditLogger.Log(uid, "CREATE", "rentals", rentalId, rentalDetails);
                    }

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

        private static object BuildRoomAuditDetails(
            int id,
            int boardingHouseId,
            string roomNo,
            string roomType,
            int? capacity,
            decimal? monthlyRate,
            string status,
            string notes)
        {
            return new
            {
                id,
                boarding_house_id = boardingHouseId,
                room_no = roomNo,
                room_type = roomType,
                capacity,
                monthly_rate = monthlyRate,
                status,
                notes = string.IsNullOrWhiteSpace(notes) ? null : notes
            };
        }

        private static object BuildRoomAuditDetailsFromReader(MySqlDataReader reader)
        {
            string notes = reader["notes"] == DBNull.Value ? "" : reader["notes"]?.ToString() ?? "";
            return BuildRoomAuditDetails(
                reader["id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["id"]),
                reader["boarding_house_id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["boarding_house_id"]),
                reader["room_no"]?.ToString() ?? "",
                reader["room_type"]?.ToString() ?? "",
                reader["capacity"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["capacity"]),
                reader["monthly_rate"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(reader["monthly_rate"], CultureInfo.InvariantCulture),
                reader["status"]?.ToString() ?? "",
                notes
            );
        }

        private void flpRooms_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void lblOccupancy_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void updateRoomBtn_Click(object sender, EventArgs e)
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

            int uid = CurrentUserId > 0 ? CurrentUserId : 1;

            try
            {
                object beforeDetails;
                int beforeBhId = 0;
                string beforeStatus = "";

                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = conn.CreateCommand())
                {
                    EnsureOpen(conn);

                    using (var beforeCmd = conn.CreateCommand())
                    {
                        beforeCmd.CommandText = @"
                SELECT id, boarding_house_id, room_no, room_type, capacity, monthly_rate, status, notes
                FROM rooms
                WHERE id = @id
                LIMIT 1;
            ";
                        beforeCmd.Parameters.AddWithValue("@id", _selectedRoomId);

                        using (var reader = beforeCmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show("Room not found.", "Info",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            beforeDetails = BuildRoomAuditDetailsFromReader(reader);
                            beforeBhId = reader["boarding_house_id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["boarding_house_id"]);
                            beforeStatus = reader["status"]?.ToString() ?? "";
                        }
                    }

                    if (beforeBhId <= 0)
                    {
                        MessageBox.Show("Room booking context not found.", "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    int bhId = beforeBhId;

                    // Prevent duplicate room_no in same BH (excluding this room)
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

                var afterDetails = BuildRoomAuditDetails(
                    _selectedRoomId,
                    beforeBhId,
                    roomNo,
                    type,
                    capacity,
                    rate,
                    beforeStatus,
                    notes
                );
                AuditLogger.Log(uid, "UPDATE", "rooms", _selectedRoomId, new
                {
                    before = beforeDetails,
                    after = afterDetails
                });

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

        private void deleteRoomBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            if (_selectedRoomId <= 0)
            {
                MessageBox.Show("Please select a room first.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

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

            int uid = CurrentUserId > 0 ? CurrentUserId : 1;

            try
            {
                int bhId = _selectedBhId;
                object deletedDetails;

                using (var conn = DbConnectionFactory.CreateConnection())
                {
                    EnsureOpen(conn);

                    using (var selectCmd = conn.CreateCommand())
                    {
                        selectCmd.CommandText = @"
                SELECT id, boarding_house_id, room_no, room_type, capacity, monthly_rate, status, notes
                FROM rooms
                WHERE id = @id
                LIMIT 1;
            ";
                        selectCmd.Parameters.AddWithValue("@id", _selectedRoomId);

                        using (var reader = selectCmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show("Room not found.", "Info",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            bhId = reader["boarding_house_id"] == DBNull.Value ? bhId : Convert.ToInt32(reader["boarding_house_id"]);
                            deletedDetails = BuildRoomAuditDetailsFromReader(reader);
                        }
                    }

                    using (var cmd = conn.CreateCommand())
                    {
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

                    AuditLogger.Log(uid, "DELETE", "rooms", _selectedRoomId, new
                    {
                        deleted = deletedDetails
                    });
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
                string msg = ex.Message ?? string.Empty;
                bool isFkDeleteError =
                    msg.IndexOf("foreign key constraint fails", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    msg.IndexOf("Cannot delete or update parent row", StringComparison.OrdinalIgnoreCase) >= 0;

                if (isFkDeleteError)
                {
                    MessageBox.Show(
                        "This room cannot be deleted because it is referenced by existing rental history.\n\n" +
                        "Although there are no active rentals, there are past rental records that reference this room.\n" +
                        "For data integrity, this room will not be deleted.\n\n" +
                        "Instead, mark the room as INACTIVE.",
                        "Cannot Delete Room",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    var setInactive = MessageBox.Show(
                        "Would you like to mark this room as INACTIVE instead?",
                        "Cannot Delete Room",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (setInactive == DialogResult.Yes)
                    {
                        TrySetRoomStatus("INACTIVE");
                        LoadRoomsTiles();
                        if (_selectedRoomId > 0)
                            LoadRoomDetails(_selectedRoomId);
                    }

                    return;
                }

                MessageBox.Show("Failed to delete room.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void detailsNotes_Click(object sender, EventArgs e)
        {

        }

        private void detailsTenantsList_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            if (_selectedRoomId <= 0)
            {
                MessageBox.Show("Select a room first.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EnsureViewTenantsContainer();
            LoadTenantsReadOnlyPanel(_selectedRoomId);
            viewTenantsPanel.Visible = true;
            viewTenantsPanel.BringToFront();
        }

        private void LoadTenantsReadOnlyPanel(int roomId)
        {
            flpViewTenants.Controls.Clear();
            var tenantRows = new List<TenantCardData>();
            int maxTextWidth = 0;

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                using var cmd = new MySqlCommand(@"
                SELECT
                    o.id AS occupant_id,
                    r.id AS rental_id,
                    o.full_name, o.lastname, o.firstname, o.middlename,
                    o.contact_no, o.address, o.email, o.profile_path
                FROM rentals r
                JOIN occupants o ON o.id = r.occupant_id
                WHERE r.room_id = @roomId
                  AND r.status = 'ACTIVE'
                  AND (r.end_date IS NULL OR r.end_date >= CURDATE())
                ORDER BY o.lastname, o.firstname;", conn);

                cmd.Parameters.AddWithValue("@roomId", roomId);

                using var reader = cmd.ExecuteReader();
                using var nameFont = new Font(FontFamily.GenericSansSerif, 10f, FontStyle.Bold);
                using var regularFont = new Font(FontFamily.GenericSansSerif, 9f, FontStyle.Regular);

                while (reader.Read())
                {
                    var fullName = (reader["full_name"]?.ToString() ?? "").Trim();
                    if (string.IsNullOrWhiteSpace(fullName))
                    {
                        fullName = FormatTenantName(
                            reader["lastname"]?.ToString(),
                            reader["firstname"]?.ToString(),
                            reader["middlename"]?.ToString());
                    }

                    var contact = NormalizeTenantField(reader["contact_no"]?.ToString());
                    var address = NormalizeTenantField(reader["address"]?.ToString());
                    var email = NormalizeTenantField(reader["email"]?.ToString());
                    var profilePath = reader["profile_path"]?.ToString();
                    int occupantId = SafeInt(reader["occupant_id"]);
                    int rentalId = SafeInt(reader["rental_id"]);

                    tenantRows.Add(new TenantCardData(
                        occupantId,
                        rentalId,
                        fullName,
                        contact,
                        address,
                        email,
                        profilePath));

                    maxTextWidth = Math.Max(maxTextWidth, MeasureTenantTextWidth(fullName, nameFont));
                    maxTextWidth = Math.Max(maxTextWidth, MeasureTenantTextWidth($"Contact : {contact}", regularFont));
                    maxTextWidth = Math.Max(maxTextWidth, MeasureTenantTextWidth($"Address : {address}", regularFont));
                    if (!string.IsNullOrWhiteSpace(email) && email != "(none)")
                        maxTextWidth = Math.Max(maxTextWidth, MeasureTenantTextWidth($"Email   : {email}", regularFont));
                }
            }
            catch (Exception ex)
            {
                var label = BuildTenantEmptyMessage("Unable to load tenants.");
                label.Text = $"Unable to load tenants: {ex.Message}";
                flpViewTenants.Controls.Add(label);
                ApplyUniformTenantCardWidth();
                flpViewTenants.PerformLayout();
                return;
            }

            EnsureTenantsModalWidthForContent(maxTextWidth);

            if (tenantRows.Count == 0)
            {
                flpViewTenants.Controls.Add(BuildTenantEmptyMessage("No active tenants for this room."));
            }
            else
            {
                foreach (var tenant in tenantRows)
                {
                    flpViewTenants.Controls.Add(BuildTenantCardReadOnly(
                        tenant.OccupantId,
                        tenant.RentalId,
                        tenant.FullName,
                        tenant.Contact,
                        tenant.Address,
                        tenant.Email,
                        tenant.ProfilePath));
                }
            }

            ApplyUniformTenantCardWidth();
            flpViewTenants.PerformLayout();
        }

        private Panel BuildTenantCardReadOnly(
            int occupantId,
            int rentalId,
            string fullName,
            string contact,
            string address,
            string email,
            string profilePath)
        {
            var baseWidth = Math.Max(480, viewTenantsPanel.Width - 40);
            if (baseWidth <= 0) baseWidth = 480;

            var panel = new Panel
            {
                Width = baseWidth,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowOnly,
                Margin = new Padding(10, 0, 10, 10),
                Padding = new Padding(8),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand,
                Tag = new OccupantCardMeta(occupantId)
            };

            var picture = new PictureBox
            {
                Size = new Size(64, 64),
                Location = new Point(8, 8),
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = LoadProfileImage(profilePath)
            };
            panel.Controls.Add(picture);

            var nameLabel = new Label
            {
                Name = "lblTenantName",
                Text = fullName,
                AutoSize = true,
                AutoEllipsis = true,
                Font = new Font(FontFamily.GenericSansSerif, 10f, FontStyle.Bold),
                Location = new Point(84, 8)
            };
            panel.Controls.Add(nameLabel);

            var contactLabel = new Label
            {
                Name = "lblTenantContact",
                Text = $"Contact : {contact}",
                AutoSize = true,
                AutoEllipsis = true,
                Location = new Point(84, 24)
            };
            panel.Controls.Add(contactLabel);

            var addressLabel = new Label
            {
                Name = "lblTenantAddress",
                Text = $"Address : {address}",
                AutoSize = true,
                AutoEllipsis = true,
                Location = new Point(84, 40),
                MaximumSize = new Size(panel.Width - 104, 0)
            };
            panel.Controls.Add(addressLabel);

            if (!string.IsNullOrWhiteSpace(email) && email != "(none)")
            {
                var emailLabel = new Label
                {
                    Name = "lblTenantEmail",
                    Text = $"Email   : {email}",
                    AutoSize = true,
                    AutoEllipsis = true,
                    Location = new Point(84, 56),
                    MaximumSize = new Size(panel.Width - 104, 0)
                };
                panel.Controls.Add(emailLabel);
            }

            panel.Click += TenantCard_Click;
            foreach (Control c in panel.Controls)
                c.Click += TenantCard_Click;

            return panel;
        }

        private void TenantCard_Click(object? sender, EventArgs e)
        {
            if (sender is not Control clicked)
                return;

            var meta = clicked.Tag as OccupantCardMeta ?? clicked.Parent?.Tag as OccupantCardMeta;
            if (meta == null)
                return;

            viewTenantsPanel.Visible = false;

            if (FindForm() is not MainLayout main)
                return;

            if (meta.OccupantId <= 0)
                return;

            main.OpenOccupantDetailsFromRooms(meta.OccupantId);
        }

        private string FormatTenantName(string? last, string? first, string? middle)
        {
            var builder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(last))
                builder.Append(last.Trim());

            if (!string.IsNullOrWhiteSpace(first))
            {
                if (builder.Length > 0)
                    builder.Append(", ");

                builder.Append(first.Trim());
            }

            if (!string.IsNullOrWhiteSpace(middle))
            {
                builder.Append(" ").Append(middle.Trim());
            }

            var result = builder.ToString().Trim();
            return string.IsNullOrWhiteSpace(result) ? "(unknown tenant)" : result;
        }

        private static int SafeInt(object v)
        {
            if (v == null || v == DBNull.Value) return 0;
            if (int.TryParse(v.ToString(), out int x)) return x;
            return 0;
        }

        private static string NormalizeTenantField(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "(none)";

            return value.Trim();
        }

        private Image LoadProfileImage(string? path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return (Image)_tenantPlaceholderImage.Clone();

            try
            {
                if (!File.Exists(path))
                    return (Image)_tenantPlaceholderImage.Clone();

                using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var img = Image.FromStream(stream, true, true);
                return new Bitmap(img);
            }
            catch
            {
                return (Image)_tenantPlaceholderImage.Clone();
            }
        }

        private Image CreateTenantPlaceholderImage()
        {
            var bmp = new Bitmap(64, 64);
            using var g = Graphics.FromImage(bmp);
            g.Clear(Color.FromArgb(230, 230, 230));
            using var pen = new Pen(Color.Gray, 1);
            g.DrawRectangle(pen, 0, 0, bmp.Width - 1, bmp.Height - 1);
            return bmp;
        }

        private Control BuildTenantEmptyMessage(string message)
        {
            return new Label
            {
                Text = message,
                AutoSize = true,
                ForeColor = Color.Gray,
                Font = new Font(FontFamily.GenericSansSerif, 9f, FontStyle.Italic),
                Margin = new Padding(10)
            };
        }

        private void FlpViewTenants_SizeChanged(object sender, EventArgs e)
        {
            ApplyUniformTenantCardWidth();
        }

        private void EnsureTenantsModalWidthForContent(int requiredTextWidth)
        {
            if (viewTenantsPanel == null) return;

            const int extraSpacing = 64 + 12 + 20 + 40;
            var desiredWidth = Math.Max(viewTenantsPanel.Width, requiredTextWidth + extraSpacing);
            var parentWidth = Parent?.ClientSize.Width ?? ClientSize.Width;
            var maxAllowed = Math.Max(viewTenantsPanel.Width, Math.Max(360, parentWidth - viewTenantsPanel.Left - 20));
            desiredWidth = Math.Min(desiredWidth, maxAllowed);

            viewTenantsPanel.Width = Math.Max(viewTenantsPanel.Width, desiredWidth);
            viewTenantsPanel.Left = Math.Max(0, (ClientSize.Width - viewTenantsPanel.Width) / 2);
            ApplyUniformTenantCardWidth();
        }

        private void ApplyUniformTenantCardWidth()
        {
            if (flpViewTenants == null || flpViewTenants.Controls.Count == 0)
                return;

            var targetWidth = Math.Max(360, flpViewTenants.ClientSize.Width - 30);
            if (targetWidth <= 0)
                targetWidth = 360;

            foreach (Control control in flpViewTenants.Controls)
            {
                if (control is Panel panel)
                {
                    panel.Width = targetWidth;
                    LayoutTenantCard(panel);
                }
            }
        }

        private void LayoutTenantCard(Panel panel)
        {
            const int horizontalOffset = 84;
            const int paddingBottom = 8;
            var labelWidth = Math.Max(100, panel.Width - 104);

            var nameLabel = panel.Controls["lblTenantName"] as Label;
            var contactLabel = panel.Controls["lblTenantContact"] as Label;
            var addressLabel = panel.Controls["lblTenantAddress"] as Label;
            var emailLabel = panel.Controls["lblTenantEmail"] as Label;

            if (nameLabel != null) nameLabel.MaximumSize = new Size(labelWidth, 0);
            if (contactLabel != null) contactLabel.MaximumSize = new Size(labelWidth, 0);
            if (addressLabel != null) addressLabel.MaximumSize = new Size(labelWidth, 0);
            if (emailLabel != null) emailLabel.MaximumSize = new Size(labelWidth, 0);

            int y = 8;
            if (nameLabel != null)
            {
                nameLabel.Location = new Point(horizontalOffset, y);
                y += nameLabel.PreferredHeight + 4;
            }

            if (contactLabel != null)
            {
                contactLabel.Location = new Point(horizontalOffset, y);
                y += contactLabel.PreferredHeight + 4;
            }

            if (addressLabel != null)
            {
                addressLabel.Location = new Point(horizontalOffset, y);
                y += addressLabel.PreferredHeight + 4;
            }

            if (emailLabel != null)
            {
                emailLabel.Location = new Point(horizontalOffset, y);
                y += emailLabel.PreferredHeight + 4;
            }

            panel.Height = Math.Max(104, y + paddingBottom);
        }

        private static int MeasureTenantTextWidth(string text, Font font)
        {
            if (string.IsNullOrWhiteSpace(text)) return 0;
            return TextRenderer.MeasureText(text, font, Size.Empty, TextFormatFlags.SingleLine).Width;
        }

        private void EnsureViewTenantsContainer()
        {
            if (flpViewTenants == null)
            {
                flpViewTenants = new FlowLayoutPanel();
                viewTenantsPanel.Controls.Add(flpViewTenants);
                flpViewTenants.SendToBack();
            }

            flpViewTenants.Dock = DockStyle.Fill;
            flpViewTenants.AutoScroll = true;
            flpViewTenants.FlowDirection = FlowDirection.TopDown;
            flpViewTenants.WrapContents = false;
            flpViewTenants.Padding = new Padding(0, 20, 0, 10);
            flpViewTenants.SizeChanged -= FlpViewTenants_SizeChanged;
            flpViewTenants.SizeChanged += FlpViewTenants_SizeChanged;
        }

        private void ViewTanantCLoseBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            viewTenantsPanel.Visible=false;
        }

        public void OpenRoomsForBoardingHouse(int bhId)
        {
            if (bhId <= 0) return;

            if (cbBoardingHouses.DataSource == null || cbBoardingHouses.Items.Count == 0)
                LoadBoardingHouseDropdown();

            _selectedBhId = bhId;

            cbBoardingHouses.SelectedValue = bhId;

            _selectedRoomId = 0;
            detailsModal.Visible = true;    
            detailsModal.BringToFront();
        }

        public void OpenRoomsForTenantAssignedRoom(int roomId)
        {
            if (roomId <= 0)
            {
                MessageBox.Show("Tenant has no assigned room.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            txtSearch.Text = "";
            if (cbStatusFilter.Items.Count > 0)
                cbStatusFilter.SelectedIndex = 0;

            int bhId = GetBoardingHouseIdForRoom(roomId);
            if (bhId <= 0)
            {
                MessageBox.Show("Unable to locate the boarding house for this room.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cbBoardingHouses.DataSource == null || cbBoardingHouses.Items.Count == 0)
                LoadBoardingHouseDropdown();

            _selectedBhId = bhId;

            cbBoardingHouses.SelectedValue = bhId;

            LoadRoomsTiles();

            _selectedRoomId = roomId;
            UpdateCardHighlights();
            LoadRoomDetails(roomId);

            detailsModal.Visible = true;
            detailsModal.BringToFront();
        }



        public void OpenRoomsForBoardingHouse(int bhId, bool autoSelectFirstRoom = true)
        {
            if (bhId <= 0) return;

            _pendingOpenBhId = bhId;
            _pendingAutoSelectFirstRoom = autoSelectFirstRoom;

            if (!IsHandleCreated) return;

            ApplyPendingOpen();
        }

        private void ApplyPendingOpen()
        {
            if (_pendingOpenBhId <= 0) return;

            int bhId = _pendingOpenBhId;
            bool autoPick = _pendingAutoSelectFirstRoom;

            _pendingOpenBhId = 0;

            if (cbBoardingHouses.DataSource == null || cbBoardingHouses.Items.Count == 0)
                LoadBoardingHouseDropdown();

            cbBoardingHouses.SelectedValue = bhId;

            _selectedBhId = bhId;
            LoadRoomsTiles();

            if (autoPick)
                SelectFirstRoomAndOpenDetails();
        }

        private void SelectFirstRoomAndOpenDetails()
        {
            foreach (Control c in flpRooms.Controls)
            {
                if (c is Panel p && p.Tag is RoomCardMeta meta && meta.RoomId > 0)
                {
                    _selectedRoomId = meta.RoomId;
                    UpdateCardHighlights();
                    LoadRoomDetails(meta.RoomId);

                    detailsModal.Visible = true;
                    detailsModal.BringToFront();
                    return;
                }
            }

            detailsModal.Visible = false;
        }

    }
}
