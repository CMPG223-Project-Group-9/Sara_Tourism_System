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
    public partial class EditBooking : Form
    {
        public EditBooking()
        {
            InitializeComponent();
        }

        private void EditBooking_Load(object sender, EventArgs e)
        {
            dtgBookingsView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgBookingsView.MultiSelect = false;
            dtgBookingsView.ReadOnly = true;

            using (MySqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                int Tourist_ID = Convert.ToInt32(Session["Tourist_ID"]);
                string sql = @"SELECT * FROM booking WHERE Tourist_ID = @Tourist_ID";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Tourist_ID", Tourist_ID);
                    try
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dtgBookingsView.DataSource = dataTable;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error (display bookings): " + ex.Message);
                    }
                }
            }   
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dtgBookingsView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a booking to update.");
                return;
            }

            int Number_of_People = int.Parse(txtNumber.Text);
            if (!int.TryParse(txtNumber.Text, out Number_of_People))
            {
                MessageBox.Show("Please enter a valid number of people.");
                return;
            }

            int Booking_ID = Convert.ToInt32(dtgBookingsView.SelectedRows[0].Cells["Booking_ID"].Value);
            int Tourist_ID = Convert.ToInt32(Session["Tourist_ID"]);
            object Activity_ID = cbxActivity.SelectedValue;
            DateTime Booking_Date = dateEditBooking.Value;
           

            using (MySqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                string sql = @"UPDATE Booking SET Booking_Date = @Booking_Date, Number_of_People = @Number_of_People, Activity_ID = 
                               @Activity_ID WHERE Booking_ID = @Booking_ID AND Tourist_ID = @Tourist_ID";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Booking_Date", Booking_Date);
                    command.Parameters.AddWithValue("@Number_of_People", Number_of_People);
                    command.Parameters.AddWithValue("@Activity_ID", Activity_ID);
                    command.Parameters.AddWithValue("@Booking_ID", Booking_ID);
                    command.Parameters.AddWithValue("@Tourist_ID", Tourist_ID);
                    command.ExecuteNonQuery();

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Booking updated successfully.");
                        else
                            MessageBox.Show("No booking was updated. It may not belong to you.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error (update booking): " + ex.Message);
                        MessageBox.Show("Error updating booking: " + ex.Message);
                    }
                }

                string qry = @"SELECT * FROM booking WHERE Tourist_ID = @Tourist_ID";
                using (MySqlCommand command = new MySqlCommand(qry, connection))
                {
                    command.Parameters.AddWithValue("@Tourist_ID", Tourist_ID);
                    try
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dtgBookingsView.DataSource = dataTable;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error (display bookings): " + ex.Message);
                    }

                }
            }
        }

        private void dtgBookingsView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dtgBookingsView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgBookingsView.MultiSelect = false;
            dtgBookingsView.ReadOnly = true;
        }
    }
}

