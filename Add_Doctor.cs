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
    public partial class Add_Doctor : Form
    {
        string Gender;
        string Specialization;
        int UserID;

        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\Nirav\Nirav\GUI\HospitalManagement\HospitalManagement\Hospital.mdf;Integrated Security=True;User Instance=True");
        public Add_Doctor()
        {
            InitializeComponent();
        }

        private bool IsValid()
        {
            if(txtContactNumber.Text==string.Empty||txtDoctorName.Text==string.Empty||txtEmail.Text==string.Empty||txtPassword.Text==string.Empty)
            {
                MessageBox.Show("Please enter give details", "Doctor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;   
             }
            return true;
        }

        private void reset()
        {
            txtContactNumber.Clear();
            txtDoctorName.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            txtDoctorName.Focus();
        }

        private void btnDoctor_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
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

                Specialization = cmbSpecialization.SelectedItem.ToString();

                SqlCommand cmd = new SqlCommand("insert into tbl_Doctor values ('" + txtPassword.Text + "','" + txtDoctorName.Text + "','" + Gender + "','" + Specialization + "','" + txtContactNumber.Text + "','" + txtEmail.Text + "')", con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Doctor add successfully","Doctor",MessageBoxButtons.OK,MessageBoxIcon.Information);

                UserID = UserID + 1;
                lblUserID.Text = Convert.ToString(UserID);
            }
            reset();
        }

        private void getUserID()
        {
            SqlCommand cmd = new SqlCommand("select Doctor_ID from tbl_Doctor order by Doctor_ID desc", con);

            con.Open();
            UserID = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
            con.Close();

            lblUserID.Text = Convert.ToString(UserID);
        }

        private void Add_Doctor_Load(object sender, EventArgs e)
        {
            getUserID();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin a = new Admin();
            a.Show();
        }
    }
}
