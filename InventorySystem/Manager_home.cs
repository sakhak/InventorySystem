using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data.SqlClient;
using InventorySystem;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;



namespace InventorySystemCsharp
{
    public partial class Manager_home : Form
    {
        public Home MyParent { get; set; }
        public Manager_home()
        {
            InitializeComponent();
            slide_panel.Height = add.Height;
            slide_panel.Top = add.Top;
            additem_panel.BringToFront();
        }

        private void Manager_home_Load(object sender, EventArgs e)
        {
            itemcode.Enabled = false;
            itemcode.Text = "Id Auto Number";
            FillGridView();

            u_itemcodeTxt.Enabled = false;
            u_itemcodeTxt.Text = "Id Auto Number";
            FillUpdateGridView();

            d_itemcodeTxt.Enabled = false;
            d_itemcodeTxt.Text = "Id Auto Number";
            FilldeleteGridView();

            p_order_idTxt.Enabled = false;
            p_order_idTxt.Text = "Id Auto Number";
            FillPaidGridView();

            unp_orderidTxt.Enabled = false;
            unp_orderidTxt.Text = "Id Auto Number";
            FillUnpaidGridView();

        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Close();
            login.Show();
        }

        private void add_Click(object sender, EventArgs e)
        {
            slide_panel.Height = add.Height;
            slide_panel.Top = add.Top;
            additem_panel.BringToFront();
            FillGridView();
            model.Clear();
            part.Clear();
            price.Clear();
            instock.Clear();
            comboBox1.SelectedIndex = -1;
            itemcode.Enabled = false;
            itemcode.Text = "Id Auto Number";
        }
        private void update_Click(object sender, EventArgs e)
        {
            slide_panel.Height = update.Height;
            slide_panel.Top = update.Top;
            updateitems_panel.BringToFront();
            FillUpdateGridView();
            u_modelTxt.Clear();
            u_partTxt.Clear();
            u_priceTxt.Clear();
            u_stockTxt.Clear();
            u_typeCombo.SelectedIndex = -1;
            u_itemcodeTxt.Enabled = false;
            u_itemcodeTxt.Text = "Id Auto Number";
        }
        private void delete_Click(object sender, EventArgs e)
        {
            slide_panel.Height = delete.Height;
            slide_panel.Top = delete.Top;
            deleteitem_panel.BringToFront();
            FilldeleteGridView();
            d_modelTxt.Clear();
            d_partTxt.Clear();
            d_priceTxt.Clear();
            d_instockTxt.Clear();
            d_typeCombo.SelectedIndex = -1;
            d_itemcodeTxt.Enabled = false;
            d_itemcodeTxt.Text = "Id Auto Number";
        }

        private void paid_orders_Click(object sender, EventArgs e)
        {
            slide_panel.Height = paid_orders.Height;
            slide_panel.Top = paid_orders.Top;
            paid_orders_panel.BringToFront();
            FillPaidGridView();
            p_order_detailsTxt.Clear();
            p_partTxt.Clear();
            p_order_priceTxt.Clear();
            p_order_paidTxt.Clear();
            p_order_idTxt.Enabled = false;
            p_order_idTxt.Text = "Id Auto Number";
        }

        private void unpaid_orders_Click(object sender, EventArgs e)
        {

            slide_panel.Height = unpaid_orders.Height;
            slide_panel.Top = unpaid_orders.Top;
            unp_order_panel.BringToFront();
            FillUnpaidGridView();
            unp_orderidTxt.Enabled = false;
            unp_orderidTxt.Text = "Id Auto Number";
            unp_orderdetailsTxt.Clear();
            unp_partTxt.Clear();
            unp_priceTxt.Clear();
            unp_ispaidTxt.Clear();
        }

        ////////////////////////////////////---------------INPUT ITEMS PANEL FUNCTIONS----------///////////////////////////////////////

        private void additem_Click(object sender, EventArgs e)
        {
            if (model.Text != "" && part.Text != "" && comboBox1.Text != "" && price.Text != "" && instock.Text != "")
            {
                try
                {
                    DBConnection _pichsu10 = new DBConnection();
                    string sql = "INSERT INTO spareparts (model, part, type, price, instock) VALUES (@model, @part, @type, @price, @instock)";
                    using (SqlConnection connection = _pichsu10.OpenConnection())
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@model", model.Text.Trim());
                        cmd.Parameters.AddWithValue("@part", part.Text.Trim());
                        cmd.Parameters.AddWithValue("@type", comboBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@price", price.Text.Trim());
                        cmd.Parameters.AddWithValue("@instock", instock.Text.Trim());
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("The item has been added successfully!");

                        model.Clear();
                        part.Clear();
                        price.Clear();
                        instock.Clear();
                        comboBox1.SelectedIndex = -1;
                        FillGridView();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("You should fill in all fields.");
            }
        }
        void FillGridView()
        {
            DBConnection _pichsu10 = new DBConnection();
            string sql = "SELECT * FROM spareparts";
            using (SqlConnection connection = _pichsu10.OpenConnection())
            using (SqlDataAdapter sda = new SqlDataAdapter(sql, connection))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);
                itemlist.DataSource = dt;
            }
        }

        private void itemlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.itemlist.Rows[e.RowIndex];
                itemcode.Text = row.Cells[0].Value.ToString();
                model.Text = row.Cells[1].Value.ToString();
                part.Text = row.Cells[2].Value.ToString();
                comboBox1.Text = row.Cells[3].Value.ToString();
                price.Text = row.Cells[4].Value.ToString();
                instock.Text = row.Cells[5].Value.ToString();
            }
        }

        ////////////////////////////////////---------------UPDATE ITEMS PANEL FUNCTIONS----------///////////////////////////////////////
        private void u_itemBtn_Click(object sender, EventArgs e)
        {
            if (u_modelTxt.Text != "" && u_partTxt.Text != "" && u_typeCombo.Text != "" && u_priceTxt.Text != "" && u_stockTxt.Text != "")
            {
                try
                {
                    DBConnection _pichsu10 = new DBConnection();
                    string sql = "UPDATE spareparts SET model = @model, part = @part, type = @type, price = @price, instock = @instock WHERE id = @id";
                    using (SqlConnection connection = _pichsu10.OpenConnection())
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@model", u_modelTxt.Text);
                        cmd.Parameters.AddWithValue("@part", u_partTxt.Text);
                        cmd.Parameters.AddWithValue("@type", u_typeCombo.Text);
                        cmd.Parameters.AddWithValue("@price", u_priceTxt.Text);
                        cmd.Parameters.AddWithValue("@instock", u_stockTxt.Text);
                        cmd.Parameters.AddWithValue("@id", u_itemcodeTxt.Text);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("The item details are updated successfully!");

                        u_itemcodeTxt.Clear();
                        u_itemcodeTxt.Enabled = false;
                        u_itemcodeTxt.Text = "Id Auto Number";
                        u_modelTxt.Clear();
                        u_partTxt.Clear();
                        u_priceTxt.Clear();
                        u_stockTxt.Clear();
                        u_typeCombo.SelectedIndex = -1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("You should select an item before updating!");
            }
        }


        void FillUpdateGridView()
        {
            DBConnection _pichsu10 = new DBConnection();
            string sql = "SELECT * FROM spareparts";
            using (SqlConnection connection = _pichsu10.OpenConnection())
            using (SqlDataAdapter u_sda = new SqlDataAdapter(sql, connection))
            {
                DataTable u_dt = new DataTable();
                u_sda.Fill(u_dt);
                u_dataGridView.DataSource = u_dt;
            }
        }

        private void u_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.u_dataGridView.Rows[e.RowIndex];
                u_itemcodeTxt.Text = row.Cells[0].Value.ToString();
                u_modelTxt.Text = row.Cells[1].Value.ToString();
                u_partTxt.Text = row.Cells[2].Value.ToString();
                u_typeCombo.Text = row.Cells[3].Value.ToString();
                u_priceTxt.Text = row.Cells[4].Value.ToString();
                u_stockTxt.Text = row.Cells[5].Value.ToString();
            }
        }

        ////////////////////////////////////---------------DELETE ITEMS PANEL FUNCTIONS----------///////////////////////////////////////
        private void del_item_btn_Click(object sender, EventArgs e)
        {
            if (d_modelTxt.Text != "" && d_partTxt.Text != "" && d_typeCombo.Text != "" && d_priceTxt.Text != "" && d_instockTxt.Text != "")
            {
                try
                {
                    DBConnection _pichsu10 = new DBConnection();
                    string sql = "DELETE FROM spareparts WHERE id = @itemCode";
                    using (SqlConnection connection = _pichsu10.OpenConnection())
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@itemCode", d_itemcodeTxt.Text);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("The item has been successfully deleted!");

                        d_itemcodeTxt.Clear();
                        d_itemcodeTxt.Enabled = false;
                        d_itemcodeTxt.Text = "Id Auto Number";
                        d_modelTxt.Clear();
                        d_partTxt.Clear();
                        d_priceTxt.Clear();
                        d_instockTxt.Clear();
                        d_typeCombo.SelectedIndex = -1;
                        FilldeleteGridView();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("You should select a row before deleting!");
            }
        }
        void FilldeleteGridView()
        {
            DBConnection _pichsu10 = new DBConnection();
            string sql = "SELECT * FROM spareparts";
            using (SqlConnection connection = _pichsu10.OpenConnection())
            using (SqlDataAdapter sda = new SqlDataAdapter(sql, connection))
            {
                try
                {                 
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    d_item_dataGridView.DataSource = dt;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        private void d_item_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.d_item_dataGridView.Rows[e.RowIndex];
                d_itemcodeTxt.Text = row.Cells[0].Value.ToString();
                d_modelTxt.Text = row.Cells[1].Value.ToString();
                d_partTxt.Text = row.Cells[2].Value.ToString();
                d_typeCombo.Text = row.Cells[3].Value.ToString();
                d_priceTxt.Text = row.Cells[4].Value.ToString();
                d_instockTxt.Text = row.Cells[5].Value.ToString();
            }
        }


        ////////////////////////////////////---------------PAID ORDES PANEL FUNCTIONS----------///////////////////////////////////////

        private void make_unPaid_btn_Click(object sender, EventArgs e)
        {
            if (p_order_idTxt.Text != "" && p_order_detailsTxt.Text != "")
            {
                try
                {
                    DBConnection _pichsu10 = new DBConnection();
                    string sql = "UPDATE orders SET paid = @paid WHERE id = @id";
                    using (SqlConnection connection = _pichsu10.OpenConnection())
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@paid", "no");
                        cmd.Parameters.AddWithValue("@id", p_order_idTxt.Text);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("The Order was marked as Unpaid & sent to the Unpaid table!");
                        p_order_idTxt.Clear();
                        p_order_idTxt.Enabled = false;
                        p_order_idTxt.Text = "Id Auto Number";
                        p_order_detailsTxt.Clear();
                        p_partTxt.Clear();
                        p_order_priceTxt.Clear();
                        p_order_paidTxt.Clear();
                        p_order_timeTxt.Clear();
                        p_order_itemTxt.Clear();

                        FillPaidGridView();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("First...you should select an order to make changes");
            }
        }

        void FillPaidGridView()
        {
            string status = "yes";
            DBConnection _pichsu10 = new DBConnection();
            string sql = "SELECT * FROM orders WHERE paid = @status";
            using (SqlConnection connection = _pichsu10.OpenConnection())
            using (SqlDataAdapter p_sda = new SqlDataAdapter(sql, connection))
            {
                p_sda.SelectCommand.Parameters.AddWithValue("@status", status);
                DataTable p_dt = new DataTable();
                p_sda.Fill(p_dt);
                paid_dataGridView1.DataSource = p_dt;
            }
        }

        private void paid_dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.paid_dataGridView1.Rows[e.RowIndex];
                p_order_idTxt.Text = row.Cells[0].Value.ToString();
                p_order_detailsTxt.Text = row.Cells[1].Value.ToString();
                p_partTxt.Text = row.Cells[2].Value.ToString();
                p_order_priceTxt.Text = row.Cells[3].Value.ToString();
                p_order_paidTxt.Text = row.Cells[4].Value.ToString();
                p_order_timeTxt.Text = row.Cells[5].Value.ToString();
                p_order_itemTxt.Text = row.Cells[6].Value.ToString();
            }
        }

        private void cancel_order_btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(p_order_idTxt.Text) && !string.IsNullOrEmpty(p_order_itemTxt.Text))
            {
                try
                {
                    DBConnection _pichsu10 = new DBConnection();
                    string sql = "UPDATE orders SET paid = @status WHERE id = @orderId";
                    using (SqlConnection connection = _pichsu10.OpenConnection())
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@status", "cancelled");
                        cmd.Parameters.AddWithValue("@orderId", p_order_idTxt.Text);
                        cmd.ExecuteNonQuery();
                    }
                    plusitem();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("First... you should select an order to cancel!");
            }
        }
        private void ClearOrderFields()
        {
            p_order_idTxt.Clear();
            p_order_idTxt.Enabled = false;
            p_order_idTxt.Text = "Id Auto Number";
            p_order_detailsTxt.Clear();
            p_partTxt.Clear();
            p_order_priceTxt.Clear();
            p_order_paidTxt.Clear();
            p_order_timeTxt.Clear();
            p_order_itemTxt.Clear();

            FillPaidGridView();
        }

        void plusitem()
        {
            try
            {
                if (int.TryParse(p_order_itemTxt.Text, out int instock) && int.TryParse(p_order_idTxt.Text, out int itemId))
                {
                    DBConnection _pichsu10 = new DBConnection();
                    string sql = "UPDATE spareparts SET instock = instock + @instock WHERE id = @id";
                    using (SqlConnection connection = _pichsu10.OpenConnection())
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@instock", instock);
                        cmd.Parameters.AddWithValue("@id", itemId.ToString());

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Stock has been successfully updated!");
                            ClearOrderFields();
                        }
                        else
                        {
                            MessageBox.Show($"Stock update failed! Item ID ({itemId}) not found in spareparts.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"Invalid values! itemId={p_order_idTxt.Text}, instock={p_order_itemTxt.Text}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating stock: " + ex.Message);
            }
        }



        ////////////////////////////////////---------------UNPAID ORDES PANEL FUNCTIONS----------///////////////////////////////////////
        private void unp_make_btn_Click(object sender, EventArgs e)
        {
            if (unp_orderidTxt.Text != "" && unp_orderdetailsTxt.Text != "")
            {
                try
                {
                    DBConnection _pichsu10 = new DBConnection();
                    string sql = "UPDATE [orders] SET [paid] = @Paid WHERE [id] = @OrderId";
                    using (SqlConnection connection = _pichsu10.OpenConnection())
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@Paid", "yes");
                        cmd.Parameters.AddWithValue("@OrderId", unp_orderidTxt.Text);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("The Order was marked as Paid & sent to the paid order table!");
                        unp_orderidTxt.Clear();
                        unp_orderidTxt.Enabled = false;
                        unp_orderidTxt.Text = "Id Auto Number";
                        unp_orderdetailsTxt.Clear();
                        unp_partTxt.Clear();
                        unp_priceTxt.Clear();
                        unp_ispaidTxt.Clear();
                        FillUnpaidGridView();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("First...you should select an order to make changes!");
            }
        }

        void FillUnpaidGridView()
        {
            String status = "no";
            DBConnection _pichsu10 = new DBConnection();
            string sql = "SELECT * FROM orders WHERE paid = @status";
            using (SqlConnection connection = _pichsu10.OpenConnection())
            using (SqlDataAdapter unp_sda = new SqlDataAdapter(sql, connection))
            {
                unp_sda.SelectCommand.Parameters.AddWithValue("@status", status);
                DataTable unp_dt = new DataTable();
                unp_sda.Fill(unp_dt);
                unp_dataGridView.DataSource = unp_dt;
            }
        }

        private void unp_dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.unp_dataGridView.Rows[e.RowIndex];
                unp_orderidTxt.Text = row.Cells[0].Value.ToString();
                unp_orderdetailsTxt.Text = row.Cells[1].Value.ToString();
                unp_partTxt.Text = row.Cells[2].Value.ToString();
                unp_priceTxt.Text = row.Cells[3].Value.ToString();
                unp_ispaidTxt.Text = row.Cells[4].Value.ToString();
            }
        }

        private void unp_cancelorder_btn_Click(object sender, EventArgs e)
        {
            if (unp_orderidTxt.Text != "" && unp_orderdetailsTxt.Text != "")
            {
                try
                {
                    DBConnection _pichsu10 = new DBConnection();
                    string sql = "UPDATE orders SET paid = @paid WHERE id = @orderId";
                    using (SqlConnection connection = _pichsu10.OpenConnection())
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@paid", "cancelled");
                        cmd.Parameters.AddWithValue("@orderId", unp_orderidTxt.Text);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("The Order was marked as Cancelled & sent to the Cancelled orders table !");
                    unp_orderidTxt.Clear();
                    unp_orderidTxt.Enabled = false;
                    unp_orderidTxt.Text = "Id Auto Number";
                    unp_orderdetailsTxt.Clear();
                    unp_partTxt.Clear();
                    unp_priceTxt.Clear();
                    unp_ispaidTxt.Clear();
                    FillUnpaidGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("First....u should select an order to cancel !");
            }
        }

        public void exportgridtopdf(DataGridView dgw, string filename)
        {
            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
            PdfPTable pdftable = new PdfPTable(dgw.Columns.Count);
            pdftable.DefaultCell.Padding = 3;
            pdftable.WidthPercentage = 100;
            pdftable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdftable.DefaultCell.BorderWidth = 1;

            iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
            foreach (DataGridViewColumn column in dgw.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, text));
                cell.BackgroundColor = new iTextSharp.text.Color(240, 240, 240);
                pdftable.AddCell(cell);
            }
            foreach (DataGridViewRow row in dgw.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    pdftable.AddCell(new Phrase(cell.Value.ToString(), text));
                }
            }

            var savefiledialoge = new SaveFileDialog();
            savefiledialoge.FileName = filename;
            savefiledialoge.DefaultExt = ".pdf";
            if (savefiledialoge.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefiledialoge.FileName, FileMode.Create))
                {
                    Document pdfdoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfdoc, stream);
                    pdfdoc.Open();
                    pdfdoc.Add(pdftable);
                    pdfdoc.Close();
                    stream.Close();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            exportgridtopdf(itemlist, "item-List Report");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            exportgridtopdf(unp_dataGridView, "unpaid order-List Report");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            exportgridtopdf(paid_dataGridView1, "Paid order-List Report");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }       
    }
}
