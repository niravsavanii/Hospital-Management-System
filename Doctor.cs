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
    public partial class Doctor : Form
    {
        public Doctor()
        {
            InitializeComponent();
        }

        private void btnViewPatient_Click(object sender, EventArgs e)
        {
            this.Hide();
            View_Patient vp = new View_Patient();
            vp.Show();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.Show();
        }

        private void btnUpdatePatient_Click(object sender, EventArgs e)
        {
            this.Hide();
            Update_Patient up = new Update_Patient();
            up.Show();
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            this.Hide();
            Generate_Bill g = new Generate_Bill();
            g.Show();
        }
    }
}
