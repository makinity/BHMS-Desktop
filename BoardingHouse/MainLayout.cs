using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BoardingHouse
{
    public partial class MainLayout : Form
    {
        private Button? _activeNavButton;
        private Color _defaultBtnBackColor;
        private Color _defaultBtnForeColor;
        private readonly Color _activeBtnBackColor = Color.FromArgb(230, 230, 255);
        private readonly Color _activeBtnForeColor = Color.FromArgb(30, 60, 180);

        public int LoggedInUserId { get; }
        public MainLayout(int userId)
        {
            InitializeComponent();
            HookSidebarSoundRecursive(buttonsPanel);

            ApplySidebarIcons();
            FixSidebarOrder(); // initial pass
            HookNavButtons();
            _defaultBtnBackColor = btnDashboard.BackColor;
            _defaultBtnForeColor = btnDashboard.ForeColor;
            SetActiveNavButton(btnDashboard);

            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.Sizable;
            LoggedInUserId = userId;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (contentPanel.Controls.Count == 0)
            {
                LoadControl(new DashboardView());
            }

            FixSidebarOrder(); // final pass after layout
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            FixSidebarOrder();
        }

        private void LoadControl(UserControl view)
        {
            // hard reset contentPanel spacing
            contentPanel.Margin = Padding.Empty;
            contentPanel.Padding = Padding.Empty;

            // dispose existing
            foreach (Control c in contentPanel.Controls)
                c.Dispose();
            contentPanel.Controls.Clear();

            // add view first
            view.Dock = DockStyle.Fill;
            view.Margin = Padding.Empty;
            view.Padding = Padding.Empty;

            contentPanel.Controls.Add(view);

            // IMPORTANT:
            // Run the "force layout" AFTER WinForms has applied final sizes
            // (this is why it only looks correct after minimize/restore)
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

        private void BoardingHousesButton_Click(object sender, EventArgs e)
        {
            SetActiveNavButton((Button)sender);
            LoadControl(new BoardingHousesView());
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

        private void FixSidebarOrder()
        {
            buttonsPanel.SuspendLayout();

            // IMPORTANT:
            // DockStyle.Top stacks in reverse based on Controls order (last added appears on top).
            // So we remove and re-add in reverse so Dashboard ends up at the top.
            Control[] orderedTopToBottom =
            {
                btnDashboard,
                btnBoardingHouses,
                btnTenants,
                btnRooms,
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
            TrySetButtonIcon(btnTenants, "IconTenants.png");
            TrySetButtonIcon(btnRooms, "IconRooms.png");
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

            // Force layout now
            view.PerformLayout();
            view.Refresh();

            // Fix common designer issues: TableLayoutPanels not filling
            foreach (Control c in view.Controls)
                FixLayoutRecursive(c);

            // One more layout pass after fixing
            view.PerformLayout();
            view.Refresh();
        }

        private void FixLayoutRecursive(Control c)
        {
            // Make layout containers fill their parent
            if (c is TableLayoutPanel tlp)
            {
                tlp.Dock = DockStyle.Fill;
                tlp.Margin = Padding.Empty;
                tlp.Padding = Padding.Empty;
            }

            if (c is Panel p)
            {
                // optional, but helps if panels were left with margins
                p.Margin = Padding.Empty;
                p.Padding = Padding.Empty;
            }

            // Center labels if needed (only if they are meant to be centered)
            if (c is Label lbl)
            {
                // safe centering defaults (won't break most titles)
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


        private void contentPanel_Paint(object sender, PaintEventArgs e)
        {
            // intentionally left blank (designer-created event)
        }

        private void MainLayout_Load(object sender, EventArgs e)
        {
            // kept for designer hook; initialization already handled in constructor
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

    }
}
