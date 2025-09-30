using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Vehicle_Management
{
    public partial class Reasons : Form
    {
        
        public Reasons()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            
                try
                {
                    using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-FD36FME\\SQLEXPRESS;Initial Catalog=VehicleDb2;Integrated Security=True"))
                    {
                        con.Open();
                        string sql = "SELECT * FROM Vehicle_Table1";
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, con);
                        DataSet ds = new DataSet();

                        dataAdapter.Fill(ds, "Vehicle_Table1");
                        dataGridView1.DataSource = ds;
                        dataGridView1.DataMember = "Vehicle_Table1";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

            }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-FD36FME\\SQLEXPRESS;Initial Catalog=VehicleDb2;Integrated Security=True"))
                {
                    con.Open();
                    string sql = "SELECT * FROM Spares_Table1";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, con);
                    DataSet ds = new DataSet();

                    dataAdapter.Fill(ds, "Spares_Table1");
                    dataGridView1.DataSource = ds;
                    dataGridView1.DataMember = "Spares_Table1";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-FD36FME\\SQLEXPRESS;Initial Catalog=VehicleDb2;Integrated Security=True"))
                {
                    con.Open();
                    string sql = "SELECT * FROM Tyre_Table1";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, con);
                    DataSet ds = new DataSet();

                    dataAdapter.Fill(ds, "Tyre_Table1");
                    dataGridView1.DataSource = ds;
                    dataGridView1.DataMember = "Tyre_Table1";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-FD36FME\\SQLEXPRESS;Initial Catalog=VehicleDb2;Integrated Security=True"))
                {
                    con.Open();
                    string sql = "SELECT * FROM Service_Table1";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, con);
                    DataSet ds = new DataSet();

                    dataAdapter.Fill(ds, "Service_Table1");
                    dataGridView1.DataSource = ds;
                    dataGridView1.DataMember = "Service_Table1";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-FD36FME\\SQLEXPRESS;Initial Catalog=VehicleDb2;Integrated Security=True"))
                {
                    con.Open();
                    string sql = "SELECT * FROM Trip_Table1";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, con);
                    DataSet ds = new DataSet();

                    dataAdapter.Fill(ds, "Trip_Table1");
                    dataGridView1.DataSource = ds;
                    dataGridView1.DataMember = "Trip_Table1";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
    }
    }

