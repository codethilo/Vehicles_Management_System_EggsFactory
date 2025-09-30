using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Vehicle_Management
{
    public partial class Spares : Form
    {
        private readonly string connectionString = "Data Source=DESKTOP-FD36FME\\SQLEXPRESS;Initial Catalog=VehicleDb2;Integrated Security=true;";
        private int Spares_ID = 0;

        public Spares()
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
            ComboBox2.SelectedIndex = -1;
            TextBox2.Clear();
            TextBox3.Clear();
            TextBox4.Clear();
            Spares_ID = 0;
        }

        private bool ValidateInput()
        {
            // Check each attribute individually
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(TextBox2.Text) ||
                string.IsNullOrWhiteSpace(TextBox3.Text) ||
                string.IsNullOrWhiteSpace(TextBox4.Text) ||
                ComboBox2.SelectedIndex == -1)
            {
                return false;
            }

            // All attributes are filled
            return true;
        }

        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Vehicle_ID", textBox1.Text);
            cmd.Parameters.AddWithValue("@Type", ComboBox2.Text);
            cmd.Parameters.AddWithValue("@SparesName", TextBox2.Text);
            cmd.Parameters.AddWithValue("@SparesNo", TextBox3.Text);
            cmd.Parameters.AddWithValue("@DateOfPurchase", DateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@Amount", TextBox4.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Spares_ID != 0)
            {
                if (ValidateInput())
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        try
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("UPDATE Spares_Table SET Vehicle_ID = @Vehicle_ID, Spares_Type = @Type, Spares_Name = @SparesName, Spares_No = @SparesNo, Date_Of_Purchase = @DateOfPurchase, Amount = @Amount WHERE Spares_ID = @Spares_ID", con);
                            AddParameters(cmd);
                            cmd.Parameters.AddWithValue("@Spares_ID", Spares_ID);
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
            if (Spares_ID != 0)
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
                            SqlCommand cmd = new SqlCommand("DELETE FROM Spares_Table WHERE Spares_ID = @Spares_ID", con);
                            cmd.Parameters.AddWithValue("@Spares_ID", Spares_ID);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Record Deleted Successfully.");

                            // Store the reason for deletion
                            cmd = new SqlCommand("UPDATE Spares_Table SET Reason = @Reason WHERE Spares_ID = @Spares_ID", con);
                            cmd.Parameters.AddWithValue("@Reason", reason);
                            cmd.Parameters.AddWithValue("@Spares_ID", Spares_ID);
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO Spares_Table(Vehicle_ID, Spares_Type, Spares_Name, Spares_No, Date_Of_Purchase, Amount) VALUES(@Vehicle_ID, @Type, @SparesName, @SparesNo, @DateOfPurchase, @Amount)", con);
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

        private void DisplayData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlDataAdapter adapt = new SqlDataAdapter("SELECT * FROM Spares_Table", con);
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                Spares_ID = Convert.ToInt32(row.Cells[0].Value);
                textBox1.Text = row.Cells[1].Value.ToString();
                ComboBox2.Text = row.Cells[2].Value.ToString();
                TextBox2.Text = row.Cells[3].Value.ToString();
                TextBox3.Text = row.Cells[4].Value.ToString();
                DateTimePicker1.Value = Convert.ToDateTime(row.Cells[5].Value);
                TextBox4.Text = row.Cells[6].Value.ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
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