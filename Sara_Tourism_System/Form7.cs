using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
namespace Sara_Tourism_System
{
    public partial class Reviews : Form
    {
        public Reviews()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Reviews_Load(object sender, EventArgs e)
        {
            dtgBookingsView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgBookingsView.MultiSelect = false;
            dtgBookingsView.ReadOnly = true;
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

                string sql = @"SELECT b.* 
                        FROM booking b
                        INNER JOIN tourist t ON b.Tourist_ID = t.Tourist_ID
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

                            dtgBookingsView.DataSource = dataTable;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error (search by email): " + ex.Message);
                        MessageBox.Show("Error retrieving bookings: " + ex.Message);
                    }
                }
            }
        }

        private void dtgBookingsView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            dtgBookingsView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgBookingsView.MultiSelect = false;
            dtgBookingsView.ReadOnly = true;
            */
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (dtgBookingsView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a booking to review.");
                return;
            }

            if (cbxRating.SelectedIndex == -1  )
            {
                MessageBox.Show("Please select a rating before submitting");
                return;
            }

            if (lstComment.Items.Count == 0)
            {
                MessageBox.Show("Please add a comment to the list before submitting");
                return;
            }

            string comment = lstComment.Text;
            int Rating = int.Parse(cbxRating.Text.Trim());
            int Booking_ID = Convert.ToInt32(dtgBookingsView.SelectedRows[0].Cells["Booking_ID"].Value);
            int Tourist_ID = Convert.ToInt32(dtgBookingsView.SelectedRows[0].Cells["Tourist_ID"].Value);
            using (MySqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string sql = @"INSERT INTO tour_review  (Booking_ID, Tourist_ID, Rating, Comment) 
                             VALUES (@Booking_ID, @Tourist_ID, @Rating, @Comment)";
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Booking_ID", Booking_ID);
                    cmd.Parameters.AddWithValue("@Tourist_ID", Tourist_ID);
                    cmd.Parameters.AddWithValue("@Rating", Rating);
                    cmd.Parameters.AddWithValue("@Comment", comment);
                    cmd.ExecuteNonQuery();

                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Review submitted successfully.");
                            lstComment.ClearSelected();
                            cbxRating.SelectedIndex = -1;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error (submit review): " + ex.Message);
                        MessageBox.Show("Error submitting review: " + ex.Message);
                    }
                }
            }
        }
    }
}

