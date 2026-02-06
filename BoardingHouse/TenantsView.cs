using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace BoardingHouse
{
    public partial class TenantsView : UserControl
    {
        private int _selectedBhId = 0;
        private int _selectedTenantId = 0;
        private int _activeRentalId = 0;
        private int _activeRoomId = 0;

        public int CurrentUserId { get; set; }

        public TenantsView()
        {
            InitializeComponent();
        }

        private void TenantsView_Load(object sender, EventArgs e)
        {

        }

        private void TenantsView_Load_1(object sender, EventArgs e)
        {
            SetupStatusFilter();
            SetupDetailsHandlers();

            LoadBoardingHouseDropdown();

            if (cbBoardingHouses.Items.Count > 0)
                cbBoardingHouses.SelectedIndex = 0;

        }

        private void SetupStatusFilter()
        {
            cbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStatusFilter.Items.Clear();
            cbStatusFilter.Items.Add("ALL");
            cbStatusFilter.Items.Add("ACTIVE");
            cbStatusFilter.Items.Add("INACTIVE");
            cbStatusFilter.SelectedIndex = 0;

            cbStatusFilter.SelectedIndexChanged += (s, e) => LoadTenantsGrid();
        }

        private void SetupDetailsHandlers()
        {
            // close buttons (you currently have two X buttons)
            btnCloseDetails.Click += (s, e) => HideDetails();

            btnSearch.Click += (s, e) => LoadTenantsGrid();
            cbBoardingHouses.SelectedIndexChanged += cbBoardingHouses_SelectedIndexChanged;

            tenantUpdateBtn.Click += tenantUpdateBtn_Click;
            tenantDeleteBtn.Click += tenantDeleteBtn_Click;

            // optional: lock the readonly ones in designer, but keep safe here too
            detailsEmail.ReadOnly = true;     // keep if you want readonly
            detailsEContact.ReadOnly = true;  // keep if you want readonly (or set false if you want editable)

            detailsCbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void HideDetails()
        {
            detailsModal.Visible = false;
            _selectedTenantId = 0;
            _activeRentalId = 0;
            _activeRoomId = 0;
        }

        private void LoadBoardingHouseDropdown()
        {
            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                using var cmd = conn.CreateCommand();
                EnsureOpen(conn);

                cmd.CommandText = @"
                    SELECT id, name
                    FROM boarding_houses
                    ORDER BY name ASC;
                ";

                var dt = new DataTable();
                using (var ad = new MySqlDataAdapter(cmd))
                    ad.Fill(dt);

                cbBoardingHouses.DisplayMember = "name";
                cbBoardingHouses.ValueMember = "id";
                cbBoardingHouses.DataSource = dt;

                // also prep details status choices
                detailsCbStatus.Items.Clear();
                detailsCbStatus.Items.Add("ACTIVE");
                detailsCbStatus.Items.Add("INACTIVE");
                if (detailsCbStatus.Items.Count > 0) detailsCbStatus.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load boarding houses.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbBoardingHouses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBoardingHouses.SelectedValue == null) return;

            if (int.TryParse(cbBoardingHouses.SelectedValue.ToString(), out int bhId))
            {
                _selectedBhId = bhId;
                LoadTenantsGrid();
                LoadRoomsDropdownForDetails(bhId);
            }
        }

        private void LoadRoomsDropdownForDetails(int bhId)
        {
            cbDetailsRoom.BeginUpdate();
            cbDetailsRoom.Items.Clear();

            // add NONE option to allow ending stay
            cbDetailsRoom.Items.Add(new RoomPickItem(0, "— NONE —"));

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                using var cmd = conn.CreateCommand();
                EnsureOpen(conn);

                cmd.CommandText = @"
                    SELECT id, room_no, room_type
                    FROM rooms
                    WHERE boarding_house_id = @bhId
                    ORDER BY room_no ASC;
                ";
                cmd.Parameters.AddWithValue("@bhId", bhId);

                using var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    int id = Convert.ToInt32(r["id"]);
                    string roomNo = r["room_no"]?.ToString() ?? "";
                    string roomType = r["room_type"]?.ToString() ?? "";
                    cbDetailsRoom.Items.Add(new RoomPickItem(id, $"{roomNo} - {roomType}".Trim()));
                }
            }
            catch
            {
                // ignore
            }
            finally
            {
                cbDetailsRoom.EndUpdate();
                cbDetailsRoom.SelectedIndex = 0;
            }
        }

        private void LoadTenantsGrid()
        {
            if (_selectedBhId <= 0) return;

            string kw = (txtSearch.Text ?? "").Trim();
            string st = cbStatusFilter.SelectedItem?.ToString() ?? "ALL";

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                using var cmd = conn.CreateCommand();
                EnsureOpen(conn);

                cmd.CommandText = @"
                    SELECT
                        t.id AS tenant_id,
                        t.lastname,
                        t.firstname,
                        t.middlename,
                        t.contact_no,
                        t.email,
                        t.address,
                        t.emergency_contact_name,
                        t.emergency_contact_no,
                        t.status AS tenant_status,

                        ren.id AS active_rental_id,
                        r.id   AS room_id,
                        r.room_no,
                        r.room_type
                    FROM tenants t
                    LEFT JOIN rentals ren
                        ON ren.tenant_id = t.id
                       AND ren.status = 'ACTIVE'
                       AND (ren.end_date IS NULL OR ren.end_date >= CURDATE())
                    LEFT JOIN rooms r
                        ON r.id = ren.room_id
                    LEFT JOIN boarding_houses bh
                        ON bh.id = r.boarding_house_id
                    WHERE
                        (
                            -- include tenant even if no room yet
                            r.boarding_house_id = @bhId OR ren.id IS NULL
                        )
                        AND (@st = 'ALL' OR t.status = @st)
                        AND (
                            @kw = '' OR
                            t.lastname LIKE @kwLike OR
                            t.firstname LIKE @kwLike OR
                            t.middlename LIKE @kwLike OR
                            t.full_name LIKE @kwLike OR
                            t.contact_no LIKE @kwLike OR
                            t.email LIKE @kwLike OR
                            r.room_no LIKE @kwLike OR
                            r.room_type LIKE @kwLike
                        )
                    ORDER BY t.lastname ASC, t.firstname ASC;
                ";

                cmd.Parameters.AddWithValue("@bhId", _selectedBhId);
                cmd.Parameters.AddWithValue("@st", st);
                cmd.Parameters.AddWithValue("@kw", kw);
                cmd.Parameters.AddWithValue("@kwLike", "%" + kw + "%");

                var dt = new DataTable();
                using (var ad = new MySqlDataAdapter(cmd))
                    ad.Fill(dt);

                dgvTenants.AutoGenerateColumns = true;
                dgvTenants.DataSource = dt;

                // optional: make grid nicer
                dgvTenants.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvTenants.MultiSelect = false;
                dgvTenants.ReadOnly = true;
                dgvTenants.AllowUserToAddRows = false;
                dgvTenants.RowHeadersVisible = false;

                // hide IDs but keep them in DataSource
                HideColumnIfExists("tenant_id");
                HideColumnIfExists("active_rental_id");
                HideColumnIfExists("room_id");

                RenameColumnIfExists("lastname", "Lastname");
                RenameColumnIfExists("firstname", "Firstname");
                RenameColumnIfExists("middlename", "Middlename");
                RenameColumnIfExists("contact_no", "Contact");
                RenameColumnIfExists("email", "Email");
                RenameColumnIfExists("tenant_status", "Status");
                RenameColumnIfExists("room_no", "Room No");
                RenameColumnIfExists("room_type", "Room Type");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load tenants.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HideColumnIfExists(string col)
        {
            if (dgvTenants.Columns.Contains(col))
                dgvTenants.Columns[col].Visible = false;
        }

        private void RenameColumnIfExists(string col, string header)
        {
            if (dgvTenants.Columns.Contains(col))
                dgvTenants.Columns[col].HeaderText = header;
        }

        private void dgvTenants_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvTenants.Rows[e.RowIndex];
            if (row == null) return;

            int tenantId = SafeInt(row.Cells["tenant_id"]?.Value);
            if (tenantId <= 0) return;

            LoadTenantDetails(tenantId);

            detailsModal.Visible = true;
            detailsModal.BringToFront();
        }

        private void LoadTenantDetails(int tenantId)
        {
            _selectedTenantId = tenantId;
            _activeRentalId = 0;
            _activeRoomId = 0;

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                using var cmd = conn.CreateCommand();
                EnsureOpen(conn);

                cmd.CommandText = @"
                    SELECT
                        t.id,
                        t.lastname,
                        t.firstname,
                        t.middlename,
                        t.contact_no,
                        t.email,
                        t.address,
                        t.emergency_contact_name,
                        t.emergency_contact_no,
                        t.status,

                        ren.id AS active_rental_id,
                        ren.room_id AS active_room_id
                    FROM tenants t
                    LEFT JOIN rentals ren
                        ON ren.tenant_id = t.id
                       AND ren.status = 'ACTIVE'
                       AND (ren.end_date IS NULL OR ren.end_date >= CURDATE())
                    WHERE t.id = @id
                    LIMIT 1;
                ";
                cmd.Parameters.AddWithValue("@id", tenantId);

                using var r = cmd.ExecuteReader();
                if (!r.Read())
                {
                    MessageBox.Show("Tenant not found.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HideDetails();
                    return;
                }

                detailsLastname.Text = r["lastname"]?.ToString() ?? "";
                detailsFirstname.Text = r["firstname"]?.ToString() ?? "";
                detailsMiddlename.Text = r["middlename"] == DBNull.Value ? "" : r["middlename"]?.ToString() ?? "";
                detailsContact.Text = r["contact_no"] == DBNull.Value ? "" : r["contact_no"]?.ToString() ?? "";
                detailsEmail.Text = r["email"] == DBNull.Value ? "" : r["email"]?.ToString() ?? "";
                detailsAddress.Text = r["address"] == DBNull.Value ? "" : r["address"]?.ToString() ?? "";
                detailsEName.Text = r["emergency_contact_name"] == DBNull.Value ? "" : r["emergency_contact_name"]?.ToString() ?? "";
                detailsEContact.Text = r["emergency_contact_no"] == DBNull.Value ? "" : r["emergency_contact_no"]?.ToString() ?? "";

                string status = r["status"]?.ToString() ?? "ACTIVE";
                int stIndex = detailsCbStatus.Items.IndexOf(status);
                detailsCbStatus.SelectedIndex = stIndex >= 0 ? stIndex : 0;

                _activeRentalId = r["active_rental_id"] == DBNull.Value ? 0 : Convert.ToInt32(r["active_rental_id"]);
                _activeRoomId = r["active_room_id"] == DBNull.Value ? 0 : Convert.ToInt32(r["active_room_id"]);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Failed to load tenant details.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // set room dropdown selection
            SelectRoomInDropdown(_activeRoomId);
        }

        private void SelectRoomInDropdown(int roomId)
        {
            if (cbDetailsRoom.Items.Count <= 0) return;

            for (int i = 0; i < cbDetailsRoom.Items.Count; i++)
            {
                if (cbDetailsRoom.Items[i] is RoomPickItem item && item.RoomId == roomId)
                {
                    cbDetailsRoom.SelectedIndex = i;
                    return;
                }
            }

            // not found => NONE
            cbDetailsRoom.SelectedIndex = 0;
        }

        private void tenantUpdateBtn_Click(object sender, EventArgs e)
        {
            if (_selectedTenantId <= 0) return;

            string ln = (detailsLastname.Text ?? "").Trim();
            string fn = (detailsFirstname.Text ?? "").Trim();
            string mn = (detailsMiddlename.Text ?? "").Trim();

            if (string.IsNullOrWhiteSpace(ln) || string.IsNullOrWhiteSpace(fn))
            {
                MessageBox.Show("Lastname and Firstname are required.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string contact = (detailsContact.Text ?? "").Trim();
            string email = (detailsEmail.Text ?? "").Trim();
            string addr = (detailsAddress.Text ?? "").Trim();
            string eName = (detailsEName.Text ?? "").Trim();
            string eNo = (detailsEContact.Text ?? "").Trim();
            string st = detailsCbStatus.SelectedItem?.ToString() ?? "ACTIVE";

            int pickedRoomId = 0;
            if (cbDetailsRoom.SelectedItem is RoomPickItem roomItem)
                pickedRoomId = roomItem.RoomId;

            // RULE: If tenant has active rental, you cannot set INACTIVE (safe)
            if (_activeRentalId > 0 && st == "INACTIVE")
            {
                MessageBox.Show(
                    "This tenant currently has an ACTIVE rental.\n" +
                    "End the rental first before setting tenant to INACTIVE.",
                    "Action Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // RULE: Room change handling
            // - If has active rental and pickedRoomId != current => block (avoid accidental moves)
            if (_activeRentalId > 0 && pickedRoomId != _activeRoomId)
            {
                MessageBox.Show(
                    "This tenant currently has an ACTIVE rental.\n" +
                    "To move rooms, end the current rental first.",
                    "Action Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                using var cmd = conn.CreateCommand();
                EnsureOpen(conn);

                string fullName = BuildFullName(ln, fn, mn);

                cmd.CommandText = @"
                    UPDATE tenants
                    SET
                        lastname = @ln,
                        firstname = @fn,
                        middlename = @mn,
                        full_name = @full,
                        contact_no = @contact,
                        email = @email,
                        address = @addr,
                        emergency_contact_name = @eName,
                        emergency_contact_no = @eNo,
                        status = @st,
                        updated_at = NOW()
                    WHERE id = @id
                    LIMIT 1;
                ";
                cmd.Parameters.AddWithValue("@ln", ln);
                cmd.Parameters.AddWithValue("@fn", fn);
                cmd.Parameters.AddWithValue("@mn", string.IsNullOrWhiteSpace(mn) ? (object)DBNull.Value : mn);
                cmd.Parameters.AddWithValue("@full", fullName);
                cmd.Parameters.AddWithValue("@contact", string.IsNullOrWhiteSpace(contact) ? (object)DBNull.Value : contact);
                cmd.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(email) ? (object)DBNull.Value : email);
                cmd.Parameters.AddWithValue("@addr", string.IsNullOrWhiteSpace(addr) ? (object)DBNull.Value : addr);
                cmd.Parameters.AddWithValue("@eName", string.IsNullOrWhiteSpace(eName) ? (object)DBNull.Value : eName);
                cmd.Parameters.AddWithValue("@eNo", string.IsNullOrWhiteSpace(eNo) ? (object)DBNull.Value : eNo);
                cmd.Parameters.AddWithValue("@st", st);
                cmd.Parameters.AddWithValue("@id", _selectedTenantId);

                cmd.ExecuteNonQuery();

                // If no active rental and picked room != 0, create ACTIVE rental (assign)
                if (_activeRentalId <= 0 && pickedRoomId > 0)
                {
                    // capacity check + tenant active rental check already implied, but re-check safely
                    if (TenantHasActiveRental(conn, _selectedTenantId))
                    {
                        MessageBox.Show("Tenant already has an ACTIVE rental. Refresh the page.", "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (RoomIsFull(conn, pickedRoomId))
                    {
                        MessageBox.Show("Selected room is already full based on capacity.", "Action Denied",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        decimal roomRate = GetRoomMonthlyRate(conn, pickedRoomId);
                        if (roomRate <= 0m)
                        {
                            MessageBox.Show("Selected room has invalid monthly rate.", "Validation",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            using var cmd2 = conn.CreateCommand();
                            cmd2.CommandText = @"
                                INSERT INTO rentals
                                    (tenant_id, room_id, start_date, end_date, monthly_rate, deposit_amount,
                                     status, notes, created_by, created_at, updated_at)
                                VALUES
                                    (@tenantId, @roomId, CURDATE(), NULL, @rate, @deposit,
                                     'ACTIVE', NULL, @createdBy, NOW(), NOW());
                            ";
                            cmd2.Parameters.AddWithValue("@tenantId", _selectedTenantId);
                            cmd2.Parameters.AddWithValue("@roomId", pickedRoomId);
                            cmd2.Parameters.AddWithValue("@rate", roomRate);
                            cmd2.Parameters.AddWithValue("@deposit", 0.00m);
                            cmd2.Parameters.AddWithValue("@createdBy", CurrentUserId > 0 ? (object)CurrentUserId : DBNull.Value);
                            cmd2.ExecuteNonQuery();
                        }
                    }
                }

                // If active rental exists and picked room == NONE => end rental
                if (_activeRentalId > 0 && pickedRoomId == 0)
                {
                    using var cmd3 = conn.CreateCommand();
                    cmd3.CommandText = @"
                        UPDATE rentals
                        SET status = 'ENDED',
                            end_date = CURDATE(),
                            updated_at = NOW()
                        WHERE id = @id
                        LIMIT 1;
                    ";
                    cmd3.Parameters.AddWithValue("@id", _activeRentalId);
                    cmd3.ExecuteNonQuery();
                }

                MessageBox.Show("Tenant updated successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // refresh
                LoadTenantsGrid();
                LoadTenantDetails(_selectedTenantId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update tenant.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tenantDeleteBtn_Click(object sender, EventArgs e)
        {
            if (_selectedTenantId <= 0) return;

            // block delete if active rental exists
            if (HasActiveRental(_selectedTenantId))
            {
                MessageBox.Show(
                    "This tenant has an ACTIVE rental.\nEnd the rental first before deleting the tenant.",
                    "Action Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            var confirm = MessageBox.Show(
                "Are you sure you want to delete this tenant?\nThis action cannot be undone.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm != DialogResult.Yes) return;

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                using var cmd = conn.CreateCommand();
                EnsureOpen(conn);

                cmd.CommandText = @"DELETE FROM tenants WHERE id = @id LIMIT 1;";
                cmd.Parameters.AddWithValue("@id", _selectedTenantId);

                int affected = cmd.ExecuteNonQuery();
                if (affected <= 0)
                {
                    MessageBox.Show("Tenant not found or already deleted.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MessageBox.Show("Tenant deleted successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                HideDetails();
                LoadTenantsGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to delete tenant.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool HasActiveRental(int tenantId)
        {
            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                using var cmd = conn.CreateCommand();
                EnsureOpen(conn);

                cmd.CommandText = @"
                    SELECT COUNT(*)
                    FROM rentals
                    WHERE tenant_id = @tenantId
                      AND status = 'ACTIVE'
                      AND (end_date IS NULL OR end_date >= CURDATE());
                ";
                cmd.Parameters.AddWithValue("@tenantId", tenantId);

                int c = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                return c > 0;
            }
            catch
            {
                return false;
            }
        }

        private static bool TenantHasActiveRental(MySqlConnection conn, int tenantId)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT COUNT(*)
                FROM rentals
                WHERE tenant_id = @tenantId
                  AND status = 'ACTIVE'
                  AND (end_date IS NULL OR end_date >= CURDATE());
            ";
            cmd.Parameters.AddWithValue("@tenantId", tenantId);
            int c = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
            return c > 0;
        }

        private static bool RoomIsFull(MySqlConnection conn, int roomId)
        {
            int cap = 0;
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT capacity FROM rooms WHERE id = @id LIMIT 1;";
                cmd.Parameters.AddWithValue("@id", roomId);
                var capObj = cmd.ExecuteScalar();
                cap = (capObj == null || capObj == DBNull.Value) ? 0 : Convert.ToInt32(capObj);
            }

            int activeCount = 0;
            using (var cmd2 = conn.CreateCommand())
            {
                cmd2.CommandText = @"
                    SELECT COUNT(*)
                    FROM rentals
                    WHERE room_id = @roomId
                      AND status = 'ACTIVE'
                      AND (end_date IS NULL OR end_date >= CURDATE());
                ";
                cmd2.Parameters.AddWithValue("@roomId", roomId);
                activeCount = Convert.ToInt32(cmd2.ExecuteScalar() ?? 0);
            }

            return cap > 0 && activeCount >= cap;
        }

        private static decimal GetRoomMonthlyRate(MySqlConnection conn, int roomId)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT monthly_rate FROM rooms WHERE id = @id LIMIT 1;";
            cmd.Parameters.AddWithValue("@id", roomId);
            var v = cmd.ExecuteScalar();
            if (v == null || v == DBNull.Value) return 0m;
            return Convert.ToDecimal(v, CultureInfo.InvariantCulture);
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

        private static void EnsureOpen(MySqlConnection conn)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
        }

        private static int SafeInt(object v)
        {
            if (v == null || v == DBNull.Value) return 0;
            if (int.TryParse(v.ToString(), out int x)) return x;
            return 0;
        }

        private class RoomPickItem
        {
            public int RoomId { get; }
            public string Display { get; }

            public RoomPickItem(int roomId, string display)
            {
                RoomId = roomId;
                Display = display;
            }

            public override string ToString() => Display;
        }

        // keep these if your designer wires them; harmless no-ops
        private void dgvTenants_CellContentClick_1(object sender, DataGridViewCellEventArgs e) { }
        private void detailsModal_Paint_1(object sender, PaintEventArgs e) { }
        private void grpDetails_Enter(object sender, EventArgs e) { }
        private void labelRoomNo_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void detailsRoomNo_TextChanged(object sender, EventArgs e) { }
        private void detailsRate_TextChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }

        private void addTenantsModal_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tenantEmergencyContactTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void tenantEmergencyNameTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void tenantLastNameTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void addTenantsBtn_Click(object sender, EventArgs e)
        {
            UpdateTotalTenantsLabel();
            addTenantsModal.Visible = true;
            addTenantsModal.BringToFront();
        }

        private void addTenantCloseBtn_Click(object sender, EventArgs e)
        {
            addTenantsModal.Visible = false;
        }

        private void registerTenantBtn_Click(object sender, EventArgs e)
        {
            if (_selectedBhId <= 0)
            {
                MessageBox.Show("Please select a Boarding House first.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ln = (tenantLastNameTxt.Text ?? "").Trim();
            string fn = (tenantFirstNameTxt.Text ?? "").Trim();
            string mn = (tenantMiddleNameTxt.Text ?? "").Trim();

            if (string.IsNullOrWhiteSpace(ln) || string.IsNullOrWhiteSpace(fn))
            {
                MessageBox.Show("Lastname and Firstname are required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fullName = $"{ln}, {fn}" + (string.IsNullOrWhiteSpace(mn) ? "" : $" {mn}");

            string contact = (tenantContactTxt.Text ?? "").Trim();
            string email = (tenantEmailTxt.Text ?? "").Trim();
            string address = (tenantAddressTxt.Text ?? "").Trim();
            string eName = (tenantEmergencyNameTxt.Text ?? "").Trim();
            string eNo = (tenantEmergencyContactTxt.Text ?? "").Trim();

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                EnsureOpen(conn);

                // optional duplicate check
                using (var chk = conn.CreateCommand())
                {
                    chk.CommandText = @"
                SELECT COUNT(*)
                FROM tenants
                WHERE full_name = @full
                  AND (@contact = '' OR contact_no = @contact)
                LIMIT 1;
            ";
                    chk.Parameters.AddWithValue("@full", fullName);
                    chk.Parameters.AddWithValue("@contact", contact);

                    int exists = Convert.ToInt32(chk.ExecuteScalar() ?? 0);
                    if (exists > 0)
                    {
                        MessageBox.Show("A tenant with the same name/contact already exists.", "Duplicate",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
            INSERT INTO tenants
                (lastname, firstname, middlename, full_name, contact_no, email, address,
                 emergency_contact_name, emergency_contact_no,
                 status, created_at, updated_at)
            VALUES
                (@lastname, @firstname, @middlename, @full_name, @contact_no, @email, @address,
                 @eName, @eNo,
                 'ACTIVE', NOW(), NOW());
        ";

                cmd.Parameters.AddWithValue("@lastname", ln);
                cmd.Parameters.AddWithValue("@firstname", fn);
                cmd.Parameters.AddWithValue("@middlename", string.IsNullOrWhiteSpace(mn) ? (object)DBNull.Value : mn);
                cmd.Parameters.AddWithValue("@full_name", fullName);
                cmd.Parameters.AddWithValue("@contact_no", string.IsNullOrWhiteSpace(contact) ? (object)DBNull.Value : contact);
                cmd.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(email) ? (object)DBNull.Value : email);
                cmd.Parameters.AddWithValue("@address", string.IsNullOrWhiteSpace(address) ? (object)DBNull.Value : address);
                cmd.Parameters.AddWithValue("@eName", string.IsNullOrWhiteSpace(eName) ? (object)DBNull.Value : eName);
                cmd.Parameters.AddWithValue("@eNo", string.IsNullOrWhiteSpace(eNo) ? (object)DBNull.Value : eNo);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Tenant registered successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadTenantsGrid();
                ClearTenantForm();
                addTenantsModal.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to register tenant.\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ClearTenantForm()
        {
            tenantLastNameTxt.Text = "";
            tenantFirstNameTxt.Text = "";
            tenantMiddleNameTxt.Text = "";
            tenantContactTxt.Text = "";
            tenantEmailTxt.Text = "";
            tenantAddressTxt.Text = "";
            tenantEmergencyNameTxt.Text = "";
            tenantEmergencyContactTxt.Text = "";
        }

        private void cancelTenantRegister_Click(object sender, EventArgs e)
        {
            addTenantsModal.Visible = false;
            ClearTenantForm();
        }

        private void UpdateTotalTenantsLabel()
        {
            if (_selectedBhId <= 0)
            {
                totalTenants.Text = "(0)";
                return;
            }

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                EnsureOpen(conn);

                using var cmd = conn.CreateCommand();

                // Counts tenants who currently have ACTIVE rentals in the selected boarding house
                cmd.CommandText = @"
            SELECT COUNT(DISTINCT t.id)
            FROM tenants t
            INNER JOIN rentals ren
                ON ren.tenant_id = t.id
               AND ren.status = 'ACTIVE'
               AND (ren.end_date IS NULL OR ren.end_date >= CURDATE())
            INNER JOIN rooms r
                ON r.id = ren.room_id
            WHERE r.boarding_house_id = @bhId;
        ";
                cmd.Parameters.AddWithValue("@bhId", _selectedBhId);

                int total = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                totalTenants.Text = $"({total})";
            }
            catch
            {
                totalTenants.Text = "(?)";
            }
        }

        private void tenantUpdateBtn_Click_1(object sender, EventArgs e)
        {

        }
    }
}
