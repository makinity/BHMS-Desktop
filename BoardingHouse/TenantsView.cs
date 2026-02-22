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
        private bool _initialized = false;
        private int _pendingSelectTenantId = 0;

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
            InitializeTenantsViewOnce();

        }

        private void InitializeTenantsViewOnce()
        {
            if (_initialized) return;

            SetupStatusFilter();
            SetupDetailsHandlers();
            SetupPaymentHistoryGrid();
            LoadBoardingHouseDropdown();

            if (cbBoardingHouses.Items.Count > 0)
                cbBoardingHouses.SelectedIndex = 0;

            // Profile-only mode: room assignment controls are not used in TenantsView.
            cbDetailsRoom.Visible = false;
            label4.Visible = false;
            ViewRoomBtn.Visible = false;
            endRentalBtn.Visible = false;
            btnViewCurrentRental.Visible = false;
            btnStartRental.Visible = false;

            _initialized = true;
        }

        private void SetupStatusFilter()
        {
            cbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStatusFilter.Items.Clear();
            cbStatusFilter.Items.Add("ALL");
            cbStatusFilter.Items.Add("ACTIVE");
            cbStatusFilter.Items.Add("INACTIVE");
            cbStatusFilter.SelectedIndex = 0;

            cbStatusFilter.SelectedIndexChanged += CbStatusFilter_SelectedIndexChanged;
        }

        private void CbStatusFilter_SelectedIndexChanged(object? sender, EventArgs e)
        {
            LoadTenantsGrid();
        }

        private void SetupDetailsHandlers()
        {
            btnCloseDetails.Click += (s, e) => HideDetails();

            btnSearch.Click += (s, e) => LoadTenantsGrid();
            cbBoardingHouses.SelectedIndexChanged += cbBoardingHouses_SelectedIndexChanged;

            dgvTenants.DataBindingComplete -= DgvTenants_DataBindingComplete;
            dgvTenants.DataBindingComplete += DgvTenants_DataBindingComplete;

            tenantUpdateBtn.Click += tenantUpdateBtn_Click;
            tenantDeleteBtn.Click += tenantDeleteBtn_Click;

            btnViewCurrentRental.Click -= btnViewCurrentRental_Click;
            btnViewCurrentRental.Click += btnViewCurrentRental_Click;
            btnStartRental.Click -= btnStartRental_Click;
            btnStartRental.Click += btnStartRental_Click;

            detailsEmail.ReadOnly = true;
            detailsEContact.ReadOnly = true;

            detailsCbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void SetupPaymentHistoryGrid()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowHeadersVisible = false;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            dataGridView1.Columns.Clear();

            // Keep ID (visible if your instructor wants it)
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPayId",
                HeaderText = "ID",
                DataPropertyName = "id",
                Width = 60
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPayDate",
                HeaderText = "Date",
                DataPropertyName = "payment_date",
                Width = 120
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colBillMonth",
                HeaderText = "Bill Month",
                DataPropertyName = "bill_month",
                Width = 110
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAmount",
                HeaderText = "Amount",
                DataPropertyName = "amount",
                Width = 90
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMethod",
                HeaderText = "Method",
                DataPropertyName = "method",
                Width = 90
            });

            // keep but hide (you still have it but Visible=false)
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colRef",
                HeaderText = "Reference No",
                DataPropertyName = "reference_no",
                Visible = false
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colRemarks",
                HeaderText = "Remarks",
                DataPropertyName = "remarks",
                Visible = false
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStatus",
                HeaderText = "Status",
                DataPropertyName = "status",
                Visible = false
            });
        }

        private int GetOccupantIdForTenant(MySqlConnection conn, int tenantId)
        {
            if (conn == null || tenantId <= 0) return 0;

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT occupant_id
                FROM tenant_occupant_map
                WHERE tenant_id = @tid
                LIMIT 1;
            ";
            cmd.Parameters.AddWithValue("@tid", tenantId);

            var result = cmd.ExecuteScalar();
            return (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
        }

        private int GetOccupantIdForTenant(int tenantId)
        {
            if (tenantId <= 0) return 0;
            using var conn = DbConnectionFactory.CreateConnection();
            EnsureOpen(conn);
            return GetOccupantIdForTenant(conn, tenantId);
        }

        private int GetTenantIdForOccupant(MySqlConnection conn, int occupantId)
        {
            if (conn == null || occupantId <= 0) return 0;

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT tenant_id
                FROM tenant_occupant_map
                WHERE occupant_id = @occId
                LIMIT 1;
            ";
            cmd.Parameters.AddWithValue("@occId", occupantId);

            var result = cmd.ExecuteScalar();
            return (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
        }

        private decimal GetTotalPaymentsForTenant(int tenantId)
        {
            if (tenantId <= 0) return 0m;

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                EnsureOpen(conn);
                int occupantId = GetOccupantIdForTenant(conn, tenantId);
                if (occupantId <= 0) return 0m;

                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
            SELECT IFNULL(SUM(p.amount), 0)
            FROM payments p
            INNER JOIN rentals r ON r.id = p.rental_id
            WHERE r.occupant_id = @occId
              AND p.status = 'POSTED';
        ";
                cmd.Parameters.AddWithValue("@occId", occupantId);

                return Convert.ToDecimal(cmd.ExecuteScalar() ?? 0m);
            }
            catch
            {
                return 0m;
            }
        }


        private void LoadPaymentHistory(int tenantId)
        {
            dataGridView1.DataSource = null;
            label17.Text = "₱ 0.00";

            if (tenantId <= 0) return;

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                EnsureOpen(conn);
                int occupantId = GetOccupantIdForTenant(conn, tenantId);
                if (occupantId <= 0) return;

                var dt = new DataTable();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT
                    p.id,
                    p.payment_date,
                    p.bill_month,
                    p.amount,
                    p.method,
                    p.reference_no,
                    p.remarks,
                    p.status
                FROM payments p
                INNER JOIN rentals r ON r.id = p.rental_id
                WHERE r.occupant_id = @occId
                  AND p.status = 'POSTED'
                ORDER BY p.payment_date DESC;
            ";
                    cmd.Parameters.AddWithValue("@occId", occupantId);

                    using var ad = new MySqlDataAdapter(cmd);
                    ad.Fill(dt);
                }

                dataGridView1.DataSource = dt;

                if (dataGridView1.Columns.Contains("colAmount"))
                    dataGridView1.Columns["colAmount"].DefaultCellStyle.Format = "N2";

                decimal total = 0m;
                using (var cmdTotal = conn.CreateCommand())
                {
                    cmdTotal.CommandText = @"
                SELECT IFNULL(SUM(p.amount), 0)
                FROM payments p
                INNER JOIN rentals r ON r.id = p.rental_id
                WHERE r.occupant_id = @occId
                  AND p.status = 'POSTED';
            ";
                    cmdTotal.Parameters.AddWithValue("@occId", occupantId);

                    total = Convert.ToDecimal(cmdTotal.ExecuteScalar() ?? 0m);
                }

                label17.Text = "₱ " + total.ToString("N2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load payment history.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadTenantSnapshot(int tenantId)
        {
            lblSnapshotTenant.Text = "—";
            lblSnapshotTenantId.Text = "—";
            lblSnapshotBoardingHouse.Text = "—";
            lblSnapshotRoomAssigned.Text = "—";
            lblSnapshotRentalStart.Text = "—";
            lblSnapshotDuration.Text = "—";
            lblSnapshotLastPayment.Text = "—";
            lblSnapshotLastAmount.Text = "₱ 0.00";

            lblSnapshotStatusBadge.Text = "INACTIVE";
            lblSnapshotStatusBadge.BackColor = Color.Gray;

            if (tenantId <= 0) return;

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                EnsureOpen(conn);

                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
            SELECT
                t.id AS tenant_id,
                t.full_name,
                t.status AS tenant_status,

                ren.id AS rental_id,
                ren.start_date,
                ren.end_date,
                ren.status AS rental_status,

                r.room_no,
                r.room_type,

                bh.name AS boarding_house,

                lp.payment_date AS last_payment_date,
                lp.amount AS last_payment_amount
            FROM tenants t
            LEFT JOIN tenant_occupant_map tom
                ON tom.tenant_id = t.id
            LEFT JOIN rentals ren
                ON ren.occupant_id = tom.occupant_id
               AND ren.status = 'ACTIVE'
               AND (ren.end_date IS NULL OR ren.end_date >= CURDATE())
            LEFT JOIN rooms r
                ON r.id = ren.room_id
            LEFT JOIN boarding_houses bh
                ON bh.id = r.boarding_house_id
            LEFT JOIN (
                SELECT p1.rental_id, p1.payment_date, p1.amount
                FROM payments p1
                INNER JOIN (
                    SELECT rental_id, MAX(payment_date) AS max_date
                    FROM payments
                    WHERE status = 'POSTED'
                    GROUP BY rental_id
                ) x ON x.rental_id = p1.rental_id AND x.max_date = p1.payment_date
                WHERE p1.status = 'POSTED'
            ) lp ON lp.rental_id = ren.id
            WHERE t.id = @tid
            LIMIT 1;
        ";
                cmd.Parameters.AddWithValue("@tid", tenantId);

                using var r = cmd.ExecuteReader();
                if (!r.Read()) return;

                lblSnapshotTenant.Text = r["full_name"]?.ToString() ?? "—";
                lblSnapshotTenantId.Text = r["tenant_id"]?.ToString() ?? "—";

                string st = r["tenant_status"]?.ToString() ?? "INACTIVE";
                bool isActive = string.Equals(st, "ACTIVE", StringComparison.OrdinalIgnoreCase);
                lblSnapshotStatusBadge.Text = isActive ? "ACTIVE" : "INACTIVE";
                lblSnapshotStatusBadge.BackColor = isActive ? Color.SeaGreen : Color.Gray;

                lblSnapshotBoardingHouse.Text = r["boarding_house"] == DBNull.Value
                    ? "—"
                    : (r["boarding_house"]?.ToString() ?? "—");

                string roomNo = r["room_no"] == DBNull.Value ? "" : (r["room_no"]?.ToString() ?? "");
                string roomType = r["room_type"] == DBNull.Value ? "" : (r["room_type"]?.ToString() ?? "");
                lblSnapshotRoomAssigned.Text = string.IsNullOrWhiteSpace(roomNo + roomType)
                    ? "—"
                    : $"{roomNo} - {roomType}".Trim();

                if (r["start_date"] != DBNull.Value)
                {
                    DateTime start = Convert.ToDateTime(r["start_date"]);
                    lblSnapshotRentalStart.Text = start.ToString("MMM dd, yyyy");

                    int days = (DateTime.Today - start.Date).Days;
                    int monthsApprox = (int)Math.Floor(days / 30.0);
                    lblSnapshotDuration.Text = $"{days} days (~{monthsApprox} months)";
                }

                if (r["last_payment_date"] != DBNull.Value)
                {
                    DateTime lastPay = Convert.ToDateTime(r["last_payment_date"]);
                    lblSnapshotLastPayment.Text = lastPay.ToString("MMM dd, yyyy");
                }

                if (r["last_payment_amount"] != DBNull.Value)
                {
                    decimal amt = Convert.ToDecimal(r["last_payment_amount"]);
                    lblSnapshotLastAmount.Text = "₱ " + amt.ToString("N2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load tenant snapshot.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private int GetActiveBoardingHouseIdForTenant(int tenantId)
        {
            if (tenantId <= 0)
                return 0;

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                using var cmd = conn.CreateCommand();
                EnsureOpen(conn);

                cmd.CommandText = @"
                    SELECT r.boarding_house_id
                    FROM rentals ren
                    JOIN rooms r ON r.id = ren.room_id
                    WHERE ren.occupant_id = @occId
                      AND ren.status = 'ACTIVE'
                      AND (ren.end_date IS NULL OR ren.end_date >= CURDATE())
                    LIMIT 1;
                ";
                int occupantId = GetOccupantIdForTenant(conn, tenantId);
                if (occupantId <= 0) return 0;
                cmd.Parameters.AddWithValue("@occId", occupantId);

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

        private int GetBoardingHouseIdForRoom(int roomId)
        {
            if (roomId <= 0)
                return 0;

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

        private void LoadTenantsGrid()
        {
            if (_selectedBhId <= 0)
            {
                dgvTenants.DataSource = null;
                UpdateTotalTenantsLabel();
                return;
            }

            string kw = (txtSearch.Text ?? "").Trim();
            string st = cbStatusFilter.SelectedItem?.ToString() ?? "ALL";

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                using var cmd = conn.CreateCommand();
                EnsureOpen(conn);

                cmd.CommandText = @"
                SELECT DISTINCT
                    t.id AS tenant_id,
                    t.full_name,
                    COALESCE(bh_rental.name, bh_tenant.name) AS boarding_house
                FROM tenants t
                JOIN tenant_occupant_map tom
                    ON tom.tenant_id = t.id
                LEFT JOIN rentals ren
                    ON ren.occupant_id = tom.occupant_id
                   AND ren.status = 'ACTIVE'
                   AND (ren.end_date IS NULL OR ren.end_date >= CURDATE())
                LEFT JOIN rooms r
                    ON r.id = ren.room_id
                LEFT JOIN boarding_houses bh_rental
                    ON bh_rental.id = r.boarding_house_id
                LEFT JOIN boarding_houses bh_tenant
                    ON bh_tenant.id = t.boarding_house_id
                WHERE
                    (
                        r.boarding_house_id = @bhId
                        OR (r.id IS NULL AND t.boarding_house_id = @bhId)
                    )
                    AND (@st = 'ALL' OR t.status = @st)
                    AND (
                        @kw = '' OR
                        t.full_name LIKE @kwLike OR
                        COALESCE(bh_rental.name, bh_tenant.name) LIKE @kwLike
                    )
                ORDER BY t.full_name ASC;
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

                // Hide all columns first
                foreach (DataGridViewColumn col in dgvTenants.Columns)
                    col.Visible = false;

                // Show only these 3
                dgvTenants.Columns["tenant_id"].Visible = true;
                dgvTenants.Columns["tenant_id"].HeaderText = "ID";
                dgvTenants.Columns["tenant_id"].Width = 60;

                dgvTenants.Columns["full_name"].Visible = true;
                dgvTenants.Columns["full_name"].HeaderText = "Tenant Name";

                dgvTenants.Columns["boarding_house"].Visible = true;
                dgvTenants.Columns["boarding_house"].HeaderText = "Boarding House";

                // Optional UI improvements
                dgvTenants.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvTenants.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvTenants.MultiSelect = false;
                dgvTenants.ReadOnly = true;
                dgvTenants.AllowUserToAddRows = false;
                dgvTenants.RowHeadersVisible = false;

                UpdateTotalTenantsLabel();
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
            SoundClicked.itemClicked();
            tenantUpdateBtn.Visible = true;
            tenantDeleteBtn.Visible = true;
            endRentalBtn.Visible = false;

            if (e.RowIndex < 0) return;

            var row = dgvTenants.Rows[e.RowIndex];
            if (row == null) return;

            int tenantId = SafeInt(row.Cells["tenant_id"]?.Value);
            if (tenantId <= 0) return;

            LoadTenantDetails(tenantId);
            LoadPaymentHistory(tenantId);
            LoadTenantSnapshot(tenantId);


            detailsModal.Visible = true;
            detailsModal.BringToFront();
        }

        private void DgvTenants_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (_pendingSelectTenantId <= 0) return;

            SelectTenantRowInGrid(_pendingSelectTenantId);
            _pendingSelectTenantId = 0;
        }

        private void SelectTenantRowInGrid(int tenantId)
        {
            if (tenantId <= 0 || dgvTenants.Rows.Count == 0)
                return;

            dgvTenants.ClearSelection();

            DataGridViewRow? targetRow = null;
            foreach (DataGridViewRow row in dgvTenants.Rows)
            {
                if (row.IsNewRow)
                    continue;

                if (SafeInt(row.Cells["tenant_id"]?.Value) == tenantId)
                {
                    targetRow = row;
                    break;
                }
            }

            if (targetRow == null)
                return;

            targetRow.Selected = true;

            DataGridViewCell? focusCell = null;
            foreach (DataGridViewCell cell in targetRow.Cells)
            {
                if (cell.Visible)
                {
                    focusCell = cell;
                    break;
                }
            }

            if (focusCell == null && targetRow.Cells.Count > 0)
                focusCell = targetRow.Cells[0];

            if (focusCell != null)
            {
                try
                {
                    dgvTenants.CurrentCell = focusCell;
                }
                catch
                {
                    // ignored
                }
            }

            try
            {
                dgvTenants.FirstDisplayedScrollingRowIndex = targetRow.Index;
            }
            catch
            {
                // ignored
            }
        }

        public void OpenDetailsByTenantId(int tenantId)
        {
            if (tenantId <= 0) return;

            InitializeTenantsViewOnce();

            _pendingSelectTenantId = tenantId;

            int statusIndex = cbStatusFilter.Items.IndexOf("ACTIVE");
            if (statusIndex >= 0)
            {
                cbStatusFilter.SelectedIndexChanged -= CbStatusFilter_SelectedIndexChanged;
                try
                {
                    cbStatusFilter.SelectedItem = "ACTIVE";
                }
                finally
                {
                    cbStatusFilter.SelectedIndexChanged += CbStatusFilter_SelectedIndexChanged;
                }
            }

            int targetBhId = GetActiveBoardingHouseIdForTenant(tenantId);
            if (targetBhId > 0)
            {
                _selectedBhId = targetBhId;
                cbBoardingHouses.SelectedIndexChanged -= cbBoardingHouses_SelectedIndexChanged;
                try
                {
                    cbBoardingHouses.SelectedValue = targetBhId;
                }
                finally
                {
                    cbBoardingHouses.SelectedIndexChanged += cbBoardingHouses_SelectedIndexChanged;
                }

            }
            else
            {
                if (cbBoardingHouses.SelectedValue != null &&
                    int.TryParse(cbBoardingHouses.SelectedValue.ToString(), out int currentBhId))
                {
                    _selectedBhId = currentBhId;
                }
            }

            LoadTenantsGrid();
            SelectTenantRowInGrid(tenantId);
            LoadTenantDetails(tenantId);
            LoadPaymentHistory(tenantId);

            tenantUpdateBtn.Visible = true;
            tenantDeleteBtn.Visible = true;
            endRentalBtn.Visible = false;

            detailsModal.Visible = true;
            detailsModal.BringToFront();
        }

        public void OpenAddTenantModal(int bhId = 0)
        {
            InitializeTenantsViewOnce();

            _pendingSelectTenantId = 0;

            int targetBhId = bhId;
            if (targetBhId <= 0 && cbBoardingHouses.SelectedValue != null &&
                int.TryParse(cbBoardingHouses.SelectedValue.ToString(), out int fallbackBh))
            {
                targetBhId = fallbackBh;
            }

            if (targetBhId > 0)
            {
                _selectedBhId = targetBhId;

                if (cbBoardingHouses.Items.Count > 0)
                {
                    cbBoardingHouses.SelectedIndexChanged -= cbBoardingHouses_SelectedIndexChanged;
                    try
                    {
                        cbBoardingHouses.SelectedValue = targetBhId;
                    }
                    finally
                    {
                        cbBoardingHouses.SelectedIndexChanged += cbBoardingHouses_SelectedIndexChanged;
                    }
                }

            }

            LoadTenantsGrid();
            UpdateTotalTenantsLabel();

            ClearTenantForm();

            addTenantsModal.Visible = true;
            addTenantsModal.BringToFront();

            tenantLastNameTxt?.Focus();
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
                        t.profile_path,

                        ren.id AS active_rental_id,
                        ren.room_id AS active_room_id
                    FROM tenants t
                    LEFT JOIN tenant_occupant_map tom
                        ON tom.tenant_id = t.id
                    LEFT JOIN rentals ren
                        ON ren.occupant_id = tom.occupant_id
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

                bool hasActiveRental = _activeRentalId > 0;
                detailsCbStatus.Enabled = !hasActiveRental;

                if (hasActiveRental)
                {
                    int activeIdx = detailsCbStatus.Items.IndexOf("ACTIVE");
                    if (activeIdx >= 0)
                        detailsCbStatus.SelectedIndex = activeIdx;
                }

                btnViewCurrentRental.Visible = hasActiveRental;
                btnStartRental.Visible = !hasActiveRental;
                endRentalBtn.Visible = false;
                cbDetailsRoom.Visible = false;
                label4.Visible = false;
                ViewRoomBtn.Visible = false;

                string prof = (r["profile_path"] == DBNull.Value) ? "" : (r["profile_path"]?.ToString() ?? "");
                profilePathTxt.Text = prof;
                LoadProfileImageIntoPictureBox(detailsTenantImg, prof);

            }

            catch (Exception ex)
            {
                MessageBox.Show("Failed to load tenant details.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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

        }

        private void tenantDeleteBtn_Click(object sender, EventArgs e)
        {

        }

        private bool HasActiveRental(int tenantId)
        {
            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                using var cmd = conn.CreateCommand();
                EnsureOpen(conn);
                int occupantId = GetOccupantIdForTenant(conn, tenantId);
                if (occupantId <= 0) return false;

                cmd.CommandText = @"
                    SELECT COUNT(*)
                    FROM rentals
                    WHERE occupant_id = @occId
                      AND status = 'ACTIVE'
                      AND (end_date IS NULL OR end_date >= CURDATE());
                ";
                cmd.Parameters.AddWithValue("@occId", occupantId);

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
            if (tenantId <= 0) return false;

            int occupantId = 0;
            using (var mapCmd = conn.CreateCommand())
            {
                mapCmd.CommandText = @"
                    SELECT occupant_id
                    FROM tenant_occupant_map
                    WHERE tenant_id = @tenantId
                    LIMIT 1;
                ";
                mapCmd.Parameters.AddWithValue("@tenantId", tenantId);
                var result = mapCmd.ExecuteScalar();
                occupantId = (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
            }

            if (occupantId <= 0) return false;

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT COUNT(*)
                FROM rentals
                WHERE occupant_id = @occId
                  AND status = 'ACTIVE'
                  AND (end_date IS NULL OR end_date >= CURDATE());
            ";
            cmd.Parameters.AddWithValue("@occId", occupantId);
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

        private static object BuildTenantAuditObject(string lastname, string firstname, string? middlename,
            string fullName, string? contactNo, string? email, string? address,
            string? emergencyName, string? emergencyNo, string? profilePath, string status)
        {
            return new
            {
                lastname = lastname,
                firstname = firstname,
                middlename = middlename,
                full_name = fullName,
                contact_no = contactNo,
                email = email,
                address = address,
                emergency_contact_name = emergencyName,
                emergency_contact_no = emergencyNo,
                profile_path = profilePath,
                status = status
            };
        }

        private static object ReadTenantAudit(MySqlDataReader reader)
        {
            return BuildTenantAuditObject(
                reader["lastname"]?.ToString() ?? "",
                reader["firstname"]?.ToString() ?? "",
                reader["middlename"] == DBNull.Value ? null : reader["middlename"]?.ToString(),
                reader["full_name"]?.ToString() ?? "",
                reader["contact_no"] == DBNull.Value ? null : reader["contact_no"]?.ToString(),
                reader["email"] == DBNull.Value ? null : reader["email"]?.ToString(),
                reader["address"] == DBNull.Value ? null : reader["address"]?.ToString(),
                reader["emergency_contact_name"] == DBNull.Value ? null : reader["emergency_contact_name"]?.ToString(),
                reader["emergency_contact_no"] == DBNull.Value ? null : reader["emergency_contact_no"]?.ToString(),
                reader["profile_path"] == DBNull.Value ? null : reader["profile_path"]?.ToString(),
                reader["status"]?.ToString() ?? ""
            );
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
            SoundClicked.operationsBtn();
            OpenAddTenantModal(_selectedBhId);
        }

        private void addTenantBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            OpenAddTenantModal(_selectedBhId);
        }

        private bool TryGetActiveRentalForTenant(int tenantId, out int occupantId, out int rentalId, out int roomId)
        {
            occupantId = 0;
            rentalId = 0;
            roomId = 0;

            if (tenantId <= 0) return false;

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                EnsureOpen(conn);

                occupantId = GetOccupantIdForTenant(conn, tenantId);
                if (occupantId <= 0) return false;

                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                    SELECT id, room_id
                    FROM rentals
                    WHERE occupant_id = @occId
                      AND status = 'ACTIVE'
                      AND (end_date IS NULL OR end_date >= CURDATE())
                    ORDER BY start_date DESC
                    LIMIT 1;
                ";
                cmd.Parameters.AddWithValue("@occId", occupantId);

                using var r = cmd.ExecuteReader();
                if (!r.Read()) return false;

                rentalId = r["id"] == DBNull.Value ? 0 : Convert.ToInt32(r["id"]);
                roomId = r["room_id"] == DBNull.Value ? 0 : Convert.ToInt32(r["room_id"]);

                return rentalId > 0;
            }
            catch
            {
                return false;
            }
        }

        private void btnViewCurrentRental_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();

            if (_selectedTenantId <= 0)
            {
                MessageBox.Show("Select a tenant first.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!TryGetActiveRentalForTenant(_selectedTenantId, out _, out int rentalId, out int roomId))
            {
                MessageBox.Show("No ACTIVE rental.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _activeRentalId = rentalId;
            _activeRoomId = roomId;

            if (FindForm() is not MainLayout main)
                return;

            var mainType = main.GetType();
            var openRentalsForRental = mainType.GetMethod("OpenRentalsForRental", new[] { typeof(int) });
            if (openRentalsForRental != null)
            {
                openRentalsForRental.Invoke(main, new object[] { _activeRentalId });
            }
            else
            {
                main.OpenPaymentsForRental(_activeRentalId);
            }
            HideDetails();
        }

        private void btnStartRental_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();

            if (_selectedTenantId <= 0)
            {
                MessageBox.Show("Select a tenant first.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (TryGetActiveRentalForTenant(_selectedTenantId, out _, out int rentalId, out int roomId))
            {
                _activeRentalId = rentalId;
                _activeRoomId = roomId;
                MessageBox.Show(
                    "This tenant already has an ACTIVE rental.\nUse 'View Current Rental' instead.",
                    "Action Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            int occupantId = GetOccupantIdForTenant(_selectedTenantId);
            if (occupantId <= 0)
            {
                MessageBox.Show("Tenant has no linked occupant record.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (FindForm() is not MainLayout main)
                return;

            var mainType = main.GetType();
            var rentalMethod =
                mainType.GetMethod("OpenRentalEngineForOccupant", new[] { typeof(int) }) ??
                mainType.GetMethod("OpenRentalsStartForOccupant", new[] { typeof(int) }) ??
                mainType.GetMethod("OpenRentalsForTenant", new[] { typeof(int) });

            if (rentalMethod != null)
            {
                rentalMethod.Invoke(main, new object[] { occupantId });
                HideDetails();
                return;
            }

            MessageBox.Show(
                "Rental engine entrypoint is missing.\n\n" +
                "Please add one of these methods in MainLayout:\n" +
                "• OpenRentalEngineForOccupant(int occupantId)\n" +
                "• OpenRentalsStartForOccupant(int occupantId)\n" +
                "• OpenRentalsForTenant(int tenantId)",
                "Missing Rental Entry",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void ViewRoomBtn_Click(object sender, EventArgs e)
        {
            btnViewCurrentRental_Click(sender, e);
        }

        private void addTenantCloseBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            addTenantsModal.Visible = false;
        }

        private void registerTenantBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();

            if (cbBoardingHouses.SelectedValue != null &&
                int.TryParse(cbBoardingHouses.SelectedValue.ToString(), out int bhId))
            {
                _selectedBhId = bhId;
            }

            if (_selectedBhId <= 0)
            {
                MessageBox.Show("Please select a Boarding House first.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string occType = "TENANT";
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
            string profile = (profilePathTxt.Text ?? "").Trim();

            string failedStep = "initialization";
            long createdTenantId = 0;
            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                EnsureOpen(conn);
                using var tx = conn.BeginTransaction();

                try
                {
                    // 1️⃣ Insert into occupants
                    long occupantId;
                    failedStep = "occupants insert";
                    using (var cmdOcc = conn.CreateCommand())
                    {
                        cmdOcc.Transaction = tx;
                        cmdOcc.CommandText = @"
                INSERT INTO occupants
                (occupant_type, lastname, firstname, middlename, full_name,
                 contact_no, email, address, profile_path,
                 emergency_contact_name, emergency_contact_no,
                 status, created_at, updated_at)
                VALUES
                (@type, @ln, @fn, @mn, @full,
                 @contact, @email, @address, @profile,
                 @eName, @eNo,
                 'ACTIVE', NOW(), NOW());
            ";

                        cmdOcc.Parameters.AddWithValue("@type", occType);
                        cmdOcc.Parameters.AddWithValue("@ln", ln);
                        cmdOcc.Parameters.AddWithValue("@fn", fn);
                        cmdOcc.Parameters.AddWithValue("@mn", string.IsNullOrWhiteSpace(mn) ? (object)DBNull.Value : mn);
                        cmdOcc.Parameters.AddWithValue("@full", fullName);
                        cmdOcc.Parameters.AddWithValue("@contact", string.IsNullOrWhiteSpace(contact) ? (object)DBNull.Value : contact);
                        cmdOcc.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(email) ? (object)DBNull.Value : email);
                        cmdOcc.Parameters.AddWithValue("@address", string.IsNullOrWhiteSpace(address) ? (object)DBNull.Value : address);
                        cmdOcc.Parameters.AddWithValue("@profile", string.IsNullOrWhiteSpace(profile) ? (object)DBNull.Value : profile);
                        cmdOcc.Parameters.AddWithValue("@eName", string.IsNullOrWhiteSpace(eName) ? (object)DBNull.Value : eName);
                        cmdOcc.Parameters.AddWithValue("@eNo", string.IsNullOrWhiteSpace(eNo) ? (object)DBNull.Value : eNo);

                        cmdOcc.ExecuteNonQuery();
                        occupantId = cmdOcc.LastInsertedId;
                    }

                    // 2️⃣ Insert into tenants
                    if (_selectedBhId <= 0)
                        throw new InvalidOperationException("Selected boarding house id is missing (0).");

                    long tenantId;
                    failedStep = "tenants insert";
                    using (var cmdTenant = conn.CreateCommand())
                    {
                        cmdTenant.Transaction = tx;
                        cmdTenant.CommandText = @"
                    INSERT INTO tenants
                    (boarding_house_id, lastname, firstname, middlename, full_name,
                     contact_no, email, address, profile_path,
                     emergency_contact_name, emergency_contact_no,
                     status, created_at, updated_at)
                    VALUES
                    (@bhId, @ln, @fn, @mn, @full,
                     @contact, @email, @address, @profile,
                     @eName, @eNo,
                     'ACTIVE', NOW(), NOW());
                ";

                        cmdTenant.Parameters.AddWithValue("@bhId", _selectedBhId);
                        cmdTenant.Parameters.AddWithValue("@ln", ln);
                        cmdTenant.Parameters.AddWithValue("@fn", fn);
                        cmdTenant.Parameters.AddWithValue("@mn", string.IsNullOrWhiteSpace(mn) ? (object)DBNull.Value : mn);
                        cmdTenant.Parameters.AddWithValue("@full", fullName);
                        cmdTenant.Parameters.AddWithValue("@contact", string.IsNullOrWhiteSpace(contact) ? (object)DBNull.Value : contact);
                        cmdTenant.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(email) ? (object)DBNull.Value : email);
                        cmdTenant.Parameters.AddWithValue("@address", string.IsNullOrWhiteSpace(address) ? (object)DBNull.Value : address);
                        cmdTenant.Parameters.AddWithValue("@profile", string.IsNullOrWhiteSpace(profile) ? (object)DBNull.Value : profile);
                        cmdTenant.Parameters.AddWithValue("@eName", string.IsNullOrWhiteSpace(eName) ? (object)DBNull.Value : eName);
                        cmdTenant.Parameters.AddWithValue("@eNo", string.IsNullOrWhiteSpace(eNo) ? (object)DBNull.Value : eNo);

                        cmdTenant.ExecuteNonQuery();
                        tenantId = cmdTenant.LastInsertedId;
                        createdTenantId = tenantId;
                    }

                    failedStep = "tenants boarding_house_id verification";
                    using (var verifyCmd = conn.CreateCommand())
                    {
                        verifyCmd.Transaction = tx;
                        verifyCmd.CommandText = @"
                    SELECT boarding_house_id
                    FROM tenants
                    WHERE id = @tid
                    LIMIT 1;
                ";
                        verifyCmd.Parameters.AddWithValue("@tid", tenantId);
                        var bhObj = verifyCmd.ExecuteScalar();
                        int savedBhId = (bhObj == null || bhObj == DBNull.Value) ? 0 : Convert.ToInt32(bhObj);
                        if (savedBhId != _selectedBhId)
                            throw new InvalidOperationException("Tenant boarding house was not saved correctly.");
                    }

                    // 3️⃣ Mapping
                    failedStep = "tenant_occupant_map insert";
                    using var mapCmd = conn.CreateCommand();
                    mapCmd.Transaction = tx;
                    mapCmd.CommandText = @"
                INSERT INTO tenant_occupant_map (tenant_id, occupant_id)
                SELECT @tid, @occId
                WHERE NOT EXISTS (
                    SELECT 1
                    FROM tenant_occupant_map
                    WHERE tenant_id = @tid OR occupant_id = @occId
                );
            ";
                    mapCmd.Parameters.AddWithValue("@tid", tenantId);
                    mapCmd.Parameters.AddWithValue("@occId", occupantId);
                    int mapRows = mapCmd.ExecuteNonQuery();
                    if (mapRows <= 0)
                        throw new InvalidOperationException("Duplicate tenant-occupant mapping detected.");

                    tx.Commit();
                }
                catch
                {
                    try { tx.Rollback(); } catch { }
                    throw;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Registration failed during {failedStep}.\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Registration failed during {failedStep}.\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Tenant registered successfully.", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            _pendingSelectTenantId = createdTenantId > 0 ? Convert.ToInt32(createdTenantId) : 0;
            LoadTenantsGrid();
            UpdateTotalTenantsLabel();

            ClearTenantForm();
            addTenantsModal.Visible = false;
        }

        private void cbOccType_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Tenant screen always registers TENANT only.
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
            profilePathTxt.Text = "";
        }

        private void cancelTenantRegister_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
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

                cmd.CommandText = @"
            SELECT COUNT(DISTINCT t.id)
            FROM tenants t
            INNER JOIN tenant_occupant_map tom
                ON tom.tenant_id = t.id
            LEFT JOIN rentals ren
                ON ren.occupant_id = tom.occupant_id
               AND ren.status = 'ACTIVE'
               AND (ren.end_date IS NULL OR ren.end_date >= CURDATE())
            LEFT JOIN rooms r
                ON r.id = ren.room_id
            WHERE
                (
                    r.boarding_house_id = @bhId
                    OR (r.id IS NULL AND t.boarding_house_id = @bhId)
                );
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
            SoundClicked.operationsBtn();
            if (_selectedTenantId <= 0) return;

            int uid = CurrentUserId > 0 ? CurrentUserId : 1;

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
            string profile = (profilePathTxt.Text ?? "").Trim();

            bool hasActiveRental = _activeRentalId > 0;
            string st = hasActiveRental ? "ACTIVE" : (detailsCbStatus.SelectedItem?.ToString() ?? "ACTIVE");

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                EnsureOpen(conn);

                using var tx = conn.BeginTransaction();
                string fullName = BuildFullName(ln, fn, mn);
                object beforeTenant = null;
                using (var snapshot = conn.CreateCommand())
                {
                    snapshot.Transaction = tx;
                    snapshot.CommandText = @"
                    SELECT
                        id,
                        lastname,
                        firstname,
                        middlename,
                        full_name,
                        contact_no,
                        email,
                        address,
                        emergency_contact_name,
                        emergency_contact_no,
                        profile_path,
                        status
                    FROM tenants
                    WHERE id = @id
                    LIMIT 1;
                ";
                    snapshot.Parameters.AddWithValue("@id", _selectedTenantId);

                    using var reader = snapshot.ExecuteReader();
                    if (reader.Read())
                        beforeTenant = ReadTenantAudit(reader);
                }

                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tx;
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
                        profile_path = @profilePath,
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
                    cmd.Parameters.AddWithValue("@profilePath", string.IsNullOrWhiteSpace(profile) ? (object)DBNull.Value : profile);
                    cmd.Parameters.AddWithValue("@st", st);
                    cmd.Parameters.AddWithValue("@id", _selectedTenantId);

                    cmd.ExecuteNonQuery();
                }

                tx.Commit();
                var afterTenant = new
                {
                    lastname = ln,
                    firstname = fn,
                    middlename = string.IsNullOrWhiteSpace(mn) ? null : mn,
                    full_name = fullName,
                    contact_no = string.IsNullOrWhiteSpace(contact) ? null : contact,
                    email = string.IsNullOrWhiteSpace(email) ? null : email,
                    address = string.IsNullOrWhiteSpace(addr) ? null : addr,
                    emergency_contact_name = string.IsNullOrWhiteSpace(eName) ? null : eName,
                    emergency_contact_no = string.IsNullOrWhiteSpace(eNo) ? null : eNo,
                    profile_path = string.IsNullOrWhiteSpace(profile) ? null : profile,
                    status = st
                };
                AuditLogger.Log(uid, "UPDATE", "tenants", _selectedTenantId, new { before = beforeTenant, after = afterTenant });

                MessageBox.Show("Tenant updated successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadTenantsGrid();
                profilePathTxt.Text = "";
                LoadProfileImageIntoPictureBox(detailsTenantImg, "");
                LoadTenantDetails(_selectedTenantId);
                UpdateTotalTenantsLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update tenant.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
        }

        private void tenantDeleteBtn_Click_1(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            if (_selectedTenantId <= 0) return;

            int uid = CurrentUserId > 0 ? CurrentUserId : 1;

            // Safety: do not allow if active rental exists
            if (HasActiveRental(_selectedTenantId))
            {
                MessageBox.Show(
                    "This tenant has an ACTIVE rental.\nEnd the rental first before deleting.",
                    "Action Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            var confirm = MessageBox.Show(
                "PERMANENT DELETE?\n\nThis removes the tenant profile record.\n\nThis cannot be undone. Continue?",
                "Confirm Permanent Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm != DialogResult.Yes) return;

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                EnsureOpen(conn);

                using var tx = conn.BeginTransaction();
                int occupantId = GetOccupantIdForTenant(conn, _selectedTenantId);
                object deletedTenant = null;
                using (var snapshot = conn.CreateCommand())
                {
                    snapshot.Transaction = tx;
                    snapshot.CommandText = @"
                    SELECT
                        id,
                        lastname,
                        firstname,
                        middlename,
                        full_name,
                        contact_no,
                        email,
                        address,
                        emergency_contact_name,
                        emergency_contact_no,
                        profile_path,
                        status
                    FROM tenants
                    WHERE id = @id
                    LIMIT 1;
                ";
                    snapshot.Parameters.AddWithValue("@id", _selectedTenantId);

                    using var reader = snapshot.ExecuteReader();
                    if (reader.Read())
                        deletedTenant = ReadTenantAudit(reader);
                }

                if (occupantId > 0)
                {
                    using var rentalHistoryCmd = conn.CreateCommand();
                    rentalHistoryCmd.Transaction = tx;
                    rentalHistoryCmd.CommandText = @"
                        SELECT COUNT(*)
                        FROM rentals
                        WHERE occupant_id = @occId;
                    ";
                    rentalHistoryCmd.Parameters.AddWithValue("@occId", occupantId);
                    int rentalHistoryCount = Convert.ToInt32(rentalHistoryCmd.ExecuteScalar() ?? 0);
                    if (rentalHistoryCount > 0)
                    {
                        tx.Rollback();
                        MessageBox.Show(
                            "This tenant has rental history.\n\n" +
                            "TenantsView is profile-only and cannot delete rental-linked tenants.\n" +
                            "Use the Rental Engine to manage rental records first.",
                            "Action Denied",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }
                }

                using (var delMap = conn.CreateCommand())
                {
                    delMap.Transaction = tx;
                    delMap.CommandText = @"DELETE FROM tenant_occupant_map WHERE tenant_id = @tid;";
                    delMap.Parameters.AddWithValue("@tid", _selectedTenantId);
                    delMap.ExecuteNonQuery();
                }

                // 5) Delete tenant
                int tenantDeleted = 0;
                using (var delTenant = conn.CreateCommand())
                {
                    delTenant.Transaction = tx;
                    delTenant.CommandText = @"DELETE FROM tenants WHERE id = @tid LIMIT 1;";
                    delTenant.Parameters.AddWithValue("@tid", _selectedTenantId);
                    tenantDeleted = delTenant.ExecuteNonQuery();
                }

                if (tenantDeleted <= 0)
                {
                    tx.Rollback();
                    MessageBox.Show("Tenant not found or already deleted.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                tx.Commit();
                var deleteAuditDetails = new
                {
                    deleted = deletedTenant
                };
                AuditLogger.Log(uid, "DELETE", "tenants", _selectedTenantId, deleteAuditDetails);

                MessageBox.Show("Tenant and related records deleted permanently.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                HideDetails();
                LoadTenantsGrid();
                UpdateTotalTenantsLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to delete tenant.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void endRentalBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            MessageBox.Show(
                "TenantsView is profile-only.\nPlease use the Rental Engine screen to end rentals.",
                "Read-Only Rental Action",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void detailsBrowseProfileBtn_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog();
            ofd.Title = "Select Tenant Profile Image";
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All Files|*.*";
            ofd.Multiselect = false;

            if (ofd.ShowDialog() != DialogResult.OK) return;

            try
            {
                // 1) Ensure folder exists
                string profilesDir = System.IO.Path.Combine(Application.StartupPath, "Profiles", "tenants");
                if (!System.IO.Directory.Exists(profilesDir))
                    System.IO.Directory.CreateDirectory(profilesDir);

                // 2) Create a unique filename (avoid overwriting)
                string ext = System.IO.Path.GetExtension(ofd.FileName);
                string safeName =
                    $"tenant_{(_selectedTenantId > 0 ? _selectedTenantId.ToString() : DateTime.Now.Ticks.ToString())}{ext}";
                string destFullPath = System.IO.Path.Combine(profilesDir, safeName);

                // If same name exists, append random
                if (System.IO.File.Exists(destFullPath))
                {
                    safeName =
                        $"tenant_{(_selectedTenantId > 0 ? _selectedTenantId.ToString() : DateTime.Now.Ticks.ToString())}_{Guid.NewGuid().ToString("N").Substring(0, 6)}{ext}";
                    destFullPath = System.IO.Path.Combine(profilesDir, safeName);
                }

                // 3) Copy selected file into app folder
                System.IO.File.Copy(ofd.FileName, destFullPath, true);

                // 4) Save RELATIVE path to textbox (DB-ready format)
                string relativePath = System.IO.Path.Combine("Profiles", "tenants", safeName);

                profilePathTxt.Text = relativePath;

                // 5) Preview in picturebox (load safely)
                if (detailsTenantImg.Image != null)
                {
                    var old = detailsTenantImg.Image;
                    detailsTenantImg.Image = null;
                    old.Dispose();
                }

                using (var temp = Image.FromFile(destFullPath))
                {
                    detailsTenantImg.Image = new Bitmap(temp);
                }

                detailsTenantImg.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to set profile image.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void browseProfileBtn_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog();
            ofd.Title = "Select Profile Image";
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All Files|*.*";
            ofd.Multiselect = false;

            if (ofd.ShowDialog() != DialogResult.OK) return;

            try
            {
                // Save under: Profiles/tenants  (change "tenants" to "owners" if this is for owners)
                string profilesDir = System.IO.Path.Combine(Application.StartupPath, "Profiles", "tenants");
                if (!System.IO.Directory.Exists(profilesDir))
                    System.IO.Directory.CreateDirectory(profilesDir);

                string ext = System.IO.Path.GetExtension(ofd.FileName);

                // If you have a selected id variable, use it; otherwise fallback to ticks
                string safeName = $"profile_{DateTime.Now.Ticks}{ext}";
                string destFullPath = System.IO.Path.Combine(profilesDir, safeName);

                // avoid overwrite
                if (System.IO.File.Exists(destFullPath))
                {
                    safeName = $"profile_{DateTime.Now.Ticks}_{Guid.NewGuid().ToString("N").Substring(0, 6)}{ext}";
                    destFullPath = System.IO.Path.Combine(profilesDir, safeName);
                }

                // copy file into app folder
                System.IO.File.Copy(ofd.FileName, destFullPath, true);

                // store relative path (DB-ready)
                string relativePath = System.IO.Path.Combine("Profiles", "tenants", safeName);

                // set textbox + preview
                profilePathTxt.Text = relativePath;

                if (addTenantImg.Image != null)
                {
                    var old = addTenantImg.Image;
                    addTenantImg.Image = null;
                    old.Dispose();
                }

                using (var temp = Image.FromFile(destFullPath))
                {
                    addTenantImg.Image = new Bitmap(temp);
                }

                addTenantImg.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to set profile image.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProfileImageIntoPictureBox(PictureBox pb, string relativePath)
        {
            try
            {
                if (pb.Image != null)
                {
                    var old = pb.Image;
                    pb.Image = null;
                    old.Dispose();
                }

                if (string.IsNullOrWhiteSpace(relativePath))
                    return;

                string fullPath = System.IO.Path.Combine(Application.StartupPath, relativePath);

                if (!System.IO.File.Exists(fullPath))
                    return;

                using (var fs = new System.IO.FileStream(fullPath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite))
                using (var img = Image.FromStream(fs))
                {
                    pb.Image = new Bitmap(img);
                }

                pb.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch
            {
                try
                {
                    if (pb.Image != null)
                    {
                        var old = pb.Image;
                        pb.Image = null;
                        old.Dispose();
                    }
                }
                catch { }
            }
        }

        private void registrationOpenCameraBtn_Click(object sender, EventArgs e)
        {
            using var cam = new CameraCaptureForm();
            if (cam.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(cam.SavedImagePath))
            {
                profilePathTxt.Text = cam.SavedImagePath;
                LoadPictureSafe(addTenantImg, cam.SavedImagePath);
            }
        }


        private void LoadPictureSafe(PictureBox pb, string path)
        {
            try
            {
                if (pb.Image != null)
                {
                    var old = pb.Image;
                    pb.Image = null;
                    old.Dispose();
                }

                if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                {
                    pb.Image = null;
                    return;
                }

                using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                using var img = Image.FromStream(fs);
                pb.Image = new Bitmap(img);
            }
            catch
            {
                pb.Image = null;
            }
        }

        private void detailsOpenCameraBtn_Click(object sender, EventArgs e)
        {
            using var cam = new CameraCaptureForm();
            if (cam.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(cam.SavedImagePath))
            {
                details_profilePathTxt.Text = cam.SavedImagePath;
                LoadPictureSafe(detailsTenantImg, cam.SavedImagePath);
            }
        }

        private void btnSnapshotOpenPayments_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();

            if (_selectedTenantId <= 0)
            {
                MessageBox.Show("Select a tenant first.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_activeRentalId <= 0)
            {
                MessageBox.Show("This tenant has no ACTIVE rental.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (FindForm() is not MainLayout main)
                return;

            main.OpenPaymentsForRental(_activeRentalId);

            HideDetails();
        }



        private void btnSnapshotViewRoom_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();

            if (_selectedTenantId <= 0)
            {
                MessageBox.Show("Select a tenant first.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_activeRoomId <= 0)
            {
                MessageBox.Show("This tenant has no assigned room yet.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (FindForm() is not MainLayout main)
                return;

            main.OpenRoomsForTenantRoom(_activeRoomId);

            HideDetails();
        }

        private void btnSnapshotRefresh_Click(object sender, EventArgs e)
        {

        }
    }
}
