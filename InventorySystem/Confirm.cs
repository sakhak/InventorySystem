using InventorySystem;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace InventorySystemCsharp
{
    public partial class Confirm : Form
    {
        public Home MyParent { get; set; }      
        public Confirm()
        {
            InitializeComponent();
        }

        private void Confirm_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = MyParent.ItemList;
        }

        private void Confirm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MyParent.Enabled = true;
        }     
        private void confirm_order_Click(object sender, EventArgs e)
        {
            DBConnection _pichsu10 = new DBConnection();
            try
            {
                string sql = "INSERT INTO [orders] ([user], details, orderDate, price, item) VALUES (@user, @details, @orderDate, @price, @item);";
                using (SqlConnection connection = _pichsu10.OpenConnection())
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    userdetail user = new userdetail();
                    cmd.Parameters.AddWithValue("@user", user.getUname());
                    cmd.Parameters.AddWithValue("@details", MyParent.ItemList);
                    cmd.Parameters.AddWithValue("@price", MyParent.TotalPrice);
                    cmd.Parameters.AddWithValue("@orderDate", GetdateFromDB());
                    cmd.Parameters.AddWithValue("@item", MyParent.Totalitem);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Success");
                    clear_order_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        public DateTime GetdateFromDB()
        {
            DBConnection _pichsu10 = new DBConnection();
            string sql = "SELECT GETDATE()"; 

            using (SqlConnection con = _pichsu10.OpenConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                object result = cmd.ExecuteScalar();
                return (result != null && result != DBNull.Value) ? Convert.ToDateTime(result) : DateTime.MinValue;
            }
        }

        private void clear_order_Click(object sender, EventArgs e)
        {
            MyParent.ItemList = "";
            MyParent.TotalPrice = 0;
            MyParent.Totalitem = 0;
            MyParent.UpdateQuery = "";
            this.Close();
        }
    }
}
