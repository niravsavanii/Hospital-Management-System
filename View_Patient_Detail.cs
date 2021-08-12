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
    public partial class View_Patient_Detail : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\Nirav\Nirav\GUI\HospitalManagement\HospitalManagement\Hospital.mdf;Integrated Security=True;User Instance=True");

        public View_Patient_Detail()
        {
            InitializeComponent();
        }

        private void View_Patient_Detail_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select p.Patient_ID,p.Patient_Name,p.Contact_Number,p.Age,p.Village,p.District,p.State,p.Current_Problem,d.Doctor_Name from tbl_Doctor d inner join tbl_Patient p on p.Doctor_ID=d.Doctor_ID",con);
  
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            dataGridView1.DataSource = dt;
            
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin a = new Admin();
            a.Show();
        }
    }
}
