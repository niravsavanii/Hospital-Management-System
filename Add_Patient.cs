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
    public partial class Add_Patient : Form
    {
        string Gender;
        int DoctorID;
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\Nirav\Nirav\GUI\HospitalManagement\HospitalManagement\Hospital.mdf;Integrated Security=True;User Instance=True");
        
        public Add_Patient()
        {
            InitializeComponent();
        }

        private bool IsValid()
        {
            if (txtAge.Text == string.Empty || txtContactNumber.Text == string.Empty || txtCurrentProblem.Text == string.Empty || txtDistrict.Text == string.Empty || cmbDoctorName.Text == string.Empty || txtPatientName.Text == string.Empty || txtState.Text == string.Empty || txtVillage.Text == string.Empty)
            {
                MessageBox.Show("Please enter give details", "Patient", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void reset()
        {
            txtAge.Clear();
            txtContactNumber.Clear();
            txtCurrentProblem.Clear();
            txtDistrict.Clear();
            txtPatientName.Clear();
            txtState.Clear();
            txtVillage.Clear();
            cmbDoctorName.Text = null;
            txtPatientName.Focus();
        }

        private void btnNewPateint_Click(object sender, EventArgs e)
        {
            if(IsValid())
            {
                SqlCommand cmd1 = new SqlCommand("select Doctor_ID from tbl_Doctor where Doctor_Name='"+cmbDoctorName.Text+"'",con);

                con.Open();
                DoctorID = Convert.ToInt32(cmd1.ExecuteScalar());
                con.Close();

                if (rbMale.Checked)
                {
                    Gender = "Male";
                }
                else if (rbFemale.Checked)
                {
                    Gender = "Female";
                }
                else
                {
                    MessageBox.Show("Please select gender", "Patient", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                SqlCommand cmd = new SqlCommand("insert into tbl_Patient values ('" + txtPatientName.Text + "','" + txtContactNumber.Text + "','" + txtAge.Text + "','" + Gender + "','" + txtVillage.Text + "','" + txtDistrict.Text + "','" + txtState.Text + "','" + txtCurrentProblem.Text + "','" + DoctorID + "')", con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Add patient successfull","Patient",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            reset();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin a = new Admin();
            a.Show();
        }

        private void Add_Patient_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select Doctor_Name from tbl_Doctor", con);

            con.Open();
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                cmbDoctorName.Items.Add(dr["Doctor_Name"].ToString());
            }
            con.Close();
        }
    }
}
