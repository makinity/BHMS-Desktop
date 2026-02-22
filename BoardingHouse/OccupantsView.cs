using System;
using System.Windows.Forms;

namespace BoardingHouse
{
    public partial class OccupantsView : UserControl
    {
        private TenantsView? _tenantsView;
        private StudentsView? _studentsView;
        private bool _initialized;
        private int _currentUserId;

        public int CurrentUserId
        {
            get => _currentUserId;
            set
            {
                _currentUserId = value;
                if (_tenantsView != null) _tenantsView.CurrentUserId = value;
                if (_studentsView != null) _studentsView.CurrentUserId = value;
            }
        }

        public OccupantsView()
        {
            InitializeComponent();
            Load -= OccupantsView_Load;
            Load += OccupantsView_Load;
        }

        private void OccupantsView_Load(object? sender, EventArgs e)
        {
            InitializeOnce();
        }

        private void InitializeOnce()
        {
            if (_initialized) return;

            cbOccupantType.Items.Clear();
            cbOccupantType.Items.Add("TENANT");
            cbOccupantType.Items.Add("STUDENT");
            cbOccupantType.DropDownStyle = ComboBoxStyle.DropDownList;

            _tenantsView = new TenantsView
            {
                Dock = DockStyle.Fill,
                Visible = false,
                CurrentUserId = CurrentUserId
            };

            _studentsView = new StudentsView
            {
                Dock = DockStyle.Fill,
                Visible = false,
                CurrentUserId = CurrentUserId
            };

            pnlHost.SuspendLayout();
            pnlHost.Controls.Add(_studentsView);
            pnlHost.Controls.Add(_tenantsView);
            pnlHost.ResumeLayout();

            cbOccupantType.SelectedIndexChanged -= cbOccupantType_SelectedIndexChanged;
            cbOccupantType.SelectedIndexChanged += cbOccupantType_SelectedIndexChanged;

            cbOccupantType.SelectedItem = "TENANT";
            ShowTenantView();

            _initialized = true;
        }

        private void cbOccupantType_SelectedIndexChanged(object? sender, EventArgs e)
        {
            SwitchHostedView();
        }

        private void SwitchHostedView()
        {
            string selected = (cbOccupantType.SelectedItem?.ToString() ?? string.Empty).Trim().ToUpperInvariant();
            if (selected == "STUDENT")
            {
                ShowStudentView();
                return;
            }

            ShowTenantView();
        }

        private void ShowTenantView()
        {
            if (_tenantsView == null || _studentsView == null) return;

            _studentsView.Visible = false;
            _tenantsView.Visible = true;
            _tenantsView.BringToFront();
        }

        private void ShowStudentView()
        {
            if (_tenantsView == null || _studentsView == null) return;

            _tenantsView.Visible = false;
            _studentsView.Visible = true;
            _studentsView.BringToFront();
        }
    }
}
