using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HospitalManagement
{
    public partial class Update_Patient : Form
    {
        int Flag;
        int PatientID;
        int DoctorID;

        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\Nirav\Nirav\GUI\HospitalManagement\HospitalManagement\Hospital.mdf;Integrated Security=True;User Instance=True");
        public Update_Patient()
        {
            InitializeComponent();
        }

        private bool IsValid()
        {
            if (txtAmount.Text == string.Empty || txtDrugDescription.Text == string.Empty || txtIdentifiedDeseases.Text == string.Empty || txtOtherSpecification.Text == string.Empty)
            {
                MessageBox.Show("Please enter details", "Update Patient", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void reset()
        {
            cmbPatientName.Text = null;
            txtAmount.Clear();
            txtDrugDescription.Clear();
            lblDoctorName.Text = null;
            txtIdentifiedDeseases.Clear();
            txtOtherSpecification.Clear();
            cmbPatientName.Focus();
        }

        private void Update_Patient_Load(object sender, EventArgs e)
        {
            SqlCommand cmd1 = new SqlCommand("Select Patient_Name from tbl_Patient", con);

            con.Open();
            cmd1.ExecuteNonQuery();

            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

            da1.Fill(dt1);

            foreach (DataRow dr in dt1.Rows)
            {
                cmbPatientName.Items.Add(dr["Patient_Name"].ToString());
            }          
            con.Close();
        }

        private void cmbPatientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPatientName.Text!=null)
            {
                SqlCommand cmd = new SqlCommand("Select Doctor_ID from tbl_Patient where Patient_Name='" + cmbPatientName.Text + "'", con);

                con.Open();
                DoctorID = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();

                if (DoctorID > 0)
                {
                    SqlCommand cmd1 = new SqlCommand("select Doctor_Name from tbl_Doctor where Doctor_ID='" + DoctorID + "'", con);

                    con.Open();
                    lblDoctorName.Text = cmd1.ExecuteScalar().ToString();
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Please select Patient","Update Patient",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Doctor d = new Doctor();
            d.Show();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                SqlCommand cmd1 = new SqlCommand("Select Patient_ID from tbl_Patient where Patient_Name='" + cmbPatientName.Text + "'", con);

                con.Open();
                PatientID = Convert.ToInt32(cmd1.ExecuteScalar());

                SqlCommand cmd2 = new SqlCommand("Select Patient_ID from tbl_PatientTreatment where Patient_ID='" + PatientID + "'", con);

                Flag=Convert.ToInt32(cmd2.ExecuteScalar());
                con.Close();

                if (Flag == 0)
                {
                    SqlCommand cmd = new SqlCommand("insert into tbl_PatientTreatment values ('" + PatientID + "','" + txtIdentifiedDeseases.Text + "','" + DoctorID + "','" + txtDrugDescription.Text + "','" + txtOtherSpecification.Text + "','" + txtAmount.Text + "')", con);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Upadte patient sucessfully", "Update Patient", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Patient is already exist\nYou can generate Bill", "Update Patient", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
            reset();
        }
    }
}
