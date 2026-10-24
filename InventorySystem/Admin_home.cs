using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; // Changed to use SQL Server
using System.Security.Cryptography;
using InventorySystem;

namespace InventorySystemCsharp
{
    public partial class Admin_home : Form
    {
        public Admin_home()
        {
            InitializeComponent();
            slide_panel.Height = dashboard_btn.Height;
            dashboard_panel.BringToFront();
        }

        // logout button
        private void logout_btn_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            User_list userlist = new User_list();
            userlist.Close();
            this.Close();

            login.Show();
        }

        // close button
        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //------------------------------------navigation pane button events
        // dashboard button
        private void dashboard_btn_Click(object sender, EventArgs e)
        {
            slide_panel.Height = dashboard_btn.Height;
            slide_panel.Top = dashboard_btn.Top;
            dashboard_panel.BringToFront();
        }

        // add manager button
        private void add_manager_btn_Click(object sender, EventArgs e)
        {
            slide_panel.Height = add_manager_btn.Height;
            slide_panel.Top = add_manager_btn.Top;
            add_manager_panel.BringToFront();
            User_list user = new User_list();
            user.Close();
        }

        // user list button
        private void user_list_btn_Click(object sender, EventArgs e)
        {
            slide_panel.Height = user_list_btn.Height;
            slide_panel.Top = user_list_btn.Top;
            User_list users_list = new User_list();
            users_list.Show();
            this.Hide();
        }

        ////////////////////////////////////---------------DASHBOARD PANEL FUNCTIONS----------///////////////////////////////////////

        // The method executes when the user home button clicks
        private void user_home_btn_Click(object sender, EventArgs e)
        {
            Home user_home = new Home();
            user_home.Show();
        }

        private void manager_home_btn_Click(object sender, EventArgs e)
        {
            string userRole = GetCurrentUserRole(); // Hypothetical method to retrieve user role
            if (userRole == "manager")
            {
                Manager_home manager = new Manager_home();
                manager.Show();
            }
            else
            {
                MessageBox.Show("You do not have permission to access this section.");
            }
        }

        private string GetCurrentUserRole()
        {
            return "manager";
        }

        private void item_check_btn_Click(object sender, EventArgs e)
        {
            Manager_home manager = new Manager_home();
            manager.Show();
        }

        private void check_orders_btn_Click(object sender, EventArgs e)
        {
            Manager_home manager = new Manager_home();
            manager.Show();
        }

        ////////////////////////////////////---------------Add manager PANEL FUNCTIONS----------///////////////////////////////////////

        private void Mregister_btn_Click(object sender, EventArgs e)
        {
            if (MfnameTxt.Text != "" && MlnameTxt.Text != "" && MusernameTxt.Text != "" && MphonenumTxt.Text != "" && MpassTxt.Text != "" && MrepassTxt.Text != "" && typecomboTxt.Text != "")
            {
                if (MpassTxt.Text == MrepassTxt.Text)
                {
                    try
                    {
                        DBConnection _pichsu10 = new DBConnection();
                        string sql = "INSERT INTO [users]([first], [last], [username], [phone], [password], [usertype]) VALUES(@First, @Last, @Username, @Phone, @Password, @UserType)";
                        using (SqlConnection connection = _pichsu10.OpenConnection())
                        using (SqlCommand cmd = new SqlCommand(sql, connection))
                        {
                            cmd.Parameters.AddWithValue("@First", MfnameTxt.Text.Trim());
                            cmd.Parameters.AddWithValue("@Last", MlnameTxt.Text.Trim());
                            cmd.Parameters.AddWithValue("@Username", MusernameTxt.Text.Trim());
                            cmd.Parameters.AddWithValue("@Phone", MphonenumTxt.Text.Trim());
                            cmd.Parameters.AddWithValue("@Password", (MpassTxt.Text.Trim()));
                            cmd.Parameters.AddWithValue("@UserType", typecomboTxt.Text.Trim());

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("New Manager account has been created successfully!");
                            ClearFields();
                        }
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show("This Username is already taken!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Given password doesn't match!!");
                }
            }
            else
            {
                MessageBox.Show("Fill all fields");
            }
        }
        private void ClearFields()
        {
            MfnameTxt.Text = "";
            MlnameTxt.Text = "";
            MusernameTxt.Text = "";
            MphonenumTxt.Text = "";
            MpassTxt.Text = "";
            MrepassTxt.Text = "";
            typecomboTxt.SelectedIndex = -1;
        }


        private void btnDashboard02_Click(object sender, EventArgs e)
        {
            slide_panel.Height = dashboard_btn.Height;
            slide_panel.Top = dashboard_btn.Top;
            dashboard_panel.BringToFront();
        }

        private void MfnameTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MlnameTxt.Focus();
            }
        }

        private void MlnameTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MusernameTxt.Focus();
            }
        }

        private void MusernameTxt_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                MphonenumTxt.Focus();
            }
        }

        private void MphonenumTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MpassTxt.Focus();
            }
        }

        private void MpassTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MrepassTxt.Focus();
            }
        }

        private void MrepassTxt_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                typecomboTxt.Focus();
            }
        }
    }
}
