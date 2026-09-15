using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Sara_Tourism_System
{
    public partial class ViewBookings : Form
    {
        public ViewBookings()
        {
            InitializeComponent();
        }

        private void ViewBookings_Load(object sender, EventArgs e)
        {
            using (MySqlConnection connection = DatabaseConnection.GetConnection())
            {
                connection.Open();

                //Total Bookings
                int Tourist_ID = Convert.ToInt32(Session["Tourist_ID"]);
                int total = 0;

                string qry = @"SELECT COUNT(*) FROM booking WHERE Tourist_ID = @Tourist_ID";
                using (MySqlCommand cmd = new MySqlCommand(qry, connection))
                {
                    cmd.Parameters.AddWithValue("@Tourist_ID", Tourist_ID);

                    try
                    {
                        object result = cmd.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int count))
                        {
                            total = count;
                            lblTotalBooking.Text = total.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }

                //Upcoming Bookings
                string confirmed = @"SELECT COUNT(*) FROM booking WHERE Tourist_ID = @Tourist_ID AND @Payment_Status = 'Confirmed'";
                using (MySqlCommand cmd = new MySqlCommand(confirmed, connection))
                {
                    cmd.Parameters.AddWithValue("@Tourist_ID", Tourist_ID);
                    try
                    {
                        object result = cmd.ExecuteScalar();
                        int confirm = (result != null && int.TryParse(result.ToString(), out int count)) ? count : 0;
                        lblUpcoming.Text = confirm.ToString();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error (confirmed): " + ex.Message);
                    }
                }

                //completed bookings
                string completed = @"SELECT COUNT(*) FROM booking WHERE Tourist_ID = @Tourist_ID AND @Payment_Status = 'Completed'";
                using (MySqlCommand cmd = new MySqlCommand(completed, connection))
                {
                    cmd.Parameters.AddWithValue("@Tourist_ID", Tourist_ID);
                    try
                    {
                        object result = cmd.ExecuteScalar();
                        int complete = (result != null && int.TryParse(result.ToString(), out int count)) ? count : 0;
                        lblCompleted.Text = complete.ToString();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error (completed): " + ex.Message);
                    }
                }

                //DataGridView Display
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
                            dtgBooking.DataSource = dataTable;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error (display bookings): " + ex.Message);
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditBooking childform = new EditBooking();
            childform.MdiParent = this;
            childform.Show();
        }

        private void btnReview_Click(object sender, EventArgs e)
        {
            Reviews childform = new Reviews();
            childform.MdiParent = this;
            childform.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Form8 childform = new Form8();
            childform.MdiParent = this;
            childform.Show();
        }
    }
}

