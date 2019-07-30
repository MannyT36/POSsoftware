using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KimtToo.VisualReactive;
using LiveCharts;
using LiveCharts.WinForms;
using Bunifu;
using System.Data.SQLite;
using Microsoft.CSharp;
using System.Globalization;

namespace POS_software
{
    public partial class subMenu : UserControl
    {
        public subMenu()
        {
            if (!this.DesignMode)
            {
                InitializeComponent();
                if (Program.IsInDesignMode()) return;

                VSReactive<int>.Subscribe("menu", e => tabControl1.SelectedIndex = e);
                VSReactive<string>.Subscribe("Boardlbl", e => Boardlbl.Text = e);

                Datelbl.Text = DateTime.Now.ToString("dddd, MM.dd.yyyy");
                TimeTimer.Start();


                InventoryDisplay();
                alert();



                CartView.Rows.Add("yogurt",DrawTextBox() , "3.00");
                
            }
        }

        Database auth;

        public static void DrawTextBox(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, System.Windows.Forms.VisualStyles.TextBoxState state) { }

        //Changes the Notifbell to NotifActive
        public void alert()
        {
            auth = new Database();
            auth.getconnection();

            using (SQLiteConnection con = new SQLiteConnection(auth.connectionstring))
            {
                con.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                string query = @"SELECT * FROM Inventory WHERE Stock<=10";

                int count = 0;
                cmd.CommandText = query;
                cmd.Connection = con;

                using (SQLiteDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        count++;
                       
                    }
                    if (count > 0)
                    {
                        NotifBell.Image = Properties.Resources.Bell_Active;
                    }
                    else
                    {
                        NotifBell.Image = Properties.Resources.Bell;
                    }
                }
                con.Close();
            }
        }


        //selects * from Inventory and displays in Datagridview
        public void InventoryDisplay()
        {
            auth = new Database();
            auth.getconnection();

            using (SQLiteConnection con = new SQLiteConnection(auth.connectionstring))
            {
                con.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                string query = @"SELECT * FROM Inventory";

                cmd.CommandText = query;
                cmd.Connection = con;

                using (SQLiteDataReader read = cmd.ExecuteReader())
                {
                    InventoryDataGrid.Rows.Clear();
                    InvCurrentlbl.Text = "All Products";
                    while (read.Read())
                    {
                        
                        InventoryDataGrid.Rows.Add();
                            InventoryDataGrid.Rows[InventoryDataGrid.Rows.Count - 1].Cells[0].Value = InventoryDataGrid.Rows.Count;
                            InventoryDataGrid.Rows[InventoryDataGrid.Rows.Count - 1].Cells[1].Value = read.GetValue(0);
                            InventoryDataGrid.Rows[InventoryDataGrid.Rows.Count - 1].Cells[2].Value = read.GetValue(1);
                            InventoryDataGrid.Rows[InventoryDataGrid.Rows.Count - 1].Cells[3].Value = read.GetValue(2);
                            InventoryDataGrid.Rows[InventoryDataGrid.Rows.Count - 1].Cells[4].Value = generate_pp(read.GetDecimal(3));
                            InventoryDataGrid.Rows[InventoryDataGrid.Rows.Count - 1].Cells[5].Value = read.GetValue(3);
                            InventoryDataGrid.Rows[InventoryDataGrid.Rows.Count - 1].Cells[6].Value = read.GetValue(4);
                            InventoryDataGrid.Rows[InventoryDataGrid.Rows.Count - 1].Cells[7].Value = String.Format("{0:0.00}", read.GetValue(5));



                    }
                }
                con.Close();

                alert();

            }
            }


        //When you type a Brand Name/Barcode in SearchItem.Text and click on search this is triggered
        public void SearchInv(string search)
        {
            auth = new Database();
            auth.getconnection();

            using (SQLiteConnection con = new SQLiteConnection(auth.connectionstring))
            {
                con.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                string query = @"SELECT * FROM Inventory WHERE Description LIKE '%" + search + "%' OR Brand LIKE '%" + search + "%' OR Barcode='" + search + "'";

                cmd.CommandText = query;
                cmd.Connection = con;

                using (SQLiteDataReader read = cmd.ExecuteReader())
                {
                    InventoryDataGrid.Rows.Clear();
                    InvCurrentlbl.Text = "Custom Search";
                    while (read.Read())
                    {

                        InventoryDataGrid.Rows.Add();
                        InventoryDataGrid.Rows[InventoryDataGrid.Rows.Count - 1].Cells[0].Value = InventoryDataGrid.Rows.Count;
                        InventoryDataGrid.Rows[InventoryDataGrid.Rows.Count - 1].Cells[1].Value = read.GetValue(0);
                        InventoryDataGrid.Rows[InventoryDataGrid.Rows.Count - 1].Cells[2].Value = read.GetValue(1);
                        InventoryDataGrid.Rows[InventoryDataGrid.Rows.Count - 1].Cells[3].Value = read.GetValue(2);
                        InventoryDataGrid.Rows[InventoryDataGrid.Rows.Count - 1].Cells[4].Value = generate_pp(read.GetDecimal(3));
                        InventoryDataGrid.Rows[InventoryDataGrid.Rows.Count - 1].Cells[5].Value = read.GetValue(3);
                        InventoryDataGrid.Rows[InventoryDataGrid.Rows.Count - 1].Cells[6].Value = read.GetValue(4);
                        InventoryDataGrid.Rows[InventoryDataGrid.Rows.Count - 1].Cells[7].Value = String.Format("{0:0.00}",read.GetValue(5));



                    }
                }
                con.Close();

            }
        }

        Random r = new Random();
       
        private void TabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            NotifBell.Image = Properties.Resources.Bell_Active;
        }

        private void TimeTimer_Tick(object sender, EventArgs e)
        {
            Timelbl.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void Searchbtn_MouseHover(object sender, EventArgs e)
        {
            Searchbtn.Image = Properties.Resources.Search;
        }

        private void SearchEnter(object sender, EventArgs e)
        {
            if (SearchItem.Text == "Search Item Name/Brand/Code")
            {
                SearchItem.Text = string.Empty;
            }
        }

        private void SearchLeave(object sender, EventArgs e)
        {
            if (SearchItem.Text == string.Empty)
            {
                SearchItem.Text = "Search Item Name/Brand/Code";
            }

        }

        private void InventoryTPbuttonsPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Panel5_Paint(object sender, PaintEventArgs e)
        {
            

        }

        //draws the stock progressbar/percentage bar
        public Image generate_pp(decimal pass)
        {
            decimal StockbarPanel_d = StockbarPanel.Width;
            decimal x = 0;

            x = (pass * StockbarPanel_d) / 100;

            PB1.Width = (int)Math.Round(x, 0);
            PB1.Dock = DockStyle.Left;
            return panelToBitmap(PB1);
        }

        
        private static Image panelToBitmap(Control pnl)
        {
            var bmp = new Bitmap(pnl.Width, pnl.Height);
            pnl.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
            pnl.Dock = DockStyle.Left;
            return bmp;
        }

        private void InventoryDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Searchbtn_Click(object sender, EventArgs e)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            SearchItem.Text = textInfo.ToTitleCase(SearchItem.Text);
            SearchInv(SearchItem.Text);
            SearchItem.Text = string.Empty;
        }

        private void BunifuFlatButton4_Click(object sender, EventArgs e)
        {

        }

        private void AllProductsBtn_Click(object sender, EventArgs e)
        {

        }

        private void AllProductBtn_Click(object sender, EventArgs e)
        {
                InventoryDisplay();
               

        }

        private void BunifuCustomLabel1_Click(object sender, EventArgs e)
        {

        }

        private void Panel17_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BunifuRange1_RangeChanged(object sender, EventArgs e)
        {
        
        }

        public string Min, Max;

        //When Clicked Deletes Selected GridviewCell and Deletes from Database and Datagridview
        private void BunifuFlatButton2_Click(object sender, EventArgs e)
        {
            auth = new Database();
            auth.getconnection();
            DialogResult dialogResult = MessageBox.Show("Are You Sure You Want To Delete This Product", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                //do something
                foreach (DataGridViewRow row in InventoryDataGrid.SelectedRows)
                {
                    using (SQLiteConnection con = new SQLiteConnection(auth.connectionstring))
                    {
                        con.Open();
                        SQLiteCommand cmd = new SQLiteCommand();
                        string query = @"DELETE FROM Inventory WHERE Barcode='" + InventoryDataGrid.Rows[row.Index].Cells[1].Value + "' AND Description='" + InventoryDataGrid.Rows[row.Index].Cells[3].Value + "' AND Brand='" + InventoryDataGrid.Rows[row.Index].Cells[2].Value + "'";

                        cmd.CommandText = query;
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();


                        MessageBox.Show("Product Deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        InventoryDataGrid.Rows.RemoveAt(row.Index);
                        InventoryDisplay();


                        con.Close();
                        alert();
                        return;
                    }
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
                

            alert();
            return;
        }

        private void InventoryDataGrid_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Chart1_Click(object sender, EventArgs e)
        {

        }

        private void SearchItem_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void BunifuFlatButton1_Click(object sender, EventArgs e)
        {
            AddProduct AddproductForm = new AddProduct();
            AddproductForm.ShowDialog();
        }

        private void BunifuFlatButton3_Click(object sender, EventArgs e)
        { 
            UpdateProduct updateProductForm = new UpdateProduct();

            foreach (DataGridViewRow row in InventoryDataGrid.SelectedRows)
            {
                updateProductForm.Barcode = InventoryDataGrid.Rows[row.Index].Cells[1].Value.ToString();
                updateProductForm.Brand = InventoryDataGrid.Rows[row.Index].Cells[2].Value.ToString();
                updateProductForm.Description = InventoryDataGrid.Rows[row.Index].Cells[3].Value.ToString() ;
                updateProductForm.Categories = InventoryDataGrid.Rows[row.Index].Cells[6].Value.ToString();
                updateProductForm.Stock = InventoryDataGrid.Rows[row.Index].Cells[5].Value.ToString() ;
                updateProductForm.Price = InventoryDataGrid.Rows[row.Index].Cells[7].Value .ToString() ;
                }
            
            updateProductForm.ShowDialog();
        }

        private void BunifuThinButton21_Click(object sender, EventArgs e)
        {

        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AllProductBtn_Click_1(object sender, EventArgs e)
        {
            InventoryDisplay();
            
        }

        
    }

}
