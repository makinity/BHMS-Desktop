using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BoardingHouse
{
    public partial class Login : Form
    {
        private int userId;

        public Login()
        {
            InitializeComponent();
            StyleLoginButton();
            LoginBtn.MouseEnter += LoginBtn_MouseEnter;
            LoginBtn.MouseLeave += LoginBtn_MouseLeave;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private int AuthenticateUser(string username, string password)
        {
            try
            {
                using (var conn = DbConnectionFactory.CreateConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT idusers
                        FROM users
                        WHERE username = @username
                          AND password = @password
                        LIMIT 1;
                    ";

                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    var result = cmd.ExecuteScalar();
                    return (result == null || result == DBNull.Value)
                        ? 0
                        : Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Database error:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return 0;
            }
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            string username = usernametxt.Text.Trim();
            string password = passwordtxt.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter username and password.", "Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            userId = AuthenticateUser(username, password);

            if (userId > 0)
            {
                LaunchMainLayout();
                usernametxt.Clear();
                passwordtxt.Clear();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LaunchMainLayout()
        {
            Hide();
            var layout = new MainLayout(userId);
            layout.FormClosed += (s, args) => Application.Exit();
            layout.Show();
        }


        private void StyleLoginButton()
        {
            LoginBtn.FlatStyle = FlatStyle.Flat;
            LoginBtn.FlatAppearance.BorderSize = 0;
            LoginBtn.ForeColor = Color.White;
            LoginBtn.Cursor = Cursors.Hand;
        }

        private void LoginBtn_MouseEnter(object sender, EventArgs e)
        {
            LoginBtn.BackColor = Color.FromArgb(0, 150, 255);
        }

        private void LoginBtn_MouseLeave(object sender, EventArgs e)
        {
            LoginBtn.BackColor = Color.FromArgb(0, 120, 215);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Register open = new Register();
            this.Hide();
            open.Show();
        }
    }
}
