using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using InventorySystem;

namespace InventorySystemCsharp
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.Show();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (bunifuMetroTextbox1.Text != "" && bunifuMetroTextbox2.Text != "" && bunifuMetroTextbox3.Text != "" && bunifuMetroTextbox4.Text != "" && bunifuMetroTextbox5.Text != "" && bunifuMetroTextbox6.Text != "")
            {
                if (bunifuMetroTextbox5.Text == bunifuMetroTextbox6.Text)
                {
                    try
                    {
                        DBConnection _pichsu10 = new DBConnection();
                        string sql = "INSERT INTO users (first, last, username, phone, password) VALUES (@First, @Last, @Username, @Phone, @Password)";
                        using (SqlConnection connection = _pichsu10.OpenConnection())
                        using (SqlCommand cmd = new SqlCommand(sql, connection))
                        {
                            cmd.Parameters.AddWithValue("@First", bunifuMetroTextbox1.Text.Trim());
                            cmd.Parameters.AddWithValue("@Last", bunifuMetroTextbox2.Text.Trim());
                            cmd.Parameters.AddWithValue("@Username", bunifuMetroTextbox3.Text.Trim());
                            cmd.Parameters.AddWithValue("@Phone", bunifuMetroTextbox4.Text.Trim());
                            cmd.Parameters.AddWithValue("@Password", (bunifuMetroTextbox5.Text.Trim()));

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Success");
                            Login login = new Login();
                            login.Show();
                            this.Hide();
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Username Taken or Database Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Passwords don't match");
                }
            }
            else
            {
                MessageBox.Show("Fill all fields");
            }
        }      

        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuMetroTextbox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bunifuMetroTextbox2.Focus();
            }
        }

        private void bunifuMetroTextbox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bunifuMetroTextbox3.Focus();
            }
        }
        private void bunifuMetroTextbox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bunifuMetroTextbox4.Focus();
            }
        }
        private void bunifuMetroTextbox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bunifuMetroTextbox5.Focus();
            }
        }

        private void bunifuMetroTextbox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bunifuMetroTextbox6.Focus();
            }
        }
        private void chk_password_CheckedChanged(object sender, EventArgs e)
        {
            bunifuMetroTextbox5.isPassword = !chk_password.Checked;
        }

        private void chk_repassword_CheckedChanged(object sender, EventArgs e)
        {
            bunifuMetroTextbox6.isPassword = !chk_repassword.Checked;
        }
    }
}
