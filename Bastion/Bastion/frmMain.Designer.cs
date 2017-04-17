namespace Bastion
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tabBastion = new System.Windows.Forms.TabControl();
            this.tpgGames = new System.Windows.Forms.TabPage();
            this.tpgRentals = new System.Windows.Forms.TabPage();
            this.tpgPayments = new System.Windows.Forms.TabPage();
            this.tpgCustomers = new System.Windows.Forms.TabPage();
            this.tabImages = new System.Windows.Forms.ImageList(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.tabBastion.SuspendLayout();
            this.tpgGames.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabBastion
            // 
            this.tabBastion.Controls.Add(this.tpgGames);
            this.tabBastion.Controls.Add(this.tpgRentals);
            this.tabBastion.Controls.Add(this.tpgPayments);
            this.tabBastion.Controls.Add(this.tpgCustomers);
            this.tabBastion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabBastion.ImageList = this.tabImages;
            this.tabBastion.ItemSize = new System.Drawing.Size(83, 45);
            this.tabBastion.Location = new System.Drawing.Point(0, 0);
            this.tabBastion.Name = "tabBastion";
            this.tabBastion.SelectedIndex = 0;
            this.tabBastion.Size = new System.Drawing.Size(1350, 729);
            this.tabBastion.TabIndex = 0;
            // 
            // tpgGames
            // 
            this.tpgGames.Controls.Add(this.button4);
            this.tpgGames.Controls.Add(this.button3);
            this.tpgGames.Controls.Add(this.button2);
            this.tpgGames.Controls.Add(this.button1);
            this.tpgGames.Controls.Add(this.dataGridView1);
            this.tpgGames.ImageIndex = 0;
            this.tpgGames.Location = new System.Drawing.Point(4, 49);
            this.tpgGames.Name = "tpgGames";
            this.tpgGames.Padding = new System.Windows.Forms.Padding(3);
            this.tpgGames.Size = new System.Drawing.Size(1342, 676);
            this.tpgGames.TabIndex = 0;
            this.tpgGames.Text = "Games";
            this.tpgGames.UseVisualStyleBackColor = true;
            // 
            // tpgRentals
            // 
            this.tpgRentals.ImageIndex = 1;
            this.tpgRentals.Location = new System.Drawing.Point(4, 49);
            this.tpgRentals.Name = "tpgRentals";
            this.tpgRentals.Padding = new System.Windows.Forms.Padding(3);
            this.tpgRentals.Size = new System.Drawing.Size(894, 360);
            this.tpgRentals.TabIndex = 1;
            this.tpgRentals.Text = "Rentals";
            this.tpgRentals.UseVisualStyleBackColor = true;
            // 
            // tpgPayments
            // 
            this.tpgPayments.ImageIndex = 2;
            this.tpgPayments.Location = new System.Drawing.Point(4, 49);
            this.tpgPayments.Name = "tpgPayments";
            this.tpgPayments.Padding = new System.Windows.Forms.Padding(3);
            this.tpgPayments.Size = new System.Drawing.Size(894, 360);
            this.tpgPayments.TabIndex = 2;
            this.tpgPayments.Text = "Payments";
            this.tpgPayments.UseVisualStyleBackColor = true;
            // 
            // tpgCustomers
            // 
            this.tpgCustomers.ImageIndex = 3;
            this.tpgCustomers.Location = new System.Drawing.Point(4, 49);
            this.tpgCustomers.Name = "tpgCustomers";
            this.tpgCustomers.Padding = new System.Windows.Forms.Padding(3);
            this.tpgCustomers.Size = new System.Drawing.Size(894, 360);
            this.tpgCustomers.TabIndex = 3;
            this.tpgCustomers.Text = "Customers";
            this.tpgCustomers.UseVisualStyleBackColor = true;
            // 
            // tabImages
            // 
            this.tabImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tabImages.ImageStream")));
            this.tabImages.TransparentColor = System.Drawing.Color.Transparent;
            this.tabImages.Images.SetKeyName(0, "gamepad.png");
            this.tabImages.Images.SetKeyName(1, "rent.png");
            this.tabImages.Images.SetKeyName(2, "coins.png");
            this.tabImages.Images.SetKeyName(3, "meeting.png");
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(267, 77);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1067, 591);
            this.dataGridView1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 77);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(253, 50);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(8, 133);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(253, 50);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(8, 189);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(253, 50);
            this.button3.TabIndex = 3;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(8, 245);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(253, 50);
            this.button4.TabIndex = 4;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.tabBastion);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "BG";
            this.tabBastion.ResumeLayout(false);
            this.tpgGames.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabBastion;
        private System.Windows.Forms.TabPage tpgGames;
        private System.Windows.Forms.TabPage tpgRentals;
        private System.Windows.Forms.TabPage tpgPayments;
        private System.Windows.Forms.TabPage tpgCustomers;
        private System.Windows.Forms.ImageList tabImages;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}

