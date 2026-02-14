using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoardingHouse
{
    public partial class StudentsView : UserControl
    {
        private int _selectedBhId = 0;
        private int _selectedStudentId = 0;
        private int _activeRentalId = 0;
        private int _activeRoomId = 0;
        private bool _initialized = false;
        private int _pendingSelectStudentId = 0;

        public int CurrentUserId { get; set; }
        public StudentsView()
        {
            InitializeComponent();
        }

        private void Students_Load(object sender, EventArgs e)
        {
            InitializeStudentsViewOnce();
        }

        private void InitializeStudentsViewOnce()
        {
            if (_initialized) return;

            SetupStatusFilter();
            SetupDetailsHandlers();
            SetupPaymentHistoryGrid();
            LoadBoardingHouseDropdown();
            dgvStudents.DataBindingComplete -= DgvStudents_DataBindingComplete;
            dgvStudents.DataBindingComplete += DgvStudents_DataBindingComplete;

            if (cbBoardingHouses.Items.Count > 0)
                cbBoardingHouses.SelectedIndex = 0;

            if (cbOccType != null)
            {
                cbOccType.Visible = false;
                cbOccType.Enabled = false;
            }
            if (label18 != null)
                label18.Visible = false;

            if (labelStudentNo != null)
                labelStudentNo.Visible = true;
            if (txtStudentNo != null)
            {
                txtStudentNo.Visible = true;
                txtStudentNo.Multiline = false;
                txtStudentNo.MaxLength = 30;
                txtStudentNo.Text = (txtStudentNo.Text ?? string.Empty).Replace("\r", "").Replace("\n", "").Trim();
            }

            addStudentCloseBtn.Visible = true;
            addStudentCloseBtn.Enabled = true;
            addTenantCloseBtn.Visible = false;
            addTenantCloseBtn.Enabled = false;

            detailsModal.Visible = true;

            endRentalBtn.Visible = false;

            _initialized = true;
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
            LoadStudentsGrid();
        }

        private void LoadStudentsGrid()
        {
            if (_selectedBhId <= 0)
            {
                dgvStudents.DataSource = null;
                UpdateTotalStudentsLabel();
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
                    s.id AS student_id,
                    s.student_no,
                    s.full_name,
                    COALESCE(bh_rental.name, bh_student.name) AS boarding_house
                FROM occupants o
                JOIN student_occupant_map som
                    ON som.occupant_id = o.id
                JOIN students s
                    ON s.id = som.student_id
                LEFT JOIN rentals ren
                    ON ren.occupant_id = o.id
                   AND ren.status = 'ACTIVE'
                   AND (ren.end_date IS NULL OR ren.end_date >= CURDATE())
                LEFT JOIN rooms r
                    ON r.id = ren.room_id
                LEFT JOIN boarding_houses bh_rental
                    ON bh_rental.id = r.boarding_house_id
                LEFT JOIN boarding_houses bh_student
                    ON bh_student.id = s.boarding_house_id
                WHERE
                    o.occupant_type = 'STUDENT'
                    AND
                    (
                        r.boarding_house_id = @bhId
                        OR (r.id IS NULL AND s.boarding_house_id = @bhId)
                    )
                    AND (@st = 'ALL' OR s.status = @st)
                    AND (
                        @kw = '' OR
                        s.full_name LIKE @kwLike OR
                        s.student_no LIKE @kwLike OR
                        COALESCE(bh_rental.name, bh_student.name) LIKE @kwLike
                    )
                ORDER BY s.full_name ASC;
            ";


                cmd.Parameters.AddWithValue("@bhId", _selectedBhId);
                cmd.Parameters.AddWithValue("@st", st);
                cmd.Parameters.AddWithValue("@kw", kw);
                cmd.Parameters.AddWithValue("@kwLike", "%" + kw + "%");

                var dt = new DataTable();
                using (var ad = new MySqlDataAdapter(cmd))
                    ad.Fill(dt);

                dgvStudents.AutoGenerateColumns = true;
                dgvStudents.DataSource = dt;

                // Hide all columns first
                foreach (DataGridViewColumn col in dgvStudents.Columns)
                    col.Visible = false;

                // Show required columns
                dgvStudents.Columns["student_id"].Visible = true;
                dgvStudents.Columns["student_id"].HeaderText = "ID";
                dgvStudents.Columns["student_id"].Width = 60;

                dgvStudents.Columns["student_no"].Visible = true;
                dgvStudents.Columns["student_no"].HeaderText = "Student No";
                dgvStudents.Columns["student_no"].Width = 120;

                dgvStudents.Columns["full_name"].Visible = true;
                dgvStudents.Columns["full_name"].HeaderText = "Student Name";

                dgvStudents.Columns["boarding_house"].Visible = true;
                dgvStudents.Columns["boarding_house"].HeaderText = "Boarding House";

                // Optional UI improvements
                dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvStudents.MultiSelect = false;
                dgvStudents.ReadOnly = true;
                dgvStudents.AllowUserToAddRows = false;
                dgvStudents.RowHeadersVisible = false;

                UpdateTotalStudentsLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load students.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDetailsHandlers()
        {
            btnSearch.Click -= BtnSearch_Click;
            btnSearch.Click += BtnSearch_Click;
            cbBoardingHouses.SelectedIndexChanged -= cbBoardingHouses_SelectedIndexChanged;
            cbBoardingHouses.SelectedIndexChanged += cbBoardingHouses_SelectedIndexChanged;
            addStudentCloseBtn.Click -= addStudentCloseBtn_Click;
            addStudentCloseBtn.Click += addStudentCloseBtn_Click;
            addTenantCloseBtn.Click -= addStudentCloseBtn_Click;
            addTenantCloseBtn.Click += addStudentCloseBtn_Click;
            cancelTenantRegister.Click -= cancelTenantRegister_Click;
            cancelTenantRegister.Click += cancelTenantRegister_Click;
            registerTenantBtn.Click -= registerTenantBtn_Click;
            registerTenantBtn.Click += registerTenantBtn_Click;

            studentUpdateBtn.Click -= studentUpdateBtn_Click;
            studentUpdateBtn.Click += studentUpdateBtn_Click;
            studentDeleteBtn.Click -= studentDeleteBtn_Click;
            studentDeleteBtn.Click += studentDeleteBtn_Click;
            endRentalBtn.Click -= endRentalBtn_Click;
            endRentalBtn.Click += endRentalBtn_Click;

            btnSnapshotRefresh.Click -= btnSnapshotRefresh_Click;
            btnSnapshotRefresh.Click += btnSnapshotRefresh_Click;
            btnSnapshotOpenPayments.Click -= btnSnapshotOpenPayments_Click;
            btnSnapshotOpenPayments.Click += btnSnapshotOpenPayments_Click;
            btnSnapshotViewRoom.Click -= btnSnapshotViewRoom_Click;
            btnSnapshotViewRoom.Click += btnSnapshotViewRoom_Click;

            detailsModal.MouseDown -= detailsModal_MouseDown;
            detailsModal.MouseDown += detailsModal_MouseDown;

            detailsEmail.ReadOnly = true;
            detailsEContact.ReadOnly = true;

            detailsCbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void BtnSearch_Click(object? sender, EventArgs e)
        {
            LoadStudentsGrid();
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

        private void UpdateTotalStudentsLabel()
        {
            if (_selectedBhId <= 0)
            {
                totalStudents.Text = "(0)";
                return;
            }

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                EnsureOpen(conn);

                using var cmd = conn.CreateCommand();

                cmd.CommandText = @"
            SELECT COUNT(DISTINCT t.id)
            FROM students t
            INNER JOIN student_occupant_map tom
                ON tom.student_id = t.id
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
                totalStudents.Text = $"({total})";
            }
            catch
            {
                totalStudents.Text = "(?)";
            }
        }

        private static void EnsureOpen(MySqlConnection conn)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
        }

        private void cbBoardingHouses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBoardingHouses.SelectedValue == null) return;

            if (int.TryParse(cbBoardingHouses.SelectedValue.ToString(), out int bhId))
            {
                _selectedBhId = bhId;
                LoadStudentsGrid();
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
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load rooms.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cbDetailsRoom.EndUpdate();
                cbDetailsRoom.SelectedIndex = 0;
            }
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

        private static bool RoomIsFull(MySqlConnection conn, int roomId)
        {
            if (conn == null || roomId <= 0) return true;

            using var capCmd = conn.CreateCommand();
            capCmd.CommandText = "SELECT capacity FROM rooms WHERE id = @roomId LIMIT 1;";
            capCmd.Parameters.AddWithValue("@roomId", roomId);
            int capacity = Convert.ToInt32(capCmd.ExecuteScalar() ?? 0);
            if (capacity <= 0) return true;

            using var cntCmd = conn.CreateCommand();
            cntCmd.CommandText = @"
                SELECT COUNT(*)
                FROM rentals
                WHERE room_id = @roomId
                  AND status = 'ACTIVE'
                  AND (end_date IS NULL OR end_date >= CURDATE());
            ";
            cntCmd.Parameters.AddWithValue("@roomId", roomId);
            int active = Convert.ToInt32(cntCmd.ExecuteScalar() ?? 0);
            return active >= capacity;
        }

        private static decimal GetRoomMonthlyRate(MySqlConnection conn, int roomId)
        {
            if (conn == null || roomId <= 0) return 0m;

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT IFNULL(monthly_rate,0) FROM rooms WHERE id = @roomId LIMIT 1;";
            cmd.Parameters.AddWithValue("@roomId", roomId);
            return Convert.ToDecimal(cmd.ExecuteScalar() ?? 0m);
        }

        private static string BuildFullName(string ln, string fn, string mn)
        {
            return $"{ln}, {fn}" + (string.IsNullOrWhiteSpace(mn) ? "" : $" {mn}");
        }

        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SoundClicked.itemClicked();
            studentUpdateBtn.Visible = true;
            studentDeleteBtn.Visible = true;
            endRentalBtn.Visible = true;

            if (e.RowIndex < 0) return;

            var row = dgvStudents.Rows[e.RowIndex];
            if (row == null) return;

            int studentId = SafeInt(row.Cells["student_id"]?.Value);
            if (studentId <= 0) return;

            LoadStudentDetails(studentId);
            LoadPaymentHistory(studentId);
            LoadStudentSnapshot(studentId);
            grpDetails.Visible = true;
            detailsModal.BringToFront();
            grpDetails.BringToFront();
        }

        private void DgvStudents_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (_pendingSelectStudentId <= 0) return;

            SelectStudentRowInGrid(_pendingSelectStudentId);
            _pendingSelectStudentId = 0;
        }

        private void SelectStudentRowInGrid(int studentId)
        {
            if (studentId <= 0 || dgvStudents.Rows.Count == 0) return;

            dgvStudents.ClearSelection();

            DataGridViewRow? target = null;
            foreach (DataGridViewRow row in dgvStudents.Rows)
            {
                if (row.IsNewRow) continue;
                if (SafeInt(row.Cells["student_id"]?.Value) == studentId)
                {
                    target = row;
                    break;
                }
            }

            if (target == null) return;

            target.Selected = true;

            DataGridViewCell? focus = null;
            foreach (DataGridViewCell cell in target.Cells)
            {
                if (cell.Visible)
                {
                    focus = cell;
                    break;
                }
            }
            if (focus == null && target.Cells.Count > 0) focus = target.Cells[0];

            if (focus != null)
            {
                try { dgvStudents.CurrentCell = focus; } catch { }
            }

            try { dgvStudents.FirstDisplayedScrollingRowIndex = target.Index; } catch { }
        }

        public void OpenDetailsByStudentId(int studentId)
        {
            if (studentId <= 0) return;

            InitializeStudentsViewOnce();

            _pendingSelectStudentId = studentId;

            int activeIdx = cbStatusFilter.Items.IndexOf("ACTIVE");
            if (activeIdx >= 0)
            {
                cbStatusFilter.SelectedIndexChanged -= CbStatusFilter_SelectedIndexChanged;
                try { cbStatusFilter.SelectedItem = "ACTIVE"; }
                finally { cbStatusFilter.SelectedIndexChanged += CbStatusFilter_SelectedIndexChanged; }
            }

            int bhId = GetEffectiveBoardingHouseIdForStudent(studentId);
            if (bhId > 0)
            {
                _selectedBhId = bhId;

                cbBoardingHouses.SelectedIndexChanged -= cbBoardingHouses_SelectedIndexChanged;
                try { cbBoardingHouses.SelectedValue = bhId; }
                finally { cbBoardingHouses.SelectedIndexChanged += cbBoardingHouses_SelectedIndexChanged; }

                LoadRoomsDropdownForDetails(bhId);
            }
            else
            {
                if (cbBoardingHouses.SelectedValue != null &&
                    int.TryParse(cbBoardingHouses.SelectedValue.ToString(), out int curBh))
                    _selectedBhId = curBh;
            }

            LoadStudentsGrid();
            SelectStudentRowInGrid(studentId);

            LoadStudentDetails(studentId);
            LoadPaymentHistory(studentId);
            LoadStudentSnapshot(studentId);

            studentUpdateBtn.Visible = true;
            studentDeleteBtn.Visible = true;
            endRentalBtn.Visible = _activeRentalId > 0;

        }

        private static int SafeInt(object v)
        {
            if (v == null || v == DBNull.Value) return 0;
            if (int.TryParse(v.ToString(), out int x)) return x;
            return 0;
        }

        private void LoadStudentDetails(int studentId)
        {
            _selectedStudentId = studentId;
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
                        t.student_no,
                        t.boarding_house_id,
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
                        ren.room_id AS active_room_id,
                        rm.boarding_house_id AS active_room_bh_id
                    FROM students t
                    LEFT JOIN student_occupant_map tom
                        ON tom.student_id = t.id
                    LEFT JOIN rentals ren
                        ON ren.occupant_id = tom.occupant_id
                        AND ren.status = 'ACTIVE'
                        AND (ren.end_date IS NULL OR ren.end_date >= CURDATE())
                    LEFT JOIN rooms rm
                        ON rm.id = ren.room_id
                    WHERE t.id = @id
                    LIMIT 1;
                ";
                cmd.Parameters.AddWithValue("@id", studentId);

                using var r = cmd.ExecuteReader();
                if (!r.Read())
                {
                    MessageBox.Show("Student not found.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HideDetails();
                    return;
                }

                int studentBhId = r["boarding_house_id"] == DBNull.Value ? 0 : Convert.ToInt32(r["boarding_house_id"]);
                int activeRoomBhId = r["active_room_bh_id"] == DBNull.Value ? 0 : Convert.ToInt32(r["active_room_bh_id"]);
                int effectiveBhId = activeRoomBhId > 0 ? activeRoomBhId : studentBhId;
                if (effectiveBhId > 0)
                    LoadRoomsDropdownForDetails(effectiveBhId);

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
                cbDetailsRoom.Enabled = !hasActiveRental;

                if (hasActiveRental)
                {
                    int activeIdx = detailsCbStatus.Items.IndexOf("ACTIVE");
                    if (activeIdx >= 0)
                        detailsCbStatus.SelectedIndex = activeIdx;
                }

                string prof = (r["profile_path"] == DBNull.Value) ? "" : (r["profile_path"]?.ToString() ?? "");
                details_profilePathTxt.Text = prof;
                LoadProfileImageIntoPictureBox(detailsStudenttImg, prof);

                endRentalBtn.Visible = hasActiveRental;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load student details.\n" + ex.Message,
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

        private void HideDetails()
        {
            _selectedStudentId = 0;
            _activeRentalId = 0;
            _activeRoomId = 0;
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
                catch (Exception innerEx)
                {
                    MessageBox.Show("Failed to clear image resource.\n" + innerEx.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadPaymentHistory(int studentId)
        {
            dataGridView1.DataSource = null;
            label17.Text = "₱ 0.00";

            if (studentId <= 0) return;

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                EnsureOpen(conn);
                int occupantId = GetOccupantIdForStudent(conn, studentId);
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

        private int GetOccupantIdForStudent(MySqlConnection conn, int studentId)
        {
            if (conn == null || studentId <= 0) return 0;

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                SELECT occupant_id
                FROM student_occupant_map
                WHERE student_id = @tid
                LIMIT 1;
            ";
            cmd.Parameters.AddWithValue("@tid", studentId);

            var result = cmd.ExecuteScalar();
            return (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
        }

        private int GetOccupantIdForStudent(int studentId)
        {
            if (studentId <= 0) return 0;
            using var conn = DbConnectionFactory.CreateConnection();
            EnsureOpen(conn);
            return GetOccupantIdForStudent(conn, studentId);
        }

        private int GetEffectiveBoardingHouseIdForStudent(int studentId)
        {
            if (studentId <= 0) return 0;

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                EnsureOpen(conn);

                int occupantId = GetOccupantIdForStudent(conn, studentId);
                if (occupantId <= 0) return 0;

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT r.boarding_house_id
                        FROM rentals ren
                        JOIN rooms r ON r.id = ren.room_id
                        WHERE ren.occupant_id = @occId
                          AND ren.status = 'ACTIVE'
                          AND (ren.end_date IS NULL OR ren.end_date >= CURDATE())
                        LIMIT 1;
                    ";
                    cmd.Parameters.AddWithValue("@occId", occupantId);
                    var v = cmd.ExecuteScalar();
                    int bh = (v == null || v == DBNull.Value) ? 0 : Convert.ToInt32(v);
                    if (bh > 0) return bh;
                }

                using (var cmd2 = conn.CreateCommand())
                {
                    cmd2.CommandText = @"SELECT boarding_house_id FROM students WHERE id=@sid LIMIT 1;";
                    cmd2.Parameters.AddWithValue("@sid", studentId);
                    var v = cmd2.ExecuteScalar();
                    return (v == null || v == DBNull.Value) ? 0 : Convert.ToInt32(v);
                }
            }
            catch
            {
                return 0;
            }
        }

        private void LoadStudentSnapshot(int studentId)
        {
            lblSnapshotStudent.Text = "—";
            lblSnapshotStudentId.Text = "—";
            lblSnapshotBoardingHouse.Text = "—";
            lblSnapshotRoomAssigned.Text = "—";
            lblSnapshotRentalStart.Text = "—";
            lblSnapshotDuration.Text = "—";
            lblSnapshotLastPayment.Text = "—";
            lblSnapshotLastAmount.Text = "₱ 0.00";

            lblSnapshotStatusBadge.Text = "INACTIVE";
            lblSnapshotStatusBadge.BackColor = Color.Gray;

            if (studentId <= 0) return;

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                EnsureOpen(conn);

                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
            SELECT
                t.id AS student_id,
                t.full_name,
                t.status AS student_status,

                ren.id AS rental_id,
                ren.start_date,
                ren.end_date,
                ren.status AS rental_status,

                r.room_no,
                r.room_type,

                bh.name AS boarding_house,

                lp.payment_date AS last_payment_date,
                lp.amount AS last_payment_amount
            FROM students t
            LEFT JOIN student_occupant_map tom
                ON tom.student_id = t.id
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
                cmd.Parameters.AddWithValue("@tid", studentId);

                using var r = cmd.ExecuteReader();
                if (!r.Read()) return;

                lblSnapshotStudent.Text = r["full_name"]?.ToString() ?? "—";
                lblSnapshotStudentId.Text = r["student_id"]?.ToString() ?? "—";

                string st = r["student_status"]?.ToString() ?? "INACTIVE";
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
                MessageBox.Show("Failed to load students snapshot.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addStudentsBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            OpenAddStudentModal(_selectedBhId);
        }

        public void OpenAddStudentModal(int bhId = 0)
        {
            InitializeStudentsViewOnce();

            _pendingSelectStudentId = 0;

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

                LoadRoomsDropdownForDetails(targetBhId);
            }

            LoadStudentsGrid();
            UpdateTotalStudentsLabel();

            ClearStudentForm();

            addStudentsModal.Visible = true;
            addStudentsModal.BringToFront();

            studentLastNameTxt?.Focus();
        }

        private void ClearStudentForm()
        {
            studentLastNameTxt.Text = "";
            studentFirstnameTxt.Text = "";
            studentMiddleNameTxt.Text = "";
            studentContactTxt.Text = "";
            studentEmailTxt.Text = "";
            studentAddressTxt.Text = "";
            studentEmergencyNameTxt.Text = "";
            studentEmergencyContactTxt.Text = "";
            profilePathTxt.Text = "";
            txtStudentNo.Text = "";

            if (cbOccType != null)
            {
                cbOccType.Visible = false;
                cbOccType.Enabled = false;
            }
            if (label18 != null)
                label18.Visible = false;

            addTenantCloseBtn.Visible = false;
            addTenantCloseBtn.Enabled = false;
        }

        private int GetSelectedBoardingHouseId()
        {
            if (cbBoardingHouses.SelectedValue != null &&
                int.TryParse(cbBoardingHouses.SelectedValue.ToString(), out int bhId))
            {
                return bhId;
            }

            return _selectedBhId;
        }

        private void registerTenantBtn_Click(object? sender, EventArgs e) => RegisterStudent();
        private void studentUpdateBtn_Click(object? sender, EventArgs e) => UpdateStudent();
        private void studentDeleteBtn_Click(object? sender, EventArgs e) => DeleteStudent();
        private void endRentalBtn_Click(object? sender, EventArgs e) => EndActiveRental();

        private void addStudentCloseBtn_Click(object? sender, EventArgs e)
        {
            addStudentsModal.Visible = false;
            ClearStudentForm();
        }

        private void cancelTenantRegister_Click(object? sender, EventArgs e)
        {
            addStudentsModal.Visible = false;
            ClearStudentForm();
        }

        private void detailsModal_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                HideDetails();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                if (addStudentsModal.Visible)
                {
                    addStudentsModal.Visible = false;
                    return true;
                }

                if (detailsModal.Visible || grpDetails.Visible)
                {
                    HideDetails();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnSnapshotRefresh_Click(object? sender, EventArgs e)
        {
            if (_selectedStudentId <= 0) return;
            LoadStudentDetails(_selectedStudentId);
            LoadStudentSnapshot(_selectedStudentId);
            LoadPaymentHistory(_selectedStudentId);
        }

        private void btnSnapshotOpenPayments_Click(object? sender, EventArgs e)
        {
            if (_selectedStudentId <= 0)
            {
                MessageBox.Show("Select a student first.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_activeRentalId <= 0)
            {
                MessageBox.Show("This student has no ACTIVE rental.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (FindForm() is not MainLayout main)
                return;

            main.OpenPaymentsForRental(_activeRentalId);
            HideDetails();
        }

        private void btnSnapshotViewRoom_Click(object? sender, EventArgs e)
        {
            if (_activeRoomId <= 0)
            {
                MessageBox.Show("This student has no assigned room yet.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (FindForm() is not MainLayout main)
                return;

            main.OpenRoomsForTenantRoom(_activeRoomId);
            HideDetails();
        }

        private void RegisterStudent()
        {
            string ln = (studentLastNameTxt.Text ?? "").Trim();
            string fn = (studentFirstnameTxt.Text ?? "").Trim();
            string mn = (studentMiddleNameTxt.Text ?? "").Trim();
            string studentNo = (txtStudentNo.Text ?? "").Replace("\r", "").Replace("\n", "").Trim();
            string fullName = $"{ln}, {fn}" + (string.IsNullOrWhiteSpace(mn) ? "" : $" {mn}");
            string contact = (studentContactTxt.Text ?? "").Trim();
            string email = (studentEmailTxt.Text ?? "").Trim();
            string address = (studentAddressTxt.Text ?? "").Trim();
            string eName = (studentEmergencyNameTxt.Text ?? "").Trim();
            string eNo = (studentEmergencyContactTxt.Text ?? "").Trim();
            string profile = (profilePathTxt.Text ?? "").Trim();
            string occType = "STUDENT";

            _selectedBhId = GetSelectedBoardingHouseId();

            if (string.IsNullOrWhiteSpace(ln) || string.IsNullOrWhiteSpace(fn))
            {
                MessageBox.Show("Lastname and Firstname are required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(studentNo))
            {
                MessageBox.Show("Student No is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (studentNo.Length > 30)
            {
                MessageBox.Show("Student No must be 30 characters max.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            long newStudentId;
            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                EnsureOpen(conn);
                using var tx = conn.BeginTransaction();

                try
                {
                    using (var dupCmd = conn.CreateCommand())
                    {
                        dupCmd.Transaction = tx;
                        dupCmd.CommandText = "SELECT COUNT(*) FROM students WHERE student_no = @studentNo LIMIT 1;";
                        dupCmd.Parameters.AddWithValue("@studentNo", studentNo);
                        int exists = Convert.ToInt32(dupCmd.ExecuteScalar() ?? 0);
                        if (exists > 0)
                            throw new InvalidOperationException("Student No already exists.");
                    }

                    long occupantId;
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

                    using (var cmdStudent = conn.CreateCommand())
                    {
                        cmdStudent.Transaction = tx;
                        cmdStudent.CommandText = @"
                            INSERT INTO students
                            (student_no, boarding_house_id, lastname, firstname, middlename, full_name,
                             contact_no, email, address, profile_path,
                             emergency_contact_name, emergency_contact_no,
                             status, created_at, updated_at)
                            VALUES
                            (@studentNo, @bhId, @ln, @fn, @mn, @full,
                             @contact, @email, @address, @profile,
                             @eName, @eNo,
                             'ACTIVE', NOW(), NOW());
                        ";
                        cmdStudent.Parameters.AddWithValue("@studentNo", studentNo);
                        cmdStudent.Parameters.AddWithValue("@bhId", _selectedBhId > 0 ? (object)_selectedBhId : DBNull.Value);
                        cmdStudent.Parameters.AddWithValue("@ln", ln);
                        cmdStudent.Parameters.AddWithValue("@fn", fn);
                        cmdStudent.Parameters.AddWithValue("@mn", string.IsNullOrWhiteSpace(mn) ? (object)DBNull.Value : mn);
                        cmdStudent.Parameters.AddWithValue("@full", fullName);
                        cmdStudent.Parameters.AddWithValue("@contact", string.IsNullOrWhiteSpace(contact) ? (object)DBNull.Value : contact);
                        cmdStudent.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(email) ? (object)DBNull.Value : email);
                        cmdStudent.Parameters.AddWithValue("@address", string.IsNullOrWhiteSpace(address) ? (object)DBNull.Value : address);
                        cmdStudent.Parameters.AddWithValue("@profile", string.IsNullOrWhiteSpace(profile) ? (object)DBNull.Value : profile);
                        cmdStudent.Parameters.AddWithValue("@eName", string.IsNullOrWhiteSpace(eName) ? (object)DBNull.Value : eName);
                        cmdStudent.Parameters.AddWithValue("@eNo", string.IsNullOrWhiteSpace(eNo) ? (object)DBNull.Value : eNo);
                        cmdStudent.ExecuteNonQuery();
                        newStudentId = cmdStudent.LastInsertedId;
                    }

                    using (var mapCmd = conn.CreateCommand())
                    {
                        mapCmd.Transaction = tx;
                        mapCmd.CommandText = @"
                            INSERT INTO student_occupant_map (student_id, occupant_id)
                            VALUES (@sid, @oid);
                        ";
                        mapCmd.Parameters.AddWithValue("@sid", newStudentId);
                        mapCmd.Parameters.AddWithValue("@oid", occupantId);
                        mapCmd.ExecuteNonQuery();
                    }

                    WriteAuditLog(conn, tx,
                        CurrentUserId > 0 ? (int?)CurrentUserId : null,
                        "CREATE",
                        "STUDENT",
                        (int)newStudentId,
                        $"student_no={studentNo}; full_name={fullName}; boarding_house_id={_selectedBhId}");

                    tx.Commit();
                }
                catch
                {
                    try { tx.Rollback(); }
                    catch (Exception rollbackEx)
                    {
                        MessageBox.Show("Failed to rollback registration transaction.\n" + rollbackEx.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    throw;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to register student.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Student registered successfully.", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            _pendingSelectStudentId = (int)newStudentId;
            addStudentsModal.Visible = false;
            ClearStudentForm();

            LoadStudentsGrid();
            UpdateTotalStudentsLabel();
            LoadStudentDetails((int)newStudentId);
            LoadPaymentHistory((int)newStudentId);
            LoadStudentSnapshot((int)newStudentId);
            detailsModal.Visible = true;
            grpDetails.Visible = true;
            detailsModal.BringToFront();
            grpDetails.BringToFront();
        }

        private void UpdateStudent()
        {
            if (_selectedStudentId <= 0)
            {
                MessageBox.Show("Select a student first.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string ln = (detailsLastname.Text ?? "").Trim();
            string fn = (detailsFirstname.Text ?? "").Trim();
            string mn = (detailsMiddlename.Text ?? "").Trim();
            string fullName = BuildFullName(ln, fn, mn);
            string contact = (detailsContact.Text ?? "").Trim();
            string email = (detailsEmail.Text ?? "").Trim();
            string address = (detailsAddress.Text ?? "").Trim();
            string eName = (detailsEName.Text ?? "").Trim();
            string eNo = (detailsEContact.Text ?? "").Trim();
            string profile = (details_profilePathTxt.Text ?? "").Trim();
            string status = detailsCbStatus.SelectedItem?.ToString() ?? "ACTIVE";
            if (_activeRentalId > 0) status = "ACTIVE";
            int pickedRoomId = (cbDetailsRoom.SelectedItem is RoomPickItem pick) ? pick.RoomId : 0;

            if (string.IsNullOrWhiteSpace(ln) || string.IsNullOrWhiteSpace(fn))
            {
                MessageBox.Show("Lastname and Firstname are required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _selectedBhId = GetSelectedBoardingHouseId();

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                EnsureOpen(conn);
                using var tx = conn.BeginTransaction();

                try
                {
                    int occupantId = GetOccupantIdForStudent(conn, _selectedStudentId);
                    if (occupantId <= 0)
                        throw new InvalidOperationException("Student mapping to occupant is missing.");

                    long createdRentalId = 0;

                    if (_activeRentalId == 0 && pickedRoomId > 0)
                    {
                        if (RoomIsFull(conn, pickedRoomId))
                            throw new InvalidOperationException("Selected room is already full.");

                        decimal monthlyRate = GetRoomMonthlyRate(conn, pickedRoomId);
                        if (monthlyRate <= 0)
                            throw new InvalidOperationException("Selected room has invalid monthly rate.");

                        status = "ACTIVE";

                        using var insRent = conn.CreateCommand();
                        insRent.Transaction = tx;
                        insRent.CommandText = @"
                            INSERT INTO rentals
                            (room_id, occupant_id, start_date, end_date, monthly_rate, deposit_amount,
                             status, notes, created_by, created_at, updated_at)
                            VALUES
                            (@roomId, @occId, CURDATE(), NULL, @rate, 0.00,
                             'ACTIVE', NULL, @createdBy, NOW(), NOW());
                        ";
                        insRent.Parameters.AddWithValue("@roomId", pickedRoomId);
                        insRent.Parameters.AddWithValue("@occId", occupantId);
                        insRent.Parameters.AddWithValue("@rate", monthlyRate);
                        insRent.Parameters.AddWithValue("@createdBy", CurrentUserId > 0 ? (object)CurrentUserId : DBNull.Value);
                        insRent.ExecuteNonQuery();
                        createdRentalId = insRent.LastInsertedId;
                    }

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.Transaction = tx;
                        cmd.CommandText = @"
                            UPDATE students
                            SET boarding_house_id = @bhId,
                                lastname = @ln,
                                firstname = @fn,
                                middlename = @mn,
                                full_name = @full,
                                contact_no = @contact,
                                email = @email,
                                address = @address,
                                profile_path = @profile,
                                emergency_contact_name = @eName,
                                emergency_contact_no = @eNo,
                                status = @status,
                                updated_at = NOW()
                            WHERE id = @sid
                            LIMIT 1;
                        ";
                        cmd.Parameters.AddWithValue("@bhId", _selectedBhId > 0 ? (object)_selectedBhId : DBNull.Value);
                        cmd.Parameters.AddWithValue("@ln", ln);
                        cmd.Parameters.AddWithValue("@fn", fn);
                        cmd.Parameters.AddWithValue("@mn", string.IsNullOrWhiteSpace(mn) ? (object)DBNull.Value : mn);
                        cmd.Parameters.AddWithValue("@full", fullName);
                        cmd.Parameters.AddWithValue("@contact", string.IsNullOrWhiteSpace(contact) ? (object)DBNull.Value : contact);
                        cmd.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(email) ? (object)DBNull.Value : email);
                        cmd.Parameters.AddWithValue("@address", string.IsNullOrWhiteSpace(address) ? (object)DBNull.Value : address);
                        cmd.Parameters.AddWithValue("@profile", string.IsNullOrWhiteSpace(profile) ? (object)DBNull.Value : profile);
                        cmd.Parameters.AddWithValue("@eName", string.IsNullOrWhiteSpace(eName) ? (object)DBNull.Value : eName);
                        cmd.Parameters.AddWithValue("@eNo", string.IsNullOrWhiteSpace(eNo) ? (object)DBNull.Value : eNo);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@sid", _selectedStudentId);
                        cmd.ExecuteNonQuery();
                    }

                    if (occupantId > 0)
                    {
                        using var cmdOcc = conn.CreateCommand();
                        cmdOcc.Transaction = tx;
                        cmdOcc.CommandText = @"
                            UPDATE occupants
                            SET lastname = @ln,
                                firstname = @fn,
                                middlename = @mn,
                                full_name = @full,
                                contact_no = @contact,
                                email = @email,
                                address = @address,
                                profile_path = @profile,
                                emergency_contact_name = @eName,
                                emergency_contact_no = @eNo,
                                status = @status,
                                updated_at = NOW()
                            WHERE id = @oid
                            LIMIT 1;
                        ";
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
                        cmdOcc.Parameters.AddWithValue("@status", status);
                        cmdOcc.Parameters.AddWithValue("@oid", occupantId);
                        cmdOcc.ExecuteNonQuery();
                    }

                    WriteAuditLog(conn, tx,
                        CurrentUserId > 0 ? (int?)CurrentUserId : null,
                        "UPDATE",
                        "STUDENT",
                        _selectedStudentId,
                        $"Updated student profile/status={status}");

                    if (createdRentalId > 0)
                    {
                        WriteAuditLog(conn, tx,
                            CurrentUserId > 0 ? (int?)CurrentUserId : null,
                            "CREATE",
                            "RENTAL",
                            (int)createdRentalId,
                            $"student_id={_selectedStudentId}; occupant_id={occupantId}; room_id={pickedRoomId}; monthly_rate={GetRoomMonthlyRate(conn, pickedRoomId)}");
                    }

                    tx.Commit();
                }
                catch
                {
                    try { tx.Rollback(); }
                    catch (Exception rollbackEx)
                    {
                        MessageBox.Show("Failed to rollback update transaction.\n" + rollbackEx.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    throw;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update student.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadStudentsGrid();
            UpdateTotalStudentsLabel();
            LoadStudentDetails(_selectedStudentId);
            LoadStudentSnapshot(_selectedStudentId);
            LoadPaymentHistory(_selectedStudentId);
        }

        private void DeleteStudent()
        {
            if (_selectedStudentId <= 0)
            {
                MessageBox.Show("Select a student first.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Deactivate this student?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                EnsureOpen(conn);
                using var tx = conn.BeginTransaction();

                try
                {
                    int occupantId = 0;
                    using (var occCmd = conn.CreateCommand())
                    {
                        occCmd.Transaction = tx;
                        occCmd.CommandText = @"
                            SELECT occupant_id
                            FROM student_occupant_map
                            WHERE student_id = @sid
                            LIMIT 1;
                        ";
                        occCmd.Parameters.AddWithValue("@sid", _selectedStudentId);
                        var occObj = occCmd.ExecuteScalar();
                        occupantId = (occObj == null || occObj == DBNull.Value) ? 0 : Convert.ToInt32(occObj);
                    }

                    if (occupantId > 0)
                    {
                        using var activeCmd = conn.CreateCommand();
                        activeCmd.Transaction = tx;
                        activeCmd.CommandText = @"
                            SELECT COUNT(*)
                            FROM rentals
                            WHERE occupant_id = @occId
                              AND status = 'ACTIVE'
                              AND (end_date IS NULL OR end_date >= CURDATE());
                        ";
                        activeCmd.Parameters.AddWithValue("@occId", occupantId);
                        int activeCount = Convert.ToInt32(activeCmd.ExecuteScalar() ?? 0);
                        if (activeCount > 0)
                            throw new InvalidOperationException("Cannot deactivate student with ACTIVE rental.");

                        using var payCmd = conn.CreateCommand();
                        payCmd.Transaction = tx;
                        payCmd.CommandText = @"
                            SELECT COUNT(*)
                            FROM payments p
                            INNER JOIN rentals r ON r.id = p.rental_id
                            WHERE r.occupant_id = @occId;
                        ";
                        payCmd.Parameters.AddWithValue("@occId", occupantId);
                        int payCount = Convert.ToInt32(payCmd.ExecuteScalar() ?? 0);
                        if (payCount > 0)
                            throw new InvalidOperationException("Cannot deactivate student with existing payment history.");
                    }

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.Transaction = tx;
                        cmd.CommandText = @"
                            UPDATE students
                            SET status = 'INACTIVE', updated_at = NOW()
                            WHERE id = @sid
                            LIMIT 1;
                        ";
                        cmd.Parameters.AddWithValue("@sid", _selectedStudentId);
                        cmd.ExecuteNonQuery();
                    }

                    if (occupantId > 0)
                    {
                        using var cmdOcc = conn.CreateCommand();
                        cmdOcc.Transaction = tx;
                        cmdOcc.CommandText = @"
                            UPDATE occupants
                            SET status = 'INACTIVE', updated_at = NOW()
                            WHERE id = @oid
                            LIMIT 1;
                        ";
                        cmdOcc.Parameters.AddWithValue("@oid", occupantId);
                        cmdOcc.ExecuteNonQuery();
                    }

                    WriteAuditLog(conn, tx,
                        CurrentUserId > 0 ? (int?)CurrentUserId : null,
                        "DEACTIVATE",
                        "STUDENT",
                        _selectedStudentId,
                        "Student deactivated from StudentsView.");

                    tx.Commit();
                }
                catch
                {
                    try { tx.Rollback(); }
                    catch (Exception rollbackEx)
                    {
                        MessageBox.Show("Failed to rollback delete transaction.\n" + rollbackEx.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    throw;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cannot Delete Student",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            HideDetails();
            LoadStudentsGrid();
            UpdateTotalStudentsLabel();
        }

        private void EndActiveRental()
        {
            if (_activeRentalId <= 0 || _selectedStudentId <= 0)
            {
                MessageBox.Show("No active rental to end.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                EnsureOpen(conn);
                using var tx = conn.BeginTransaction();

                int roomId = 0;
                try
                {
                    using (var roomCmd = conn.CreateCommand())
                    {
                        roomCmd.Transaction = tx;
                        roomCmd.CommandText = "SELECT room_id FROM rentals WHERE id=@rid LIMIT 1;";
                        roomCmd.Parameters.AddWithValue("@rid", _activeRentalId);
                        var roomObj = roomCmd.ExecuteScalar();
                        roomId = (roomObj == null || roomObj == DBNull.Value) ? 0 : Convert.ToInt32(roomObj);
                    }

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.Transaction = tx;
                        cmd.CommandText = @"
                            UPDATE rentals
                            SET end_date = CURDATE(),
                                status = 'ENDED',
                                updated_at = NOW()
                            WHERE id = @rid
                              AND status = 'ACTIVE'
                            LIMIT 1;
                        ";
                        cmd.Parameters.AddWithValue("@rid", _activeRentalId);
                        cmd.ExecuteNonQuery();
                    }

                    if (roomId > 0)
                    {
                        int stillActive = 0;
                        using (var chk = conn.CreateCommand())
                        {
                            chk.Transaction = tx;
                            chk.CommandText = @"
                                SELECT COUNT(*)
                                FROM rentals
                                WHERE room_id = @roomId
                                  AND status = 'ACTIVE'
                                  AND (end_date IS NULL OR end_date >= CURDATE());
                            ";
                            chk.Parameters.AddWithValue("@roomId", roomId);
                            stillActive = Convert.ToInt32(chk.ExecuteScalar() ?? 0);
                        }

                        if (stillActive == 0)
                        {
                            using var roomUp = conn.CreateCommand();
                            roomUp.Transaction = tx;
                            roomUp.CommandText = @"
                                UPDATE rooms
                                SET status = 'AVAILABLE'
                                WHERE id = @roomId
                                  AND status = 'OCCUPIED';
                            ";
                            roomUp.Parameters.AddWithValue("@roomId", roomId);
                            roomUp.ExecuteNonQuery();
                        }
                    }

                    WriteAuditLog(conn, tx,
                        CurrentUserId > 0 ? (int?)CurrentUserId : null,
                        "END_RENTAL",
                        "RENTAL",
                        _activeRentalId,
                        $"student_id={_selectedStudentId}; room_id={roomId}");

                    tx.Commit();
                }
                catch
                {
                    try { tx.Rollback(); }
                    catch (Exception rollbackEx)
                    {
                        MessageBox.Show("Failed to rollback end-rental transaction.\n" + rollbackEx.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    throw;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to end rental.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadStudentsGrid();
            UpdateTotalStudentsLabel();
            LoadStudentDetails(_selectedStudentId);
            LoadStudentSnapshot(_selectedStudentId);
            LoadPaymentHistory(_selectedStudentId);
        }

        private void WriteAuditLog(MySqlConnection conn, MySqlTransaction tx, int? userId, string action, string entity, int? entityId, string details)
        {
            using var cmd = conn.CreateCommand();
            cmd.Transaction = tx;
            cmd.CommandText = @"
                INSERT INTO audit_logs
                (user_id, action, entity, entity_id, details, created_at)
                VALUES
                (@uid, @action, @entity, @entityId, @details, NOW());
            ";
            cmd.Parameters.AddWithValue("@uid", userId.HasValue && userId.Value > 0 ? (object)userId.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@action", action ?? "");
            cmd.Parameters.AddWithValue("@entity", entity ?? "");
            cmd.Parameters.AddWithValue("@entityId", entityId.HasValue ? (object)entityId.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@details", string.IsNullOrWhiteSpace(details) ? (object)DBNull.Value : details);
            cmd.ExecuteNonQuery();
        }
    }
}
