﻿using System;
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
        Models.Game game;
        Models.Rental rental;

        public frmMain()
        {
            InitializeComponent();

            //Initialize games grid
            gameList.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn GameID = new DataGridViewTextBoxColumn();
            GameID.HeaderText = "ID";
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

            gameList.Columns.AddRange(GameID, title, releaseYear, rentalRate, developers, genres, publishers);

            gameQuery = db.Games.ToList();
            populateGameGrid(gameQuery);

            addGameGroup.Hide();
            editGameGroup.Hide();

            //Initialize rentals grid
            rentalsList.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn();
            ID.HeaderText = "ID";
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

            rentalsList.Columns.AddRange(ID, rentalDate, inventory, customer, returnDate, staff);

            rentalQuery = db.Rentals.ToList();
            populateRentalsGrid(rentalQuery);

            addRentalGroup.Hide();
            //editRentalGroup.Hide();

            // Customer tab init
            grpAddCustomer.Hide();
            grpUpdateCustomer.Hide();
            populateCustomersDatagridview();

            // Management tab init
            grpAddEmployee.Hide();
            grpUpdateEmployee.Hide();
            grpAddStore.Hide();
            grpUpdateStore.Hide();

            populateEmployeeDatagridview();
            populateStoreDatagridview();
        }

        //***************************************Games Section********************************************//

        //populate the game grid
        private void populateGameGrid(List<Models.Game> games)
        {
            for (int i = 0; i < games.Count; i++)
            {
                gameList.Rows.Add();
                gameList[0, i].Value = games[i].GameID;
                gameList[1, i].Value = games[i].Title;
                gameList[2, i].Value = games[i].ReleaseYear;
                gameList[3, i].Value = games[i].RentalRrate;

                foreach (Models.Developer dev in games[i].Developers)
                {
                    gameList[4, i].Value += dev.Name + ", ";
                }

                foreach (Models.Genre genre in games[i].Genres)
                {
                    gameList[5, i].Value += genre.Name + ", ";
                }

                foreach (Models.Publisher publisher in games[i].Publishers)
                {
                    gameList[6, i].Value += publisher.Name + ", ";
                }
            }
        }


        private void gameSearchBTN_Click(object sender, EventArgs e)
        {
            string searchQuery = gameSearchTextBox.Text;
            string searchIndex = searchBy.Text;

            if (searchIndex == "")
            {
                MessageBox.Show("Please select a search crieteria");
            }
            else if(searchQuery == "")
            {
                MessageBox.Show("Please enter a search term");
            }
            else 
            {
                if(searchIndex == "Title")
                {
                    gameList.Rows.Clear();
                    gameQuery = db.Games.Where(u => u.Title.ToLower().Contains(searchQuery.ToLower())).ToList();
                    populateGameGrid(gameQuery);

                }
                else if(searchIndex == "Release Year")
                {
                    gameList.Rows.Clear();
                    gameQuery = db.Games.Where(u => u.ReleaseYear.ToString().ToLower().Contains(searchQuery.ToLower())).ToList();
                    populateGameGrid(gameQuery);
                }

            }
        }

        private void clearSearchResultsBTN_Click(object sender, EventArgs e)
        {
            gameList.Rows.Clear();
            gameQuery = db.Games.ToList();
            populateGameGrid(gameQuery);
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
            gameStoreComboBox.DataSource = db.Stores.Select(u => u.StoreID).ToList();

        }

        //Add game
        private void addGameBTN_Click(object sender, EventArgs e)
        {
            if (gameTitleTextBox.Text == "" || gameReleaseYearTextBox.Text == "" || gameRentalRateTextBox.Text == "")
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

                Models.Inventory inventory = new Models.Inventory();
                inventory.StoreID = Convert.ToInt32(gameStoreComboBox.Text);
                game.Inventories.Add(inventory);

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

        private void btnUpdateGame_Click(object sender, EventArgs e)
        {
            game = db.Games.Find((int)gameList.SelectedRows[0].Cells[0].Value);
            editGameTitle.Text = game.Title;
            editGameReleaseYear.Text = game.ReleaseYear.ToString();
            editGameRentalRate.Text = game.RentalRrate.ToString();

            editGameGroup.Show();
            gameList.Hide();
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
                game.Title = editGameTitle.Text;
                game.ReleaseYear = Convert.ToDecimal(editGameReleaseYear.Text);
                game.RentalRrate = Convert.ToDecimal(editGameRentalRate.Text);

                db.Entry(game).State = EntityState.Modified;
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
                game = db.Games.Find((int)gameList.SelectedRows[0].Cells[0].Value);

                foreach (Models.Inventory inv in game.Inventories.ToList())
                {
                    foreach(Models.Rental rental in inv.Rentals.ToList())
                    {
                        foreach(Models.Payment payment in rental.Payments.ToList())
                        {
                            db.Payments.Remove(payment);
                        }
                        db.Rentals.Remove(rental);
                    }
                    db.Inventories.Remove(inv);
                }

                db.Games.Remove(game);
                db.SaveChanges();

                MessageBox.Show("The game has been deleted");

                gameList.Rows.Clear();
                gameQuery = db.Games.ToList();
                populateGameGrid(gameQuery);

                rentalsList.Rows.Clear();
                rentalQuery = db.Rentals.ToList();
                populateRentalsGrid(rentalQuery);
            }
        }

        private void gamesBack_Click(object sender, EventArgs e)
        {
            gameList.Show();
            editGameGroup.Hide();
        }

        private void addGameBack_Click(object sender, EventArgs e)
        {
            gameList.Show();
            addGameGroup.Hide();  
        }

        //***************************************Rentals Section********************************************//

        private void populateRentalsGrid(List<Models.Rental> rentalQ)
        {
            rentalsList.Rows.Clear();
            for (int i = 0; i < rentalQ.Count; i++)
            {
                rentalsList.Rows.Add();
                rentalsList[0, i].Value = rentalQ[i].RentalID;
                rentalsList[1, i].Value = rentalQ[i].RentalDate;
                rentalsList[2, i].Value = rentalQ[i].Inventory.Game.Title;
                rentalsList[3, i].Value = rentalQ[i].Customer.FirstName + " " + rentalQ[i].Customer.LastName;
                rentalsList[4, i].Value = rentalQ[i].RentalDate;
                rentalsList[5, i].Value = rentalQ[i].Staff.FirstName + " " + rentalQ[i].Staff.LastName;
            }
        }

        private void rentalSearchBtn_Click(object sender, EventArgs e)
        {
            var searchIndex = rentalSearchBy.Text;
            var searchTerm = rentalSearchTerm.Text;

            if(searchIndex == "")
            {
                MessageBox.Show("Please select a search criteria");
            }
            else if(searchTerm == "")
            {
                MessageBox.Show("Please enter a search term");
            }
            else
            {
                if (searchIndex == "Inventory")
                {
                    rentalQuery = db.Rentals.Where(u => u.Inventory.Game.Title.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                else if (searchIndex == "Customer")
                {
                    rentalQuery = db.Rentals.Where(u => u.Customer.FirstName.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                else if(searchIndex == "Staff")
                {
                    rentalQuery = db.Rentals.Where(u => u.Staff.FirstName.ToLower().Contains(searchTerm.ToLower())).ToList();
                }

                populateRentalsGrid(rentalQuery);

            }
        }

        private void rentalsClearSearch_Click(object sender, EventArgs e)
        {
           
            rentalQuery = db.Rentals.ToList();
            populateRentalsGrid(rentalQuery);
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
            if (addRentalDate.Text == ""
                || addRentalsReturnDate.Text == ""
                || addRentalSearchCustomer.Text == ""
                || addRentalSelectInventory.Text == ""
                || addRentalSelectStaff.Text == "")
            {
                MessageBox.Show("Please fill in all the fields");
            }
            else
            {
                rental = new Models.Rental();
                rental.RentalDate = addRentalDate.Value;
                rental.ReturnDate = addRentalsReturnDate.Value;
                //rental.InventoryID = db.Inventories.Where(u => u.Game.Title == addRentalSelectInventory.Text).Select(u => u.InventoryID).FirstOrDefault();
                rental.Inventory = db.Inventories.Where(u => u.Game.Title == addRentalSelectInventory.Text).FirstOrDefault();
                rental.Customer = db.Customers.Where(u => u.FirstName + " " + u.LastName == addRentalSelectCustomer.Text).FirstOrDefault();
                rental.Staff = db.Staffs.Where(u => u.FirstName + " " + u.LastName == addRentalSelectStaff.Text).FirstOrDefault();

                db.Rentals.Add(rental);

                Models.Payment payment = new Models.Payment();
                payment.Amount = rental.Inventory.Game.RentalRrate;
                payment.Customer = rental.Customer;
                payment.Staff = rental.Staff;
                payment.PaymentDate = rental.RentalDate;

                db.Payments.Add(payment);
                db.SaveChanges();

                MessageBox.Show("The rental has been saved");

                rentalsList.Rows.Clear();
                rentalQuery = db.Rentals.ToList();
                populateRentalsGrid(rentalQuery);

                addRentalGroup.Hide();
                rentalsList.Show();
            }
        }

        private void deleteRentalBtn_Click(object sender, EventArgs e)
        {
            Models.Rental rental = db.Rentals.Find((int)rentalsList.SelectedRows[0].Cells[0].Value);
            DialogResult dialogue = MessageBox.Show("Are you sure you want to delete this rental?", "Delete Rental", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogue == DialogResult.Yes)
            {
                foreach (Models.Payment payment in rental.Payments)
                {
                    db.Payments.Remove(payment);
                }
                db.Rentals.Remove(rental);
                db.SaveChanges();
                MessageBox.Show("The rental has been successfully deleted");

                rentalQuery = db.Rentals.ToList();
                populateRentalsGrid(rentalQuery);
            }

        }

        private void addRentalBack_Click(object sender, EventArgs e)
        {
            rentalsList.Show();
            addRentalGroup.Hide();
        }


        //***************************************Customer Section********************************************//

        private class CustomerProjection
        {
            public int CustomerID { get; set; }
            public string FName { get; set; }
            public string LName { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            public bool Active { get; set; }
        }

        private void populateCustomersDatagridview()
        {
            var query = from cu in db.Customers
                        join ad in db.Addresses on cu.AddressID equals ad.AddressID
                        join ci in db.Cities on ad.CityID equals ci.CityID
                        join co in db.Countries on ci.CountryID equals co.CountryID
                        select new CustomerProjection()
                        {
                            CustomerID = cu.CustomerID,
                            FName = cu.FirstName,
                            LName = cu.LastName,
                            Email = cu.Email,
                            Address = ad.Address1,
                            City = ci.City1,
                            Country = co.Country1,
                            Active = cu.Active
                        };

            if (cmbCustomerSearchOrderBy.Text == "First Name")
            {
                query = query.Where(x => x.FName.Contains(txtSearchCustomer.Text));

                if (cmbCustomerSearchOrder.Text == "Descending")
                {
                    query = query.OrderByDescending(x => x.FName);
                }

                else
                {
                    query = query.OrderBy(x => x.FName);
                }
            }

            else if (cmbCustomerSearchOrderBy.Text == "Last Name")
            {
                query = query.Where(x => x.LName.Contains(txtSearchCustomer.Text));

                if (cmbCustomerSearchOrder.Text == "Descending")
                {
                    query = query.OrderByDescending(x => x.LName);
                }

                else
                {
                    query = query.OrderBy(x => x.LName);
                }
            }

            else if (cmbCustomerSearchOrderBy.Text == "Email Address")
            {
                query = query.Where(x => x.Email.Contains(txtSearchCustomer.Text));

                if (cmbCustomerSearchOrder.Text == "Descending")
                {
                    query = query.OrderByDescending(x => x.Email);
                }

                else
                {
                    query = query.OrderBy(x => x.Email);
                }
            }

            else if (cmbCustomerSearchOrderBy.Text == "Country")
            {
                query = query.Where(x => x.Country.Contains(txtSearchCustomer.Text));

                if (cmbCustomerSearchOrder.Text == "Descending")
                {
                    query = query.OrderByDescending(x => x.Country);
                }

                else
                {
                    query = query.OrderBy(x => x.Country);
                }
            }

            var customers = query.ToList();

            dgvCustomer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCustomer.DataSource = null;
            dgvCustomer.DataSource = customers;
            dgvCustomer.Columns[1].HeaderText = "First Name";
            dgvCustomer.Columns[2].HeaderText = "Last Name";
        }

        private void dbCityHandler(Models.Address address, string city, string country)
        {
            if(db.Cities.Count(ci => ci.City1 == city) > 0)
            {
                address.CityID = db.Cities.First(ci => ci.City1 == city).CityID;

                Models.City cityEntity = db.Cities.First(ci => ci.CityID == address.CityID);
                cityEntity.CountryID = db.Countries.First(co => co.Country1 == country).CountryID;
            }

            else
            {
                Models.City newCity = new Models.City();
                newCity.City1 = city;
                newCity.CountryID = db.Countries.First(co => co.Country1 == country).CountryID;
                db.Cities.Add(newCity);
                db.SaveChanges();

                address.CityID = newCity.CityID;
            }
        }

        private void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            populateCustomersDatagridview();
        }

        private void btnClearSearchCustomer_Click(object sender, EventArgs e)
        {
            txtSearchCustomer.Clear();
            cmbCustomerSearchOrderBy.Text = "";
            cmbCustomerSearchOrder.Text = "";
            populateCustomersDatagridview();
        }

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
            var storeQuery = from s in db.Stores
                        join a in db.Addresses on s.AddressID equals a.AddressID
                        join ci in db.Cities on a.CityID equals ci.CityID
                        join co in db.Countries on ci.CountryID equals co.CountryID
                        select new
                        {
                            DisplayValue = a.Address1 + " > " + ci.City1 +  " > " + co.Country1,
                            Value = s.StoreID 
                        };

            cmbAddCustomerStore.DataSource = storeQuery.ToList();
            cmbAddCustomerStore.DisplayMember = "DisplayValue";
            cmbAddCustomerStore.ValueMember = "Value";

            var countryQuery = from co in db.Countries
                        select new
                        {
                            DisplayValue = co.Country1,
                            Value = co.CountryID
                        };

            cmbAddCustomerCountry.DataSource = countryQuery.ToList();
            cmbAddCustomerCountry.DisplayMember = "DisplayValue";
            cmbAddCustomerCountry.ValueMember = "Value";

            dgvCustomer.Hide();
            grpUpdateCustomer.Hide();
            grpAddCustomer.Show();
        }

        private void btnAddCustomerAddCustomer_Click(object sender, EventArgs e)
        {
            if (txtAddCustomerFName.Text == "" || txtAddCustomerLName.Text == "" || txtAddCustomerEmail.Text == "" ||
                txtAddCustomerAddress1.Text == "" || txtAddCustomerCity.Text == "")
            {
                MessageBox.Show("Please complete all the required fields.");
            }

            else
            {
                Models.Customer newCustomer = new Models.Customer();
                Models.Address newAddress = new Models.Address();

                newCustomer.FirstName = txtAddCustomerFName.Text;
                newCustomer.LastName = txtAddCustomerLName.Text;
                newCustomer.Email = txtAddCustomerEmail.Text;
                newCustomer.Active = true;
                newCustomer.StoreID = (int)cmbAddCustomerStore.SelectedValue;
                newCustomer.CreateDate = new DateTime();

                newAddress.Address1 = txtAddCustomerAddress1.Text;
                newAddress.Address2 = txtAddCustomerAddress2.Text;
                newAddress.PostalCode = txtAddCustomerPostalCode.Text;
                newAddress.District = txtAddCustomerDistrict.Text;
                newAddress.Phone = txtAddCustomerPhone.Text;
                dbCityHandler(newAddress, txtAddCustomerCity.Text, cmbAddCustomerCountry.Text);
            
                db.Addresses.Add(newAddress);
                newCustomer.AddressID = newAddress.AddressID;
                db.Customers.Add(newCustomer);
                db.SaveChanges();

                populateCustomersDatagridview();

                grpAddCustomer.Hide();
                grpUpdateCustomer.Hide();
                dgvCustomer.Show();
            }
        }

        private void btnAddCustomerCancel_Click(object sender, EventArgs e)
        {
            grpAddCustomer.Hide();
            grpUpdateCustomer.Hide();
            dgvCustomer.Show();
        }

        private void btnUpdateCustomerUpdateCustomer_Click(object sender, EventArgs e)
        {
            if (txtUpdateCustomerFName.Text == "" || txtUpdateCustomerLName.Text == "" || txtUpdateCustomerEmail.Text == "" ||
                txtUpdateCustomerAddress1.Text == "" || txtUpdateCustomerCity.Text == "")
            {
                MessageBox.Show("Please complete all the required fields.");
            }

            else
            {
                int recordToUpdateID = (int)dgvCustomer.SelectedRows[0].Cells[0].Value;

                Models.Customer customer = db.Customers.First(c => c.CustomerID == recordToUpdateID);
                Models.Address address = db.Addresses.First(a => a.AddressID == customer.AddressID);
                Models.City city = db.Cities.First(ci => ci.CityID == address.CityID);

                customer.FirstName = txtUpdateCustomerFName.Text;
                customer.LastName = txtUpdateCustomerLName.Text;
                customer.Email = txtUpdateCustomerEmail.Text;

                address.Address1 = txtUpdateCustomerAddress1.Text;
                address.Address2 = txtUpdateCustomerAddress2.Text;
                address.District = txtUpdateCustomerDistrict.Text;
                address.Phone = txtUpdateCustomerPhone.Text;
                address.PostalCode = txtUpdateCustomerPostalCode.Text;
                dbCityHandler(address, txtUpdateCustomerCity.Text, cmbUpdateCustomerCountry.Text);
                customer.Active = chkUpdateCustomerActive.Checked;

                customer.AddressID = address.AddressID;
                db.SaveChanges();

                populateCustomersDatagridview();

                grpAddCustomer.Hide();
                grpUpdateCustomer.Hide();
                dgvCustomer.Show();
            }
        }

        private void btnUpdateCustomerCancel_Click(object sender, EventArgs e)
        {
            grpAddCustomer.Hide();
            grpUpdateCustomer.Hide();
            dgvCustomer.Show();
        }

        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            var storeQuery = from s in db.Stores
                             join a in db.Addresses on s.AddressID equals a.AddressID
                             join ci in db.Cities on a.CityID equals ci.CityID
                             join co in db.Countries on ci.CountryID equals co.CountryID
                             select new
                             {
                                 DisplayValue = a.Address1 + " > " + ci.City1 + " > " + co.Country1,
                                 Value = s.StoreID
                             };

            cmbUpdateCustomerStore.DataSource = storeQuery.ToList();
            cmbUpdateCustomerStore.DisplayMember = "DisplayValue";
            cmbUpdateCustomerStore.ValueMember = "Value";

            var countryQuery = from co in db.Countries
                               select new
                               {
                                   DisplayValue = co.Country1,
                                   Value = co.CountryID
                               };

            cmbUpdateCustomerCountry.DataSource = countryQuery.ToList();
            cmbUpdateCustomerCountry.DisplayMember = "DisplayValue";
            cmbUpdateCustomerCountry.ValueMember = "Value";

            int recordToUpdateID = (int)dgvCustomer.SelectedRows[0].Cells[0].Value;

            Models.Customer customer = db.Customers.First(c => c.CustomerID == recordToUpdateID);
            Models.Address address = db.Addresses.First(a => a.AddressID == customer.AddressID);
            Models.City city = db.Cities.First(ci => ci.CityID == address.CityID);

            txtUpdateCustomerFName.Text = customer.FirstName;
            txtUpdateCustomerLName.Text = customer.LastName;
            txtUpdateCustomerEmail.Text = customer.Email;
            txtUpdateCustomerAddress1.Text = address.Address1;
            txtUpdateCustomerAddress2.Text = address.Address2;
            txtUpdateCustomerCity.Text = city.City1;
            txtUpdateCustomerDistrict.Text = address.District;
            txtUpdateCustomerPhone.Text = address.Phone;
            txtUpdateCustomerPostalCode.Text = address.PostalCode;
            cmbUpdateCustomerStore.SelectedValue = customer.StoreID;
            cmbUpdateCustomerCountry.Text = db.Countries.First(co => co.CountryID == city.CountryID).Country1;
            chkUpdateCustomerActive.Checked = customer.Active;

            dgvCustomer.Hide();
            grpAddCustomer.Hide();
            grpUpdateCustomer.Show();
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you wish to delete this customer record?" +
                "\n\nNote: The customer record will only be set to inactive and not completely removed.", "Delete Customer",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                int recordToDeleteID = (int)dgvCustomer.SelectedRows[0].Cells[0].Value;
                db.Customers.First(cu => cu.CustomerID == recordToDeleteID).Active = false;
                db.SaveChanges();

                populateCustomersDatagridview();

                grpAddCustomer.Hide();
                grpUpdateCustomer.Hide();
                dgvCustomer.Show();
            }
        }

        //***************************************Management Section********************************************//

        private class EmployeeProjection
        {
            public int EmployeeID { get; set; }
            public string FName { get; set; }
            public string LName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public string Store { get; set; }
            public bool Active { get; set; }
        }

        private void populateEmployeeDatagridview()
        {
            var query = from em in db.Staffs
                        join ea in db.Addresses on em.AddressID equals ea.AddressID
                        join st in db.Stores on em.StoreID equals st.StoreID
                        join ad in db.Addresses on st.AddressID equals ad.AddressID
                        join ci in db.Cities on ad.CityID equals ci.CityID
                        join co in db.Countries on ci.CountryID equals co.CountryID
                        select new EmployeeProjection()
                        {
                            EmployeeID = em.StaffID,
                            FName = em.FirstName,
                            LName = em.LastName,
                            Email = em.Email,
                            Phone = ea.Phone,
                            Address = ea.Address1,
                            Store = ad.Address1 + " > " + ci.City1 + " > " + co.Country1,
                            Active = em.Active
                        };

            if (cmbEmployeeSearchOrderBy.Text == "First Name")
            {
                query = query.Where(x => x.FName.Contains(txtSearchEmployee.Text));

                if (cmbEmployeeSearchOrder.Text == "Descending")
                {
                    query = query.OrderByDescending(x => x.FName);
                }

                else
                {
                    query = query.OrderBy(x => x.FName);
                }
            }

            else if (cmbEmployeeSearchOrderBy.Text == "Last Name")
            {
                query = query.Where(x => x.LName.Contains(txtSearchEmployee.Text));

                if (cmbEmployeeSearchOrder.Text == "Descending")
                {
                    query = query.OrderByDescending(x => x.LName);
                }

                else
                {
                    query = query.OrderBy(x => x.LName);
                }
            }

            else if (cmbEmployeeSearchOrderBy.Text == "Email Address")
            {
                query = query.Where(x => x.Email.Contains(txtSearchEmployee.Text));

                if (cmbEmployeeSearchOrder.Text == "Descending")
                {
                    query = query.OrderByDescending(x => x.Email);
                }

                else
                {
                    query = query.OrderBy(x => x.Email);
                }
            }

            else if (cmbEmployeeSearchOrderBy.Text == "Store")
            {
                query = query.Where(x => x.Store.Contains(txtSearchEmployee.Text));

                if (cmbEmployeeSearchOrder.Text == "Descending")
                {
                    query = query.OrderByDescending(x => x.Store);
                }

                else
                {
                    query = query.OrderBy(x => x.Store);
                }
            }

            var employees = query.ToList();

            dgvEmployees.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEmployees.DataSource = null;
            dgvEmployees.DataSource = employees;
            dgvEmployees.Columns[1].HeaderText = "First Name";
            dgvEmployees.Columns[2].HeaderText = "Last Name";
        }

        private void btnSearchEmployee_Click(object sender, EventArgs e)
        {
            populateEmployeeDatagridview();
        }

        private void btnSearchEmployeeClear_Click(object sender, EventArgs e)
        {
            txtSearchEmployee.Clear();
            cmbEmployeeSearchOrderBy.Text = "";
            cmbEmployeeSearchOrder.Text = "";
            populateEmployeeDatagridview();
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            var storeQuery = from s in db.Stores
                             join a in db.Addresses on s.AddressID equals a.AddressID
                             join ci in db.Cities on a.CityID equals ci.CityID
                             join co in db.Countries on ci.CountryID equals co.CountryID
                             select new
                             {
                                 DisplayValue = a.Address1 + " > " + ci.City1 + " > " + co.Country1,
                                 Value = s.StoreID
                             };

            cmbAddEmployeeStore.DataSource = storeQuery.ToList();
            cmbAddEmployeeStore.DisplayMember = "DisplayValue";
            cmbAddEmployeeStore.ValueMember = "Value";

            var countryQuery = from co in db.Countries
                               select new
                               {
                                   DisplayValue = co.Country1,
                                   Value = co.CountryID
                               };

            cmbAddEmployeeCountry.DataSource = countryQuery.ToList();
            cmbAddEmployeeCountry.DisplayMember = "DisplayValue";
            cmbAddEmployeeCountry.ValueMember = "Value";

            dgvEmployees.Hide();
            grpUpdateEmployee.Hide();
            grpAddEmployee.Show();
        }

        private void btnUpdateEmployee_Click(object sender, EventArgs e)
        {
            var storeQuery = from s in db.Stores
                             join a in db.Addresses on s.AddressID equals a.AddressID
                             join ci in db.Cities on a.CityID equals ci.CityID
                             join co in db.Countries on ci.CountryID equals co.CountryID
                             select new
                             {
                                 DisplayValue = a.Address1 + " > " + ci.City1 + " > " + co.Country1,
                                 Value = s.StoreID
                             };

            cmbUpdateEmployeeStore.DataSource = storeQuery.ToList();
            cmbUpdateEmployeeStore.DisplayMember = "DisplayValue";
            cmbUpdateEmployeeStore.ValueMember = "Value";

            var countryQuery = from co in db.Countries
                               select new
                               {
                                   DisplayValue = co.Country1,
                                   Value = co.CountryID
                               };

            cmbUpdateEmployeeCountry.DataSource = countryQuery.ToList();
            cmbUpdateEmployeeCountry.DisplayMember = "DisplayValue";
            cmbUpdateEmployeeCountry.ValueMember = "Value";

            int recordToUpdateID = (int)dgvEmployees.SelectedRows[0].Cells[0].Value;

            Models.Staff employee = db.Staffs.First(em => em.StaffID == recordToUpdateID);
            Models.Address address = db.Addresses.First(a => a.AddressID == employee.AddressID);
            Models.City city = db.Cities.First(ci => ci.CityID == address.CityID);

            txtUpdateEmployeeFName.Text = employee.FirstName;
            txtUpdateEmployeeLName.Text = employee.LastName;
            txtUpdateEmployeeEmail.Text = employee.Email;
            txtUpdateEmployeeAddress1.Text = address.Address1;
            txtUpdateEmployeeAddress2.Text = address.Address2;
            txtUpdateEmployeeCity.Text = city.City1;
            txtUpdateEmployeeDistrict.Text = address.District;
            txtUpdateEmployeePhone.Text = address.Phone;
            txtUpdateEmployeePostalCode.Text = address.PostalCode;
            cmbUpdateEmployeeStore.SelectedValue = employee.StoreID;
            cmbUpdateEmployeeCountry.Text = db.Countries.First(co => co.CountryID == city.CountryID).Country1;
            chkUpdateEmployeeActive.Checked = employee.Active;

            dgvEmployees.Hide();
            grpAddEmployee.Hide();
            grpUpdateEmployee.Show();
        }

        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you wish to delete this employee record?" +
                "\n\nNote: The employee record will only be set to inactive and not completely removed.", "Delete Employee",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                int recordToDeleteID = (int)dgvEmployees.SelectedRows[0].Cells[0].Value;
                db.Staffs.First(em => em.StaffID == recordToDeleteID).Active = false;
                db.SaveChanges();

                populateEmployeeDatagridview();

                grpAddEmployee.Hide();
                grpUpdateEmployee.Hide();
                dgvEmployees.Show();
            }
        }

        private void btnAddEmployeeAddEmployee_Click(object sender, EventArgs e)
        {
            if (txtAddEmployeeFName.Text == "" || txtAddEmployeeLName.Text == "" || txtAddEmployeeEmail.Text == "" ||
                txtAddEmployeeAddress1.Text == "" || txtAddEmployeeCity.Text == "")
            {
                MessageBox.Show("Please complete all the required fields.");
            }

            else
            {
                Models.Staff newEmployee = new Models.Staff();
                Models.Address newAddress = new Models.Address();

                newEmployee.FirstName = txtAddEmployeeFName.Text;
                newEmployee.LastName = txtAddEmployeeLName.Text;
                newEmployee.Email = txtAddEmployeeEmail.Text;
                newEmployee.Active = true;
                newEmployee.StoreID = (int)cmbAddEmployeeStore.SelectedValue;

                newAddress.Address1 = txtAddEmployeeAddress1.Text;
                newAddress.Address2 = txtAddEmployeeAddress2.Text;
                newAddress.PostalCode = txtAddEmployeePostalCode.Text;
                newAddress.District = txtAddEmployeeDistrict.Text;
                newAddress.Phone = txtAddEmployeePhone.Text;
                dbCityHandler(newAddress, txtAddEmployeeCity.Text, cmbAddEmployeeCountry.Text);

                db.Addresses.Add(newAddress);
                newEmployee.AddressID = newAddress.AddressID;
                db.Staffs.Add(newEmployee);
                db.SaveChanges();

                populateEmployeeDatagridview();

                grpAddEmployee.Hide();
                grpUpdateEmployee.Hide();
                dgvEmployees.Show();
            }
        }

        private void btnAddEmployeeCancel_Click(object sender, EventArgs e)
        {
            grpAddEmployee.Hide();
            grpUpdateEmployee.Hide();
            dgvEmployees.Show();
        }

        private void btnUpdateEmployeeUpdateEmployee_Click(object sender, EventArgs e)
        {
            if (txtUpdateEmployeeFName.Text == "" || txtUpdateEmployeeLName.Text == "" || txtUpdateEmployeeEmail.Text == "" ||
                txtUpdateEmployeeAddress1.Text == "" || txtUpdateEmployeeCity.Text == "")
            {
                MessageBox.Show("Please complete all the required fields.");
            }

            else
            {
                int recordToUpdateID = (int)dgvEmployees.SelectedRows[0].Cells[0].Value;

                Models.Staff employee = db.Staffs.First(em => em.StaffID == recordToUpdateID);
                Models.Address address = db.Addresses.First(a => a.AddressID == employee.AddressID);
                Models.City city = db.Cities.First(ci => ci.CityID == address.CityID);

                employee.FirstName = txtUpdateEmployeeFName.Text;
                employee.LastName = txtUpdateEmployeeLName.Text;
                employee.Email = txtUpdateEmployeeEmail.Text;
                address.Address1 = txtUpdateEmployeeAddress1.Text;
                address.Address2 = txtUpdateEmployeeAddress2.Text;
                address.District = txtUpdateEmployeeDistrict.Text;
                address.Phone = txtUpdateEmployeePhone.Text;
                address.PostalCode = txtUpdateEmployeePostalCode.Text;
                dbCityHandler(address, txtUpdateEmployeeCity.Text, cmbUpdateEmployeeCountry.Text);
                employee.Active = chkUpdateEmployeeActive.Checked;
                employee.StoreID = (int)cmbUpdateEmployeeStore.SelectedValue;

                employee.AddressID = address.AddressID;
                db.SaveChanges();

                populateEmployeeDatagridview();
                populateStoreDatagridview();

                grpAddEmployee.Hide();
                grpUpdateEmployee.Hide();
                dgvEmployees.Show();
            }
        }

        private void btnUpdateEmployeeCancel_Click(object sender, EventArgs e)
        {
            grpAddEmployee.Hide();
            grpUpdateEmployee.Hide();
            dgvEmployees.Show();
        }

        private void dgvEmployees_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count != 0)
            {
                btnUpdateEmployee.Enabled = true;
                btnDeleteEmployee.Enabled = true;
            }

            else
            {
                btnUpdateEmployee.Enabled = false;
                btnUpdateEmployee.Enabled = false;
            }
        }

        private class StoreProjection
        {
            public int StoreID { get; set; }
            public string Address { get; set; }
            public string District { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            public string PostalCode { get; set; }
            public string Telephone { get; set; }
            public string Manager { get; set; }
        }

        private void populateStoreDatagridview()
        {
            var query = from st in db.Stores
                        join ad in db.Addresses on st.AddressID equals ad.AddressID
                        join ci in db.Cities on ad.CityID equals ci.CityID
                        join co in db.Countries on ci.CountryID equals co.CountryID
                        join mn in db.Staffs on st.ManagerStaffID equals mn.StaffID
                        select new StoreProjection()
                        {
                            StoreID = st.StoreID,
                            Address = ad.Address1,
                            District = ad.District,
                            City = ci.City1,
                            Country = co.Country1,
                            PostalCode = ad.PostalCode,
                            Telephone = ad.Phone,
                            Manager = mn.FirstName + " " + mn.LastName
                        };

            if (cmbStoreSearchOrderBy.Text == "Manager")
            {
                query = query.Where(x => x.Manager.Contains(txtSearchStore.Text));

                if (cmbStoreSearchOrder.Text == "Descending")
                {
                    query = query.OrderByDescending(x => x.Manager);
                }

                else
                {
                    query = query.OrderBy(x => x.Manager);
                }
            }

            else if (cmbStoreSearchOrderBy.Text == "City")
            {
                query = query.Where(x => x.City.Contains(txtSearchStore.Text));

                if (cmbStoreSearchOrder.Text == "Descending")
                {
                    query = query.OrderByDescending(x => x.City);
                }

                else
                {
                    query = query.OrderBy(x => x.City);
                }
            }

            else if (cmbStoreSearchOrderBy.Text == "District")
            {
                query = query.Where(x => x.District.Contains(txtSearchStore.Text));

                if (cmbStoreSearchOrder.Text == "Descending")
                {
                    query = query.OrderByDescending(x => x.District);
                }

                else
                {
                    query = query.OrderBy(x => x.District);
                }
            }

            else if (cmbStoreSearchOrderBy.Text == "Country")
            {
                query = query.Where(x => x.Country.Contains(txtSearchStore.Text));

                if (cmbStoreSearchOrder.Text == "Descending")
                {
                    query = query.OrderByDescending(x => x.Country);
                }

                else
                {
                    query = query.OrderBy(x => x.Country);
                }
            }

            var stores = query.ToList();

            dgvStores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStores.DataSource = null;
            dgvStores.DataSource = stores;
            dgvStores.Columns[5].HeaderText = "Postal Code";
        }

        private void btnSearchStore_Click(object sender, EventArgs e)
        {
            populateStoreDatagridview();
        }

        private void btnSearchStoreClear_Click(object sender, EventArgs e)
        {
            txtSearchStore.Clear();
            cmbStoreSearchOrderBy.Text = "";
            cmbStoreSearchOrder.Text = "";
            populateStoreDatagridview();
        }

        private void dgvStores_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStores.SelectedRows.Count != 0)
            {
                btnUpdateStore.Enabled = true;
            }

            else
            {
                btnUpdateStore.Enabled = false;
            }
        }

        private void btnAddStore_Click(object sender, EventArgs e)
        {
            var managerQuery = from em in db.Staffs
                               select new
                               {
                                   DisplayValue = em.FirstName + " " + em.LastName,
                                   Value = em.StaffID
                               };

            cmbAddStoreManager.DataSource = managerQuery.ToList();
            cmbAddStoreManager.DisplayMember = "DisplayValue";
            cmbAddStoreManager.ValueMember = "Value";

            var countryQuery = from co in db.Countries
                               select new
                               {
                                   DisplayValue = co.Country1,
                                   Value = co.CountryID
                               };

            cmbAddStoreCountry.DataSource = countryQuery.ToList();
            cmbAddStoreCountry.DisplayMember = "DisplayValue";
            cmbAddStoreCountry.ValueMember = "Value";

            dgvStores.Hide();
            grpUpdateStore.Hide();
            grpAddStore.Show();
        }

        private void btnUpdateStore_Click(object sender, EventArgs e)
        {
            var managerQuery = from em in db.Staffs
                               select new
                               {
                                   DisplayValue = em.FirstName + " " + em.LastName,
                                   Value = em.StaffID
                               };

            cmbUpdateStoreManager.DataSource = managerQuery.ToList();
            cmbUpdateStoreManager.DisplayMember = "DisplayValue";
            cmbUpdateStoreManager.ValueMember = "Value";

            var countryQuery = from co in db.Countries
                               select new
                               {
                                   DisplayValue = co.Country1,
                                   Value = co.CountryID
                               };

            cmbUpdateStoreCountry.DataSource = countryQuery.ToList();
            cmbUpdateStoreCountry.DisplayMember = "DisplayValue";
            cmbUpdateStoreCountry.ValueMember = "Value";

            int recordToUpdateID = (int)dgvStores.SelectedRows[0].Cells[0].Value;

            Models.Store store = db.Stores.First(st => st.StoreID == recordToUpdateID);
            Models.Address address = db.Addresses.First(a => a.AddressID == store.AddressID);
            Models.City city = db.Cities.First(ci => ci.CityID == address.CityID);
            Models.Staff manager = db.Staffs.First(em => em.StaffID == store.ManagerStaffID);

            txtUpdateStoreAddress1.Text = address.Address1;
            txtUpdateStoreAddress2.Text = address.Address2;
            txtUpdateStoreCity.Text = city.City1;
            txtUpdateStoreDistrict.Text = address.District;
            txtUpdateStorePhone.Text = address.Phone;
            txtUpdateStorePostalCode.Text = address.PostalCode;
            cmbUpdateStoreManager.SelectedValue = manager.StaffID;
            cmbUpdateStoreCountry.Text = db.Countries.First(co => co.CountryID == city.CountryID).Country1;

            dgvStores.Hide();
            grpAddStore.Hide();
            grpUpdateStore.Show();
        }

        private void btnDeleteStore_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you wish to delete this store record?", "Delete Store",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                int recordToDeleteID = (int)dgvStores.SelectedRows[0].Cells[0].Value;
                db.Stores.Remove(db.Stores.First(st => st.StoreID == recordToDeleteID));

                foreach (Models.Staff employee in db.Staffs.Where(em => em.StoreID == recordToDeleteID))
                {
                    employee.Active = false;
                }

                db.SaveChanges();

                populateEmployeeDatagridview();
                populateStoreDatagridview();

                grpAddStore.Hide();
                grpUpdateStore.Hide();
                dgvStores.Show();
            }
        }

        private void btnUpdateStoreUpdateStore_Click(object sender, EventArgs e)
        {
            if (txtUpdateStorePhone.Text == "" || txtUpdateStoreCity.Text == "" || txtUpdateStoreAddress1.Text == "" ||
                            txtUpdateStorePostalCode.Text == "" || txtUpdateStoreDistrict.Text == "")
            {
                MessageBox.Show("Please complete all the required fields.");
            }

            else
            {
                int recordToUpdateID = (int)dgvStores.SelectedRows[0].Cells[0].Value;

                Models.Store store = db.Stores.First(st => st.StoreID == recordToUpdateID);
                Models.Address address = db.Addresses.First(ad => ad.AddressID == store.AddressID);

                store.ManagerStaffID = (int)cmbUpdateStoreManager.SelectedValue;

                address.Address1 = txtUpdateStoreAddress1.Text;
                address.Address2 = txtUpdateStoreAddress2.Text;
                address.PostalCode = txtUpdateStorePostalCode.Text;
                address.District = txtUpdateStoreDistrict.Text;
                address.Phone = txtUpdateStorePhone.Text;
                dbCityHandler(address, txtUpdateStoreCity.Text, cmbUpdateStoreCountry.Text);

                store.AddressID = address.AddressID;
                db.SaveChanges();

                populateStoreDatagridview();

                grpUpdateStore.Hide();
                grpAddStore.Hide();
                dgvStores.Show();
            }
        }

        private void btnUpdateStoreCancel_Click(object sender, EventArgs e)
        {
            grpAddStore.Hide();
            grpUpdateStore.Hide();
            dgvStores.Show();
        }

        private void btnAddStoreAddStore_Click(object sender, EventArgs e)
        {
            if (txtAddStorePhone.Text == "" || txtAddStoreCity.Text == "" || txtAddStoreAddress1.Text == "" ||
                txtAddStorePostalCode.Text == "" || txtAddStoreDistrict.Text == "")
            {
                MessageBox.Show("Please complete all the required fields.");
            }

            else
            {
                Models.Store newStore = new Models.Store();
                Models.Address newAddress = new Models.Address();

                newStore.ManagerStaffID = (int)cmbAddStoreManager.SelectedValue;

                newAddress.Address1 = txtAddStoreAddress1.Text;
                newAddress.Address2 = txtAddStoreAddress2.Text;
                newAddress.PostalCode = txtAddStorePostalCode.Text;
                newAddress.District = txtAddStoreDistrict.Text;
                newAddress.Phone = txtAddStorePhone.Text;
                dbCityHandler(newAddress, txtAddStoreCity.Text, cmbAddStoreCountry.Text);

                db.Addresses.Add(newAddress);
                newStore.AddressID = newAddress.AddressID;
                db.Stores.Add(newStore);
                db.SaveChanges();

                populateStoreDatagridview();

                grpUpdateStore.Hide();
                grpAddStore.Hide();
                dgvStores.Show();
            }
        }

        private void btnAddStoreCancel_Click(object sender, EventArgs e)
        {
            grpAddStore.Hide();
            grpUpdateStore.Hide();
            dgvStores.Show();
        }

        
    }
}
