using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RailwayReservationManagement
{
    public partial class Analysis_GetPassengersOnTravel : Form
    {
        public Analysis_GetPassengersOnTravel()
        {
            InitializeComponent();
            FillTravCode();
            populate();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=LEDUONG\SQLEXPRESS;Initial Catalog=QLVT;Integrated Security=True");

        private void populate()
        {
            Con.Open();
            string query = "SELECT * FROM dbo.GetPassengersOnTrain(@TravCode)";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@TravCode", TravelCb.SelectedValue.ToString());
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            var ds = new DataSet();
            sda.Fill(ds);
            CancelDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void FillTravCode()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select CONVERT(varchar(10), TravDate, 103) + ' - ' + Src + ' - ' + Dest as TravelInfo, TravCode from TravelTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TravelInfo", typeof(string));
            dt.Columns.Add("TravCode", typeof(int));
            dt.Load(rdr);
            TravelCb.DisplayMember = "TravelInfo";
            TravelCb.ValueMember = "TravCode";
            TravelCb.DataSource = dt;
            Con.Close();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TravelCb_SelectedIndexChanged(object sender, EventArgs e)
        {

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
