using InventorySystem;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace InventorySystemCsharp
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignUp signup = new SignUp();
            signup.Show();
            this.Hide();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            try
            {
                DBConnection _pichsu10 = new DBConnection();
                string sql = "SELECT * FROM users WHERE username=@username AND password=@password";
                using (SqlConnection connection = _pichsu10.OpenConnection())
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {

                    // Use parameters to prevent SQL Injection
                    cmd.Parameters.AddWithValue("@username", bunifuMetroTextbox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", (bunifuMetroTextbox2.Text.Trim()));

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                userdetail user = new userdetail();
                                user.setUname(reader["username"].ToString());

                                string userType = reader["usertype"].ToString();
                                if (userType == "member")
                                {
                                    Home home = new Home();
                                    this.Hide();
                                    home.Show();
                                }
                                else if (userType == "manager")
                                {
                                    Manager_home manager = new Manager_home();
                                    this.Hide();
                                    manager.Show();
                                }
                                else if (userType == "admin")
                                {
                                    Admin_home admin = new Admin_home();
                                    this.Hide();
                                    admin.Show();
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Check Again");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }     
        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ckhpassword_CheckedChanged(object sender, EventArgs e)
        {
            bunifuMetroTextbox2.isPassword = !ckhpassword.Checked;
        }

        private void bunifuMetroTextbox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bunifuMetroTextbox2.Focus();
            }
        }
    }
}
