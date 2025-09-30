using System;
using System.Windows.Forms;

namespace Vehicle_Management
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }

        private void Home_Load(object sender, EventArgs e)
        {
            // Any additional initialization code you may want to add
        }

        private void vehicleDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create a new instance of the VehicleData form
            Vehicle_Data vehicleForm = new Vehicle_Data();

            // Set the MdiParent property of the VehicleData form to the main form
            vehicleForm.MdiParent = this;

            // Show the VehicleData form
            vehicleForm.Show();
        }

        private void sparesDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
        
            Spares s = new Spares();
            s.MdiParent = this;
           s.Show();

        }

        private void tyreDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tyre t = new Tyre();
            t.MdiParent = this;
            t.Show();
        }

        private void serviceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Service_Entry s1 = new Service_Entry();
            s1.MdiParent = this;
            s1.Show();
        }

        private void tripToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Trip t = new Trip();
            t.MdiParent = this;
            t.Show();
        }

        private void vehicleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Vehicle_Report VR = new Vehicle_Report();
            VR.MdiParent = this;
            VR.Show();
        }

        private void reasonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reasons R = new Reasons();
            R.MdiParent = this;
            R.Show();
        }

        private void sparesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Spares_Report SR = new Spares_Report();
            SR.MdiParent = this;
            SR.Show();
        }

        private void tyreToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Trip_Report TR = new Trip_Report();
            TR.MdiParent = this;
            TR.Show();
        }

        private void serviceToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Service_Report SR1 = new Service_Report();
            SR1.MdiParent = this;
            SR1.Show();
        }

        private void tripToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Trip_Report T = new Trip_Report();
            T.MdiParent = this;
            T.Show();
        }

        
    }
}
