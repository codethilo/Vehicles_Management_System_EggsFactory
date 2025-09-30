using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace Vehicle_Management
{
   
    public partial class Trip_Report : Form
    {
        string connectionString = "Data Source=DESKTOP-FD36FME\\SQLEXPRESS;Initial Catalog=VehicleDb2;Integrated Security=true;";

        public Trip_Report()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string searchTerm = TextBox1.Text.Trim();

            // Create the SQL query  
            string query = "SELECT * FROM  Trip_Table WHERE Driver_NameLIKE @SearchTerm OR  Trip_Place";

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
    
