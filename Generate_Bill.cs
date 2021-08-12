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
    public partial class Generate_Bill : Form
    {
        int Flag;
        int PatientID;

        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\Nirav\Nirav\GUI\HospitalManagement\HospitalManagement\Hospital.mdf;Integrated Security=True;User Instance=True");
        public Generate_Bill()
        {
            InitializeComponent();
        }

        private void Generate_Bill_Load(object sender, EventArgs e)
        {
            SqlCommand cmd1 = new SqlCommand("Select PWTD_ID from tbl_PatientTreatment", con);

            con.Open();
            cmd1.ExecuteNonQuery();

            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

            da1.Fill(dt1);

            foreach (DataRow dr in dt1.Rows)
            {
                cmbSelectIPatientID.Items.Add(dr["PWTD_ID"].ToString());
            }
            con.Close();
        }

        private void cmbSelectIPatientD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSelectIPatientID.Text != null)
            {
                SqlCommand cmd = new SqlCommand("select pt.Patient_ID,p.Patient_Name,pt.Identified_deseases,pt.Drug,pt.Other_Specification,pt.Amount from tbl_PatientTreatment pt inner join tbl_Patient p on pt.Patient_ID=p.Patient_ID where pt.PWTD_ID='"+cmbSelectIPatientID.Text+"'",con);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PatientID = Convert.ToInt32(reader[0].ToString());
                        lblPatientID.Text = reader[0].ToString();
                        lblPatientName.Text = reader[1].ToString();
                        lblIdentifiedDeseases.Text = reader[2].ToString();
                        lblDrugDescription.Text = reader[3].ToString();
                        lblOtherSpecification.Text = reader[4].ToString();
                        lblAmount.Text = reader[5].ToString();
                    }
                }

                SqlCommand cmd1 = new SqlCommand("select d.Doctor_Name from tbl_PatientTreatment pt inner join tbl_Doctor d on pt.Doctor_ID=d.Doctor_ID where pt.Patient_ID='" + PatientID + "'", con);

                using (SqlDataReader reader = cmd1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lblDoctorName.Text = reader[0].ToString();
                    }
                }
                Flag = 1;
                con.Close();
            }
        }

        private void reset()
        {
            cmbSelectIPatientID.Text = null;
            lblPatientID.Text = "...";
            lblPatientName.Text = "...";
            lblIdentifiedDeseases.Text = "...";
            lblDoctorName.Text = "...";
            lblDrugDescription.Text = "...";
            lblOtherSpecification.Text = "...";
            lblAmount.Text = "...";
            cmbSelectIPatientID.Focus();

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Doctor d = new Doctor();
            d.Show();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Flag == 1)
            {
                MessageBox.Show("Bill generated successfully", "Generate Bill", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select PatientID", "Generate Bill", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            reset();
        }
    }
}
