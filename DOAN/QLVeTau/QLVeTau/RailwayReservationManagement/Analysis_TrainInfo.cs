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
    public partial class Analysis_TrainInfo : Form
    {
        public Analysis_TrainInfo()
        {
            InitializeComponent();
            FillTCode();
            populate();

        }
        SqlConnection Con = new SqlConnection(@"Data Source=LEDUONG\SQLEXPRESS;Initial Catalog=QLVT;Integrated Security=True");

        private void populate()
        {
            Con.Open();
            string query = "SELECT * FROM dbo.GetTrainSchedule(@TrainId)";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@TrainId", TidCb.SelectedValue.ToString());
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            var ds = new DataSet();
            sda.Fill(ds);
            CancelDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void FillTCode()
        {
            string TrStatus = "Busy";
            Con.Open();
            SqlCommand cmd = new SqlCommand("select TrainId from TRAINTBL where TrainStatus='" + TrStatus + "'", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TrainId", typeof(int));
            dt.Load(rdr);
            TidCb.ValueMember = "TrainId";
            TidCb.DataSource = dt;
            Con.Close();
        }
        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            Analysis analysis = new Analysis();
            analysis.Show();
            this.Hide();
        }

        private void CancelDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void TravelCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TidCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
