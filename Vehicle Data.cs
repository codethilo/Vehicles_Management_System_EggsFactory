using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Vehicle_Management
{
    public partial class Vehicle_Data : Form
    {
        private readonly string connectionString = "Data Source=DESKTOP-FD36FME\\SQLEXPRESS;Initial Catalog=VehicleDb2;Integrated Security=true;";
        private int ID = 0;

        public Vehicle_Data()
        {
            InitializeComponent();
            DisplayData();
            panel3.Visible = false;
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
                        SqlCommand cmd = new SqlCommand("INSERT INTO Vehicle(Register_No, Chassis_No, Engine_No, OwnerName, Address, Date_Of_Registration, Register_Valid, Owner_Sr_no, Year_of_Mfg, WheelBase, CubicCapacity, No_of_Cylinders, Laden, MakerName, ModelName, Colors, Body_Type, Seating, TaxiPaid, Vehicle_Class, RcNo, Fuel_Use) VALUES(@RegNo, @ChassisNo, @EngineNo, @OwnerName, @Address, @Date1, @Regvalid, @OwnerSrno, @YearofMgf, @WheelBase, @CubicCapacity, @NoofCylinders, @Laden, @MakerName, @ModelName, @Colors, @BodyType, @Seating, @TaxiPaid, @VehicleClass, @RcNo, @Fueluse)", con);
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                if (ValidateInput())
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        try
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("UPDATE Vehicle SET Register_No = @RegNo, Chassis_No = @ChassisNo, Engine_No = @EngineNo, OwnerName = @OwnerName, Address = @Address, Date_Of_Registration = @Date1, Register_Valid = @Regvalid, Owner_Sr_no = @OwnerSrno, Year_of_Mfg = @YearofMgf, WheelBase = @WheelBase, CubicCapacity = @CubicCapacity, No_of_Cylinders = @NoofCylinders, Laden = @Laden, MakerName = @MakerName, ModelName = @ModelName, Colors = @Colors, Body_Type = @BodyType, Seating = @Seating, TaxiPaid = @TaxiPaid, Vehicle_Class = @VehicleClass, RcNo = @RcNo, Fuel_Use = @Fueluse WHERE ID = @id", con);
                            AddParameters(cmd);
                            cmd.Parameters.AddWithValue("@id", ID);
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

        private bool ValidateInput()
        {
            // Check each attribute individually
            if (string.IsNullOrWhiteSpace(ComboBox1.Text) ||
                string.IsNullOrWhiteSpace(TextBox1.Text) ||
                string.IsNullOrWhiteSpace(TextBox2.Text) ||
                string.IsNullOrWhiteSpace(TextBox3.Text) ||
                string.IsNullOrWhiteSpace(TextBox4.Text) ||
                string.IsNullOrWhiteSpace(TextBox5.Text) ||
                string.IsNullOrWhiteSpace(TextBox6.Text) ||
                string.IsNullOrWhiteSpace(TextBox7.Text) ||
                string.IsNullOrWhiteSpace(TextBox8.Text) ||
                string.IsNullOrWhiteSpace(TextBox9.Text) ||
                string.IsNullOrWhiteSpace(TextBox10.Text) ||
                string.IsNullOrWhiteSpace(TextBox11.Text) ||
                string.IsNullOrWhiteSpace(TextBox12.Text) ||
                string.IsNullOrWhiteSpace(TextBox13.Text) ||
                string.IsNullOrWhiteSpace(TextBox14.Text) ||
                ComboBox2.SelectedIndex == -1 ||
                ComboBox3.SelectedIndex == -1 ||
                ComboBox4.SelectedIndex == -1 ||
                ComboBox5.SelectedIndex == -1)
            {
                return false;
            }

            // All attributes are filled
            return true;
        }

        private void DisplayData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlDataAdapter adapt = new SqlDataAdapter("SELECT * FROM Vehicle", con);
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

        private void button1_Click(object sender, EventArgs e)
        {
            ClearData();
            panel3.Visible = true;
            
            button3.Enabled = false;
        }

        private void ClearData()
        {
            foreach (Control control in Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Clear();
                }
                else if (control is ComboBox comboBox)
                {
                    comboBox.SelectedIndex = -1;
                }
            }
            ID = 0;
        }


        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RegNo", ComboBox1.Text);
            cmd.Parameters.AddWithValue("@ChassisNo", TextBox1.Text);
            cmd.Parameters.AddWithValue("@EngineNo", TextBox2.Text);
            cmd.Parameters.AddWithValue("@OwnerName", TextBox3.Text);
            cmd.Parameters.AddWithValue("@Address", TextBox4.Text);
            cmd.Parameters.AddWithValue("@Date1", DateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@Regvalid", DateTimePicker2.Value);
            cmd.Parameters.AddWithValue("@OwnerSrno", TextBox5.Text);
            cmd.Parameters.AddWithValue("@YearofMgf", DateTimePicker3.Value);
            cmd.Parameters.AddWithValue("@WheelBase", TextBox6.Text);
            cmd.Parameters.AddWithValue("@CubicCapacity", TextBox7.Text);
            cmd.Parameters.AddWithValue("@NoofCylinders", TextBox8.Text);
            cmd.Parameters.AddWithValue("@Laden", TextBox9.Text);
            cmd.Parameters.AddWithValue("@MakerName", ComboBox2.Text);
            cmd.Parameters.AddWithValue("@ModelName", ComboBox3.Text);
            cmd.Parameters.AddWithValue("@Colors", ComboBox4.Text);
            cmd.Parameters.AddWithValue("@BodyType", TextBox10.Text);
            cmd.Parameters.AddWithValue("@Seating", TextBox11.Text);
            cmd.Parameters.AddWithValue("@TaxiPaid", TextBox12.Text);
            cmd.Parameters.AddWithValue("@VehicleClass", TextBox13.Text);
            cmd.Parameters.AddWithValue("@RcNo", TextBox14.Text);
            cmd.Parameters.AddWithValue("@Fueluse", ComboBox5.Text);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (ID != 0)
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
                            SqlCommand cmd = new SqlCommand("DELETE FROM Vehicle WHERE ID = @id", con);
                            cmd.Parameters.AddWithValue("@id", ID);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Record Deleted Successfully.");

                            // Store the reason for deletion
                            cmd = new SqlCommand("INSERT INTO Vehicle_Deletion_Reasons (VehicleID, Reason) VALUES (@VehicleID, @Reason)", con);
                            cmd.Parameters.AddWithValue("@VehicleID", ID);
                            cmd.Parameters.AddWithValue("@Reason", reason);
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

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                ID = Convert.ToInt32(row.Cells[0].Value);
                ComboBox1.Text = row.Cells[1].Value.ToString();
                TextBox1.Text = row.Cells[2].Value.ToString();
                TextBox2.Text = row.Cells[3].Value.ToString();
                TextBox3.Text = row.Cells[4].Value.ToString();
                TextBox4.Text = row.Cells[5].Value.ToString();
                DateTimePicker1.Value = Convert.ToDateTime(row.Cells[6].Value);
                DateTimePicker2.Value = Convert.ToDateTime(row.Cells[7].Value);
                TextBox5.Text = Convert.ToInt32(row.Cells[8].Value).ToString();
                DateTimePicker3.Value = Convert.ToDateTime(row.Cells[9].Value);
                TextBox6.Text = Convert.ToInt32(row.Cells[10].Value).ToString();
                TextBox7.Text = Convert.ToInt32(row.Cells[11].Value).ToString();
                TextBox8.Text = Convert.ToInt32(row.Cells[12].Value).ToString();
                TextBox9.Text = row.Cells[13].Value.ToString();
                ComboBox2.Text = row.Cells[14].Value.ToString();
                ComboBox3.Text = row.Cells[15].Value.ToString();
                ComboBox4.Text = row.Cells[16].Value.ToString();
                TextBox10.Text = row.Cells[17].Value.ToString();
                TextBox11.Text = row.Cells[18].Value.ToString();
                TextBox12.Text = row.Cells[19].Value.ToString();
                TextBox13.Text = row.Cells[20].Value.ToString();
                TextBox14.Text = row.Cells[21].Value.ToString();
                ComboBox5.Text = row.Cells[22].Value.ToString();
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
