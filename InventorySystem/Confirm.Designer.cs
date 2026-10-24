namespace InventorySystemCsharp
{
    partial class Confirm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.confirm_order = new System.Windows.Forms.Button();
            this.clear_order = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.bunifuElipse2 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.bunifuElipse3 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.richTextBox1.Location = new System.Drawing.Point(31, 60);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(477, 264);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // confirm_order
            // 
            this.confirm_order.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(179)))), ((int)(((byte)(75)))));
            this.confirm_order.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.confirm_order.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirm_order.ForeColor = System.Drawing.Color.White;
            this.confirm_order.Location = new System.Drawing.Point(310, 337);
            this.confirm_order.Name = "confirm_order";
            this.confirm_order.Size = new System.Drawing.Size(198, 49);
            this.confirm_order.TabIndex = 30;
            this.confirm_order.Text = "Confirm Order";
            this.confirm_order.UseVisualStyleBackColor = false;
            this.confirm_order.Click += new System.EventHandler(this.confirm_order_Click);
            // 
            // clear_order
            // 
            this.clear_order.BackColor = System.Drawing.Color.DarkRed;
            this.clear_order.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clear_order.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clear_order.ForeColor = System.Drawing.Color.White;
            this.clear_order.Location = new System.Drawing.Point(31, 337);
            this.clear_order.Name = "clear_order";
            this.clear_order.Size = new System.Drawing.Size(198, 49);
            this.clear_order.TabIndex = 31;
            this.clear_order.Text = "Clear Order";
            this.clear_order.UseVisualStyleBackColor = false;
            this.clear_order.Click += new System.EventHandler(this.clear_order_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(110)))), ((int)(((byte)(92)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(545, 26);
            this.panel1.TabIndex = 32;
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 25;
            this.bunifuElipse1.TargetControl = this.clear_order;
            // 
            // bunifuElipse2
            // 
            this.bunifuElipse2.ElipseRadius = 25;
            this.bunifuElipse2.TargetControl = this.confirm_order;
            // 
            // bunifuElipse3
            // 
            this.bunifuElipse3.ElipseRadius = 35;
            this.bunifuElipse3.TargetControl = this;
            // 
            // Confirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 405);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.clear_order);
            this.Controls.Add(this.confirm_order);
            this.Controls.Add(this.richTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "Confirm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Confirm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Confirm_FormClosed);
            this.Load += new System.EventHandler(this.Confirm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button confirm_order;
        private System.Windows.Forms.Button clear_order;
        private System.Windows.Forms.Panel panel1;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse2;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse3;
    }
}