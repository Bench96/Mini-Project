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
            this.tabImages = new System.Windows.Forms.ImageList(this.components);
            this.tpgCustomers = new System.Windows.Forms.TabPage();
            this.tpgRentals = new System.Windows.Forms.TabPage();
            this.tpgGames = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnAddGame = new System.Windows.Forms.Button();
            this.btnUpdateGame = new System.Windows.Forms.Button();
            this.btnDeleteGame = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.tabBastion = new System.Windows.Forms.TabControl();
            this.tpgManagement = new System.Windows.Forms.TabPage();
            this.tpgGames.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabBastion.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabImages
            // 
            this.tabImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tabImages.ImageStream")));
            this.tabImages.TransparentColor = System.Drawing.Color.Transparent;
            this.tabImages.Images.SetKeyName(0, "gamepad.png");
            this.tabImages.Images.SetKeyName(1, "coins.png");
            this.tabImages.Images.SetKeyName(2, "meeting.png");
            this.tabImages.Images.SetKeyName(3, "manage.png");
            // 
            // tpgCustomers
            // 
            this.tpgCustomers.ImageIndex = 2;
            this.tpgCustomers.Location = new System.Drawing.Point(4, 49);
            this.tpgCustomers.Name = "tpgCustomers";
            this.tpgCustomers.Padding = new System.Windows.Forms.Padding(3);
            this.tpgCustomers.Size = new System.Drawing.Size(1342, 676);
            this.tpgCustomers.TabIndex = 3;
            this.tpgCustomers.Text = "Customers";
            this.tpgCustomers.UseVisualStyleBackColor = true;
            // 
            // tpgRentals
            // 
            this.tpgRentals.ImageIndex = 1;
            this.tpgRentals.Location = new System.Drawing.Point(4, 49);
            this.tpgRentals.Name = "tpgRentals";
            this.tpgRentals.Padding = new System.Windows.Forms.Padding(3);
            this.tpgRentals.Size = new System.Drawing.Size(1342, 676);
            this.tpgRentals.TabIndex = 1;
            this.tpgRentals.Text = "Rentals";
            this.tpgRentals.UseVisualStyleBackColor = true;
            // 
            // tpgGames
            // 
            this.tpgGames.Controls.Add(this.button4);
            this.tpgGames.Controls.Add(this.btnDeleteGame);
            this.tpgGames.Controls.Add(this.btnUpdateGame);
            this.tpgGames.Controls.Add(this.btnAddGame);
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
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(267, 77);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1067, 591);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnAddGame
            // 
            this.btnAddGame.Location = new System.Drawing.Point(8, 77);
            this.btnAddGame.Name = "btnAddGame";
            this.btnAddGame.Size = new System.Drawing.Size(253, 50);
            this.btnAddGame.TabIndex = 1;
            this.btnAddGame.Text = "Add Game";
            this.btnAddGame.UseVisualStyleBackColor = true;
            // 
            // btnUpdateGame
            // 
            this.btnUpdateGame.Location = new System.Drawing.Point(8, 133);
            this.btnUpdateGame.Name = "btnUpdateGame";
            this.btnUpdateGame.Size = new System.Drawing.Size(253, 50);
            this.btnUpdateGame.TabIndex = 2;
            this.btnUpdateGame.Text = "Update Game";
            this.btnUpdateGame.UseVisualStyleBackColor = true;
            // 
            // btnDeleteGame
            // 
            this.btnDeleteGame.Location = new System.Drawing.Point(8, 189);
            this.btnDeleteGame.Name = "btnDeleteGame";
            this.btnDeleteGame.Size = new System.Drawing.Size(253, 50);
            this.btnDeleteGame.TabIndex = 3;
            this.btnDeleteGame.Text = "Delete Game";
            this.btnDeleteGame.UseVisualStyleBackColor = true;
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
            // tabBastion
            // 
            this.tabBastion.Controls.Add(this.tpgGames);
            this.tabBastion.Controls.Add(this.tpgRentals);
            this.tabBastion.Controls.Add(this.tpgCustomers);
            this.tabBastion.Controls.Add(this.tpgManagement);
            this.tabBastion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabBastion.ImageList = this.tabImages;
            this.tabBastion.ItemSize = new System.Drawing.Size(83, 45);
            this.tabBastion.Location = new System.Drawing.Point(0, 0);
            this.tabBastion.Name = "tabBastion";
            this.tabBastion.SelectedIndex = 0;
            this.tabBastion.Size = new System.Drawing.Size(1350, 729);
            this.tabBastion.TabIndex = 0;
            // 
            // tpgManagement
            // 
            this.tpgManagement.ImageIndex = 3;
            this.tpgManagement.Location = new System.Drawing.Point(4, 49);
            this.tpgManagement.Name = "tpgManagement";
            this.tpgManagement.Padding = new System.Windows.Forms.Padding(3);
            this.tpgManagement.Size = new System.Drawing.Size(1342, 676);
            this.tpgManagement.TabIndex = 4;
            this.tpgManagement.Text = "Management";
            this.tpgManagement.UseVisualStyleBackColor = true;
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
            this.tpgGames.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabBastion.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList tabImages;
        private System.Windows.Forms.TabPage tpgCustomers;
        private System.Windows.Forms.TabPage tpgRentals;
        private System.Windows.Forms.TabPage tpgGames;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btnDeleteGame;
        private System.Windows.Forms.Button btnUpdateGame;
        private System.Windows.Forms.Button btnAddGame;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabControl tabBastion;
        private System.Windows.Forms.TabPage tpgManagement;
    }
}

