using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RailwayReservationManagement
{
    public partial class Analysis : Form
    {
        public Analysis()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Analysis_TrainInfo analysis = new Analysis_TrainInfo();
            analysis.Show();
            this.Close();
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm(); 
            mainForm.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Analysis_GetTravelsBetweenDates analysis = new Analysis_GetTravelsBetweenDates();
            analysis.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Analysis_GetPassengersOnTravel analysis_GetPassengersOnTravel  = new Analysis_GetPassengersOnTravel();
            analysis_GetPassengersOnTravel.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
