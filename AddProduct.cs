using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace POS_software
{
    public partial class AddProduct : Form
    {
        Database auth;
        subMenu newSubMenu = new subMenu();
        

        public AddProduct()
        {
            InitializeComponent();
            LoadCategories();
        }

        public void LoadCategories()
        {
            CategoriesBox.Items.Clear();
            auth = new Database();
            auth.getconnection();

            using (SQLiteConnection con = new SQLiteConnection(auth.connectionstring))
            {
                con.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                string query = @"SELECT Category FROM Categories ORDER BY Category ASC";

                cmd.CommandText = query;
                cmd.Connection = con;

                using (SQLiteDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        CategoriesBox.Items.Add(read.GetValue(0));

                    }
                }
                CategoriesBox.SelectedIndex = 0;
                con.Close();
            }
        }



        private void checkCategory(string category)
        {
            auth = new Database();


            auth.createDatabase();
            auth.getconnection();

            using (SQLiteConnection con = new SQLiteConnection(auth.connectionstring))
            {
                SQLiteCommand cmd = new SQLiteCommand();
                con.Open();

                int count = 0;
                string query = @"SELECT * FROM Categories WHERE Category='" + category + "'";
                cmd.CommandText = query;
                cmd.Connection = con;

                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    count++;
                }
                if (count != 0)
                {
                    MessageBox.Show("Category Already Exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (count == 0)
                {
                    AddCategory(category);
                }

                con.Close();
            }
        }



        private void AddCategory(string category)
        {
            auth = new Database();
            auth.getconnection();

            using (SQLiteConnection con = new SQLiteConnection(auth.connectionstring))
            {
                con.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                string query = @"INSERT INTO Categories(Category) VALUES (@category)";
                cmd.CommandText = query;
                cmd.Connection = con;
                cmd.Parameters.Add(new SQLiteParameter("@category", category));
                cmd.ExecuteNonQuery();

                MessageBox.Show("Category Added Successfully!", "Success", MessageBoxButtons.OK);
                con.Close();
            };

            LoadCategories();

        }





        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AddProductCancelbtn_Click(object sender, EventArgs e)
        {
           
            Close();
        
        }

        private void IncreasePricebtn_Click(object sender, EventArgs e)
        {
            decimal x = 0.01m;


            decimal amount;
            bool parsedOk = decimal.TryParse(PriceText.Text, out amount);
            amount = amount + x;

            PriceText.Text = amount.ToString();

        }

        private void DecreasePricebtn_Click(object sender, EventArgs e)
        {

            decimal x = 0.01m;


            decimal amount;
            bool parsedOk = decimal.TryParse(PriceText.Text, out amount);
            amount = amount - x;

            PriceText.Text = amount.ToString();
        }

        private void AddStockPlus_Click(object sender, EventArgs e)
        {
            int x = 1;


            int amount;
            bool parsedOk = int.TryParse(StockText.Text, out amount);
            amount = amount + x;

            StockText.Text = amount.ToString();
        }

        private void AddStockMinus_Click(object sender, EventArgs e)
        {

            int x = 1;


            int amount;
            bool parsedOk = int.TryParse(StockText.Text, out amount);
            amount = amount - x;

            StockText.Text = amount.ToString();
        }

        private void StockText_Enter(object sender, EventArgs e)
        {
            if (StockText.Text == "1")
            {
                StockText.Text = string.Empty;
            }
        }

        private void StockText_Leave(object sender, EventArgs e)
        {
            if (StockText.Text == string.Empty)
            {
                StockText.Text = "1";
            }
        }

        private void PriceText_Enter(object sender, EventArgs e)
        {
            if (PriceText.Text == "0.00")
            {
                PriceText.Text = string.Empty;
            }
        }

        private void PriceText_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal Ko;
                bool PassedOK = decimal.TryParse(PriceText.Text, out Ko);

                if (PriceText.Text == string.Empty)
                {
                    PriceText.Text = "0.00";
                }
                else
                {
                    PriceText.Text = String.Format("{0:0.00}", double.Parse(PriceText.Text));
                }
            }
            catch{
                MessageBox.Show("Invalid Input", "Error");
                PriceText.Text = "0.00";
            }
           
        
        }

        private void AddStockPlus_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void IncreasePricebtn_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void AddStockMinus_MouseDown(object sender, MouseEventArgs e)
        {

        }
        private void DecreasePricebtn_MouseDown(object sender, MouseEventArgs e)
        {


        }

        private void Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddProduct_Load(object sender, EventArgs e)
        {

        }

        private void BunifuMaterialTextbox1_Enter(object sender, EventArgs e)
        {
            if (EnterBarcode.Text == "Enter Barcode Here")
            {
                EnterBarcode.Text = string.Empty;
            }
        }

        private void EnterBarcode_Leave(object sender, EventArgs e)
        {
            if (EnterBarcode.Text == string.Empty)
            {
                EnterBarcode.Text = "Enter Barcode Here";
            }
        }

        private void EnterBrand_Enter(object sender, EventArgs e)
        {
            if (EnterBrand.Text == "Enter Brand Here")
            {
                EnterBrand.Text = string.Empty;
            }
        }

        private void EnterBrand_Leave(object sender, EventArgs e)
        {

            if (EnterBrand.Text == string.Empty)
            {
                EnterBrand.Text = "Enter Brand Here";
            }
        }

        private void EnterDescription_Enter(object sender, EventArgs e)
        {
            if (EnterDescription.Text == "Enter Description Here")
            {
                EnterDescription.Text = string.Empty;
            }
        }

        private void EnterDescription_Leave(object sender, EventArgs e)
        {
            if (EnterDescription.Text == string.Empty)
            {
                EnterDescription.Text = "Enter Description Here";
            }
        }

        private void AddProductfbtn_Click(object sender, EventArgs e)
        {
            if (EnterBarcode.Text != "Enter Barcode Here" && EnterBrand.Text != "Enter Brand Here" && EnterDescription.Text != "Enter Description Here" && StockText.Text != string.Empty && PriceText.Text != "0.00" && CategoriesBox.SelectedItem.ToString() != string.Empty) {
                int Stock;
                bool passedOK = int.TryParse(StockText.Text, out Stock);

                Decimal Price;
                bool passsedOK = Decimal.TryParse(PriceText.Text, out Price);

                Addproduct(EnterBarcode.Text, EnterBrand.Text, EnterDescription.Text, Stock, CategoriesBox.SelectedItem.ToString(), Price);
                clearAddProduct();

   
                

            }
            else
            {
                MessageBox.Show("Form Incomplete!", "Detail Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
        }

        public void clearAddProduct()
        {
            EnterBarcode.Text = "Enter Barcode Here";
            EnterBrand.Text = "Enter Brand Here";
            EnterDescription.Text = "Enter Description Here";
            StockText.Text = "1";
            CategoriesBox.Text = string.Empty;
            PriceText.Text = "0.00";
        }


        private void Addproduct(string Barcode, string Brand, string Description, int Stock, string Category, decimal Price )
        {
            try
            {
                auth = new Database();
                auth.getconnection();

                using (SQLiteConnection con = new SQLiteConnection(auth.connectionstring))
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand();

                    string query2 = @"INSERT INTO Inventory (Barcode,Brand,Description,Stock,Category,Price) VALUES (@Barcode,@Brand,@Description,@Stock,@Category,@Price)";
                    cmd.CommandText = query2;
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SQLiteParameter("@Barcode", Barcode));
                    cmd.Parameters.Add(new SQLiteParameter("@Brand", Brand));
                    cmd.Parameters.Add(new SQLiteParameter("@Description", Description));
                    cmd.Parameters.Add(new SQLiteParameter("@Stock", Stock));
                    cmd.Parameters.Add(new SQLiteParameter("@Category", Category));
                    cmd.Parameters.Add(new SQLiteParameter("@Price", Price));
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Product Added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    con.Close();

                }
            }
            catch
            {
                MessageBox.Show("Product Already Exists!", "Error", MessageBoxButtons.OK);
            }

            
        }

        private void PriceText_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void PriceText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '.')
            {
                e.Handled = !Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar);
            }
        }

        private void StockText_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar);
        }

        private void BunifuThinButton22_Click(object sender, EventArgs e)
        {

        }

        private void AddNewCategory(object sender, EventArgs e)
        {
            checkCategory(CategoriesBox.Text.ToString());
        }
    }
}

