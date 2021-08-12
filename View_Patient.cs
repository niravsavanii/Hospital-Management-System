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
    public partial class View_Patient : Form
    {
        int DoctorID;

        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\Nirav\Nirav\GUI\HospitalManagement\HospitalManagement\Hospital.mdf;Integrated Security=True;User Instance=True");
        public View_Patient()
        {
            InitializeComponent();
        }

        private void View_Patient_Load(object sender, EventArgs e)
        {

            SqlCommand cmd1 = new SqlCommand("Select Doctor_Name from tbl_Doctor",con);
            con.Open();
            cmd1.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);

            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                cmbDoctorName.Items.Add(dr["Doctor_Name"].ToString());
            }
            con.Close();

            
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            SqlCommand cmd1 = new SqlCommand("Select Doctor_ID from tbl_Doctor where Doctor_Name='"+cmbDoctorName.Text+"'",con);

            con.Open();
            DoctorID = Convert.ToInt32(cmd1.ExecuteScalar());
            con.Close();

            SqlCommand cmd = new SqlCommand("Select p.Patient_ID,p.Patient_Name,p.Contact_Number,p.Age,p.Village,p.District,p.State,p.Current_Problem,d.Doctor_Name from tbl_Doctor d inner join tbl_Patient p on p.Doctor_ID=d.Doctor_ID where d.Doctor_ID='" + DoctorID + "'", con);

            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);

            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Doctor d = new Doctor();
            d.Show();
        }

    }
}
