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
    public partial class Form1 : Form
    {
        int AdminID;
        int DoctorID;      
            
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\Nirav\MVC\HospitalManagement\HospitalManagement\Hospital.mdf;Integrated Security=True;User Instance=True");

        public Form1()
        {
            InitializeComponent();
        }

        private bool IsValid()
        {
            if (txtID.Text == string.Empty || txtPassword.Text == string.Empty)
            {
                MessageBox.Show("Enter Id and Password","Login",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnAdminLogin_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                SqlCommand cmd = new SqlCommand("SP_AdminLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", txtID.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                con.Open();
                AdminID = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();

                if (AdminID > 0)
                {
                    MessageBox.Show("Login admin successfully", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide();
                    Admin a = new Admin();
                    a.Show();
                }
                else
                {
                    MessageBox.Show("Login error", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDoctorLogin_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                SqlCommand cmd = new SqlCommand("SP_DoctorLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", txtID.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                con.Open();
                DoctorID = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();

                if (DoctorID > 0)
                {
                    MessageBox.Show("Login Doctor successfully", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide();
                    Doctor d = new Doctor();
                    d.Show();
                }
                else
                {
                    MessageBox.Show("Login error", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
