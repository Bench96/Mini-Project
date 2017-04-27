using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bastion
{
    public partial class frmMain : Form
    {
        Models.GameRentalsEntities db = new Models.GameRentalsEntities();
        List<Models.Game> gameQuery;

        public frmMain()
        {
            InitializeComponent();

            gameList.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn title = new DataGridViewTextBoxColumn();
            title.HeaderText = "Title";
            DataGridViewTextBoxColumn releaseYear = new DataGridViewTextBoxColumn();
            releaseYear.HeaderText = "Release Year";
            DataGridViewTextBoxColumn rentalRate = new DataGridViewTextBoxColumn();
            rentalRate.HeaderText = "Rental Rate";
            DataGridViewTextBoxColumn developers = new DataGridViewTextBoxColumn();
            developers.HeaderText = "Developers";
            DataGridViewTextBoxColumn genres = new DataGridViewTextBoxColumn();
            genres.HeaderText = "Genres";
            DataGridViewTextBoxColumn publishers = new DataGridViewTextBoxColumn();
            publishers.HeaderText = "Publishers";

            gameList.Columns.AddRange(title, releaseYear, rentalRate, developers, genres, publishers);

            gameQuery = db.Games.ToList();
            populateGameGrid(gameQuery);

            btnUpdateGame.Enabled = false;
            btnDeleteGame.Enabled = false;

            addGameGroup.Hide();
            editGameGroup.Hide();
            grpAddCustomer.Hide();
            grpUpdateCustomer.Hide();
        }

        //***************************************Games Section********************************************//

        //populate the game grid
        private void populateGameGrid(List<Models.Game> games)
        {
            for (int i = 0; i < games.Count; i++)
            {
                gameList.Rows.Add();
                gameList[0, i].Value = games[i].Title;
                gameList[1, i].Value = games[i].ReleaseYear;
                gameList[2, i].Value = games[i].RentalRrate;

                foreach (Models.Developer dev in games[i].Developers)
                {
                    gameList[3, i].Value += dev.Name + ", ";
                }

                foreach (Models.Genre genre in games[i].Genres)
                {
                    gameList[4, i].Value += genre.Name + ", ";
                }

                foreach (Models.Publisher publisher in games[i].Publishers)
                {
                    gameList[5, i].Value += publisher.Name + ", ";
                }
            }
        }


        private void gameSearchBTN_Click(object sender, EventArgs e)
        {
            string searchQuery = gameSearchTextBox.Text;

            if(searchQuery != null)
            {
                gameList.Rows.Clear();
                gameQuery = db.Games.Where(u => u.Title.ToLower().Contains(searchQuery.ToLower())).ToList();
                populateGameGrid(gameQuery);

                if(gameQuery.Count == 1)
                {
                    btnUpdateGame.Enabled = true;
                    btnDeleteGame.Enabled = true;
                }
            }
        }

        private void clearSearchResultsBTN_Click(object sender, EventArgs e)
        {
            gameList.Rows.Clear();
            gameQuery = db.Games.ToList();
            populateGameGrid(gameQuery);

            btnUpdateGame.Enabled = false;
            btnDeleteGame.Enabled = false;
        }

        //View add game form
        private void btnAddGame_Click(object sender, EventArgs e)
        {
            gameList.Hide();
            addGameGroup.Show();
            
            //populate combo boxes
            gameDeveloperComboBox.DataSource = db.Developers.Select(u => u.Name).ToList();
            gameGenreComboBox.DataSource = db.Genres.Select(u => u.Name).ToList();
            gamePublisherComboBox.DataSource = db.Publishers.Select(u => u.Name).ToList();
        }

        //Add game
        private void addGameBTN_Click(object sender, EventArgs e)
        {
            if(gameTitleTextBox.Text == "" || gameReleaseYearTextBox.Text == "" || gameRentalRateTextBox.Text == "")
            {
                MessageBox.Show("Please complete all the fields");
            }
            else
            {
                //create game
                Models.Game game = new Models.Game();
                game.Title = gameTitleTextBox.Text;
                game.ReleaseYear = Convert.ToDecimal(gameReleaseYearTextBox.Text);
                game.RentalRrate = Convert.ToDecimal(gameRentalRateTextBox.Text);
                //create developer
                Models.Developer developer = new Models.Developer();
                developer.Name = gameDeveloperComboBox.Text;
                game.Developers.Add(developer);
                //create genre
                Models.Genre genre = new Models.Genre();
                genre.Name = gameGenreComboBox.Text;
                game.Genres.Add(genre);
                //create publisher
                Models.Publisher publisher = new Models.Publisher();
                publisher.Name = gamePublisherComboBox.Text;
                game.Publishers.Add(publisher);
                //add game to db
                db.Games.Add(game);

                db.SaveChanges();

                MessageBox.Show("The game has been saved!");

                gameList.Rows.Clear();
                gameQuery = db.Games.ToList();
                populateGameGrid(gameQuery);

                gameList.Show();
                addGameGroup.Hide();
            }   
        }

        //view update game
        private void btnUpdateGame_Click(object sender, EventArgs e)
        {
            if (gameQuery != null && gameQuery.Count == 1)
            {
                editGameTitle.Text = gameQuery[0].Title;
                editGameReleaseYear.Text = gameQuery[0].ReleaseYear.ToString();
                editGameRentalRate.Text = gameQuery[0].RentalRrate.ToString();

                editGameGroup.Show();
                gameList.Hide();
            }
        }

        //update game
        private void updateGame_Click(object sender, EventArgs e)
        {
            if (editGameTitle.Text == "" || editGameReleaseYear.Text == "" || editGameRentalRate.Text == "")
            {
                MessageBox.Show("Please complete all the fields");
            }
            else
            {
                gameQuery[0].Title = editGameTitle.Text;
                gameQuery[0].ReleaseYear = Convert.ToDecimal(editGameReleaseYear.Text);
                gameQuery[0].RentalRrate = Convert.ToDecimal(editGameRentalRate.Text);

                db.Entry(gameQuery[0]).State = EntityState.Modified;
                db.SaveChanges();

                MessageBox.Show("The game has been updated!");

                gameList.Rows.Clear();
                gameQuery = db.Games.ToList();
                populateGameGrid(gameQuery);

                editGameGroup.Hide();
                gameList.Show();

            }
        }

        private void btnDeleteGame_Click(object sender, EventArgs e)
        {
            DialogResult dialogue = MessageBox.Show("Are you sure you want to delete this game?", "Delete Game", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogue == DialogResult.Yes)
            {
                //Models.Inventory inv = db.Inventories.Where(u => u.GameID == gameQuery[0].GameID).FirstOrDefault();
                //db.Inventories.Remove(inv);
                db.Games.Remove(gameQuery[0]);
                db.SaveChanges();

                MessageBox.Show("The game has been deleted");

                gameList.Rows.Clear();
                gameQuery = db.Games.ToList();
                populateGameGrid(gameQuery);
            }
        }


        //***************************************Customer Section********************************************//

        private void dgvCustomer_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomer.SelectedRows.Count != 0)
            {
                btnUpdateCustomer.Enabled = true;
                btnDeleteCustomer.Enabled = true;
            }

            else
            {
                btnUpdateCustomer.Enabled = false;
                btnDeleteCustomer.Enabled = false;
            }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            dgvCustomer.Hide();
            grpUpdateCustomer.Hide();
            grpAddCustomer.Show();
        }

        private void btnAddCustomerAddCustomer_Click(object sender, EventArgs e)
        {

        }

        private void btnAddCustomerCancel_Click(object sender, EventArgs e)
        {
            grpAddCustomer.Hide();
            grpUpdateCustomer.Hide();
            dgvCustomer.Show();
        }

        private void btnUpdateCustomerUpdateCustomer_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdateCustomerCancel_Click(object sender, EventArgs e)
        {
            grpAddCustomer.Hide();
            grpUpdateCustomer.Hide();
            dgvCustomer.Show();
        }

        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            dgvCustomer.Hide();
            grpAddCustomer.Hide();
            grpUpdateCustomer.Show();
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you wish to delete this customer record?", "Delete Customer",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                // TODO - delete record

                // TODO - refresh dgv

                grpAddCustomer.Hide();
                grpUpdateCustomer.Hide();
                dgvCustomer.Show();
            }
        }

        
    }
}
