using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Sara_Tourism_System
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void LoadDashBoardCounts()
        {
            const string query = @"SELECT(SELECT COUNT(*) FROM Activity) AS ActivityCount, (SELECT COUNT(*) FROM Tour_Guide) AS GuideCount,
                                    (SELECT COUNT(*) FROM Tourist) AS TouristCount,
                                    COUNT(*) AS BookingCount, SUM(CASE WHEN Payment_Status = 'Successful' THEN 1 ELSE 0 END) AS PaidCount,
                                    SUM(CASE WHEN Payment_Status = 'Pending' THEN 1 ELSE 0 END) AS PendingCount,
                                    
                                    SUM(CASE WHEN Payment_Status = 'Refunded' THEN 1 ELSE 0 END) AS CancelledCount FROM Booking;";

            try
            {
                using(MySqlConnection connection = DatabaseConnection.GetConnection())
                using(MySqlCommand command = new MySqlCommand(query,connection))
                {
                    connection.Open();
                    using(MySqlDataReader reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            lblActivityCount.Text = reader["ActivityCount"] == DBNull.Value ? "0" : reader["ActivityCount"].ToString();

                            lblGuideCount.Text = reader["GuideCount"] == DBNull.Value ? "0" : reader["GuideCount"].ToString();

                            lblBookingCount.Text = reader["BookingCount"] == DBNull.Value ? "0" : reader["BookingCount"].ToString();

                            lblPaidCount.Text = reader["PaidCount"] == DBNull.Value ? "0" : reader["PaidCount"].ToString();

                            lblPendingCount.Text = reader["PendingCount"] == DBNull.Value ? "0" : reader["PendingCount"].ToString();

                            lblCancelled.Text = reader["CancelledCount"] == DBNull.Value ? "0" : reader["CancelledCount"].ToString();

                            lblTouristCount.Text = reader["TouristCount"] == DBNull.Value ? "0" : reader["TouristCount"].ToString();
                        }
                    }
                }
            }
            catch(MySqlException ex)
            {
                MessageBox.Show("The dashboard statistics could not be loaded.\nPlease check your database connection and try again "," Database Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

            }
            catch(Exception)
            {
                MessageBox.Show("An unexpected error occured while oading the dashboard","Dashboard Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblBookingCount_Click(object sender, EventArgs e)
        {

        }

        private void Admin_Load(object sender, EventArgs e)
        {
            tabAdmin.TabPages.Remove(Activities);
            tabAdmin.TabPages.Remove(Tourists);
            tabAdmin.TabPages.Remove(Bookings);
            tabAdmin.TabPages.Remove(Guides);
            tabAdmin.TabPages.Remove(Reports);

            LoadDashBoardCounts();
            

        }

        private void button18_Click(object sender, EventArgs e)
        {

        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            tabAdmin.TabPages.Remove(Dashboard);
            tabAdmin.TabPages.Remove(Activities);
            tabAdmin.TabPages.Remove(Tourists);
            tabAdmin.TabPages.Remove(Bookings);
            tabAdmin.TabPages.Remove(Guides);
            tabAdmin.TabPages.Remove(Reports);
            tabAdmin.TabPages.Add(Dashboard);

            LoadDashBoardCounts();
            
        }

        private void btnActivities_Click(object sender, EventArgs e)
        {
            tabAdmin.TabPages.Remove(Activities);
            tabAdmin.TabPages.Remove(Dashboard);
            tabAdmin.TabPages.Remove(Tourists);
            tabAdmin.TabPages.Remove(Bookings);
            tabAdmin.TabPages.Remove(Guides);
            tabAdmin.TabPages.Remove(Reports);
            tabAdmin.TabPages.Add(Activities);

            try
            {
                using(MySqlConnection connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    string query = @"SELECT * FROM Activity";

                    using(MySqlDataAdapter adapter = new MySqlDataAdapter(query,connection))
                    {
                        DataTable table = new DataTable();


                        adapter.Fill(table);

                        dtgActivity.DataSource = table;


                    }
                }
            }
            catch(MySqlException)
            {
                MessageBox.Show("The activities could not be loaded from the database\nPlease check your database connection and try again.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(Exception)
            {
                MessageBox.Show("An unexpected error occured while loading the activities. Please try again later","Activities Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnTourists_Click(object sender, EventArgs e)
        {
            tabAdmin.TabPages.Remove(Tourists);
            tabAdmin.TabPages.Remove(Dashboard);
            tabAdmin.TabPages.Remove(Activities);
            tabAdmin.TabPages.Remove(Bookings);
            tabAdmin.TabPages.Remove(Guides);
            tabAdmin.TabPages.Remove(Reports);
            tabAdmin.TabPages.Add(Tourists);
        }

        private void btnBookingsTab_Click(object sender, EventArgs e)
        {
            tabAdmin.TabPages.Remove(Bookings);
            tabAdmin.TabPages.Remove(Dashboard);
            tabAdmin.TabPages.Remove(Tourists);
            tabAdmin.TabPages.Remove(Activities);
            tabAdmin.TabPages.Remove(Guides);
            tabAdmin.TabPages.Remove(Reports);
            tabAdmin.TabPages.Add(Bookings);
        }

        private void btnTourGuidesTab_Click(object sender, EventArgs e)
        {
            tabAdmin.TabPages.Remove(Guides);
            tabAdmin.TabPages.Remove(Dashboard);
            tabAdmin.TabPages.Remove(Tourists);
            tabAdmin.TabPages.Remove(Bookings);
            tabAdmin.TabPages.Remove(Activities);
            tabAdmin.TabPages.Remove(Reports);
            tabAdmin.TabPages.Add(Guides);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            tabAdmin.TabPages.Remove(Reports);
            tabAdmin.TabPages.Remove(Dashboard);
            tabAdmin.TabPages.Remove(Tourists);
            tabAdmin.TabPages.Remove(Bookings);
            tabAdmin.TabPages.Remove(Guides);
            tabAdmin.TabPages.Remove(Activities);
            tabAdmin.TabPages.Add(Reports);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtActivitySearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(dtgActivity.DataSource is DataTable table)
                {
                    string searchText = txtActivitySearch.Text;

                    searchText = searchText.Replace("'","''");



                    if(string.IsNullOrWhiteSpace(searchText))
                    {
                        table.DefaultView.RowFilter = "";
                    }
                    else
                    {
                        table.DefaultView.RowFilter = $"Activity_Name LIKE '%{searchText}%'";
                    }
                }
            }
            catch(EvaluateException)
            {
                MessageBox.Show("The activity search could not be applied","Search Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            catch(Exception)
            {
                MessageBox.Show("An unexpected error occurred while searching for activities","Search Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void dtgActivity_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgActivity_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0)
            {
                return;
            }

            try
            {
                DataGridViewRow selectedRow = dtgActivity.Rows[e.RowIndex];

                lblSelectedActivity.Text = selectedRow.Cells["Activity_ID"].Value?.ToString() ?? "";

                txtActivity.Text = selectedRow.Cells["Activity_Name"].Value?.ToString() ?? "";

                txtDuration.Text = selectedRow.Cells["Duration"].Value?.ToString() ?? "";

                if (selectedRow.Cells["Price_Per_Person"].Value != null && selectedRow.Cells["Price_Per_Person"].Value != DBNull.Value)
                {
                    decimal price = Convert.ToDecimal(selectedRow.Cells["Price_Per_Person"].Value);
                    txtPrice.Text = price.ToString("0.00");
                }
                else
                {
                    txtPrice.Clear();
                }

                lstDescription.Items.Clear();

                string description = selectedRow.Cells["Description"].Value?.ToString() ?? "";

                if(!string.IsNullOrWhiteSpace(description))
                {
                    lstDescription.Items.Add(description);
                }
            }
            catch(Exception)
            {
                MessageBox.Show("The selected activity details could not be displayed","Selection Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
    }
}
