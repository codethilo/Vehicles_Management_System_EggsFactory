using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Vehicle_Management
{
    public partial class Registration : Form
    {
        SqlCommand cmd;
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-FD36FME\\SQLEXPRESS;Initial Catalog=VehicleDb2;Integrated Security=true;");
        SqlDataReader dr;

        public Registration()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text))
            {
                if (textBox2.Text == textBox3.Text)
                {
                    try
                    {
                        con.Open();
                        cmd = new SqlCommand("SELECT * FROM LoginTable1 WHERE username=@username", con);
                        cmd.Parameters.AddWithValue("@username", textBox1.Text);
                        dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            dr.Close();
                            MessageBox.Show("Username already exists. Please try another one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            dr.Close();
                            cmd = new SqlCommand("INSERT INTO LoginTable1 (username, password) VALUES (@username, @password)", con);
                            cmd.Parameters.AddWithValue("@username", textBox1.Text);
                            cmd.Parameters.AddWithValue("@password", textBox2.Text);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Your account is created. Please login now.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Please enter the same password in both fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter values in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.ShowDialog();
        }
    }
}
