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
