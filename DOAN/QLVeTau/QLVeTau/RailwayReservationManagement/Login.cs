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
    

    public partial class Login : Form
    {
        public static string myConnectionType;
        public Login()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection Con = new SqlConnection("Data Source=LEDUONG\\SQLEXPRESS;Initial Catalog=QLVT;Integrated Security=True");
                Con.Open();
                string query = "Select * from users where username = '" + UnameTb.Text + "' and password = '" + PassTb.Text + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            
                DataTable dataTable = new DataTable();
                sda.Fill(dataTable);

                if (dataTable.Rows[0]["type"].ToString() == "quantrivien")
                {
                    myConnectionType = "A";
                }
                else if (dataTable.Rows[0]["type"].ToString() == "quanlychuyentau")
                {
                    myConnectionType = "B";
                }
                else if (dataTable.Rows[0]["type"].ToString() == "quanlyvetau")
                {
                    myConnectionType = "C";
                }

                if (dataTable.Rows.Count > 0 )
                {
                    MainForm Main = new MainForm();
                    Main.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Nhập sai vui lòng nhập lại");
                    UnameTb.Clear();
                    PassTb.Clear();
                }
            }
            catch 
            {
                MessageBox.Show("Lỗi đăng nhập");
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UnameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void PassTb_TextChanged(object sender, EventArgs e)
        {
            PassTb.UseSystemPasswordChar = true;
        }
    }
}
