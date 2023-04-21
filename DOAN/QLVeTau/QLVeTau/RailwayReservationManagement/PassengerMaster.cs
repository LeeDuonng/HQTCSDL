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
    public partial class PassengerMaster : Form
    {
        public PassengerMaster()
        {
            InitializeComponent();
            populate();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        SqlConnection Con = new SqlConnection(@"Data Source=LEDUONG\SQLEXPRESS;Initial Catalog=QLVT;Integrated Security=True");
        private void populate()
        {
            Con.Open();
            string query = "select * from PASSENGERTBL";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            var ds = new DataSet();
            sda.Fill(ds);
            PassengerDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            string Gender = "";
            if (PnameTb.Text == "" || PPhoneTb.Text == ""||PaddressTb.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
            else
            {
                if (MaleRd.Checked == true)
                {
                    Gender = "Male";
                }
                else if (FemaleRd.Checked == true)
                {
                    Gender = "Female";
                }
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("AddPassenger", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PName", PnameTb.Text);
                    cmd.Parameters.AddWithValue("@PAdd", PaddressTb.Text);
                    cmd.Parameters.AddWithValue("@PGender", Gender);
                    cmd.Parameters.AddWithValue("@PNat", NatCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Pphone", PPhoneTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm thành công");
                    Con.Close();
                    populate();
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
            PnameTb.Text = "";
            PaddressTb.Text = "";
            PPhoneTb.Text = "";
            MaleRd.Checked = false;
            FemaleRd.Checked = false;
            NatCb.SelectedIndex = -1;
            key = 0;
        }
        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            Reset();
        }
        int key = 0;
        private void PassengerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PnameTb.Text = PassengerDGV.SelectedRows[0].Cells[1].Value.ToString();
            PaddressTb.Text = PassengerDGV.SelectedRows[0].Cells[2].Value.ToString();
            NatCb.Text = PassengerDGV.SelectedRows[0].Cells[4].Value.ToString();
            PPhoneTb.Text = PassengerDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (PnameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(PassengerDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            string Gender = "";
            if (PnameTb.Text == "" || PPhoneTb.Text == "" || PaddressTb.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
            else
            {
                if (MaleRd.Checked == true)
                {
                    Gender = "Male";
                }
                else if (FemaleRd.Checked == true)
                {
                    Gender = "Female";
                }
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("UpdatePassenger", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PName", PnameTb.Text);
                    cmd.Parameters.AddWithValue("@PAdd", PaddressTb.Text);
                    cmd.Parameters.AddWithValue("@PGender", Gender);
                    if (NatCb.SelectedItem != null)
                    {
                        cmd.Parameters.AddWithValue("@PNat", NatCb.SelectedItem.ToString());
                    }
                    cmd.Parameters.AddWithValue("@Pphone", PPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@PId", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật thành công");
                    Con.Close();
                    populate();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Vui lòng chọn hành khách muốn xoá");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("DeletePassenger", Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PId", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Xoá thành công");
                    Con.Close();
                    populate();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PassengerMaster_Load(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
            MainForm Main = new MainForm();
            Main.Show();
            this.Hide();
        }

        private void NatCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FemaleRd_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
