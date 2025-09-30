using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace Vehicle_Management
{
    public partial class Spares_Report : Form
    {
        string connectionString = "Data Source=DESKTOP-FD36FME\\SQLEXPRESS;Initial Catalog=VehicleDb2;Integrated Security=true;";

        public Spares_Report()
        {
            InitializeComponent();
        }

        

        private void Button1_Click_1(object sender, EventArgs e)
        {
            string searchTerm = TextBox1.Text.Trim();

            // Create the SQL query    
            string query = "SELECT * FROM Spares_Table WHERE Spares_Name LIKE @SearchTerm OR Spares_Type  ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Add parameters to the command
                command.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                // Open the connection
                connection.Open();

                // Execute the query and fill a DataTable with the results
                DataTable dt = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dt);
                }

                // Bind the DataTable to the DataGridView
                dataGridView1.DataSource = dt;
            }

        }
    }
}
