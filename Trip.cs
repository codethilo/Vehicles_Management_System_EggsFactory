using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Vehicle_Management
{
    public partial class Trip : Form
    {
        private readonly string connectionString = "Data Source=DESKTOP-FD36FME\\SQLEXPRESS;Initial Catalog=VehicleDb2;Integrated Security=true;";
        private int Trip_No = 0;
        private string selectedOption = "";

        public Trip()
        {
            InitializeComponent();
            DisplayData();
            panel3.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearData();
            panel3.Visible = true;
            
            button3.Enabled = false;
        }

        private void ClearData()
        {
            textBox1.Text = "";
            ComboBox2.Text = "";
            TextBox3.Text = "";
            TextBox2.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            Trip_No = 0;
            RadioButton1.Checked = false;
            RadioButton2.Checked = false;
        }

        private bool ValidateInput()
        {
            // Check each attribute individually
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
               
                string.IsNullOrWhiteSpace(TextBox2.Text) ||
                string.IsNullOrWhiteSpace(TextBox3.Text) ||
                string.IsNullOrWhiteSpace(TextBox4.Text) ||
                string.IsNullOrWhiteSpace(TextBox5.Text) ||
                string.IsNullOrWhiteSpace(TextBox6.Text) ||
                ComboBox2.SelectedIndex == -1)
            {
                return false;
            }

            // All attributes are filled
            return true;
        }

        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Vehicle_ID ", textBox1.Text);
            cmd.Parameters.AddWithValue("@DriverName", ComboBox2.Text);
            cmd.Parameters.AddWithValue("@TimeIn", DateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@TimeOut", DateTimePicker3.Value);
            cmd.Parameters.AddWithValue("@Supervisor", TextBox2.Text);
            cmd.Parameters.AddWithValue("@KMFrom", TextBox3.Text);
            cmd.Parameters.AddWithValue("@KMTo", TextBox4.Text);
            cmd.Parameters.AddWithValue("@KMDistance", TextBox5.Text);
            cmd.Parameters.AddWithValue("@TripPlace", TextBox6.Text);
            cmd.Parameters.AddWithValue("@Complaint", selectedOption);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Trip_No != 0)
            {
                if (ValidateInput())
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        try
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("UPDATE Trip_Table SET Vehicle_ID = @Vehicle_ID , Driver_Name = @DriverName, TimeIn = @TimeIn, TimeOut = @TimeOut, Supervisor = @Supervisor, KM_From = @KMFrom, Km_To = @KMTo, KM_Distance = @KMDistance, Trip_Place = @TripPlace, Complaint = @Complaint WHERE Trip_No = @Trip_No", con);
                            AddParameters(cmd);
                            cmd.Parameters.AddWithValue("@Trip_No", Trip_No);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Record Updated Successfully");
                            ClearData();
                            DisplayData();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please Provide Details!");
                }
            }
            else
            {
                MessageBox.Show("Please Select Record to Update");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Trip_No != 0)
            {
                string reason = PromptForReason();
                if (!string.IsNullOrEmpty(reason))
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        try
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("DELETE FROM Trip_Table WHERE Trip_No = @Trip_No", con);
                            cmd.Parameters.AddWithValue("@Trip_No", Trip_No);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Record Deleted Successfully.");

                            cmd = new SqlCommand("UPDATE Trip_Table1 SET Reason = @Reason WHERE Trip_No = @Trip_No", con);
                            cmd.Parameters.AddWithValue("@Reason", reason);
                            cmd.Parameters.AddWithValue("@Trip_No", Trip_No);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Reason stored successfully.");

                            ClearData();
                            DisplayData();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error deleting record: " + ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please provide a reason for deletion.");
                }
            }
            else
            {
                MessageBox.Show("Please select a record to delete.");
            }
        }

        private string PromptForReason()
        {
            using (var form = new Form())
            {
                form.Text = "Enter Reason for Deletion";
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false; // Prevent maximizing
                form.MinimizeBox = false; // Prevent minimizing

                var label = new Label() { Left = 50, Top = 20, Text = "Reason:" };
                var textBox = new TextBox() { Left = 50, Top = 50, Width = 200 };
                var okButton = new Button() { Text = "OK", Left = 50, Width = 75, Top = 80, DialogResult = DialogResult.OK };
                var closeButton = new Button() { Text = "Close", Left = 150, Width = 75, Top = 80, DialogResult = DialogResult.Cancel };

                okButton.Click += (sender, e) => { form.Close(); };
                closeButton.Click += (sender, e) => { form.Close(); };

                form.Controls.Add(textBox);
                form.Controls.Add(okButton);
                form.Controls.Add(closeButton);
                form.Controls.Add(label);

                form.AcceptButton = okButton;
                form.CancelButton = closeButton;

                return form.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            }
        }





        private void DisplayData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlDataAdapter adapt = new SqlDataAdapter("SELECT * FROM Trip_Table", con);
                    DataTable dt = new DataTable();
                    adapt.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            selectedOption = "Yes";
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            selectedOption = "No";
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            // Confirm whether to hide the panel
            DialogResult result = MessageBox.Show("Are you sure you want to hide the panel?", "Confirm", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                // Hide the panel
                panel3.Visible = false;
                
                button3.Enabled = true;
            }
        }

        
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                Trip_No = Convert.ToInt32(row.Cells[0].Value);
                textBox1.Text = row.Cells[1].Value.ToString();
                ComboBox2.Text = row.Cells[2].Value.ToString();
                DateTimePicker1.Value = Convert.ToDateTime(row.Cells[3].Value);
                DateTimePicker3.Value = Convert.ToDateTime(row.Cells[4].Value);
                TextBox2.Text = row.Cells[5].Value.ToString();
                TextBox3.Text = row.Cells[6].Value.ToString();
                TextBox4.Text = row.Cells[7].Value.ToString();
                TextBox5.Text = row.Cells[8].Value.ToString();
                TextBox6.Text = row.Cells[9].Value.ToString();
                selectedOption = row.Cells[10].Value.ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO Trip_Table(Vehicle_ID , Driver_Name, TimeIn, TimeOut, Supervisor, KM_From, Km_To, KM_Distance, Trip_Place, Complaint) VALUES(@Vehicle_ID,@DriverName, @TimeIn, @TimeOut, @Supervisor, @KMFrom, @KMTo, @KMDistance, @TripPlace, @Complaint)", con);
                        AddParameters(cmd);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record Inserted Successfully");
                        ClearData();
                        DisplayData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }

        }
    }
}
