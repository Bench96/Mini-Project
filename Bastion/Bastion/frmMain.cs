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

            // Customer tab init
            grpAddCustomer.Hide();
            grpUpdateCustomer.Hide();
            populateCustomersDatagridview();

            // Management tab init
            grpAddEmployee.Hide();
            grpUpdateEmployee.Hide();

            populateEmployeeDatagridview();
            populateStoreDatagridview();
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

        private int dbCityHandler(string city, string country)
        {
            if(db.Cities.Count(ci => ci.City1 == city) > 0)
            {
                return db.Cities.First(ci => ci.City1 == city).CityID;
            }

            else
            {
                Models.City newCity = new Models.City();
                newCity.City1 = city;
                newCity.CountryID = db.Countries.First(co => co.Country1 == country).CountryID;
                db.Cities.Add(newCity);
                db.SaveChanges();

                return newCity.CityID;
            }
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
                txtUpdateCustomerAddress1.Text == "" || txtAddCustomerCity.Text == "")
            {
                MessageBox.Show("Please complete all the fields");
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
                newAddress.CityID = dbCityHandler(txtAddCustomerCity.Text, cmbAddCustomerCountry.Text);

                db.Addresses.Add(newAddress);
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
                address.District = txtUpdateCustomerDistrict.Text;
                address.Phone = txtUpdateCustomerPhone.Text;
                address.PostalCode = txtUpdateCustomerPostalCode.Text;
                address.CityID = dbCityHandler(txtUpdateCustomerCity.Text, cmbUpdateCustomerCountry.Text);
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

        //***************************************Management Section********************************************//

        private class EmployeeProjection
        {
            public int EmployeeID { get; set; }
            public string FName { get; set; }
            public string LName { get; set; }
            public string Email { get; set; }
            public string Store { get; set; }
            public bool Active { get; set; }
        }

        private void populateEmployeeDatagridview()
        {
            var query = from em in db.Staffs
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
                            Store = ad.Address1 + " > " + ci.City1 + " > " + co.Country1,
                            Active = em.Active
                        };

            var employees = query.ToList();

            dgvEmployees.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvEmployees.DataSource = null;
            dgvEmployees.DataSource = employees;
            dgvEmployees.Columns[1].HeaderText = "First Name";
            dgvEmployees.Columns[2].HeaderText = "Last Name";
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdateEmployee_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {

        }

        private void btnAddEmployeeAddEmployee_Click(object sender, EventArgs e)
        {

        }

        private void btnAddEmployeeCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdateEmployeeUpdateEmployee_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdateEmployeeCancel_Click(object sender, EventArgs e)
        {

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
        }

        private void populateStoreDatagridview()
        {
            var query = from st in db.Stores
                        join ad in db.Addresses on st.AddressID equals ad.AddressID
                        join ci in db.Cities on ad.CityID equals ci.CityID
                        join co in db.Countries on ci.CountryID equals co.CountryID
                        select new StoreProjection()
                        {
                            StoreID = st.StoreID,
                            Address = ad.Address1,
                            District = ad.District,
                            City = ci.City1,
                            Country = co.Country1,
                            PostalCode = ad.PostalCode,
                            Telephone = ad.Phone
                        };

            var stores = query.ToList();

            dgvStores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvStores.DataSource = null;
            dgvStores.DataSource = stores;
            dgvStores.Columns[5].HeaderText = "Postal Code";
        }

        private void dgvStores_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStores.SelectedRows.Count != 0)
            {
                btnUpdateStore.Enabled = true;
                btnDeleteStore.Enabled = true;
            }

            else
            {
                btnUpdateStore.Enabled = false;
                btnDeleteStore.Enabled = false;
            }
        }

        private void btnAddStore_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdateStore_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteStore_Click(object sender, EventArgs e)
        {

        }
    }
}
