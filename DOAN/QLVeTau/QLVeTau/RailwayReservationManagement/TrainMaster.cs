using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RailwayReservationManagement
{
    public partial class TrainMaster : Form
    {
        public TrainMaster()
        {
            InitializeComponent();
            populate();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuMaterialTextbox2_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }
        SqlConnection Con = new SqlConnection(@"Data Source=LEDUONG\SQLEXPRESS;Initial Catalog=QLVT;Integrated Security=True"); 
        private void populate()
        {
            Con.Open();
            string query = "select * from TRAINTBL";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            var ds = new DataSet();
            sda.Fill(ds);
            TrainDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            string TrStatus = "";
            if (TrNameTb.Text == "" || TrainCapTb.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
            else
            {
                if (BusyRd.Checked == true)
                {
                    TrStatus = "Busy";
                }
                else if (FreeRd.Checked == true)
                {
                    TrStatus = "Available";
                }

                try
                {
                    Con.Open();

                    SqlCommand cmd = new SqlCommand("AddTrain", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TrainName", TrNameTb.Text);
                    cmd.Parameters.AddWithValue("@TrainCap", TrainCapTb.Text);
                    cmd.Parameters.AddWithValue("@TrainStatus", TrStatus);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Thêm tàu thành công");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void reset()
        {
            TrNameTb.Text = "";
            TrainCapTb.Text = "";
            BusyRd.Checked = false;
            FreeRd.Checked = false;
            key = 0;
        }
        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        int key = 0;
        private void TrainDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            TrNameTb.Text = TrainDGV.SelectedRows[0].Cells[1].Value.ToString();
            TrainCapTb.Text = TrainDGV.SelectedRows[0].Cells[2].Value.ToString();
            if(TrNameTb.Text == "")
            {
                key = 0;
            }else
            {
                key = Convert.ToInt32(TrainDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            if(key == 0)
            {
                MessageBox.Show("Chọn tàu muốn xoá");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("DeleteTrain", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TrainId", key);
                    SqlDataReader reader = cmd.ExecuteReader();
                    MessageBox.Show("Xoá thành công!");
                    if (reader.HasRows)
                    {
                        reader.Read();
                        MessageBox.Show(reader["Message"].ToString());
                    }
                    
                    Con.Close();
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            string TrStatus = "";
            if (TrNameTb.Text == "" || TrainCapTb.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
            else
            {
                if (BusyRd.Checked == true)
                {
                    TrStatus = "Busy";
                }
                else if (FreeRd.Checked == true)
                {
                    TrStatus = "Available";
                }
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("UpdateTrain", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TrainId", key);
                    cmd.Parameters.AddWithValue("@TrainName", TrNameTb.Text);
                    cmd.Parameters.AddWithValue("@TrainCap", TrainCapTb.Text);
                    cmd.Parameters.AddWithValue("@TrainStatus", TrStatus);
                    SqlDataReader reader = cmd.ExecuteReader();
                    MessageBox.Show("Cập nhật thành công");
                    if (reader.HasRows)
                    {
                        reader.Read();
                        MessageBox.Show(reader["Message"].ToString());
                    }
                    Con.Close();
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
            MainForm Main = new MainForm();
            Main.Show();
            this.Hide();
        }

        private void BusyRd_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
