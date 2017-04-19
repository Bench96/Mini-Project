using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        public frmMain()
        {
            InitializeComponent();

            var games = db.Games.ToList();
            var developers = db.Developers.ToList();
            //foreach(Models.Game game in games)
            //{
            //    game.Developers = Where(u => u.Developers.Any(v => v.DeveloperID))
            //}
            //gameList.DataSource = db.Games.ToList();
            //foreach(Models.Game game in gameList)
            //{

            //}
            addGameGroup.Hide();

            grpAddCustomer.Hide();
            grpUpdateCustomer.Hide();
            populateCustomersDatagridview();
        }

        //***************************************Games Section********************************************//

        private void gameSearchBTN_Click(object sender, EventArgs e)
        {
            string searchQuery = gameSearchTextBox.Text;

            if(searchQuery != null)
            {
                gameList.DataSource = db.Games.Where(u => u.Title.ToLower().Contains(searchQuery.ToLower())).ToList();
            }
        }

        private void clearSearchResultsBTN_Click(object sender, EventArgs e)
        {
            gameList.DataSource = db.Games.ToList();
        }

        private void btnAddGame_Click(object sender, EventArgs e)
        {
            gameList.Hide();
            addGameGroup.Show();
            
            //populate combo boxes
            gameDeveloperComboBox.DataSource = db.Developers.Select(u => u.Name).ToList();
            gameGenreComboBox.DataSource = db.Genres.Select(u => u.Name).ToList();
            gamePublisherComboBox.DataSource = db.Publishers.Select(u => u.Name).ToList();
        }

        private void addGameBTN_Click(object sender, EventArgs e)
        {
            if(gameTitleTextBox.Text == "" || gameReleaseYearTextBox.Text == "" || gameRentalRateTextBox.Text == "")
            {
                MessageBox.Show("Please complete all the fields");
            }
            else
            {
                gameList.Show();
                addGameGroup.Hide();
            }   
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

            var customers = query.ToList();

            dgvCustomer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvCustomer.DataSource = null;
            dgvCustomer.DataSource = customers;
            dgvCustomer.Columns[1].HeaderText = "First Name";
            dgvCustomer.Columns[2].HeaderText = "Last Name";
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
            cmbAddCustomerCountry.DataSource = db.Countries.Select(c => c.Country1).ToList();            

            dgvCustomer.Hide();
            grpUpdateCustomer.Hide();
            grpAddCustomer.Show();
        }

        private void btnAddCustomerAddCustomer_Click(object sender, EventArgs e)
        {
            if (txtAddCustomerFName.Text == "" || txtAddCustomerLName.Text == "" || txtAddCustomerEmail.Text == "" ||
                txtUpdateCustomerAddress1.Text == "" || txtAddCustomerCity.Text == "")
            {
                MessageBox.Show("Please complete all the fields");
            }

            else
            {
                // TODO Add Customer
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
                MessageBox.Show("Please complete all the fields");
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
                city.City1 = txtUpdateCustomerCity.Text;
                city.CountryID = db.Countries.First(co => co.Country1 == cmbUpdateCustomerCountry.Text).CountryID;
                customer.Active = chkUpdateCustomerActive.Checked;

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
            cmbUpdateCustomerCountry.DataSource = db.Countries.Select(c => c.Country1).ToList();

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
            cmbUpdateCustomerCountry.Text = db.Countries.First(co => co.CountryID == city.CountryID).Country1;
            chkUpdateCustomerActive.Checked = customer.Active;

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
                int recordToDeleteID = (int)dgvCustomer.SelectedRows[0].Cells[0].Value;
                db.Customers.Remove(db.Customers.First(c => c.CustomerID == recordToDeleteID));
                db.SaveChanges();

                populateCustomersDatagridview();

                grpAddCustomer.Hide();
                grpUpdateCustomer.Hide();
                dgvCustomer.Show();
            }
        }
    }
}
