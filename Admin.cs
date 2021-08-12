using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HospitalManagement
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void btnAddPatient_Click(object sender, EventArgs e)
        {
            this.Hide();
            Add_Patient ap = new Add_Patient();
            ap.Show();
        }

        private void btnAddDoctor_Click(object sender, EventArgs e)
        {
            this.Hide();
            Add_Doctor ad = new Add_Doctor();
            ad.Show();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }

        private void btnViewPatient_Click(object sender, EventArgs e)
        {
            this.Hide();
            View_Patient_Detail vp = new View_Patient_Detail();
            vp.Show();
        }

        private void btnViewDoctor_Click(object sender, EventArgs e)
        {
            this.Hide();
            View_Doctor_Detail vd = new View_Doctor_Detail();
            vd.Show();
        }
    }
}
