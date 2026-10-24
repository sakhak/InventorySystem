using InventorySystem;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace InventorySystemCsharp
{
    public partial class User_list : Form
    {
        public User_list()
        {
            InitializeComponent();
        }

        // Fill grid view method
        void Filluserlist()
        {
            DBConnection _pichsu10 = new DBConnection();
            string sql = "SELECT first, last, username, phone,password, usertype FROM users";
            using (SqlConnection connection = _pichsu10.OpenConnection())
            using (SqlDataAdapter list = new SqlDataAdapter(sql, connection))
            {
                DataTable dtlist = new DataTable();
                list.Fill(dtlist);
                users_list.DataSource = dtlist;
            }
        }

        private void User_list_Load(object sender, EventArgs e)
        {
            Filluserlist();
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            Admin_home admin = new Admin_home();
            admin.Show();
            this.Close();
        }
    }
}
