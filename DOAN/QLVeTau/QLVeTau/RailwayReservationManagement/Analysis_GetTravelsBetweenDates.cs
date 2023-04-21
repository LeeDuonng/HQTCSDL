using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RailwayReservationManagement
{
    public partial class Analysis_GetTravelsBetweenDates : Form
    {
        public Analysis_GetTravelsBetweenDates()
        {
            InitializeComponent();
            FillTCode();
            populate();

        }
        SqlConnection Con = new SqlConnection(@"Data Source=LEDUONG\SQLEXPRESS;Initial Catalog=QLVT;Integrated Security=True");

        private void populate()
        {
            Con.Open();
            string query = "SELECT * FROM dbo.GetTravelsBetweenDates(@SrcDate, @DestDate)";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@SrcDate", SrcDate.Value);
            cmd.Parameters.AddWithValue("@DestDate", DestDate.Value);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            var ds = new DataSet();
            sda.Fill(ds);
            SearchlDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void FillTCode()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("SELECT TravDate FROM TravelTbl", Con);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                SrcDate.Text = rdr["TravDate"].ToString();
                DestDate.Text = rdr["TravDate"].ToString();
            }
            rdr.Close();
            Con.Close();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            populate();

        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            Analysis analysis = new Analysis();
            analysis.Show();
            this.Hide();
        }
    }
}
