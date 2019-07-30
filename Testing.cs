using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_software
{
    public partial class Testing : Form
    {
        [DefaultValue(null)]
        public ContextMenuStrip Menu { get; set; }

        [DefaultValue(false)]
        public bool ShowMenuUnderCursor { get; set; }


        public Testing()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
           
        }

        private void Button1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void BunifuDropdown1_onItemSelected(object sender, EventArgs e)
        {

        }

        private void Testing_Load(object sender, EventArgs e)
        {
           
        }
    }
}
