using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace BoardingHouse
{
    public partial class Login : Form
    {
        private int userId;

        public Login()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ✅ return user id (0 = failed)
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

            // ✅ store the id
            userId = AuthenticateUser(username, password);

            if (userId > 0)
            {
                LaunchMainLayout();
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
    }
}
