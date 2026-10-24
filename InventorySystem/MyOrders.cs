using InventorySystem;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace InventorySystemCsharp
{
    public partial class MyOrders : Form
    {
        public MyOrders()
        {
            InitializeComponent();
        }

        private void MyOrders_Load(object sender, EventArgs e)
        {
            userdetail user = new userdetail();
            label9.Text = user.getUname();
            FillMyOrders();
        }

        void FillMyOrders()
        {
            try
            {
                DBConnection _pichsu10 = new DBConnection();
                string sql = "SELECT id, details, price, paid FROM orders WHERE [user] = @user";
                using (SqlConnection connection = _pichsu10.OpenConnection())
                using (SqlDataAdapter sda = new SqlDataAdapter(sql, connection))
                {
                    sda.SelectCommand.Parameters.AddWithValue("@user", label9.Text);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    DBConnection _pichsu10 = new DBConnection();
                    string sql = "DELETE FROM orders WHERE id = @id";
                    using (SqlConnection connection = _pichsu10.OpenConnection())
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Order Deleted");
                        FillMyOrders();
                    }
                }
                else
                {
                    MessageBox.Show("No item Selected");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            this.Close();
            home.Show();
        }
    }
}
