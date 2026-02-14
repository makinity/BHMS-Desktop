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
using Mysqlx.Expr;


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

        private void HideModals()
        {
            detailsModal.Visible = false;
            AddModal.Visible = false;
        }

        private void ShowDetailsModal()
        {
            AddModal.Visible = false;
            detailsModal.Visible = true;
            detailsModal.BringToFront();
        }

        private void ShowAddModal()
        {
            detailsModal.Visible = false;
            AddModal.Visible = true;
            AddModal.BringToFront();
        }

        public void OpenDetailsById(int bhId)
        {
            if (bhId <= 0) return;

            if (dgvBoardingHouses.DataSource == null || dgvBoardingHouses.Rows.Count == 0)
                LoadBoardingHouses();

            DataGridViewRow found = null;

            foreach (DataGridViewRow row in dgvBoardingHouses.Rows)
            {
                if (row.Cells["colId"].Value == null) continue;

                if (int.TryParse(row.Cells["colId"].Value.ToString(), out int id) && id == bhId)
                {
                    found = row;
                    break;
                }
            }

            if (found == null)
            {
                MessageBox.Show("Boarding house not found in list.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            dgvBoardingHouses.ClearSelection();
            found.Selected = true;
            dgvBoardingHouses.CurrentCell = found.Cells["colName"];
            dgvBoardingHouses.FirstDisplayedScrollingRowIndex = Math.Max(found.Index, 0);

            dgvBoardingHouses_CellClick(
                dgvBoardingHouses,
                new DataGridViewCellEventArgs(found.Cells["colId"].ColumnIndex, found.Index)
            );
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

                    cmd.CommandText = $@"
                                        SELECT 
                                            bh.id,
                                            bh.name,
                                            bh.address,
                                            {OwnerDisplaySql("o")} AS owner_name,
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
                Width = 160,
            });

            dgvBoardingHouses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colContact",
                HeaderText = "Contact",
                DataPropertyName = "contact_no",
                Width = 130,
                Visible = false
            });

            dgvBoardingHouses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTotal",
                HeaderText = "Total",
                DataPropertyName = "total_rooms",
                Width = 70,
                Visible = false
            });

            dgvBoardingHouses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAvailable",
                HeaderText = "Available",
                DataPropertyName = "available_rooms",
                Width = 80,
                Visible = false
            });

            dgvBoardingHouses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colLat",
                HeaderText = "Lat",
                DataPropertyName = "latitude",
                Visible = false,
                Width = 90
            });

            dgvBoardingHouses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colLng",
                HeaderText = "Lng",
                DataPropertyName = "longitude",
                Width = 90,
                Visible = false
            });

            dgvBoardingHouses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStatus",
                HeaderText = "Status",
                DataPropertyName = "status",
                Width = 90,
                Visible = false
            });
        }


        private async void BoardingHousesView_Load(object sender, EventArgs e)
        {
            SetupBoardingHouseGrid();
            details_txtTotalRooms.ReadOnly = true;
            details_txtAvailableRooms.ReadOnly = true;

            details_txtTotalRooms.TabStop = false;
            details_txtAvailableRooms.TabStop = false;

            details_txtTotalRooms.BackColor = SystemColors.Control;
            details_txtAvailableRooms.BackColor = SystemColors.Control;

            LoadOwnersForCombo(cbOwner);
            LoadOwnersForCombo(details_cbOwner);
            LoadBoardingHouses();
            LoadBoardingHouseStats();
            await InitializeSingleMapAsync();
            InitializeRoomsHostPanelUI();
            RefreshRoomsHostPanel();
        }

        private async Task InitializeSingleMapAsync()
        {
            try
            {
                await PrepareSingleMapAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[SingleMap] Init failed: " + ex.Message);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void addNewBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            LoadOwnersForCombo(cbOwner);
            txtContactNo.Text = "";
            _selectedBoardingHouseId = 0;           
            _pendingSingleMarkerJson = null;     
            await ResetSingleMapAsync();
            ShowAddModal();
        }

        private async Task ResetSingleMapAsync()
        {
            if (mapSingleWebView?.CoreWebView2 == null)
                return;

            try
            {
                await mapSingleWebView.ExecuteScriptAsync(
                    "window.resetSingleMap && window.resetSingleMap();"
                );
            }
            catch
            {

            }
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
                object ownerIdValue = cbOwner.SelectedValue ?? DBNull.Value;
                long? ownerIdForDetails = NormalizeOwnerId(ownerIdValue);
                string contactNo = txtContactNo.Text.Trim();
                string thumbnailPath = txtThumbnailPath.Text.Trim();

                // Lat/Lng (nullable decimals)
                decimal? latitude = TryParseNullableDecimal(txtLatitude.Text);
                decimal? longitude = TryParseNullableDecimal(txtLongitude.Text);

                var auditDetails = BuildBoardingHouseAuditDetails(
                    name, address, ownerIdForDetails, contactNo, thumbnailPath, latitude, longitude, "ACTIVE");

                // Insert
                using (var conn = DbConnectionFactory.CreateConnection())
                {
                    EnsureOpen(conn);

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
                        cmd.Parameters.AddWithValue("@owner_id", ownerIdValue);

                        // thumbnail_path: store the relative path you decided (e.g., "Thumbnails\\bh_1.jpg")
                        cmd.Parameters.AddWithValue("@thumbnail_path", string.IsNullOrWhiteSpace(thumbnailPath) ? DBNull.Value : thumbnailPath);

                        cmd.Parameters.AddWithValue("@latitude", latitude.HasValue ? latitude.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@longitude", longitude.HasValue ? longitude.Value : DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }

                    using (var idCmd = conn.CreateCommand())
                    {
                        idCmd.CommandText = "SELECT LAST_INSERT_ID();";
                        long newId = Convert.ToInt64(idCmd.ExecuteScalar());
                        AuditLogger.Log(GetCurrentUserId(), "CREATE", "boarding_houses", newId, auditDetails);
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
        private int _selectedBhId = 0;
        private void dgvBoardingHouses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SoundClicked.operationsBtn();
            btnDelete.Visible = true;
            btnUpdate.Visible = true;
            viewRoomsBtn.Visible = true;
            editBrowseBtn.Visible = true;

            if (e.RowIndex < 0) return;

            var row = dgvBoardingHouses.Rows[e.RowIndex];

            _selectedBoardingHouseId = Convert.ToInt32(row.Cells["colId"].Value);
            _selectedBhId = _selectedBoardingHouseId;

            details_txtBHName.Text = row.Cells["colName"].Value?.ToString() ?? "";
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
            LoadBhStats();
            RefreshRoomsHostPanel();

            ShowDetailsModal();
            viewSingleMap();
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

                cmd.CommandText = $@"
                                    SELECT 
                                        bh.address,
                                        bh.thumbnail_path,
                                        bh.owner_id,
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
                    details_txtContactNo.Text = r["contact_no"] == DBNull.Value ? "" : r["contact_no"].ToString();
                    ulong? ownerId = r["owner_id"] == DBNull.Value ? null : Convert.ToUInt64(r["owner_id"]);
                    LoadOwnersForCombo(details_cbOwner, ownerId);
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

                object ownerIdValue = details_cbOwner.SelectedValue ?? DBNull.Value;
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

                    object beforeDetails;
                    using (var beforeCmd = conn.CreateCommand())
                    {
                        beforeCmd.CommandText = @"
                    SELECT bh.name, bh.address, bh.owner_id,
                           COALESCE(o.contact_no, '') AS contact_no,
                           bh.status, bh.thumbnail_path, bh.latitude, bh.longitude
                    FROM boarding_houses bh
                    LEFT JOIN owners o ON o.id = bh.owner_id
                    WHERE bh.id = @id
                    LIMIT 1;
                ";
                        beforeCmd.Parameters.AddWithValue("@id", _selectedBoardingHouseId);

                        using (var reader = beforeCmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show("Boarding house not found.", "Info",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            beforeDetails = ReadBoardingHouseAuditDetails(reader);
                        }
                    }

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

                        cmd.Parameters.AddWithValue("@owner_id", ownerIdValue);
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

                    var ownerIdForDetails = NormalizeOwnerId(ownerIdValue);
                    var afterDetails = BuildBoardingHouseAuditDetails(
                        name, address, ownerIdForDetails, contactNo, thumbnailPath, latitude, longitude, status);
                    AuditLogger.Log(GetCurrentUserId(), "UPDATE", "boarding_houses", _selectedBoardingHouseId, new
                    {
                        before = beforeDetails,
                        after = afterDetails
                    });
                }

                MessageBox.Show("Boarding House updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadBoardingHouses();
                LoadBoardingHouseStats();
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
                {
                    EnsureOpen(conn);

                    object deletedDetails;
                    using (var selectCmd = conn.CreateCommand())
                    {
                        selectCmd.CommandText = @"
                        SELECT bh.name, bh.address, bh.owner_id,
                               COALESCE(o.contact_no, '') AS contact_no,
                               bh.status, bh.thumbnail_path, bh.latitude, bh.longitude
                        FROM boarding_houses bh
                        LEFT JOIN owners o ON o.id = bh.owner_id
                        WHERE bh.id = @id
                        LIMIT 1;
                    ";
                        selectCmd.Parameters.AddWithValue("@id", _selectedBoardingHouseId);

                        using (var reader = selectCmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show("Boarding house not found.", "Info",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            deletedDetails = ReadBoardingHouseAuditDetails(reader);
                        }
                    }

                    using (var cmd = conn.CreateCommand())
                    {
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

                    AuditLogger.Log(GetCurrentUserId(), "DELETE", "boarding_houses", _selectedBoardingHouseId, new
                    {
                        deleted = deletedDetails
                    });
                }

                MessageBox.Show("Boarding house deleted successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                _selectedBoardingHouseId = 0;
                _selectedBhId = 0;
                RefreshRoomsHostPanel();
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

        private static string OwnerDisplaySql(string alias)
        {
            alias = (alias ?? "").Trim();
            if (string.IsNullOrWhiteSpace(alias))
                throw new ArgumentException("alias is required", nameof(alias));

            return $@"TRIM(CONCAT(
                COALESCE({alias}.lastname, ''), ', ',
                COALESCE({alias}.firstname, ''),
                CASE
                  WHEN {alias}.middlename IS NULL OR {alias}.middlename = '' THEN ''
                  ELSE CONCAT(' ', {alias}.middlename)
                END
            ))";
        }

        private void LoadOwnersForCombo(ComboBox combo, ulong? selectedOwnerId = null)
        {
            using (var conn = DbConnectionFactory.CreateConnection())
            using (var cmd = conn.CreateCommand())
            {
                EnsureOpen(conn);
                cmd.CommandText = @"
                    SELECT
                        id,
                        TRIM(CONCAT(
                            COALESCE(lastname, ''), ', ',
                            COALESCE(firstname, ''),
                            CASE
                              WHEN middlename IS NULL OR middlename = '' THEN ''
                              ELSE CONCAT(' ', middlename)
                            END
                        )) AS display_name,
                        COALESCE(contact_no, '') AS contact_no
                    FROM owners
                    WHERE status = 'ACTIVE'
                    ORDER BY lastname, firstname;
                ";

                var table = new DataTable();
                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }

                combo.DisplayMember = "display_name";
                combo.ValueMember = "id";
                combo.DataSource = table;

                if (selectedOwnerId.HasValue)
                {
                    combo.SelectedValue = selectedOwnerId.Value;
                }
                else
                {
                    combo.SelectedIndex = -1;
                }
            }
        }

        private void cbOwner_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbOwner.SelectedItem is DataRowView row)
                txtContactNo.Text = row["contact_no"]?.ToString() ?? "";
        }

        private void details_cbOwner_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (details_cbOwner.SelectedItem is DataRowView row)
                details_txtContactNo.Text = row["contact_no"]?.ToString() ?? "";
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

                    cmd.CommandText = $@"
                                    SELECT 
                                        bh.id,
                                        bh.name,
                                        bh.address,
                                        {OwnerDisplaySql("o")} AS owner_name,
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
                                        OR CONCAT_WS(' ', o.firstname, o.middlename, o.lastname) LIKE @kw
                                        OR CONCAT_WS(' ', o.lastname, o.firstname, o.middlename) LIKE @kw
                                        OR {OwnerDisplaySql("o")} LIKE @kw
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

        public async void viewSingleMap()
        {
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

                mapSingleModal.Visible = true;
                mapSingleModal.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open map.\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void viewMapBtn_Click(object sender, EventArgs e)
        {
            try
            {
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
            detailsModal.Visible = true;
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
            ShowAddModal();
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
            core.SetVirtualHostNameToFolderMapping(
                "appassets",
                mapDir,
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
            ShowAddModal();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
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

            await mapSingleWebView.EnsureCoreWebView2Async(null);
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

        }

        private void LoadRooms(int bhId)
        {
            
        }

        private void InitializeRoomsHostPanelUI()
        {
            if (splitRooms != null)
                return;

            if (roomsHostPanel == null)
                return;

            splitRooms = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
            };

            roomsLeftHeaderPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                Padding = new Padding(10, 6, 10, 6),
                BackColor = Color.WhiteSmoke
            };

            lblRoomTitle = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Text = "Select a room",
                Location = new Point(10, 6)
            };

            lblRoomMeta = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                Text = "",
                Location = new Point(10, 30)
            };

            lblOccupancy = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                Text = "Occupancy: - / -",
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            roomsLeftHeaderPanel.SizeChanged += (s, e) =>
            {
                lblOccupancy.Location = new Point(
                    Math.Max(10, roomsLeftHeaderPanel.Width - lblOccupancy.Width - 10),
                    18);
            };
            roomsLeftHeaderPanel.Controls.Add(lblRoomTitle);
            roomsLeftHeaderPanel.Controls.Add(lblRoomMeta);
            roomsLeftHeaderPanel.Controls.Add(lblOccupancy);
            lblOccupancy.Location = new Point(
                Math.Max(10, roomsLeftHeaderPanel.Width - lblOccupancy.Width - 10),
                18);

            dgvRoomTenants = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            if (dgvRoomTenants.Columns.Count == 0)
            {
                dgvRoomTenants.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tenant Name" });
                dgvRoomTenants.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Contact" });
                dgvRoomTenants.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Rental Status" });
                dgvRoomTenants.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Start Date" });
                dgvRoomTenants.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "End Date" });
            }

            roomsRightHeaderPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                Padding = new Padding(10, 6, 10, 6),
                BackColor = Color.WhiteSmoke
            };

            var lblRoomsHeader = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Text = "Rooms",
                Location = new Point(10, 10)
            };

            roomsRightHeaderPanel.Controls.Add(lblRoomsHeader);

            // ✅ Move existing button into this header panel
            viewRoomsBtn.Visible = true;
            viewRoomsBtn.Parent = roomsRightHeaderPanel;
            viewRoomsBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            viewRoomsBtn.Location = new Point(
                roomsRightHeaderPanel.Width - viewRoomsBtn.Width - 10,
                6
            );

            // Auto reposition when resized
            roomsRightHeaderPanel.SizeChanged += (s, e) =>
            {
                viewRoomsBtn.Location = new Point(
                    roomsRightHeaderPanel.Width - viewRoomsBtn.Width - 10,
                    6
                );
            };


            flpRooms = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                WrapContents = true,
                Padding = new Padding(10)
            };

            splitRooms.Panel1.Controls.Add(flpRooms);
            splitRooms.Panel1.Controls.Add(roomsRightHeaderPanel);
            splitRooms.Panel2.Controls.Add(dgvRoomTenants);
            splitRooms.Panel2.Controls.Add(roomsLeftHeaderPanel);

            roomsHostPanel.Controls.Clear();
            roomsHostPanel.Controls.Add(splitRooms);

            // ✅ SAFE SplitterDistance AFTER layout has a real width
            roomsHostPanel.BeginInvoke(new Action(() =>
            {
                if (splitRooms == null) return;

                int desiredLeft = 500;
                int minLeft = splitRooms.Panel1MinSize;
                int maxLeft = splitRooms.Width - splitRooms.Panel2MinSize;

                if (maxLeft <= minLeft) return; // not enough space yet

                int safeLeft = Math.Max(minLeft, Math.Min(desiredLeft, maxLeft));
                splitRooms.SplitterDistance = safeLeft;
            }));
        }


        private void RefreshRoomsHostPanel()
        {
            _selectedRoomId = 0;
            ClearLeftTenantView();
            LoadRoomsIntoRightPanel();
        }

        private void LoadRoomsIntoRightPanel()
        {
            if (flpRooms == null)
                return;

            flpRooms.SuspendLayout();
            flpRooms.Controls.Clear();

            if (_selectedBhId <= 0)
            {
                flpRooms.ResumeLayout();
                return;
            }

            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = conn.CreateCommand())
                {
                    EnsureOpen(conn);

                    cmd.CommandText = @"
                        SELECT id, room_no, status
                        FROM rooms
                        WHERE boarding_house_id = @bhId
                        ORDER BY room_no ASC;
                    ";
                    cmd.Parameters.AddWithValue("@bhId", _selectedBhId);

                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            int roomId = Convert.ToInt32(r["id"]);
                            string roomNo = r["room_no"]?.ToString() ?? "";
                            string status = r["status"]?.ToString() ?? "";

                            var card = BuildRoomMiniCard(roomId, roomNo, status);
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
                HighlightSelectedRoomCards();
            }
        }

        private Control BuildRoomMiniCard(int roomId, string roomNo, string status)
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

            var panel = new Panel
            {
                Width = 245,
                Height = 110,
                Margin = new Padding(10),
                BackColor = baseColor,
                Cursor = Cursors.Hand,
                Tag = meta
            };

            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Paint += RoomMiniCard_Paint;

            var lblTitle = new Label
            {
                Text = $"Room: {roomNo}",
                AutoSize = false,
                Dock = DockStyle.Top,
                Height = 28,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(6, 0, 6, 0),
                BackColor = Color.Transparent
            };

            panel.Controls.Add(lblTitle);
            panel.Controls.Add(chip);
            chip.BringToFront();

            panel.Click += RoomMiniCard_Click;
            foreach (Control c in panel.Controls)
                c.Click += RoomMiniCard_Click;

            PositionChip(panel, chip);
            panel.SizeChanged += (s, e) => PositionChip(panel, chip);

            ApplyRoomCardStyling(panel, meta);
            return panel;
        }

        private void RoomMiniCard_Click(object sender, EventArgs e)
        {
            Control clicked = sender as Control;
            if (clicked == null) return;

            int roomId = 0;
            if (clicked.Tag is RoomCardMeta directMeta)
                roomId = directMeta.RoomId;
            else if (clicked.Parent != null && clicked.Parent.Tag is RoomCardMeta parentMeta)
                roomId = parentMeta.RoomId;

            if (roomId <= 0) return;

            _selectedRoomId = roomId;
            HighlightSelectedRoomCards();
            ShowRoomTenants(_selectedRoomId);
        }

        private void HighlightSelectedRoomCards()
        {
            if (flpRooms == null) return;

            foreach (Control control in flpRooms.Controls)
            {
                if (control is Panel panel && panel.Tag is RoomCardMeta meta)
                {
                    ApplyRoomCardStyling(panel, meta);
                    panel.Invalidate();
                }
            }
        }

        private void ApplyRoomCardStyling(Panel card, RoomCardMeta meta)
        {
            bool isSelected = meta.RoomId == _selectedRoomId;
            card.Padding = new Padding(10);
            card.BorderStyle = isSelected ? BorderStyle.Fixed3D : BorderStyle.FixedSingle;
            card.BackColor = isSelected ? LightenColor(meta.BaseColor, 0.06f) : meta.BaseColor;
        }

        private void RoomMiniCard_Paint(object sender, PaintEventArgs e)
        {
            if (sender is Panel card && card.Tag is RoomCardMeta meta && meta.RoomId == _selectedRoomId)
            {
                using var brush = new SolidBrush(Color.FromArgb(0, 120, 215));
                e.Graphics.FillRectangle(brush, 0, 0, 6, card.Height);
            }
        }

        private void ShowRoomTenants(int roomId)
        {
            if (roomId <= 0)
            {
                ClearLeftTenantView();
                return;
            }

            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = conn.CreateCommand())
                {
                    EnsureOpen(conn);
                    cmd.CommandText = @"
                        SELECT room_no, room_type, capacity, monthly_rate, status
                        FROM rooms
                        WHERE id = @id
                        LIMIT 1;
                    ";
                    cmd.Parameters.AddWithValue("@id", roomId);

                    using (var r = cmd.ExecuteReader())
                    {
                        if (!r.Read())
                        {
                            ClearLeftTenantView();
                            return;
                        }

                        string roomNo = r["room_no"]?.ToString() ?? "";
                        string roomType = r["room_type"]?.ToString() ?? "";
                        string status = r["status"]?.ToString() ?? "";
                        int capacity = r["capacity"] == DBNull.Value ? 0 : Convert.ToInt32(r["capacity"]);

                        decimal rate = 0m;
                        if (r["monthly_rate"] != DBNull.Value)
                            rate = Convert.ToDecimal(r["monthly_rate"], CultureInfo.InvariantCulture);

                        lblRoomTitle.Text = $"Room: {roomNo}";
                        lblRoomMeta.Text = $"Type: {roomType} | Rate: {rate:N2} | Status: {status}";
                        lblOccupancy.Text = $"Occupancy: - / {capacity}";
                    }
                }

                LoadTenants(roomId);
            }
            catch
            {
                ClearLeftTenantView();
            }
        }

        private void LoadTenants(int roomId)
        {
            if (dgvRoomTenants == null) return;

            try
            {
                dgvRoomTenants.Rows.Clear();
                if (roomId <= 0) return;

                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = conn.CreateCommand())
                {
                    EnsureOpen(conn);

                    cmd.CommandText = @"
                SELECT
                    COALESCE(
                        CASE WHEN o.occupant_type = 'TENANT'  THEN t.full_name END,
                        CASE WHEN o.occupant_type = 'STUDENT' THEN s.full_name END,
                        o.full_name
                    ) AS occupant_name,
                    COALESCE(
                        CASE WHEN o.occupant_type = 'TENANT'  THEN t.contact_no END,
                        CASE WHEN o.occupant_type = 'STUDENT' THEN s.contact_no END,
                        o.contact_no,
                        ''
                    ) AS contact_no,
                    r.status AS rental_status,
                    r.start_date,
                    r.end_date
                FROM rentals r
                INNER JOIN occupants o ON o.id = r.occupant_id
                LEFT JOIN tenant_occupant_map tom ON tom.occupant_id = o.id
                LEFT JOIN tenants t ON t.id = tom.tenant_id
                LEFT JOIN student_occupant_map som ON som.occupant_id = o.id
                LEFT JOIN students s ON s.id = som.student_id
                WHERE r.room_id = @roomId
                  AND r.status = 'ACTIVE'
                  AND (r.end_date IS NULL OR r.end_date >= CURDATE())
                ORDER BY r.start_date DESC;
            ";

                    cmd.Parameters.AddWithValue("@roomId", roomId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        bool hasRows = false;

                        while (reader.Read())
                        {
                            hasRows = true;

                            string tenantName = reader["occupant_name"]?.ToString() ?? "";
                            string contact = reader["contact_no"]?.ToString() ?? "";
                            string rentalStatus = reader["rental_status"]?.ToString() ?? "";

                            string startDate = reader["start_date"] == DBNull.Value
                                ? ""
                                : Convert.ToDateTime(reader["start_date"]).ToString("yyyy-MM-dd");

                            string endDate = reader["end_date"] == DBNull.Value
                                ? ""
                                : Convert.ToDateTime(reader["end_date"]).ToString("yyyy-MM-dd");

                            dgvRoomTenants.Rows.Add(tenantName, contact, rentalStatus, startDate, endDate);
                        }

                        if (!hasRows)
                            dgvRoomTenants.Rows.Add("(No active tenants)", "", "", "", "");
                    }
                }
            }
            catch (Exception ex)
            {
                dgvRoomTenants.Rows.Clear();
                dgvRoomTenants.Rows.Add("(Failed to load tenants)", "", "", "", "");
                // optional debug:
                // MessageBox.Show(ex.Message);
            }
        }


        private void ClearLeftTenantView()
        {
            if (lblRoomTitle != null)
                lblRoomTitle.Text = "Select a room";
            if (lblRoomMeta != null)
                lblRoomMeta.Text = "";
            if (lblOccupancy != null)
                lblOccupancy.Text = "Occupancy: - / -";
            dgvRoomTenants?.Rows.Clear();
        }

        private static Color GetStatusColor(string status)
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

        private static Color DarkenColor(Color color, double factor)
        {
            int r = (int)(color.R * (1 - factor));
            int g = (int)(color.G * (1 - factor));
            int b = (int)(color.B * (1 - factor));
            return Color.FromArgb(Math.Max(0, r), Math.Max(0, g), Math.Max(0, b));
        }

        private static Color LightenColor(Color color, double factor)
        {
            int r = color.R + (int)((255 - color.R) * factor);
            int g = color.G + (int)((255 - color.G) * factor);
            int b = color.B + (int)((255 - color.B) * factor);
            return Color.FromArgb(Math.Min(255, r), Math.Min(255, g), Math.Min(255, b));
        }

        private static void PositionChip(Control host, Control chip)
        {
            int x = Math.Max(6, host.Width - chip.Width - 8);
            int y = 6;
            chip.Location = new Point(x, y);
        }

        private sealed class RoomCardMeta
        {
            public int RoomId { get; }
            public Color BaseColor { get; }

            public RoomCardMeta(int roomId, Color baseColor)
            {
                RoomId = roomId;
                BaseColor = baseColor;
            }
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
            
        }

        private int _selectedRoomId = 0;
        private void dgvRooms_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
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
                int roomId = _selectedRoomId;
                int boardingHouseId = _roomsBhId;
                object beforeRoomDetails;
                string roomNo = "";
                string roomType = "";
                decimal? monthlyRate = null;
                int? capacity = null;

                using (var conn = DbConnectionFactory.CreateConnection())
                {
                    EnsureOpen(conn);

                    using (var selectCmd = conn.CreateCommand())
                    {
                        selectCmd.CommandText = @"
                        SELECT id, boarding_house_id, room_no, room_type, monthly_rate, capacity, status
                        FROM rooms
                        WHERE id = @roomId
                          AND boarding_house_id = @bhId
                        LIMIT 1;
                    ";
                        selectCmd.Parameters.AddWithValue("@roomId", roomId);
                        selectCmd.Parameters.AddWithValue("@bhId", boardingHouseId);

                        using (var reader = selectCmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show("Room not found or already updated.", "Info",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            roomNo = reader["room_no"]?.ToString() ?? "";
                            roomType = reader["room_type"]?.ToString() ?? "";
                            monthlyRate = reader["monthly_rate"] == DBNull.Value ? null : Convert.ToDecimal(reader["monthly_rate"]);
                            capacity = reader["capacity"] == DBNull.Value ? null : Convert.ToInt32(reader["capacity"]);
                            string statusBefore = reader["status"]?.ToString() ?? "";

                            beforeRoomDetails = BuildRoomAuditDetails(
                                roomId, boardingHouseId, roomNo, roomType, monthlyRate, capacity, statusBefore);
                        }
                    }

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                        UPDATE rooms
                        SET status = @status
                        WHERE id = @roomId
                          AND boarding_house_id = @bhId
                        LIMIT 1;
                    ";

                        cmd.Parameters.AddWithValue("@status", newStatus);
                        cmd.Parameters.AddWithValue("@roomId", roomId);
                        cmd.Parameters.AddWithValue("@bhId", boardingHouseId);

                        int affected = cmd.ExecuteNonQuery();
                        if (affected <= 0)
                        {
                            MessageBox.Show("Room not found or already updated.", "Info",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    var afterRoomDetails = BuildRoomAuditDetails(
                        roomId, boardingHouseId, roomNo, roomType, monthlyRate, capacity, newStatus);
                    AuditLogger.Log(GetCurrentUserId(), "UPDATE", "rooms", roomId, new
                    {
                        before = beforeRoomDetails,
                        after = afterRoomDetails,
                        context = new
                        {
                            boarding_house_id = _roomsBhId,
                            boarding_house_name = _roomsBhName
                        }
                    });
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

        private static object BuildBoardingHouseAuditDetails(
            string name,
            string address,
            long? ownerId,
            string contactNo,
            string thumbnailPath,
            decimal? latitude,
            decimal? longitude,
            string status)
        {
            string? normalizedThumbnail = string.IsNullOrWhiteSpace(thumbnailPath) ? null : thumbnailPath;
            return new
            {
                name,
                address,
                owner_id = ownerId,
                contact_no = contactNo,
                thumbnail_path = normalizedThumbnail,
                latitude,
                longitude,
                status
            };
        }

        private static object ReadBoardingHouseAuditDetails(MySqlDataReader reader)
        {
            long? ownerId = reader["owner_id"] == DBNull.Value ? null : Convert.ToInt64(reader["owner_id"]);
            decimal? latitude = reader["latitude"] == DBNull.Value ? null : Convert.ToDecimal(reader["latitude"]);
            decimal? longitude = reader["longitude"] == DBNull.Value ? null : Convert.ToDecimal(reader["longitude"]);

            string GetString(string key)
            {
                if (reader[key] == DBNull.Value) return "";
                return reader[key]?.ToString() ?? "";
            }

            return BuildBoardingHouseAuditDetails(
                GetString("name"),
                GetString("address"),
                ownerId,
                GetString("contact_no"),
                GetString("thumbnail_path"),
                latitude,
                longitude,
                GetString("status"));
        }

        private static long? NormalizeOwnerId(object value)
        {
            if (value == null || value == DBNull.Value) return null;

            return value switch
            {
                long l => l,
                int i => i,
                short s => s,
                byte b => b,
                ulong ul => Convert.ToInt64(ul),
                uint ui => Convert.ToInt64(ui),
                ushort us => us,
                decimal d => Convert.ToInt64(d),
                string s when long.TryParse(s, out var parsed) => parsed,
                _ => Convert.ToInt64(value)
            };
        }

        private static object BuildRoomAuditDetails(
            int id,
            int boardingHouseId,
            string roomNo,
            string roomType,
            decimal? monthlyRate,
            int? capacity,
            string status)
        {
            return new
            {
                id,
                boarding_house_id = boardingHouseId,
                room_no = roomNo,
                room_type = roomType,
                monthly_rate = monthlyRate,
                capacity,
                status
            };
        }

        public void OpenAddModalForOwner(long ownerId)
        {
            LoadOwnersForCombo(cbOwner);

            cbOwner.SelectedValue = ownerId;

            if (cbOwner.SelectedItem is System.Data.DataRowView row)
                txtContactNo.Text = row["contact_no"]?.ToString() ?? "";

            txtBHName.Text = "";
            txtAddress.Text = "";
            txtLatitude.Text = "";
            txtLongitude.Text = "";
            txtThumbnailPath.Text = "";
            picThumbnail.Image?.Dispose();
            picThumbnail.Image = null;

            ShowAddModal();
        }


        private int GetCurrentUserId()
        {
            return 1;
        }

        private void viewRoomsBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();

            if (_selectedBoardingHouseId <= 0)
            {
                MessageBox.Show("Please select a boarding house first.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var main = this.FindForm() as MainLayout;
            if (main == null)
            {
                MessageBox.Show("Main form not found.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            detailsModal.Visible = false;
            main.OpenRoomsFromBoardingHouse(_selectedBoardingHouseId);
        }

        private void LoadBhStats()
        {
            if (_selectedBhId <= 0)
            {
                earningsTxt.Text = "(000)";
                thisMontEarningsTxt.Text = "(000)";
                activeRenatlsTxt.Text = "(000)";
                totalRoomsTxt.Text = "(000)";
                tootalRoomsOccupiedTxt.Text = "(000)";
                totalAvailableRoomsTxt.Text = "(000)";
                return;
            }

            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = conn.CreateCommand())
                {
                    EnsureOpen(conn);

                    // We compute all stats in one roundtrip.
                    cmd.CommandText = @"
                SELECT
                    -- Total earnings (posted payments) for this boarding house
                    COALESCE((
                        SELECT SUM(p.amount)
                        FROM payments p
                        INNER JOIN rentals ren ON ren.id = p.rental_id
                        INNER JOIN rooms rm ON rm.id = ren.room_id
                        WHERE rm.boarding_house_id = @bhId
                          AND p.status = 'POSTED'
                    ), 0) AS total_earnings,

                    -- This month earnings (posted payments) using bill_month
                    COALESCE((
                        SELECT SUM(p.amount)
                        FROM payments p
                        INNER JOIN rentals ren ON ren.id = p.rental_id
                        INNER JOIN rooms rm ON rm.id = ren.room_id
                        WHERE rm.boarding_house_id = @bhId
                          AND p.status = 'POSTED'
                          AND p.bill_month >= DATE_FORMAT(CURDATE(), '%Y-%m-01')
                          AND p.bill_month <  DATE_ADD(DATE_FORMAT(CURDATE(), '%Y-%m-01'), INTERVAL 1 MONTH)
                    ), 0) AS this_month_earnings,

                    -- Active rentals count for this boarding house
                    COALESCE((
                        SELECT COUNT(*)
                        FROM rentals ren
                        INNER JOIN rooms rm ON rm.id = ren.room_id
                        WHERE rm.boarding_house_id = @bhId
                          AND ren.status = 'ACTIVE'
                          AND (ren.end_date IS NULL OR ren.end_date >= CURDATE())
                    ), 0) AS active_rentals,

                    -- Total rooms
                    COALESCE((
                        SELECT COUNT(*)
                        FROM rooms rm
                        WHERE rm.boarding_house_id = @bhId
                    ), 0) AS total_rooms,

                    -- Occupied rooms (based on rooms.status)
                    COALESCE((
                        SELECT COUNT(*)
                        FROM rooms rm
                        WHERE rm.boarding_house_id = @bhId
                          AND rm.status = 'OCCUPIED'
                    ), 0) AS occupied_rooms,

                    -- Available rooms (based on rooms.status)
                    COALESCE((
                        SELECT COUNT(*)
                        FROM rooms rm
                        WHERE rm.boarding_house_id = @bhId
                          AND rm.status = 'AVAILABLE'
                    ), 0) AS available_rooms
            ";

                    cmd.Parameters.AddWithValue("@bhId", _selectedBhId);

                    using (var r = cmd.ExecuteReader())
                    {
                        if (!r.Read()) return;

                        decimal totalEarnings = Convert.ToDecimal(r["total_earnings"]);
                        decimal thisMonth = Convert.ToDecimal(r["this_month_earnings"]);
                        int activeRentals = Convert.ToInt32(r["active_rentals"]);
                        int totalRooms = Convert.ToInt32(r["total_rooms"]);
                        int occupiedRooms = Convert.ToInt32(r["occupied_rooms"]);
                        int availableRooms = Convert.ToInt32(r["available_rooms"]);

                        earningsTxt.Text = $"₱ {totalEarnings:N2}";
                        thisMontEarningsTxt.Text = $"₱ {thisMonth:N2}";
                        activeRenatlsTxt.Text = activeRentals.ToString();
                        totalRoomsTxt.Text = totalRooms.ToString();
                        tootalRoomsOccupiedTxt.Text = occupiedRooms.ToString();
                        totalAvailableRoomsTxt.Text = availableRooms.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                earningsTxt.Text = "₱ 0.00\r\n";
                thisMontEarningsTxt.Text = "₱ 0.00\r\n";
                activeRenatlsTxt.Text = "0";
                totalRoomsTxt.Text = "0";
                tootalRoomsOccupiedTxt.Text = "0";
                totalAvailableRoomsTxt.Text = "0";

                MessageBox.Show("Failed to load boarding house stats.\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}

