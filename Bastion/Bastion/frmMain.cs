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
        List<Models.Rental> rentalQuery;

        public frmMain()
        {
            InitializeComponent();

            //Initialize games grid
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

            addGameGroup.Hide();
            editGameGroup.Hide();

            btnUpdateGame.Enabled = false;
            btnDeleteGame.Enabled = false;

            //Initialize rentals grid
            rentalsList.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn rentalDate = new DataGridViewTextBoxColumn();
            rentalDate.HeaderText = "Rental Date";
            DataGridViewTextBoxColumn inventory = new DataGridViewTextBoxColumn();
            inventory.HeaderText = "Inventory";
            DataGridViewTextBoxColumn customer = new DataGridViewTextBoxColumn();
            customer.HeaderText = "Customer";
            DataGridViewTextBoxColumn returnDate = new DataGridViewTextBoxColumn();
            returnDate.HeaderText = "Return Date";
            DataGridViewTextBoxColumn staff = new DataGridViewTextBoxColumn();
            staff.HeaderText = "Staff";
            DataGridViewTextBoxColumn payment = new DataGridViewTextBoxColumn();
            payment.HeaderText = "Payment";

            rentalsList.Columns.AddRange(rentalDate, inventory, customer, returnDate, staff, payment);

            rentalQuery = db.Rentals.ToList();
            populateRentalsGrid(rentalQuery);

            addRentalGroup.Hide();
            editRentalGroup.Hide();

            deleteRentalBtn.Enabled = false;
            updateRentalBtn.Enabled = false;

            //Initialize customer grid
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

        //***************************************Rentals Section********************************************//

        private void populateRentalsGrid(List<Models.Rental> rental)
        {
            for (int i = 0; i < rental.Count; i++)
            {
                rentalsList.Rows.Add();
                rentalsList[0, i].Value = rental[i].RentalDate;
                rentalsList[1, i].Value = rental[i].Inventory.Game.Title;
                rentalsList[2, i].Value = rental[i].Customer.FirstName + " " + rental[i].Customer.LastName;
                rentalsList[3, i].Value = rental[i].RentalDate;
                rentalsList[4, i].Value = rental[i].Staff.FirstName + " " + rental[i].Staff.LastName;
            }
        }

        private void addRentalBtn_Click(object sender, EventArgs e)
        {
            addRentalGroup.Show();
            rentalsList.Hide();

            addRentalSelectInventory.DataSource = db.Games.Select(u => u.Title).ToList();
            addRentalSelectCustomer.DataSource = db.Customers.Select(u => u.FirstName + " " + u.LastName).ToList();
            addRentalSelectStaff.DataSource = db.Staffs.Select(u => u.FirstName + " " + u.LastName).ToList();
        }

        private void searchInventoryBtn_Click(object sender, EventArgs e)
        {
            if (addRentalSearchInventory.Text != "")
            {
                addRentalSelectInventory.DataSource = db.Games.Where(u => u.Title.ToLower().Contains(addRentalSearchInventory.Text.ToLower())).Select(u => u.Title).ToList();
            }
        }

        private void searchCustomerBtn_Click(object sender, EventArgs e)
        {
            if (addRentalSearchCustomer.Text != "")
            {
                addRentalSelectCustomer.DataSource = db.Customers.Where(u => u.FirstName.ToLower().Contains(addRentalSearchCustomer.Text) || u.LastName.ToLower().Contains(addRentalSearchCustomer.Text)).Select(u => u.FirstName + " " + u.LastName).ToList();
            }
        }

        private void searchStaffBtn_Click(object sender, EventArgs e)
        {
            if (addRentalSearchStaff.Text != "")
            {
                addRentalSelectStaff.DataSource = db.Staffs.Where(u => u.FirstName.ToLower().Contains(addRentalSearchStaff.Text) || u.LastName.ToLower().Contains(addRentalSearchStaff.Text)).Select(u => u.FirstName + " " + u.LastName).ToList();
            }
        }

        //save rental
        private void saveAddRental_Click(object sender, EventArgs e)
        {
            if (addRentalDate.Text == "" || addRentalsReturnDate.Text == "")
            {
                MessageBox.Show("Please fill in all the fields");
            }
            else
            {
                Models.Rental rental = new Models.Rental();
                rental.RentalDate = addRentalDate.Value;
                rental.RentalDate = addRentalsReturnDate.Value;
                //rental.InventoryID = db.Inventories.Where(u => u.Game.Title == addRentalSelectInventory.Text).Select(u => u.InventoryID).FirstOrDefault();
                rental.Inventory = db.Inventories.Where(u => u.Game.Title == addRentalSelectInventory.Text).FirstOrDefault();
                rental.Customer = db.Customers.Where(u => u.FirstName + " " + u.LastName == addRentalSelectCustomer.Text).FirstOrDefault();
                rental.Staff = db.Staffs.Where(u => u.FirstName + " " + u.LastName == addRentalSelectStaff.Text).FirstOrDefault();

                db.Rentals.Add(rental);
                db.SaveChanges();

                MessageBox.Show("The rental has been saved");

                rentalsList.Rows.Clear();
                rentalQuery = db.Rentals.ToList();
                populateRentalsGrid(rentalQuery);

                addRentalGroup.Hide();
                rentalsList.Show();
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
