using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Vehicle_Management
{
    public partial class Service_Entry : Form
    {
        private readonly string connectionString = "Data Source=DESKTOP-FD36FME\\SQLEXPRESS;Initial Catalog=VehicleDb2;Integrated Security=true;";
        private int Service_No = 0;
        
        private string selectedOption = "";

        public Service_Entry()
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
            textBox1.Clear(); // Assuming TextBox1 contains the Vehicle_ID
            ComboBox2.SelectedIndex = -1;
            ComboBox3.SelectedIndex = -1;
            Service_No = 0;
            RadioButton1.Checked = false;
            RadioButton2.Checked = false;
        }

        private bool ValidateInput()
        {
            // Check each attribute individually
            if (string.IsNullOrWhiteSpace(textBox1.Text) || // Assuming TextBox1 contains the Vehicle_ID
                string.IsNullOrWhiteSpace(ComboBox2.Text) ||
                ComboBox3.SelectedIndex == -1)
            {
                return false;
            }

            // All attributes are filled
            return true;
        }

        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Vehicle_ID", textBox1.Text); // Assuming TextBox1 contains the Vehicle_ID
            cmd.Parameters.AddWithValue("@serviceType", ComboBox2.Text);
            cmd.Parameters.AddWithValue("@mechanicName", ComboBox3.Text);
            cmd.Parameters.AddWithValue("@date1", DateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@status1", selectedOption);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Service_No != 0)
            {
                if (ValidateInput())
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        try
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("UPDATE Service_Table SET  Vehicle_ID = @Vehicle_ID, Spares_Type = @serviceType, Mechanic_Name = @mechanicName, Date_Of_Service = @date1, status1 = @status1 WHERE Service_No = @Service_No", con);
                            AddParameters(cmd);
                            cmd.Parameters.AddWithValue("@Service_No", Service_No);
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
            if (Service_No != 0)
            {
                string reason = PromptForReason();
                if (!string.IsNullOrEmpty(reason))
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        try
                        {
                            con.Open();

                            // Delete the record from the database
                            SqlCommand cmd = new SqlCommand("DELETE FROM Service_Table WHERE Service_No = @Service_No", con);
                            cmd.Parameters.AddWithValue("@Service_No", Service_No);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Record Deleted Successfully.");

                            // Store the reason for deletion
                            cmd = new SqlCommand("UPDATE Service_Table1 SET Reason = @Reason WHERE Service_No = @Service_No", con);
                            cmd.Parameters.AddWithValue("@Reason", reason);
                            cmd.Parameters.AddWithValue("@Service_No", Service_No);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Reason stored successfully.");

                            // Clear data and refresh display
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
                    SqlDataAdapter adapt = new SqlDataAdapter("SELECT * FROM Service_Table", con);
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
            RadioButton rb = sender as RadioButton;
            if (rb.Checked)
            {
                selectedOption = rb.Text;
            }
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.Checked)
            {
                selectedOption = rb.Text;
            }
        }

        private void Button6_Click_1(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO Service_Table(Vehicle_ID, Spares_Type, Date_Of_Service, Mechanic_Name, status1) VALUES(@Vehicle_ID, @serviceType, @date1, @mechanicName, @status1)", con);
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

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                Service_No = Convert.ToInt32(row.Cells["Service_No"].Value);
                textBox1.Text = row.Cells["Vehicle_ID"].Value.ToString(); // Assuming TextBox1 contains the Vehicle_ID
                ComboBox2.Text = row.Cells["Spares_Type"].Value.ToString();
                DateTimePicker1.Value = Convert.ToDateTime(row.Cells["Date_Of_Service"].Value);
                ComboBox3.Text = row.Cells["Mechanic_Name"].Value.ToString();

                selectedOption = row.Cells["status1"].Value.ToString();
            }

        }

        private void Button5_Click_1(object sender, EventArgs e)
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

        
    }
}
