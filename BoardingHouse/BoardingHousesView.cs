using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;


namespace BoardingHouse
{
    public partial class BoardingHousesView : UserControl
    {
        private string? _pendingMarkersJson;
        private bool _mapEventsWired;
        private bool _locatorEventsWired;
        private string? _pendingSingleMarkerJson;
        private bool _singleMapEventsWired;
        private bool _roomsGridSetupDone = false;

        public BoardingHousesView()
        {
            InitializeComponent();
        }

        private void CenterModal(Panel modal)
        {
            if (modal == null)
                return;

            var parentSize = this.ClientSize;
            var x = (parentSize.Width - modal.Width) / 2;
            var y = (parentSize.Height - modal.Height) / 2;
            modal.Location = new Point(Math.Max(x, 0), Math.Max(y, 0));
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void LoadBoardingHouses()
        {
            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = conn.CreateCommand())
                {
                    EnsureOpen(conn);

                    cmd.CommandText = @"
                                        SELECT 
                                            bh.id,
                                            bh.name,
                                            bh.address,
                                            COALESCE(o.full_name, '') AS owner_name,
                                            COALESCE(o.contact_no, '') AS contact_no,
                                            bh.status,
                                            IFNULL(rc.total_rooms, 0) AS total_rooms,
                                            IFNULL(rc.available_rooms, 0) AS available_rooms,
                                            bh.latitude,
                                            bh.longitude
                                        FROM boarding_houses bh
                                        LEFT JOIN owners o ON o.id = bh.owner_id
                                        LEFT JOIN (
                                            SELECT
                                                boarding_house_id,
                                                COUNT(*) AS total_rooms,
                                                SUM(CASE WHEN status = 'AVAILABLE' THEN 1 ELSE 0 END) AS available_rooms
                                            FROM rooms
                                            GROUP BY boarding_house_id
                                        ) rc ON rc.boarding_house_id = bh.id
                                        ORDER BY bh.id DESC;
                                        ";

                    var table = new DataTable();
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }

                    dgvBoardingHouses.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to load boarding houses.\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void LoadBoardingHouseStats()
        {
            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = conn.CreateCommand())
                {
                    EnsureOpen(conn);
                    cmd.CommandText = @"
                        SELECT
                            COUNT(*) AS TotalCount,
                            SUM(CASE WHEN status = 'ACTIVE' THEN 1 ELSE 0 END) AS ActiveCount,
                            SUM(CASE WHEN status = 'INACTIVE' THEN 1 ELSE 0 END) AS InactiveCount
                        FROM boarding_houses;
                    ";

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                            return;

                        int total = reader["TotalCount"] != DBNull.Value ? Convert.ToInt32(reader["TotalCount"]) : 0;
                        int active = reader["ActiveCount"] != DBNull.Value ? Convert.ToInt32(reader["ActiveCount"]) : 0;
                        int inactive = reader["InactiveCount"] != DBNull.Value ? Convert.ToInt32(reader["InactiveCount"]) : 0;

                        totalBH.Text = total.ToString();
                        totalActive.Text = active.ToString();
                        totalInactive.Text = inactive.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to load boarding house statistics.\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private void SetupBoardingHouseGrid()
        {
            dgvBoardingHouses.AutoGenerateColumns = false;
            dgvBoardingHouses.AllowUserToAddRows = false;
            dgvBoardingHouses.AllowUserToDeleteRows = false;
            dgvBoardingHouses.ReadOnly = true;
            dgvBoardingHouses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBoardingHouses.MultiSelect = false;
            dgvBoardingHouses.RowHeadersVisible = false;

            dgvBoardingHouses.Dock = DockStyle.Fill;
            dgvBoardingHouses.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            dgvBoardingHouses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBoardingHouses.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            dgvBoardingHouses.Columns.Clear();

            dgvBoardingHouses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colId",
                HeaderText = "ID",
                DataPropertyName = "id",
                Width = 60
            });

            dgvBoardingHouses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colName",
                HeaderText = "Boarding House",
                DataPropertyName = "name",
                Width = 200
            });

            dgvBoardingHouses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAddress",
                HeaderText = "Address",
                DataPropertyName = "address",
                Width = 200
            });

            dgvBoardingHouses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colOwner",
                HeaderText = "Owner",
                DataPropertyName = "owner_name",
                Width = 160
            });

            dgvBoardingHouses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colContact",
                HeaderText = "Contact",
                DataPropertyName = "contact_no",
                Width = 130
            });

            dgvBoardingHouses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTotal",
                HeaderText = "Total",
                DataPropertyName = "total_rooms",
                Width = 70
            });

            dgvBoardingHouses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAvailable",
                HeaderText = "Available",
                DataPropertyName = "available_rooms",
                Width = 80
            });

            dgvBoardingHouses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colLat",
                HeaderText = "Lat",
                DataPropertyName = "latitude",
                Width = 90
            });

            dgvBoardingHouses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colLng",
                HeaderText = "Lng",
                DataPropertyName = "longitude",
                Width = 90
            });

            dgvBoardingHouses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStatus",
                HeaderText = "Status",
                DataPropertyName = "status",
                Width = 90
            });
        }


        private void BoardingHousesView_Load(object sender, EventArgs e)
        {
            SetupBoardingHouseGrid();
            details_txtTotalRooms.ReadOnly = true;
            details_txtAvailableRooms.ReadOnly = true;

            details_txtTotalRooms.TabStop = false;
            details_txtAvailableRooms.TabStop = false;

            details_txtTotalRooms.BackColor = SystemColors.Control;
            details_txtAvailableRooms.BackColor = SystemColors.Control;

            SetupRoomsGrid();
            LoadBoardingHouses();
            LoadBoardingHouseStats();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void addNewBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            CenterModal(AddModal);
            AddModal.Visible = true;
        }

        private void addNewCloseBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            AddModal.Visible = false;
        }

        private void detailsClosebtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            detailsModal.Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            try
            {
                // basic required validation
                string name = txtBHName.Text.Trim();
                string address = txtAddress.Text.Trim();

                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("BH Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBHName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(address))
                {
                    MessageBox.Show("Address is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAddress.Focus();
                    return;
                }

                // Optional fields
                string ownerName = txtOwnerName.Text.Trim();
                string contactNo = txtContactNo.Text.Trim();
                string thumbnailPath = txtThumbnailPath.Text.Trim();

                // Lat/Lng (nullable decimals)
                decimal? latitude = TryParseNullableDecimal(txtLatitude.Text);
                decimal? longitude = TryParseNullableDecimal(txtLongitude.Text);

                // Insert
                using (var conn = DbConnectionFactory.CreateConnection())
                {
                    EnsureOpen(conn);
                    ulong? ownerId = ResolveOwnerId(conn, ownerName, contactNo);

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                    INSERT INTO boarding_houses
                    (name, address, owner_id, thumbnail_path, latitude, longitude)
                    VALUES
                    (@name, @address, @owner_id, @thumbnail_path, @latitude, @longitude);
                    ";

                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@address", address);
                        cmd.Parameters.AddWithValue("@owner_id", ownerId.HasValue ? (object)ownerId.Value : DBNull.Value);

                        // thumbnail_path: store the relative path you decided (e.g., "Thumbnails\\bh_1.jpg")
                        cmd.Parameters.AddWithValue("@thumbnail_path", string.IsNullOrWhiteSpace(thumbnailPath) ? DBNull.Value : thumbnailPath);

                        cmd.Parameters.AddWithValue("@latitude", latitude.HasValue ? latitude.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@longitude", longitude.HasValue ? longitude.Value : DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Boarding House saved successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBoardingHouses();
                LoadBoardingHouseStats();


                AddModal.Visible = false;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Database error:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private static int? TryParseNullableInt(string text)
        {
            text = (text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(text)) return null;

            if (int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out int v))
                return v;

            return null;
        }

        private static decimal? TryParseNullableDecimal(string text)
        {
            text = (text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(text)) return null;

            // invariant culture prevents issues with comma/decimal separators
            if (decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal v))
                return v;

            return null;
        }

        private void addBrowseBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = "Select Boarding House Thumbnail";
                ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Multiselect = false;

                if (ofd.ShowDialog() != DialogResult.OK)
                    return;

                // Ensure thumbnails folder exists
                string thumbsDir = Path.Combine(Application.StartupPath, "Thumbnails");
                Directory.CreateDirectory(thumbsDir);

                // Build a unique filename (no DB id yet since this is "create")
                string ext = Path.GetExtension(ofd.FileName);
                string fileName = $"bh_{DateTime.Now:yyyyMMdd_HHmmss}{ext}";
                string destPath = Path.Combine(thumbsDir, fileName);

                // Copy file (overwrite false by default)
                File.Copy(ofd.FileName, destPath, overwrite: true);

                // Store RELATIVE path in hidden/readonly textbox (for DB insert)
                txtThumbnailPath.Text = Path.Combine("Thumbnails", fileName);

                picThumbnail.Image?.Dispose();
                picThumbnail.Image = Image.FromFile(destPath);
            }
        }

        private int _selectedBoardingHouseId = 0;
        private void dgvBoardingHouses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SoundClicked.operationsBtn();
            if (e.RowIndex < 0) return; // header click

            var row = dgvBoardingHouses.Rows[e.RowIndex];

            _selectedBoardingHouseId = Convert.ToInt32(row.Cells["colId"].Value);

            details_txtBHName.Text = row.Cells["colName"].Value?.ToString() ?? "";
            details_txtOwnerName.Text = row.Cells["colOwner"].Value?.ToString() ?? "";
            details_txtContactNo.Text = row.Cells["colContact"].Value?.ToString() ?? "";
            details_txtTotalRooms.Text = row.Cells["colTotal"].Value?.ToString() ?? "";
            details_txtAvailableRooms.Text = row.Cells["colAvailable"].Value?.ToString() ?? "";
            details_txtLatitude.Text = row.Cells["colLat"].Value?.ToString() ?? "";
            details_txtLongitude.Text = row.Cells["colLng"].Value?.ToString() ?? "";
            SetupStatusCombo();
            string status = row.Cells["colStatus"].Value?.ToString()?.Trim();

            if (string.IsNullOrWhiteSpace(status))
                status = "ACTIVE";

            details_cbstatus.SelectedIndex = -1;

            int idx = details_cbstatus.FindStringExact(status);
            if (idx >= 0)
                details_cbstatus.SelectedIndex = idx;
            else
                details_cbstatus.SelectedIndex = details_cbstatus.FindStringExact("ACTIVE");

            LoadBoardingHouseDetailsFromDb(_selectedBoardingHouseId);
            LoadSelectedBhRoomCounts(_selectedBoardingHouseId);


            // Show modal
            CenterModal(detailsModal);
            detailsModal.Visible = true;
            detailsModal.BringToFront();
        }

        private void SetupStatusCombo()
        {
            details_cbstatus.Items.Clear();
            details_cbstatus.Items.Add("ACTIVE");
            details_cbstatus.Items.Add("INACTIVE");
            details_cbstatus.DropDownStyle = ComboBoxStyle.DropDownList;

        }


        private void LoadBoardingHouseDetailsFromDb(int id)
        {
            using (var conn = DbConnectionFactory.CreateConnection())
            using (var cmd = conn.CreateCommand())
            {
                EnsureOpen(conn);

                cmd.CommandText = @"
                                    SELECT 
                                        bh.address,
                                        bh.thumbnail_path,
                                        COALESCE(o.full_name, '') AS owner_name,
                                        COALESCE(o.contact_no, '') AS contact_no
                                    FROM boarding_houses bh
                                    LEFT JOIN owners o ON o.id = bh.owner_id
                                    WHERE bh.id = @id
                                    LIMIT 1;
                                    ";
                cmd.Parameters.AddWithValue("@id", id);

                using (var r = cmd.ExecuteReader())
                {
                    if (!r.Read()) return;

                    details_txtAddress.Text = r["address"] == DBNull.Value ? "" : r["address"].ToString();
                    details_txtThumbnailPath.Text = r["thumbnail_path"] == DBNull.Value ? "" : r["thumbnail_path"].ToString();
                    details_txtOwnerName.Text = r["owner_name"] == DBNull.Value ? "" : r["owner_name"].ToString();
                    details_txtContactNo.Text = r["contact_no"] == DBNull.Value ? "" : r["contact_no"].ToString();
                }
            }

            // Optional: preview image if you have PictureBox in details modal
            TryLoadThumbnailPreview(details_txtThumbnailPath.Text);
        }
        private void TryLoadThumbnailPreview(string relativePath)
        {
            if (details_picThumbnail == null) return;

            // clear previous
            if (details_picThumbnail.Image != null)
            {
                var old = details_picThumbnail.Image;
                details_picThumbnail.Image = null;
                old.Dispose();
            }

            if (string.IsNullOrWhiteSpace(relativePath)) return;

            string fullPath = Path.Combine(Application.StartupPath, relativePath);
            if (!File.Exists(fullPath)) return;

            // avoid locking the file
            using (var fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
            {
                details_picThumbnail.Image = Image.FromStream(fs);
            }

            details_picThumbnail.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            if (_selectedBoardingHouseId <= 0)
            {
                MessageBox.Show("Please select a boarding house first.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string name = details_txtBHName.Text.Trim();
                string address = details_txtAddress.Text.Trim();

                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("BH Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    details_txtBHName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(address))
                {
                    MessageBox.Show("Address is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    details_txtAddress.Focus();
                    return;
                }

                string ownerName = details_txtOwnerName.Text.Trim();
                string contactNo = details_txtContactNo.Text.Trim();
                string thumbnailPath = details_txtThumbnailPath.Text.Trim();

                decimal? latitude = TryParseNullableDecimal(details_txtLatitude.Text);
                decimal? longitude = TryParseNullableDecimal(details_txtLongitude.Text);

                string status = details_cbstatus.SelectedItem?.ToString();
                if (string.IsNullOrWhiteSpace(status))
                    status = "ACTIVE";

                using (var conn = DbConnectionFactory.CreateConnection())
                {
                    EnsureOpen(conn);

                    ulong? ownerId = ResolveOwnerId(conn, ownerName, contactNo);

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                    UPDATE boarding_houses
                    SET
                        name = @name,
                        address = @address,
                        owner_id = @owner_id,
                        status = @status,
                        thumbnail_path = @thumbnail_path,
                        latitude = @latitude,
                        longitude = @longitude
                    WHERE id = @id
                    LIMIT 1;
                ";

                        cmd.Parameters.AddWithValue("@id", _selectedBoardingHouseId);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@address", address);

                        cmd.Parameters.AddWithValue("@owner_id", ownerId.HasValue ? (object)ownerId.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@status", status);

                        cmd.Parameters.AddWithValue("@thumbnail_path", string.IsNullOrWhiteSpace(thumbnailPath) ? (object)DBNull.Value : thumbnailPath);

                        cmd.Parameters.AddWithValue("@latitude", latitude.HasValue ? (object)latitude.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@longitude", longitude.HasValue ? (object)longitude.Value : DBNull.Value);

                        int affected = cmd.ExecuteNonQuery();

                        if (affected <= 0)
                        {
                            MessageBox.Show("No changes were saved. The record may have been removed.", "Info",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }

                MessageBox.Show("Boarding House updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadBoardingHouses();
                LoadBoardingHouseStats();
                detailsModal.Visible = false;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Database error:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            if (_selectedBoardingHouseId <= 0)
            {
                MessageBox.Show("Please select a boarding house to delete.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                "Are you sure you want to delete this boarding house?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = conn.CreateCommand())
                {
                    EnsureOpen(conn);

                    cmd.CommandText = @"
                        DELETE FROM boarding_houses
                        WHERE id = @id
                        LIMIT 1;
                    ";
                    cmd.Parameters.AddWithValue("@id", _selectedBoardingHouseId);

                    int affected = cmd.ExecuteNonQuery();
                    if (affected <= 0)
                    {
                        MessageBox.Show("Record not found or already deleted.", "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                MessageBox.Show("Boarding house deleted successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                _selectedBoardingHouseId = 0;
                LoadBoardingHouses();
                LoadBoardingHouseStats();
                detailsModal.Visible = false;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Database error:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void EnsureOpen(MySqlConnection conn)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
        }

        private static ulong? ResolveOwnerId(MySqlConnection conn, string ownerName, string contactNo)
        {
            if (string.IsNullOrWhiteSpace(ownerName))
                return null;

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    SELECT id
                    FROM owners
                    WHERE full_name = @full_name
                      AND (
                        (@contact IS NULL AND contact_no IS NULL)
                        OR contact_no = @contact
                      )
                    LIMIT 1;
                ";

                cmd.Parameters.AddWithValue("@full_name", ownerName);
                cmd.Parameters.AddWithValue("@contact", string.IsNullOrWhiteSpace(contactNo) ? DBNull.Value : (object)contactNo);

                var existing = cmd.ExecuteScalar();
                if (existing != null && existing != DBNull.Value)
                    return Convert.ToUInt64(existing);

                cmd.Parameters.Clear();
                cmd.CommandText = @"
                    INSERT INTO owners (full_name, contact_no, status)
                    VALUES (@full_name, @contact_no, 'ACTIVE');
                ";
                cmd.Parameters.AddWithValue("@full_name", ownerName);
                cmd.Parameters.AddWithValue("@contact_no", string.IsNullOrWhiteSpace(contactNo) ? DBNull.Value : (object)contactNo);
                cmd.ExecuteNonQuery();

                return (ulong)cmd.LastInsertedId;
            }
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {

        }

        private void searchbtn_Click_1(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            try
            {
                string keyword = (textBox1.Text ?? "").Trim();

                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = conn.CreateCommand())
                {
                    EnsureOpen(conn);

                    cmd.CommandText = @"
                                    SELECT 
                                        bh.id,
                                        bh.name,
                                        bh.address,
                                        COALESCE(o.full_name, '') AS owner_name,
                                        COALESCE(o.contact_no, '') AS contact_no,
                                        bh.status,
                                        bh.total_rooms,
                                        bh.available_rooms,
                                        bh.latitude,
                                        bh.longitude
                                    FROM boarding_houses bh
                                    LEFT JOIN owners o ON o.id = bh.owner_id
                                    WHERE
                                        bh.name LIKE @kw
                                        OR bh.address LIKE @kw
                                        OR o.full_name LIKE @kw
                                        OR o.contact_no LIKE @kw
                                    ORDER BY bh.id DESC;
                                ";

                    cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                    var table = new DataTable();
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }

                    dgvBoardingHouses.DataSource = table;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Database error:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void viewMapBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            try
            {
                // collect markers (unchanged)
                var markers = new List<object>();

                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = conn.CreateCommand())
                {
                    EnsureOpen(conn);

                    cmd.CommandText = @"
                                        SELECT 
                                            bh.id,
                                            bh.name,
                                            bh.address,
                                            COALESCE(o.contact_no, '') AS contact_no,
                                            bh.latitude,
                                            bh.longitude,
                                            bh.status
                                        FROM boarding_houses bh
                                        LEFT JOIN owners o ON o.id = bh.owner_id
                                        WHERE bh.latitude IS NOT NULL
                                          AND bh.longitude IS NOT NULL
                                        ORDER BY bh.id DESC;
                                    ";

                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            if (r["latitude"] == DBNull.Value || r["longitude"] == DBNull.Value)
                                continue;

                            if (!double.TryParse(r["latitude"].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double lat))
                                continue;
                            if (!double.TryParse(r["longitude"].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double lng))
                                continue;

                            markers.Add(new
                            {
                                id = Convert.ToInt32(r["id"]),
                                name = r["name"]?.ToString() ?? "",
                                address = r["address"]?.ToString() ?? "",
                                contactNo = r["contact_no"]?.ToString() ?? "",
                                status = r["status"]?.ToString() ?? "",
                                latitude = lat,
                                longitude = lng
                            });

                        }
                    }
                }

                if (markers.Count == 0)
                {
                    MessageBox.Show("No boarding houses with valid coordinates.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _pendingMarkersJson = System.Text.Json.JsonSerializer.Serialize(markers);

                if (!await PrepareMapAsync())
                    return;
                await Task.Delay(150);
                await mapWebView.ExecuteScriptAsync("window.invalidateMap && window.invalidateMap();");
                CenterModal(mapModal);
                mapModal.Visible = true;
                mapModal.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load map.\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<bool> PrepareMapAsync()
        {
            string mapDir = Path.Combine(Application.StartupPath, "Map");
            string htmlPath = Path.Combine(mapDir, "index.html");

            if (!File.Exists(htmlPath))
            {
                MessageBox.Show($"Map file not found: {htmlPath}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            await mapWebView.EnsureCoreWebView2Async();
            var core = mapWebView.CoreWebView2;

            // IMPORTANT: mapping MUST be set before navigation (every time)
            core.SetVirtualHostNameToFolderMapping(
                "appassets",
                mapDir,
                CoreWebView2HostResourceAccessKind.Allow
            );

            core.Settings.AreDevToolsEnabled = true;

            if (!_mapEventsWired)
            {
                _mapEventsWired = true;

                core.WebMessageReceived += (s, e) =>
                {
                    var msg = e.TryGetWebMessageAsString();
                    Debug.WriteLine("[MapConsole] " + msg);
                };

                await core.AddScriptToExecuteOnDocumentCreatedAsync(@"
                (function () {
                    if (!window.chrome || !window.chrome.webview) return;

                    function stringify(a) {
                    try { return (typeof a === 'object') ? JSON.stringify(a) : String(a); }
                    catch(e) { return '[object]'; }
                    }

                    function send(level, args) {
                    try {
                        const text = Array.from(args).map(stringify).join(' ');
                        window.chrome.webview.postMessage(level + ': ' + text);
                    } catch(e) {}
                    }

                    const _log = console.log, _warn = console.warn, _err = console.error;
                    console.log  = function(){ send('log',  arguments); _log.apply(console, arguments); };
                    console.warn = function(){ send('warn', arguments); _warn.apply(console, arguments); };
                    console.error= function(){ send('error',arguments); _err.apply(console, arguments); };
                })();
                ");

                core.NavigationCompleted += async (s, e) =>
                {
                    if (!e.IsSuccess) return;
                    if (string.IsNullOrWhiteSpace(_pendingMarkersJson)) return;

                    string json = _pendingMarkersJson;
                    _pendingMarkersJson = null;

                    await mapWebView.ExecuteScriptAsync($"window.renderBoardingHouses({json});");
                    await mapWebView.ExecuteScriptAsync("window.invalidateMap && window.invalidateMap();");
                };
            }

            // Always navigate via the virtual host (no file://)
            core.Navigate("https://appassets/index.html");
            return true;
        }



        private void closemapBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            mapModal.Visible = false;
        }

        private void mapModal_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            AddModal.Visible = false;
        }

        private async void locateBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            try
            {
                if (!await PrepareLocatorMapAsync())
                    return;

                CenterModal(mapLocatorModal);
                mapLocatorModal.Visible = true;
                mapLocatorModal.BringToFront();

                AddModal.Visible = false;

                // Fix size after showing modal
                await Task.Delay(150);
                await mapLocatorWebView.ExecuteScriptAsync("window.invalidateMap && window.invalidateMap();");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open locator map.\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            mapLocatorModal.Visible = false;
            CenterModal(AddModal);
            AddModal.Visible = true;
        }

        private async Task<bool> PrepareLocatorMapAsync()
        {
            string mapDir = Path.Combine(Application.StartupPath, "Map");
            string htmlPath = Path.Combine(mapDir, "locator.html");

            if (!File.Exists(htmlPath))
            {
                MessageBox.Show($"Locator file not found: {htmlPath}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            await mapLocatorWebView.EnsureCoreWebView2Async();
            var core = mapLocatorWebView.CoreWebView2;
            core.Settings.AreDevToolsEnabled = true;

            if (!_locatorEventsWired)
            {
                _locatorEventsWired = true;

                core.WebMessageReceived += (s, e) =>
                {
                    try
                    {
                        var raw = e.TryGetWebMessageAsString();
                        if (string.IsNullOrWhiteSpace(raw)) return;

                        // raw is JSON string: {"latitude":..,"longitude":..,"address":".."}
                        using var doc = System.Text.Json.JsonDocument.Parse(raw);

                        double lat = 0;
                        double lng = 0;
                        string address = "Unknown address";

                        if (doc.RootElement.TryGetProperty("latitude", out var latEl) && latEl.ValueKind == System.Text.Json.JsonValueKind.Number)
                            lat = latEl.GetDouble();

                        if (doc.RootElement.TryGetProperty("longitude", out var lngEl) && lngEl.ValueKind == System.Text.Json.JsonValueKind.Number)
                            lng = lngEl.GetDouble();

                        if (doc.RootElement.TryGetProperty("address", out var addrEl) && addrEl.ValueKind == System.Text.Json.JsonValueKind.String)
                            address = addrEl.GetString() ?? "Unknown address";

                        void Apply()
                        {
                            locateLatTxt.Text = lat.ToString("0.000000", CultureInfo.InvariantCulture);
                            locateLongTxt.Text = lng.ToString("0.000000", CultureInfo.InvariantCulture);
                            locateAddressTxt.Text = string.IsNullOrWhiteSpace(address) ? "Unknown address" : address;
                        }

                        if (InvokeRequired) BeginInvoke((Action)Apply);
                        else Apply();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("[LocatorMap] WebMessage parse failed: " + ex.Message);
                    }
                };
            }

            // ✅ IMPORTANT: map local folder to a fake HTTPS host
            core.SetVirtualHostNameToFolderMapping(
                "appassets",
                mapDir, // MUST be Map folder
                CoreWebView2HostResourceAccessKind.Allow
            );

            core.Navigate("https://appassets/locator.html");
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            txtAddress.Text = locateAddressTxt.Text;
            txtLongitude.Text = locateLongTxt.Text;
            txtLatitude.Text = locateLatTxt.Text;

            mapLocatorModal.Visible = false;
            CenterModal(AddModal);
            AddModal.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            mapSingleModal.Visible = false;
            CenterModal(detailsModal);
            detailsModal.Visible = true;
        }

        private void btnViewMap_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            CenterModal(mapSingleModal);
            mapSingleModal.Visible = true;
        }

        private void btnViewMap_Click_1(object sender, EventArgs e)
        {

        }

        private async void btnViewMap_Click_2(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();

            if (_selectedBoardingHouseId <= 0)
            {
                MessageBox.Show("Please select a boarding house first.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = conn.CreateCommand())
                {
                    EnsureOpen(conn);

                    cmd.CommandText = @"
                SELECT id, name, address, latitude, longitude, status
                FROM boarding_houses
                WHERE id = @id
                LIMIT 1;
            ";
                    cmd.Parameters.AddWithValue("@id", _selectedBoardingHouseId);

                    using (var r = cmd.ExecuteReader())
                    {
                        if (!r.Read())
                        {
                            MessageBox.Show("Boarding house not found.", "Info",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        if (r["latitude"] == DBNull.Value || r["longitude"] == DBNull.Value)
                        {
                            MessageBox.Show("This boarding house has no location set.", "Info",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        double lat = Convert.ToDouble(r["latitude"], CultureInfo.InvariantCulture);
                        double lng = Convert.ToDouble(r["longitude"], CultureInfo.InvariantCulture);

                        var marker = new
                        {
                            id = Convert.ToInt32(r["id"]),
                            name = r["name"]?.ToString() ?? "",
                            address = r["address"]?.ToString() ?? "",
                            status = r["status"]?.ToString() ?? "",
                            latitude = lat,
                            longitude = lng
                        };

                        _pendingSingleMarkerJson =
                            System.Text.Json.JsonSerializer.Serialize(marker);
                    }
                }

                if (!await PrepareSingleMapAsync())
                    return;

                await Task.Delay(150);
                await mapSingleWebView.ExecuteScriptAsync(
                    "window.invalidateMap && window.invalidateMap();"
                );

                CenterModal(mapSingleModal);
                mapSingleModal.Visible = true;
                mapSingleModal.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open map.\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<bool> PrepareSingleMapAsync()
        {
            string mapDir = Path.Combine(Application.StartupPath, "Map");
            string htmlPath = Path.Combine(mapDir, "single.html");

            if (!File.Exists(htmlPath))
            {
                MessageBox.Show($"Map file not found: {htmlPath}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            await mapSingleWebView.EnsureCoreWebView2Async();
            var core = mapSingleWebView.CoreWebView2;

            core.SetVirtualHostNameToFolderMapping(
                "appassets",
                mapDir,
                CoreWebView2HostResourceAccessKind.Allow
            );

            core.Settings.AreDevToolsEnabled = true;

            if (!_singleMapEventsWired)
            {
                _singleMapEventsWired = true;

                core.NavigationCompleted += async (s, e) =>
                {
                    if (!e.IsSuccess) return;
                    if (string.IsNullOrWhiteSpace(_pendingSingleMarkerJson)) return;

                    string json = _pendingSingleMarkerJson;
                    _pendingSingleMarkerJson = null;

                    await mapSingleWebView.ExecuteScriptAsync(
                        $"window.renderBoardingHouse({json});"
                    );
                    await mapSingleWebView.ExecuteScriptAsync(
                        "window.invalidateMap && window.invalidateMap();"
                    );
                };
            }

            core.Navigate("https://appassets/single.html");
            return true;
        }

        private void editRoomBtn_Click(object sender, EventArgs e)
        {

        }

        private int _roomsBhId = 0;
        private string _roomsBhName = "";

        private void button4_Click(object sender, EventArgs e)
        {
            if (_selectedBoardingHouseId <= 0)
            {
                MessageBox.Show("Please select a boarding house first.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _roomsBhId = _selectedBoardingHouseId;
            _roomsBhName = details_txtBHName.Text.Trim();

            lblRoomsFor.Text = $"Rooms for: {_roomsBhName}";
            LoadRooms(_roomsBhId);

            CenterModal(manageRoomsModal);
            manageRoomsModal.Visible = true;
            manageRoomsModal.BringToFront();
        }

        private void LoadRooms(int bhId)
        {
            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = conn.CreateCommand())
                {
                    EnsureOpen(conn);

                    cmd.CommandText = @"
                SELECT id, room_no, room_type, monthly_rate, capacity, status
                FROM rooms
                WHERE boarding_house_id = @bhId
                ORDER BY room_no ASC;
            ";
                    cmd.Parameters.AddWithValue("@bhId", bhId);

                    var table = new DataTable();
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }

                    dgvRooms.SuspendLayout();
                    try
                    {
                        dgvRooms.DataSource = table;
                    }
                    finally
                    {
                        dgvRooms.ResumeLayout();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load rooms.\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupRoomsGrid()
        {
            if (_roomsGridSetupDone) return;
            dgvRooms.AutoGenerateColumns = false;
            dgvRooms.AllowUserToAddRows = false;
            dgvRooms.AllowUserToDeleteRows = false;
            dgvRooms.ReadOnly = true;
            dgvRooms.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRooms.MultiSelect = false;
            dgvRooms.RowHeadersVisible = false;

            dgvRooms.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRooms.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            dgvRooms.Columns.Clear();

            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colRoomId",
                HeaderText = "ID",
                DataPropertyName = "id",
                Width = 60
            });

            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colRoomNo",
                HeaderText = "Room No",
                DataPropertyName = "room_no",
                Width = 120
            });

            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colRoomType",
                HeaderText = "Type",
                DataPropertyName = "room_type",
                Width = 160
            });

            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colRate",
                HeaderText = "Monthly Rate",
                DataPropertyName = "monthly_rate",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" }
            });

            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCapacity",
                HeaderText = "Capacity",
                DataPropertyName = "capacity",
                Width = 90
            });

            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colRoomStatus",
                HeaderText = "Status",
                DataPropertyName = "status",
                Width = 120
            });

            _roomsGridSetupDone = true;
        }

        private void LoadSelectedBhRoomCounts(int bhId)
        {
            if (bhId <= 0)
            {
                details_txtTotalRooms.Text = "0";
                details_txtAvailableRooms.Text = "0";
                return;
            }

            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = conn.CreateCommand())
                {
                    EnsureOpen(conn);

                    cmd.CommandText = @"
                SELECT
                    COUNT(*) AS TotalRooms,
                    SUM(CASE WHEN status = 'AVAILABLE' THEN 1 ELSE 0 END) AS AvailableRooms
                FROM rooms
                WHERE boarding_house_id = @bhId;
            ";
                    cmd.Parameters.AddWithValue("@bhId", bhId);

                    using (var r = cmd.ExecuteReader())
                    {
                        if (!r.Read())
                        {
                            details_txtTotalRooms.Text = "0";
                            details_txtAvailableRooms.Text = "0";
                            return;
                        }

                        int total = r["TotalRooms"] == DBNull.Value ? 0 : Convert.ToInt32(r["TotalRooms"]);
                        int avail = r["AvailableRooms"] == DBNull.Value ? 0 : Convert.ToInt32(r["AvailableRooms"]);

                        details_txtTotalRooms.Text = total.ToString();
                        details_txtAvailableRooms.Text = avail.ToString();
                    }
                }
            }
            catch
            {
                // fail safe
                details_txtTotalRooms.Text = "0";
                details_txtAvailableRooms.Text = "0";
            }
        }

        private void closeManageRoom_Click(object sender, EventArgs e)
        {
            manageRoomsModal.Visible = false;
        }

        private int _selectedRoomId = 0;
        private void dgvRooms_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvRooms.Rows[e.RowIndex];
            _selectedRoomId = row.Cells["colRoomId"].Value == null ? 0 : Convert.ToInt32(row.Cells["colRoomId"].Value);

            panel9.Visible = true;
        }

        private void UpdateSelectedRoomStatus(string newStatus)
        {
            if (_roomsBhId <= 0)
            {
                MessageBox.Show("No boarding house context set.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_selectedRoomId <= 0)
            {
                MessageBox.Show("Please select a room first.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        SET status = @status
                        WHERE id = @roomId
                          AND boarding_house_id = @bhId
                        LIMIT 1;
                    ";

                    cmd.Parameters.AddWithValue("@status", newStatus);
                    cmd.Parameters.AddWithValue("@roomId", _selectedRoomId);
                    cmd.Parameters.AddWithValue("@bhId", _roomsBhId);

                    int affected = cmd.ExecuteNonQuery();
                    if (affected <= 0)
                    {
                        MessageBox.Show("Room not found or already updated.", "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                LoadRooms(_roomsBhId);
                LoadSelectedBhRoomCounts(_roomsBhId);

                LoadBoardingHouses();

                _selectedRoomId = 0;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Database error:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMarkAvailable_Click(object sender, EventArgs e)
        {
            UpdateSelectedRoomStatus("AVAILABLE");
            panel9.Visible = false;
            SoundClicked.operationsBtn();
            MessageBox.Show("Status Updated");
        }

        private void btnMarkOccupied_Click(object sender, EventArgs e)
        {
            UpdateSelectedRoomStatus("OCCUPIED");
            panel9.Visible = false;
            SoundClicked.operationsBtn();
            MessageBox.Show("Status Updated");
        }

        private void btnMarkMaintenance_Click(object sender, EventArgs e)
        {
            UpdateSelectedRoomStatus("MAINTENANCE");
            panel9.Visible = false;
            SoundClicked.operationsBtn();
            MessageBox.Show("Status Updated");
        }

        private void btnMarkInactive_Click(object sender, EventArgs e)
        {
            UpdateSelectedRoomStatus("INACTIVE");
            panel9.Visible = false;
            SoundClicked.operationsBtn();
            MessageBox.Show("Status Updated");
        }

        private void manageRoomsModal_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mapSingleModal_Paint(object sender, PaintEventArgs e)
        {

        }

        private void editBrowseBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = "Select Boarding House Thumbnail";
                ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Multiselect = false;

                if (ofd.ShowDialog() != DialogResult.OK)
                    return;
                string thumbsDir = Path.Combine(Application.StartupPath, "Thumbnails");
                Directory.CreateDirectory(thumbsDir);

                string ext = Path.GetExtension(ofd.FileName);
                string fileName = $"bh_{DateTime.Now:yyyyMMdd_HHmmss}{ext}";
                string destPath = Path.Combine(thumbsDir, fileName);

                File.Copy(ofd.FileName, destPath, overwrite: true);

                details_txtThumbnailPath.Text = Path.Combine("Thumbnails", fileName);

                details_picThumbnail.Image?.Dispose();
                details_picThumbnail.Image = Image.FromFile(destPath);
            }
        }
    }
}

