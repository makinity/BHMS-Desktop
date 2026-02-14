using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BoardingHouse
{

    public partial class BHOwnersView : UserControl
    {
        private int? _selectedOwnerId = null;
        private int? _selectedBoardingHouseId = null;

        public BHOwnersView()
        {
            InitializeComponent();
            this.Resize += BHOwnersView_Resize;
            addOwnerModal.Visible = false;
            deleteOwnerBtn.Visible = false;
            updateOwnerBtn.Visible = false;

            addOwnerBrowseImageBtn.Click += addOwnerBrowseImageBtn_Click;
            detailsBrowseImgBtn.Click += detailsBrowseImgBtn_Click;
            addOwnerSaveBtn.Click += addOwnerSaveBtn_Click;
            addOwnerCancelBtn.Click += addOwnerCancelBtn_Click;
            addRoomCloseBtn.Click += addRoomCloseBtn_Click;

            SetupBoardingHousesListView();
        }

        private void BHOwnersView_Load(object sender, EventArgs e)
        {
            LoadOwners();
            CenterModal(addOwnerModal);
            CenterModal(ownerBHModal);
        }

        private void BHOwnersView_Resize(object sender, EventArgs e)
        {
            if (addOwnerModal.Visible) CenterModal(addOwnerModal);
            if (ownerBHModal.Visible) CenterModal(ownerBHModal);
        }


        private void CenterModal(Panel modal)
        {
            if (modal == null) return;

            int x = (this.ClientSize.Width - modal.Width) / 2;
            int y = (this.ClientSize.Height - modal.Height) / 2;

            modal.Left = Math.Max(0, x);
            modal.Top = Math.Max(0, y);
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            txtSearch.Text = "";
            LoadOwners();
            HideDetails();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            LoadOwners(txtSearch.Text.Trim());
            HideDetails();
        }

        private void LoadOwners(string keyword = "")
        {
            flpOwners.SuspendLayout();
            flpOwners.Controls.Clear();

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                using var cmd = conn.CreateCommand();

                cmd.CommandText = @"
                    SELECT id,
                           lastname, firstname, middlename,
                           contact_no, email, address,
                           profile_path, status,
                           created_at, updated_at
                    FROM owners
                    WHERE (@kw = ''
                        OR lastname  LIKE CONCAT('%', @kw, '%')
                        OR firstname LIKE CONCAT('%', @kw, '%')
                        OR middlename LIKE CONCAT('%', @kw, '%')
                        OR email     LIKE CONCAT('%', @kw, '%')
                        OR contact_no LIKE CONCAT('%', @kw, '%'))
                    ORDER BY id DESC;
                ";
                cmd.Parameters.AddWithValue("@kw", keyword);

                using var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    int id = r.GetInt32("id");

                    string ln = r["lastname"]?.ToString() ?? "";
                    string fn = r["firstname"]?.ToString() ?? "";
                    string mn = r["middlename"]?.ToString() ?? "";
                    string fullName = $"{ln}, {fn} {mn}".Replace("  ", " ").Trim();

                    string contact = r["contact_no"]?.ToString() ?? "";
                    string email = r["email"]?.ToString() ?? "";
                    string status = r["status"]?.ToString() ?? "";

                    flpOwners.Controls.Add(BuildOwnerCard(id, fullName, contact, email, status));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load owners.\n\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                flpOwners.ResumeLayout();
            }
        }

        private Control BuildOwnerCard(int ownerId, string fullName, string contact, string email, string status)
        {
            var pnl = new Panel
            {
                Width = flpOwners.ClientSize.Width - 60,
                Height = 78,
                BackColor = Color.WhiteSmoke,
                Margin = new Padding(10),
                Cursor = Cursors.Hand,
                Tag = ownerId
            };

            var lblName = new Label
            {
                Text = fullName,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(12, 10),
                AutoSize = true
            };

            var lblMeta = new Label
            {
                Text = $"{contact}  •  {email}",
                Location = new Point(12, 36),
                AutoSize = true
            };

            var lblStatus = new Label
            {
                Text = status,
                AutoSize = true,
                Location = new Point(pnl.Width - 110, 28),
                ForeColor = Color.DimGray
            };

            pnl.Controls.Add(lblName);
            pnl.Controls.Add(lblMeta);
            pnl.Controls.Add(lblStatus);

            pnl.Click += OwnerCard_Click;
            lblName.Click += OwnerCard_Click;
            lblMeta.Click += OwnerCard_Click;
            lblStatus.Click += OwnerCard_Click;

            return pnl;
        }

        private void OwnerCard_Click(object sender, EventArgs e)
        {
            Control ctrl = sender as Control;
            if (ctrl == null) return;

            while (ctrl != null && ctrl.Tag == null)
                ctrl = ctrl.Parent;

            if (ctrl?.Tag == null) return;

            int ownerId = (int)ctrl.Tag;
            OpenOwnerDetails(ownerId);
        }

        private void OpenOwnerDetails(int ownerId)
        {
            _selectedOwnerId = ownerId;

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                using var cmd = conn.CreateCommand();

                cmd.CommandText = @"
                    SELECT id, lastname, firstname, middlename,
                           contact_no, email, address,
                           profile_path, status
                    FROM owners
                    WHERE id = @id
                    LIMIT 1;
                ";
                cmd.Parameters.AddWithValue("@id", ownerId);

                using var r = cmd.ExecuteReader();
                if (!r.Read())
                {
                    MessageBox.Show("Owner not found.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                detailsOwnerLastnametxt.Text = r["lastname"]?.ToString() ?? "";
                detailsOwnerFirstnametxt.Text = r["firstname"]?.ToString() ?? "";
                detailsOwnerMiddlenametxt.Text = r["middlename"]?.ToString() ?? "";
                detailsOwnerContacttxt.Text = r["contact_no"]?.ToString() ?? "";
                detailsOwnerEmailtxt.Text = r["email"]?.ToString() ?? "";
                detailsOwnerAddresstxt.Text = r["address"]?.ToString() ?? "";

                var path = r["profile_path"]?.ToString() ?? "";
                detailsOwnerImgTXT.Text = path;
                LoadPictureSafe(detailsOwnerImg, path);

                viewBoardingHousesBtn.Visible = true;
                deleteOwnerBtn.Visible = true;
                updateOwnerBtn.Visible = true;

                ShowDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open owner details.\n\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowDetails()
        {
            detailsModal.Visible = true;
            detailsModal.BringToFront();

            // Ensure it sits at the right edge of the UserControl
            detailsModal.Location = new Point(this.Width - detailsModal.Width, 0);
            detailsModal.Height = this.Height;
        }

        private void HideDetails()
        {
            _selectedOwnerId = null;

            deleteOwnerBtn.Visible = false;
            updateOwnerBtn.Visible = false;

            detailsOwnerLastnametxt.Text = "";
            detailsOwnerFirstnametxt.Text = "";
            detailsOwnerMiddlenametxt.Text = "";
            detailsOwnerContacttxt.Text = "";
            detailsOwnerEmailtxt.Text = "";
            detailsOwnerAddresstxt.Text = "";
            detailsOwnerImgTXT.Text = "";
            detailsOwnerImg.Image = null;
        }

        private void addTenantBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            CenterModal(addOwnerModal);

            addOwnerModal.Visible = true;
            addOwnerModal.BringToFront();

            addOwnerLastnametxt.Text = "";
            addOwnerFirstnametxt.Text = "";
            addOwnerMiddlenametxt.Text = "";
            addOwnerContacttxt.Text = "";
            addOwnerEmailtxt.Text = "";
            addOwnerAddresstxt.Text = "";
            addOwnerImageTXT.Text = "";
            addOwnerImg.Image = null;
        }

        private void addOwnerCancelBtn_Click(object sender, EventArgs e)
        {
            addOwnerModal.Visible = false;
        }

        private void addRoomCloseBtn_Click(object sender, EventArgs e)
        {
            addOwnerModal.Visible = false;
        }

        private void addOwnerSaveBtn_Click(object sender, EventArgs e)
        {

        }

        private void updateTenantBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();

            if (_selectedOwnerId == null) return;

            int id = _selectedOwnerId.Value;

            string ln = detailsOwnerLastnametxt.Text.Trim();
            string fn = detailsOwnerFirstnametxt.Text.Trim();
            string mn = detailsOwnerMiddlenametxt.Text.Trim();
            string contact = detailsOwnerContacttxt.Text.Trim();
            string email = detailsOwnerEmailtxt.Text.Trim();
            string address = detailsOwnerAddresstxt.Text.Trim();
            string profilePath = detailsOwnerImgTXT.Text.Trim();

            if (string.IsNullOrWhiteSpace(ln) || string.IsNullOrWhiteSpace(fn))
            {
                MessageBox.Show("Lastname and Firstname are required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                using var beforeCmd = conn.CreateCommand();

                beforeCmd.CommandText = @"
                    SELECT lastname, firstname, middlename,
                           contact_no, email, address,
                           profile_path, status
                    FROM owners
                    WHERE id = @id
                    LIMIT 1;
                ";
                beforeCmd.Parameters.AddWithValue("@id", id);

                object? beforeDetails = null;
                string statusBefore = "";

                using (var reader = beforeCmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        MessageBox.Show("Owner not found.", "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    beforeDetails = ReadOwnerAuditDetails(reader);
                    statusBefore = reader["status"]?.ToString() ?? "";
                }

                using var cmd = conn.CreateCommand();

                cmd.CommandText = @"
                    UPDATE owners
                    SET lastname = @ln,
                        firstname = @fn,
                        middlename = @mn,
                        contact_no = @contact,
                        email = @email,
                        address = @address,
                        profile_path = @profile,
                        updated_at = NOW()
                    WHERE id = @id;
                ";

                cmd.Parameters.AddWithValue("@ln", ln);
                cmd.Parameters.AddWithValue("@fn", fn);
                cmd.Parameters.AddWithValue("@mn", mn);
                cmd.Parameters.AddWithValue("@contact", contact);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@profile", profilePath);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();

                LoadOwners(txtSearch.Text.Trim());
                var afterDetails = BuildOwnerAuditDetails(
                    ln, fn, mn, contact, email, address, profilePath, statusBefore);
                AuditLogger.Log(GetCurrentUserId(), "UPDATE", "owners", id, new
                {
                    before = beforeDetails,
                    after = afterDetails
                });
                MessageBox.Show("Owner updated successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update owner.\n\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteTenantBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();

            if (_selectedOwnerId == null) return;

            int id = _selectedOwnerId.Value;

            var confirm = MessageBox.Show("Delete this owner permanently?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                using var selectCmd = conn.CreateCommand();

                selectCmd.CommandText = @"
                    SELECT lastname, firstname, middlename,
                           contact_no, email, address,
                           profile_path, status
                    FROM owners
                    WHERE id = @id
                    LIMIT 1;
                ";
                selectCmd.Parameters.AddWithValue("@id", id);

                object? deletedDetails = null;

                using (var reader = selectCmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        MessageBox.Show("Owner not found.", "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    deletedDetails = ReadOwnerAuditDetails(reader);
                }

                using var cmd = conn.CreateCommand();

                cmd.CommandText = "DELETE FROM owners WHERE id = @id;";
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();

                HideDetails();
                LoadOwners(txtSearch.Text.Trim());

                AuditLogger.Log(GetCurrentUserId(), "DELETE", "owners", id, new
                {
                    deleted = deletedDetails
                });

                MessageBox.Show("Owner deleted.", "Deleted",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to delete owner.\n\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addOwnerBrowseImageBtn_Click(object sender, EventArgs e)
        {

        }

        private void detailsBrowseImgBtn_Click(object sender, EventArgs e)
        {

        }

        private string PickImageFile()
        {
            using var ofd = new OpenFileDialog
            {
                Title = "Select Profile Image",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
                Multiselect = false
            };

            return ofd.ShowDialog() == DialogResult.OK ? ofd.FileName : "";
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

        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void detailsModal_Paint(object sender, PaintEventArgs e) { }

        private void openCameraBtn_Click(object sender, EventArgs e)
        {
            using var cam = new CameraCaptureForm();
            if (cam.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(cam.SavedImagePath))
            {
                addOwnerImageTXT.Text = cam.SavedImagePath;
                LoadPictureSafe(addOwnerImg, cam.SavedImagePath);
            }
        }

        private void detailsCameraBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();

            using var cam = new CameraCaptureForm();
            if (cam.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(cam.SavedImagePath))
            {
                detailsOwnerImgTXT.Text = cam.SavedImagePath;
                LoadPictureSafe(detailsOwnerImg, cam.SavedImagePath);
            }
        }

        private void topBar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void addOwnerCancelBtn_Click_1(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            addOwnerModal.Visible = false;

            addOwnerLastnametxt.Text = "";
            addOwnerFirstnametxt.Text = "";
            addOwnerMiddlenametxt.Text = "";
            addOwnerContacttxt.Text = "";
            addOwnerEmailtxt.Text = "";
            addOwnerImageTXT.Text = "";
            addOwnerAddresstxt.Text = "";

        }

        private void addOwnerSaveBtn_Click_1(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();

            string ln = addOwnerLastnametxt.Text.Trim();
            string fn = addOwnerFirstnametxt.Text.Trim();
            string mn = addOwnerMiddlenametxt.Text.Trim();
            string contact = addOwnerContacttxt.Text.Trim();
            string email = addOwnerEmailtxt.Text.Trim();
            string address = addOwnerAddresstxt.Text.Trim();
            string profilePath = addOwnerImageTXT.Text.Trim();

            if (string.IsNullOrWhiteSpace(ln) || string.IsNullOrWhiteSpace(fn))
            {
                MessageBox.Show("Lastname and Firstname are required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var ownerDetails = BuildOwnerAuditDetails(
                    ln, fn, mn, contact, email, address, profilePath, "active");

                using var conn = DbConnectionFactory.CreateConnection();
                using var cmd = conn.CreateCommand();

                cmd.CommandText = @"
                    INSERT INTO owners
                        (lastname, firstname, middlename,
                         contact_no, email, address, profile_path,
                         status, created_at, updated_at)
                    VALUES
                        (@ln, @fn, @mn,
                         @contact, @email, @address, @profile,
                         'active', NOW(), NOW());
                ";

                cmd.Parameters.AddWithValue("@ln", ln);
                cmd.Parameters.AddWithValue("@fn", fn);
                cmd.Parameters.AddWithValue("@mn", mn);
                cmd.Parameters.AddWithValue("@contact", contact);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@profile", profilePath);

                cmd.ExecuteNonQuery();

                using var idCmd = conn.CreateCommand();
                idCmd.CommandText = "SELECT LAST_INSERT_ID();";
                long newOwnerId = Convert.ToInt64(idCmd.ExecuteScalar());

                addOwnerModal.Visible = false;
                LoadOwners(txtSearch.Text.Trim());

                AuditLogger.Log(GetCurrentUserId(), "CREATE", "owners", newOwnerId, ownerDetails);

                MessageBox.Show("Owner added successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add owner.\n\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void detailsBrowseImgBtn_Click_1(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();

            var path = PickImageFile();
            if (string.IsNullOrWhiteSpace(path)) return;

            detailsOwnerImgTXT.Text = path;
            LoadPictureSafe(detailsOwnerImg, path);
        }

        private void addOwnerBrowseImageBtn_Click_1(object sender, EventArgs e)
        {
            var path = PickImageFile();
            if (string.IsNullOrWhiteSpace(path)) return;

            addOwnerImageTXT.Text = path;
            LoadPictureSafe(addOwnerImg, path);
        }

        private static object BuildOwnerAuditDetails(
            string lastname,
            string firstname,
            string middlename,
            string contactNo,
            string email,
            string address,
            string profilePath,
            string status)
        {
            return new
            {
                lastname,
                firstname,
                middlename,
                contact_no = contactNo,
                email,
                address,
                profile_path = profilePath,
                status
            };
        }

        private static object ReadOwnerAuditDetails(MySqlDataReader reader)
        {
            string GetString(string key) => reader[key]?.ToString() ?? "";

            return BuildOwnerAuditDetails(
                GetString("lastname"),
                GetString("firstname"),
                GetString("middlename"),
                GetString("contact_no"),
                GetString("email"),
                GetString("address"),
                GetString("profile_path"),
                GetString("status"));
        }

        private int GetCurrentUserId()
        {
            return 1;
        }

        private void viewBoardingHousesBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();
            CenterModal(addOwnerModal);

            if (_selectedOwnerId == null)
            {
                MessageBox.Show("Please select an owner first.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            label11.Text = $"Boarding Houses of: {detailsOwnerLastnametxt.Text}, {detailsOwnerFirstnametxt.Text}";

            LoadOwnerBoardingHouses(_selectedOwnerId.Value);

            ownerBHModal.Visible = true;
            ownerBHModal.BringToFront();
        }

        private void closeBHModal_Click(object sender, EventArgs e)
        {
            ownerBHModal.Visible = false;
        }

        private void lvBoardingHouses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvBoardingHouses.SelectedItems.Count == 0)
            {
                _selectedBoardingHouseId = null;
                return;
            }

            var selected = lvBoardingHouses.SelectedItems[0];
            if (int.TryParse(selected.Text, out int bhId))
                _selectedBoardingHouseId = bhId;
            else
                _selectedBoardingHouseId = null;
        }

        private void viewBhBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();

            if (_selectedBoardingHouseId == null)
            {
                MessageBox.Show("Please select a boarding house first.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int bhId = _selectedBoardingHouseId.Value;

            // close the modal (optional, but feels cleaner)
            ownerBHModal.Visible = false;

            // call parent form navigator
            var main = this.FindForm() as MainLayout; // change MainForm to your real form class name
            if (main == null)
            {
                MessageBox.Show("Main form not found.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            main.OpenBoardingHouseDetailsFromOwner(bhId);

        }

        private void addNewBhBtn_Click(object sender, EventArgs e)
        {
            SoundClicked.operationsBtn();

            if (_selectedOwnerId == null)
            {
                MessageBox.Show("Please select an owner first.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int ownerId = _selectedOwnerId.Value;

            ownerBHModal.Visible = false;

            var main = this.FindForm() as MainLayout;
            if (main == null)
            {
                MessageBox.Show("Main form not found.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            main.OpenAddBoardingHouseFromOwner(ownerId);
        }

        private void SetupBoardingHousesListView()
        {
            lvBoardingHouses.Clear();
            lvBoardingHouses.View = View.Details;
            lvBoardingHouses.FullRowSelect = true;
            lvBoardingHouses.MultiSelect = false;
            lvBoardingHouses.HideSelection = false;

            lvBoardingHouses.Columns.Add("ID", 0);
            lvBoardingHouses.Columns.Add("Name", 220);
            lvBoardingHouses.Columns.Add("Address", 260);
            lvBoardingHouses.Columns.Add("Status", 90);
            lvBoardingHouses.Columns.Add("Total Rooms", 90);
            lvBoardingHouses.Columns.Add("Available", 90);
        }

        private void LoadOwnerBoardingHouses(int ownerId)
        {
            lvBoardingHouses.Items.Clear();
            _selectedBoardingHouseId = null;

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                using var cmd = conn.CreateCommand();

                cmd.CommandText = @"
            SELECT id, name, address, status, total_rooms, available_rooms
            FROM boarding_houses
            WHERE owner_id = @ownerId
            ORDER BY id DESC;
        ";
                cmd.Parameters.AddWithValue("@ownerId", ownerId);

                using var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    int bhId = r.GetInt32("id");
                    string name = r["name"]?.ToString() ?? "";
                    string address = r["address"]?.ToString() ?? "";
                    string status = r["status"]?.ToString() ?? "";
                    string totalRooms = r["total_rooms"]?.ToString() ?? "0";
                    string availableRooms = r["available_rooms"]?.ToString() ?? "0";

                    var item = new ListViewItem(bhId.ToString()); // hidden ID column
                    item.SubItems.Add(name);
                    item.SubItems.Add(address);
                    item.SubItems.Add(status);
                    item.SubItems.Add(totalRooms);
                    item.SubItems.Add(availableRooms);

                    lvBoardingHouses.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load boarding houses.\n\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
