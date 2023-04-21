using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RailwayReservationManagement
{
    public partial class TravelMaster : Form
    {
        public TravelMaster()
        {
            InitializeComponent();
            populate();
            FillTCode();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=LEDUONG\SQLEXPRESS;Initial Catalog=QLVT;Integrated Security=True");
        private void populate()
        {
            Con.Open();
            string query = "select * from TRAVELTBL";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            var ds = new DataSet();
            sda.Fill(ds);
            TravelDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void FillTCode()
        {
            string TrStatus = "Busy";
            Con.Open();
            SqlCommand cmd = new SqlCommand("select TrainId from TRAINTBL where TrainStatus='"+TrStatus+"'", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TrainId", typeof(int));
            dt.Load(rdr);
            TCode.ValueMember = "TrainId";
            TCode.DataSource = dt;
            Con.Close();
        }
        private void TravelMaster_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void ChangeStatus()
        {
            string TrStatus = "Busy";
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("UpdateTrainStatus", Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TrainId", TCode.SelectedValue);
                cmd.Parameters.AddWithValue("@TrainStatus", TrStatus);
                cmd.ExecuteNonQuery();
                Con.Close();
                populate();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (TCostTb.Text == "" || TCode.SelectedIndex == -1 || SrcCb.SelectedIndex == -1 || DestCb.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
            else
            {
                try
                {
                    Con.Open();
                    DateTime travDate = TravDate.Value;
                    int trainCode = int.Parse(TCode.SelectedValue.ToString());
                    string src = SrcCb.SelectedItem.ToString();
                    string dest = DestCb.SelectedItem.ToString();
                    int cost = int.Parse(TCostTb.Text);

                    // Create SQL command
                    SqlCommand cmd = new SqlCommand("AddTravel", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TravDate", travDate);
                    cmd.Parameters.AddWithValue("@Train", trainCode);
                    cmd.Parameters.AddWithValue("@Src", src);
                    cmd.Parameters.AddWithValue("@Dest", dest);
                    cmd.Parameters.AddWithValue("@Cost", cost);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm lịch trình thành công");
                    Con.Close();
                    populate();
                    ChangeStatus();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }
        private void Reset()
        {
            SrcCb.SelectedIndex = -1;
            DestCb.SelectedIndex = -1;
            TCode.SelectedIndex = -1;
            TCostTb.Text = "";
        }
        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            if (SrcCb.SelectedIndex == -1 || DestCb.SelectedIndex == -1 || TCostTb.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
            else
            {


                try
                {
                    Con.Open();
                    DateTime travDate = TravDate.Value;
                    int trainCode = int.Parse(TCode.SelectedValue.ToString());
                    string src = SrcCb.SelectedItem.ToString();
                    string dest = DestCb.SelectedItem.ToString();
                    int cost = int.Parse(TCostTb.Text);

                    // Create SQL command
                    SqlCommand cmd = new SqlCommand("UpdateTravel", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TravCode", key);
                    cmd.Parameters.AddWithValue("@TravDate", travDate);
                    cmd.Parameters.AddWithValue("@Train", trainCode);
                    cmd.Parameters.AddWithValue("@Src", src);
                    cmd.Parameters.AddWithValue("@Dest", dest);
                    cmd.Parameters.AddWithValue("@Cost", cost);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sửa lịch trình thành công");
                    Con.Close();
                    populate();
                    ChangeStatus();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int key = 0;
        private void TravelDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            TravDate.Text = TravelDGV.SelectedRows[0].Cells[1].Value.ToString();
            TCode.SelectedValue = TravelDGV.SelectedRows[0].Cells[2].Value.ToString();
            SrcCb.SelectedItem = TravelDGV.SelectedRows[0].Cells[3].Value.ToString();
            DestCb.SelectedItem = TravelDGV.SelectedRows[0].Cells[4].Value.ToString();
            TCostTb.Text = TravelDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (TCode.SelectedIndex == -1)
            {
                key = 0;
                TCostTb.Text = "";
                SrcCb.SelectedIndex = -1;
                DestCb.SelectedIndex = -1;
            }
            else
            {
                key = Convert.ToInt32(TravelDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            MainForm Main = new MainForm();
            Main.Show();
            this.Hide();
        }

        private void TCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
