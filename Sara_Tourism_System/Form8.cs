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
using MySqlX.XDevAPI;
namespace Sara_Tourism_System
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            dtgCompleteBookings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgCompleteBookings.MultiSelect = false;
            dtgCompleteBookings.ReadOnly = true;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            string Email = txtEmail.Text.Trim();
            if (string.IsNullOrEmpty(Email))
            {
                MessageBox.Show("Please enter an email to search.");
                return;
            }

            using (MySqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                int Tourist_ID = Convert.ToInt32(Session["Tourist_ID"]);

                string sql = @"SELECT b.Booking_ID, 
                               b.Booking_Date, 
                               a.Activity_Name, 
                               g.Name AS TourGuide_Name,,
                               b.Payment_Status
                        FROM booking b
                        INNER JOIN tourist t   ON b.Tourist_ID = t.Tourist_ID
                        INNER JOIN activity a  ON b.Activity_ID = a.Activity_ID
                        INNER JOIN tour_guide g ON b.Tour_Guide_ID = g.TourGuide_ID
                        WHERE t.Email = @Email";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Email", Email);

                    try
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            if (dataTable.Rows.Count == 0)
                            {
                                MessageBox.Show("No bookings found for that email.");
                            }

                            dtgCompleteBookings.DataSource = dataTable;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error retrieving bookings: " + ex.Message);
                    }
                }
                DataGridViewRow row = dtgCompleteBookings.SelectedRows[0];

                lblSelectedActivity.Text = row.Cells["Activity_Name"].Value.ToString();
                lblSelectedTourGuide.Text = row.Cells["TourGuide_Name"].Value.ToString();
                lblSelectedDate.Text = Convert.ToDateTime(row.Cells["Booking_Date"].Value).ToString("yyyy-MM-dd");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dtgCompleteBookings.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a booking to review.");
                return;
            }

            using (MySqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                int Booking_ID = Convert.ToInt32(dtgCompleteBookings.SelectedRows[0].Cells["Booking_ID"].Value);

                string sql = @"DELETE FROM booking WHERE Booking_ID = @Booking_ID";

                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Booking_ID", Booking_ID);

                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Booking deleted successfully.");
                            lblSelectedActivity.Text = "";
                            lblSelectedTourGuide.Text = "";
                            lblSelectedDate.Text = "";
                            btnFind_Click(sender, e); // refresh the grid with current address search
                        }
                        else
                        {
                            MessageBox.Show("Booking could not be deleted. It may no longer exist.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error (delete booking): " + ex.Message);
                        MessageBox.Show("Error deleting booking: " + ex.Message);
                    }
                }
            }
        }

        private void dtgCompleteBookings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgCompleteBookings.SelectedRows.Count == 0)
            {
                lblSelectedActivity.Text = "";
                lblSelectedTourGuide.Text = "";
                lblSelectedDate.Text = "";
                return;
            }

            DataGridViewRow row = dtgCompleteBookings.SelectedRows[0];

            lblSelectedActivity.Text = row.Cells["Activity_Name"].Value.ToString();
            lblSelectedTourGuide.Text = row.Cells["TourGuide_Name"].Value.ToString();
            lblSelectedDate.Text = Convert.ToDateTime(row.Cells["Booking_Date"].Value).ToString("yyyy-MM-dd");
        }
    }
}

