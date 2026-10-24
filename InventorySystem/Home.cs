using InventorySystem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace InventorySystemCsharp
{
    public partial class Home : Form
    {
        public string ItemList = "";
        public float TotalPrice = 0;
        public string UpdateQuery = "";
        public int Totalitem = 0;

        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            FillComboBox();
            ItemList = "";
            TotalPrice = 0;
            UpdateQuery = "";
            Totalitem = 0;
            userdetail user = new userdetail();
            label9.Text = user.getUname();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            try
            {
                DBConnection _pichsu10 = new DBConnection();
                string sql = "SELECT * FROM spareparts WHERE model=@model AND part=@part";
                using (SqlConnection connection = _pichsu10.OpenConnection())
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@model", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@part", comboBox2.Text);

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        void FillComboBox()
        {
            DBConnection _pichsu10 = new DBConnection();
            string sql = "SELECT DISTINCT model FROM spareparts";
            using (SqlConnection connection = _pichsu10.OpenConnection())
            using (SqlDataAdapter sda = new SqlDataAdapter(sql, connection))
            {
                DataSet dt = new DataSet();
                sda.Fill(dt);

                comboBox1.DataSource = dt.Tables[0];
                comboBox1.DisplayMember = "model";
                comboBox1.ValueMember = "model";

                string sql1 = "SELECT DISTINCT part FROM spareparts";
                using (SqlConnection connection1 = _pichsu10.OpenConnection())
                using (SqlDataAdapter sda1 = new SqlDataAdapter(sql1, connection1))
                {
                    DataSet dt1 = new DataSet();
                    sda1.Fill(dt1);
                    comboBox2.DataSource = dt1.Tables[0];
                    comboBox2.DisplayMember = "part";
                    comboBox2.ValueMember = "part";
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();
                textBox5.Text = row.Cells[4].Value.ToString();
                textBox6.Text = row.Cells[5].Value.ToString();
            }
        }

       

        

        private void button1_Click(object sender, EventArgs e)
        {
            MyOrders orders = new MyOrders();
            this.Hide();
            orders.Show();
        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.Show();
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Add_To_Cart_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(textBox6.Text) >= int.Parse(textBox7.Text))
                {
                    
                    string orderDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    ItemList += textBox2.Text + " " + textBox3.Text + " " + textBox4.Text + " " + textBox5.Text + "*" + textBox7.Text + " - " + orderDate + Environment.NewLine;
                    TotalPrice += float.Parse(textBox5.Text) * float.Parse(textBox7.Text);
                    Totalitem += int.Parse(textBox7.Text);
                    UpdateQuery = "UPDATE spareparts SET instock= (instock-" + float.Parse(textBox7.Text) + ") WHERE id=" + textBox1.Text;
                    DBConnection _pichsu10 = new DBConnection();
                    SqlConnection con = _pichsu10.OpenConnection();
                    SqlCommand cmd = new SqlCommand(UpdateQuery, con);
                    object result = cmd.ExecuteNonQuery();

                    string msg = textBox1.Text + " " + textBox2.Text + " " + textBox3.Text + " " + textBox4.Text + "*" + textBox7.Text + " - " + orderDate;
                    MessageBox.Show(msg + Environment.NewLine + "Added to Cart");
                }
                else
                {
                    MessageBox.Show("Not Enough Items in Stock");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Enter in Correct Format");
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }
        private void Continue_Checkout_Click(object sender, EventArgs e)
        {
            if (this.ItemList == "")
            {
                MessageBox.Show("No Items Selected");
            }
            else
            {
                var childform = new Confirm();
                childform.MyParent = this;
                childform.Show();
                this.Enabled = false;
            }
        }
        public DateTime GetCurrentDateFromDB()
        {
            DBConnection _pichsu10 = new DBConnection();
            string sql = "SELECT GETDATE();";

            using (SqlConnection con = _pichsu10.OpenConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                object result = cmd.ExecuteScalar();
                return (result != null && result != DBNull.Value) ? Convert.ToDateTime(result) : DateTime.Now;
            }
        }
    }
}
