using POS_software.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KimtToo.VisualReactive;

namespace POS_software
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

          

        }

        public string Mainusername;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;


            usernamelbl.Text = Mainusername;
            usernamelbl.Show();
        }

        //same as salespersonbtn
        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void Exitbutton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Managerbtn_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.ShowDialog();
        }

        private void Backbutton_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }


        private void MouseDetect_Tick(object sender, EventArgs e)
        {


        }

        private void BunifuFlatButton7_Click(object sender, EventArgs e)
        {
            this.Hide();

            Login log = new Login();
            log.ShowDialog();

            this.Close();

        }

        private void SideMenu_Click(object sender, EventArgs e)
        {
            VSReactive<int>.SetState("menu", int.Parse(((Control)sender).Tag.ToString()));
            VSReactive<string>.SetState("Boardlbl", ((Control)sender).Text);

        }

        private void SubMenu1_Load(object sender, EventArgs e)
        {

        }
    }
}
