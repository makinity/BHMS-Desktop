using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BoardingHouse
{
    public partial class MainLayout : Form
    {
        private Button? _activeNavButton;
        private Color _defaultBtnBackColor;
        private Color _defaultBtnForeColor;
        private readonly Color _activeBtnBackColor = Color.FromArgb(230, 230, 255);
        private readonly Color _activeBtnForeColor = Color.FromArgb(30, 60, 180);
        private DashboardView? _dashboardView;
        private BHOwnersView? _ownersView;
        private BoardingHousesView? _boardingHousesView;
        private RoomsView? _roomsView;
        private PaymentsView? _paymentsView;
        private ToolStripDropDown? _profileDropDown;
        private ToolStripControlHost? _profileDropDownHost;
        private bool _isLoggingOut;
        private bool _isOpeningProfileScreen;
        private bool _isOpeningSettingsScreen;


        public int LoggedInUserId { get; }
        public MainLayout(int userId)
        {
            InitializeComponent();
            MakeCircular(profileImg);
            profileImg.Resize += (s, e) => MakeCircular(profileImg);
            logoutBtn.MouseDown += LogoutBtn_MouseDown;
            profileBtn.MouseDown += ProfileBtn_MouseDown;
            settingsBtn.MouseDown += SettingsBtn_MouseDown;
            HookSidebarSoundRecursive(buttonsPanel);

            ApplySidebarIcons();
            FixSidebarOrder();
            HookNavButtons();
            _defaultBtnBackColor = btnDashboard.BackColor;
            _defaultBtnForeColor = btnDashboard.ForeColor;
            SetActiveNavButton(btnDashboard);

            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.Sizable;
            LoggedInUserId = userId;
        }

        private void MakeCircular(PictureBox pic)
        {
            using System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, pic.Width - 1, pic.Height - 1);
            pic.Region = new Region(path);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (contentPanel.Controls.Count == 0)
            {
                LoadControl(new DashboardView());
            }

            FixSidebarOrder();
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            FixSidebarOrder();
        }

        private void LoadControl(UserControl view)
        {
            contentPanel.Margin = Padding.Empty;
            contentPanel.Padding = Padding.Empty;

            foreach (Control c in contentPanel.Controls)
                c.Dispose();
            contentPanel.Controls.Clear();

            view.Dock = DockStyle.Fill;
            view.Margin = Padding.Empty;
            view.Padding = Padding.Empty;

            contentPanel.Controls.Add(view);

            BeginInvoke(new Action(() =>
            {
                ForceViewLayout(view);
            }));
        }


        private void DashboardButton_Click(object sender, EventArgs e)
        {
            SetActiveNavButton((Button)sender);
            LoadControl(new DashboardView());
        }

        private void btnBHOwners_Click(object sender, EventArgs e)
        {
            SetActiveNavButton((Button)sender);
            LoadControl(new BHOwnersView());
        }

        private void BoardingHousesButton_Click(object sender, EventArgs e)
        {
            SetActiveNavButton((Button)sender);

            if (_boardingHousesView == null || _boardingHousesView.IsDisposed)
                _boardingHousesView = new BoardingHousesView();

            LoadControl(_boardingHousesView);
        }

        private void TenantsButton_Click(object sender, EventArgs e)
        {
            SetActiveNavButton((Button)sender);
            LoadControl(new TenantsView());
        }

        private void RoomsButton_Click(object sender, EventArgs e)
        {
            SetActiveNavButton((Button)sender);
            var view = new RoomsView();
            view.CurrentUserId = LoggedInUserId;
            LoadControl(view);
        }
        private void PaymentsButton_Click(object sender, EventArgs e)
        {
            SetActiveNavButton((Button)sender);
            LoadControl(new PaymentsView());
        }

        private void ReportsButton_Click(object sender, EventArgs e)
        {
            SetActiveNavButton((Button)sender);
            LoadControl(new ReportsView());
        }

        private void btnStudents_Click(object sender, EventArgs e)
        {
            SetActiveNavButton((Button)sender);
            LoadControl(new StudentsView());
        }

        private void btnReservations_Click(object sender, EventArgs e)
        {
            SetActiveNavButton((Button)sender);
            LoadControl(new ReservationsView());
        }

        private void profileBtn_Click(object sender, EventArgs e)
        {
            PerformOpenProfileScreen();
        }

        private void settingsBtn_Click(object sender, EventArgs e)
        {
            PerformOpenSettingsScreen();
        }

        private void FixSidebarOrder()
        {
            buttonsPanel.SuspendLayout();

            Control[] orderedTopToBottom =
            {
                btnDashboard,
                btnBoardingHouses,
                btnBHOwners,
                btnTenants,
                btnStudents,
                btnRooms,
                btnReservations,
                btnPayments,
                btnReports
            };

            foreach (var c in orderedTopToBottom)
                c.Dock = DockStyle.Top;

            foreach (var c in orderedTopToBottom)
                buttonsPanel.Controls.Remove(c);

            for (int i = orderedTopToBottom.Length - 1; i >= 0; i--)
                buttonsPanel.Controls.Add(orderedTopToBottom[i]);

            buttonsPanel.ResumeLayout(true);
        }

        private void ApplySidebarIcons()
        {
            TrySetButtonIcon(btnDashboard, "IconDashboard.png");
            TrySetButtonIcon(btnBoardingHouses, "IconBoardinghouse.png");
            TrySetButtonIcon(btnBHOwners, "BHOwnersIcon.png");
            TrySetButtonIcon(btnTenants, "IconTenants.png");
            TrySetButtonIcon(btnStudents, "IconTenants.png");
            TrySetButtonIcon(btnRooms, "IconRooms.png");
            TrySetButtonIcon(btnReservations, "IconRooms.png");
            TrySetButtonIcon(btnPayments, "IconPayments.png");
            TrySetButtonIcon(btnReports, "IconReports.png");
        }

        private void TrySetButtonIcon(Button button, string iconFile)
        {
            if (button == null) return;

            string path = Path.Combine(Application.StartupPath, "Icons", iconFile);
            if (!File.Exists(path)) return;

            button.AutoSize = false;
            button.Height = 80;
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.ImageAlign = ContentAlignment.MiddleLeft;
            button.TextImageRelation = TextImageRelation.ImageBeforeText;
            button.Padding = new Padding(12, 0, 0, 0);

            const int iconSize = 28;

            using (var original = Image.FromFile(path))
            {
                var resized = new Bitmap(iconSize, iconSize);
                using (var g = Graphics.FromImage(resized))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    g.DrawImage(original, 0, 0, iconSize, iconSize);
                }

                button.Image?.Dispose();
                button.Image = resized;
            }
        }

        private void ForceViewLayout(UserControl view)
        {
            // Make sure the view truly matches the final client size
            view.Dock = DockStyle.Fill;
            view.Margin = Padding.Empty;
            view.Padding = Padding.Empty;
            view.Bounds = contentPanel.ClientRectangle;

            view.PerformLayout();
            view.Refresh();

            foreach (Control c in view.Controls)
                FixLayoutRecursive(c);

            view.PerformLayout();
            view.Refresh();
        }

        private void FixLayoutRecursive(Control c)
        {
            if (c is TableLayoutPanel tlp)
            {
                tlp.Dock = DockStyle.Fill;
                tlp.Margin = Padding.Empty;
                tlp.Padding = Padding.Empty;
            }

            if (c is Panel p)
            {
                p.Margin = Padding.Empty;
                p.Padding = Padding.Empty;
            }

            if (c is Label lbl)
            {
                lbl.AutoSize = true;
                lbl.Anchor = AnchorStyles.None;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
            }

            foreach (Control child in c.Controls)
                FixLayoutRecursive(child);
        }

        private void HookNavButtons()
        {
            buttonsPanel.SizeChanged += (s, e) => RealignIndicator();
            buttonsPanel.Layout += (s, e) => RealignIndicator();
        }

        private void RealignIndicator()
        {
            if (_activeNavButton == null || activeIndicator == null)
                return;

            activeIndicator.Height = _activeNavButton.Height;
            activeIndicator.Top = _activeNavButton.Top;
            activeIndicator.Left = 0;
            activeIndicator.BringToFront();
        }

        private void SetActiveNavButton(Button btn)
        {
            if (btn == null || _activeNavButton == btn)
                return;

            if (_activeNavButton != null)
            {
                _activeNavButton.BackColor = _defaultBtnBackColor;
                _activeNavButton.ForeColor = _defaultBtnForeColor;
            }

            _activeNavButton = btn;
            btn.BackColor = _activeBtnBackColor;
            btn.ForeColor = _activeBtnForeColor;

            if (activeIndicator != null)
            {
                activeIndicator.Height = btn.Height;
                activeIndicator.Top = btn.Top;
                activeIndicator.Left = 0;
                activeIndicator.Visible = true;
                activeIndicator.BringToFront();
            }
        }

        public void OpenBoardingHouseDetailsFromOwner(int bhId)
        {
            ShowBoardingHousesView();
            _boardingHousesView?.OpenDetailsById(bhId);
        }

        public void OpenAddBoardingHouseFromOwner(long ownerId)
        {
            ShowBoardingHousesView();
            _boardingHousesView?.OpenAddModalForOwner(ownerId);
        }

        private void ShowPaymentsView()
        {
            SetActiveNavButton(btnPayments);

            if (_paymentsView == null || _paymentsView.IsDisposed)
                _paymentsView = new PaymentsView();

            _paymentsView.CurrentUserId = LoggedInUserId;
            LoadControl(_paymentsView);
        }


        public void OpenPaymentsForRental(int rentalId)
        {
            if (rentalId <= 0) return;

            ShowPaymentsView();

            BeginInvoke(new Action(() =>
            {
                _paymentsView?.OpenPaymentsFromRental(rentalId, autoSelectFirstRoom: true);
            }));
        }



        public void OpenRoomsFromBoardingHouse(int bhId)
        {
            ShowRoomsView();

            BeginInvoke(new Action(() =>
            {
                _roomsView?.OpenRoomsForBoardingHouse(bhId, autoSelectFirstRoom: true);
            }));
        }

        public void OpenRoomsForTenantRoom(int roomId)
        {
            if (roomId <= 0) return;

            ShowRoomsView();

            BeginInvoke(new Action(() =>
            {
                _roomsView?.OpenRoomsForTenantAssignedRoom(roomId);
            }));
        }


        public void OpenTenantDetailsFromRooms(int tenantId)
        {
            if (tenantId <= 0) return;

            SetActiveNavButton(btnTenants);

            var view = new TenantsView
            {
                CurrentUserId = LoggedInUserId
            };

            LoadControl(view);
            view.OpenDetailsByTenantId(tenantId);
        }

        public void OpenOccupantDetailsFromRooms(int occupantId)
        {
            if (occupantId <= 0) return;

            try
            {
                using var conn = DbConnectionFactory.CreateConnection();
                if (conn.State != ConnectionState.Open) conn.Open();

                string occType = "";
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT occupant_type FROM occupants WHERE id = @id LIMIT 1;";
                    cmd.Parameters.AddWithValue("@id", occupantId);
                    occType = cmd.ExecuteScalar()?.ToString() ?? "";
                }

                if (string.Equals(occType, "TENANT", StringComparison.OrdinalIgnoreCase))
                {
                    int tenantId = 0;
                    using (var cmd2 = conn.CreateCommand())
                    {
                        cmd2.CommandText = @"SELECT tenant_id FROM tenant_occupant_map WHERE occupant_id = @occId LIMIT 1;";
                        cmd2.Parameters.AddWithValue("@occId", occupantId);
                        var v = cmd2.ExecuteScalar();
                        tenantId = (v == null || v == DBNull.Value) ? 0 : Convert.ToInt32(v);
                    }

                    if (tenantId <= 0)
                    {
                        MessageBox.Show("Tenant mapping not found for this occupant.", "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    OpenTenantDetailsFromRooms(tenantId);
                    return;
                }

                if (string.Equals(occType, "STUDENT", StringComparison.OrdinalIgnoreCase))
                {
                    int studentId = 0;
                    using (var cmd3 = conn.CreateCommand())
                    {
                        cmd3.CommandText = @"SELECT student_id FROM student_occupant_map WHERE occupant_id = @occId LIMIT 1;";
                        cmd3.Parameters.AddWithValue("@occId", occupantId);
                        var v = cmd3.ExecuteScalar();
                        studentId = (v == null || v == DBNull.Value) ? 0 : Convert.ToInt32(v);
                    }

                    if (studentId <= 0)
                    {
                        MessageBox.Show("Student mapping not found for this occupant.", "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    SetActiveNavButton(btnStudents);

                    var view = new StudentsView
                    {
                        CurrentUserId = LoggedInUserId
                    };

                    LoadControl(view);
                    view.OpenDetailsByStudentId(studentId);
                    return;
                }

                MessageBox.Show("Unknown occupant type.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open occupant details.\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void OpenAddTenantModalFromRooms(int boardingHouseId)
        {
            SetActiveNavButton(btnTenants);

            var view = new TenantsView
            {
                CurrentUserId = LoggedInUserId
            };

            LoadControl(view);
            view.OpenAddTenantModal(boardingHouseId);
        }

        public void OpenRoomsViewFromTenants(int bhId, bool autoSelectFirstRoom)
        {
            ShowRoomsView();

            if (bhId <= 0)
                return;

            BeginInvoke(new Action(() =>
            {
                _roomsView?.OpenRoomsForBoardingHouse(bhId, autoSelectFirstRoom);
            }));
        }



        private void ShowBoardingHousesView()
        {
            SetActiveNavButton(btnBoardingHouses);

            if (_boardingHousesView == null || _boardingHousesView.IsDisposed)
                _boardingHousesView = new BoardingHousesView();

            LoadControl(_boardingHousesView);
        }

        private void ShowRoomsView()
        {
            SetActiveNavButton(btnRooms);

            if (_roomsView == null || _roomsView.IsDisposed)
                _roomsView = new RoomsView();

            _roomsView.CurrentUserId = LoggedInUserId;
            LoadControl(_roomsView);
        }


        private void contentPanel_Paint(object sender, PaintEventArgs e)
        {
            // intentionally left blank (designer-created event)
        }

        private void MainLayout_Load(object sender, EventArgs e)
        {
            // kept for designer hook; initialization already handled in constructor
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            _profileDropDown?.Close();
        }

        private void EnsureProfileDropDown()
        {
            if (_profileDropDown != null && !_profileDropDown.IsDisposed)
                return;

            if (dropDownPanel.Parent != null)
            {
                dropDownPanel.Parent.Controls.Remove(dropDownPanel);
            }

            dropDownPanel.Visible = true;
            dropDownPanel.Margin = Padding.Empty;
            dropDownPanel.Padding = Padding.Empty;

            _profileDropDownHost = new ToolStripControlHost(dropDownPanel)
            {
                Margin = Padding.Empty,
                Padding = Padding.Empty,
                AutoSize = false,
                Size = dropDownPanel.Size
            };

            _profileDropDown = new ToolStripDropDown
            {
                AutoClose = true,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };

            _profileDropDown.Items.Add(_profileDropDownHost);
        }

        private void HookSidebarSoundRecursive(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is Button btn)
                {
                    btn.Click -= SidebarSound_Click;
                    btn.Click += SidebarSound_Click;
                }

                if (c.HasChildren)
                    HookSidebarSoundRecursive(c);
            }
        }

        private void SidebarSound_Click(object? sender, EventArgs e)
        {
            SoundClicked.sidebarButton();
        }

        private void profileImg_Click(object sender, EventArgs e)
        {
            EnsureProfileDropDown();

            if (_profileDropDown != null && _profileDropDown.Visible)
            {
                _profileDropDown.Close();
            }
            else if (_profileDropDown != null)
            {
                var screenPoint = profileImg.PointToScreen(new Point(profileImg.Width - dropDownPanel.Width, profileImg.Height));
                _profileDropDown.Show(screenPoint);
            }
        }

        private void closeDPPanelBtn_Click(object sender, EventArgs e)
        {
            _profileDropDown?.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PerformLogout();
        }

        private void LogoutBtn_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PerformLogout();
            }
        }

        private void ProfileBtn_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PerformOpenProfileScreen();
            }
        }

        private void SettingsBtn_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PerformOpenSettingsScreen();
            }
        }

        private void PerformLogout()
        {
            if (_isLoggingOut) return;
            _isLoggingOut = true;

            _profileDropDown?.Close();

            Login? loginForm = null;
            foreach (Form form in Application.OpenForms)
            {
                if (form is Login existingLogin && !existingLogin.IsDisposed)
                {
                    loginForm = existingLogin;
                    break;
                }
            }

            loginForm ??= new Login();
            loginForm.Show();
            loginForm.WindowState = FormWindowState.Normal;
            loginForm.BringToFront();
            loginForm.Activate();
            this.Hide();
        }

        private void PerformOpenProfileScreen()
        {
            if (_isOpeningProfileScreen) return;
            _isOpeningProfileScreen = true;
            _profileDropDown?.Close();
            MessageBox.Show("Profile Screen");
            _isOpeningProfileScreen = false;
        }

        private void PerformOpenSettingsScreen()
        {
            if (_isOpeningSettingsScreen) return;
            _isOpeningSettingsScreen = true;
            _profileDropDown?.Close();
            MessageBox.Show("Settings Screen");
            _isOpeningSettingsScreen = false;
        }
    }
}
