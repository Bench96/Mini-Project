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
            this.clearSearchResultsBTN = new System.Windows.Forms.Button();
            this.gameSearchTextBox = new System.Windows.Forms.TextBox();
            this.gameSearchBTN = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btnDeleteGame = new System.Windows.Forms.Button();
            this.btnUpdateGame = new System.Windows.Forms.Button();
            this.btnAddGame = new System.Windows.Forms.Button();
            this.gameList = new System.Windows.Forms.DataGridView();
            this.addGameGroup = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.addGameBTN = new System.Windows.Forms.Button();
            this.gameTitleTextBox = new System.Windows.Forms.TextBox();
            this.gamePublisherComboBox = new System.Windows.Forms.ComboBox();
            this.gameReleaseYearTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.gameRentalRateTextBox = new System.Windows.Forms.TextBox();
            this.gameGenreComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gameDeveloperComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabBastion = new System.Windows.Forms.TabControl();
            this.tpgManagement = new System.Windows.Forms.TabPage();
            this.tpgGames.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameList)).BeginInit();
            this.addGameGroup.SuspendLayout();
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
            this.tpgGames.Controls.Add(this.clearSearchResultsBTN);
            this.tpgGames.Controls.Add(this.gameSearchTextBox);
            this.tpgGames.Controls.Add(this.gameSearchBTN);
            this.tpgGames.Controls.Add(this.button4);
            this.tpgGames.Controls.Add(this.btnDeleteGame);
            this.tpgGames.Controls.Add(this.btnUpdateGame);
            this.tpgGames.Controls.Add(this.btnAddGame);
            this.tpgGames.Controls.Add(this.addGameGroup);
            this.tpgGames.Controls.Add(this.gameList);
            this.tpgGames.ImageIndex = 0;
            this.tpgGames.Location = new System.Drawing.Point(4, 49);
            this.tpgGames.Name = "tpgGames";
            this.tpgGames.Padding = new System.Windows.Forms.Padding(3);
            this.tpgGames.Size = new System.Drawing.Size(1342, 676);
            this.tpgGames.TabIndex = 0;
            this.tpgGames.Text = "Games";
            this.tpgGames.UseVisualStyleBackColor = true;
            // 
            // clearSearchResultsBTN
            // 
            this.clearSearchResultsBTN.Location = new System.Drawing.Point(604, 28);
            this.clearSearchResultsBTN.Name = "clearSearchResultsBTN";
            this.clearSearchResultsBTN.Size = new System.Drawing.Size(132, 23);
            this.clearSearchResultsBTN.TabIndex = 7;
            this.clearSearchResultsBTN.Text = "Clear Search Results";
            this.clearSearchResultsBTN.UseVisualStyleBackColor = true;
            this.clearSearchResultsBTN.Click += new System.EventHandler(this.clearSearchResultsBTN_Click);
            // 
            // gameSearchTextBox
            // 
            this.gameSearchTextBox.Location = new System.Drawing.Point(276, 31);
            this.gameSearchTextBox.Name = "gameSearchTextBox";
            this.gameSearchTextBox.Size = new System.Drawing.Size(175, 20);
            this.gameSearchTextBox.TabIndex = 6;
            // 
            // gameSearchBTN
            // 
            this.gameSearchBTN.Location = new System.Drawing.Point(486, 28);
            this.gameSearchBTN.Name = "gameSearchBTN";
            this.gameSearchBTN.Size = new System.Drawing.Size(76, 23);
            this.gameSearchBTN.TabIndex = 5;
            this.gameSearchBTN.Text = "Search";
            this.gameSearchBTN.UseVisualStyleBackColor = true;
            this.gameSearchBTN.Click += new System.EventHandler(this.gameSearchBTN_Click);
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
            // btnDeleteGame
            // 
            this.btnDeleteGame.Location = new System.Drawing.Point(8, 189);
            this.btnDeleteGame.Name = "btnDeleteGame";
            this.btnDeleteGame.Size = new System.Drawing.Size(253, 50);
            this.btnDeleteGame.TabIndex = 3;
            this.btnDeleteGame.Text = "Delete Game";
            this.btnDeleteGame.UseVisualStyleBackColor = true;
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
            // btnAddGame
            // 
            this.btnAddGame.Location = new System.Drawing.Point(8, 77);
            this.btnAddGame.Name = "btnAddGame";
            this.btnAddGame.Size = new System.Drawing.Size(253, 50);
            this.btnAddGame.TabIndex = 1;
            this.btnAddGame.Text = "Add Game";
            this.btnAddGame.UseVisualStyleBackColor = true;
            this.btnAddGame.Click += new System.EventHandler(this.btnAddGame_Click);
            // 
            // gameList
            // 
            this.gameList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gameList.Location = new System.Drawing.Point(276, 77);
            this.gameList.Name = "gameList";
            this.gameList.Size = new System.Drawing.Size(868, 357);
            this.gameList.TabIndex = 0;
            // 
            // addGameGroup
            // 
            this.addGameGroup.Controls.Add(this.label1);
            this.addGameGroup.Controls.Add(this.addGameBTN);
            this.addGameGroup.Controls.Add(this.gameTitleTextBox);
            this.addGameGroup.Controls.Add(this.gamePublisherComboBox);
            this.addGameGroup.Controls.Add(this.gameReleaseYearTextBox);
            this.addGameGroup.Controls.Add(this.label6);
            this.addGameGroup.Controls.Add(this.gameRentalRateTextBox);
            this.addGameGroup.Controls.Add(this.gameGenreComboBox);
            this.addGameGroup.Controls.Add(this.label2);
            this.addGameGroup.Controls.Add(this.label5);
            this.addGameGroup.Controls.Add(this.label3);
            this.addGameGroup.Controls.Add(this.gameDeveloperComboBox);
            this.addGameGroup.Controls.Add(this.label4);
            this.addGameGroup.Location = new System.Drawing.Point(301, 77);
            this.addGameGroup.Name = "addGameGroup";
            this.addGameGroup.Size = new System.Drawing.Size(303, 409);
            this.addGameGroup.TabIndex = 24;
            this.addGameGroup.TabStop = false;
            this.addGameGroup.Text = "Add Game";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Title";
            // 
            // addGameBTN
            // 
            this.addGameBTN.Location = new System.Drawing.Point(58, 355);
            this.addGameBTN.Name = "addGameBTN";
            this.addGameBTN.Size = new System.Drawing.Size(75, 23);
            this.addGameBTN.TabIndex = 23;
            this.addGameBTN.Text = "Add Game";
            this.addGameBTN.UseVisualStyleBackColor = true;
            this.addGameBTN.Click += new System.EventHandler(this.addGameBTN_Click);
            // 
            // gameTitleTextBox
            // 
            this.gameTitleTextBox.Location = new System.Drawing.Point(58, 34);
            this.gameTitleTextBox.Name = "gameTitleTextBox";
            this.gameTitleTextBox.Size = new System.Drawing.Size(175, 20);
            this.gameTitleTextBox.TabIndex = 8;
            // 
            // gamePublisherComboBox
            // 
            this.gamePublisherComboBox.FormattingEnabled = true;
            this.gamePublisherComboBox.Location = new System.Drawing.Point(58, 317);
            this.gamePublisherComboBox.Name = "gamePublisherComboBox";
            this.gamePublisherComboBox.Size = new System.Drawing.Size(175, 21);
            this.gamePublisherComboBox.TabIndex = 22;
            // 
            // gameReleaseYearTextBox
            // 
            this.gameReleaseYearTextBox.Location = new System.Drawing.Point(58, 90);
            this.gameReleaseYearTextBox.Name = "gameReleaseYearTextBox";
            this.gameReleaseYearTextBox.Size = new System.Drawing.Size(175, 20);
            this.gameReleaseYearTextBox.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(55, 302);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Publisher";
            // 
            // gameRentalRateTextBox
            // 
            this.gameRentalRateTextBox.Location = new System.Drawing.Point(58, 146);
            this.gameRentalRateTextBox.Name = "gameRentalRateTextBox";
            this.gameRentalRateTextBox.Size = new System.Drawing.Size(175, 20);
            this.gameRentalRateTextBox.TabIndex = 13;
            // 
            // gameGenreComboBox
            // 
            this.gameGenreComboBox.FormattingEnabled = true;
            this.gameGenreComboBox.Location = new System.Drawing.Point(58, 258);
            this.gameGenreComboBox.Name = "gameGenreComboBox";
            this.gameGenreComboBox.Size = new System.Drawing.Size(175, 21);
            this.gameGenreComboBox.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Release Year";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(55, 243);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Genre";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Rental Rate";
            // 
            // gameDeveloperComboBox
            // 
            this.gameDeveloperComboBox.FormattingEnabled = true;
            this.gameDeveloperComboBox.Location = new System.Drawing.Point(58, 202);
            this.gameDeveloperComboBox.Name = "gameDeveloperComboBox";
            this.gameDeveloperComboBox.Size = new System.Drawing.Size(175, 21);
            this.gameDeveloperComboBox.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(55, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Developer";
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
            this.tpgGames.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameList)).EndInit();
            this.addGameGroup.ResumeLayout(false);
            this.addGameGroup.PerformLayout();
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
        private System.Windows.Forms.DataGridView gameList;
        private System.Windows.Forms.TabControl tabBastion;
        private System.Windows.Forms.TabPage tpgManagement;
        private System.Windows.Forms.TextBox gameSearchTextBox;
        private System.Windows.Forms.Button gameSearchBTN;
        private System.Windows.Forms.Button clearSearchResultsBTN;
        private System.Windows.Forms.TextBox gameRentalRateTextBox;
        private System.Windows.Forms.TextBox gameReleaseYearTextBox;
        private System.Windows.Forms.TextBox gameTitleTextBox;
        private System.Windows.Forms.ComboBox gamePublisherComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox gameGenreComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox gameDeveloperComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox addGameGroup;
        private System.Windows.Forms.Button addGameBTN;
    }
}

