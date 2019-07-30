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
    public partial class UpdateProduct : Form
    {
        Database auth;
        subMenu subMenuForm;
        public UpdateProduct()
        {
            InitializeComponent();
           

            LoadCategories();

        }

        public string Barcode, Brand, Description, Categories, Stock, Price;

        private void UpdateProductCancelbtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateProductbtn_Click(object sender, EventArgs e)
        {
            int Stock;
            Stock = int.Parse(StockText.Text) + int.Parse(CurrentStocklbl.Text);
            


            Update(UpdateBarcode.Text, UpdateBrand.Text, UpdateDescription.Text, Stock.ToString(), CategoriesBox.Text, PriceText.Text, Barcode);

           

          
            
  
        }


        public void Update(string Barcode, string Brand, string Description, string Stock, string Category, string Price, string wherevalue)
        {

            if (UpdateBarcode.Text != string.Empty && UpdateBrand.Text != string.Empty && UpdateDescription.Text != string.Empty && StockText.Text != string.Empty && PriceText.Text != "0.00" && CategoriesBox.Text != string.Empty)
            {
                auth = new Database();
                auth.getconnection();

                using (SQLiteConnection con = new SQLiteConnection(auth.connectionstring))
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand();

                    string query1 = "UPDATE Inventory SET Barcode='" + Barcode + "',Brand='" + Brand + "',Description='" + Description + "',Stock='" + Stock + "',Category='" + Category + "',Price='" + Price + "'  WHERE Barcode='" + wherevalue + "'";
                    cmd.CommandText = query1;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();


                    MessageBox.Show("Product Updated!", "Success", MessageBoxButtons.OK);


                   
                    con.Close();
                    Close();
                }

            }
            else
            {
                MessageBox.Show("Form Incomplete", "Error", MessageBoxButtons.OK);
                return;
            };
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

                MessageBox.Show("Category Added!", "Success", MessageBoxButtons.OK);
                con.Close();
            };

        }

        private void UpdateProduct_Load(object sender, EventArgs e)
        {
            UpdateBarcode.Text = Barcode;
            UpdateBrand.Text = Brand;
            UpdateDescription.Text = Description;
            CurrentStocklbl.Text = Stock;
            PriceText.Text = Price;
            CategoriesBox.SelectedItem = Categories;
        }

      

        public void LoadCategories()
        {
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
                con.Close();
                
            }
           
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

        private void UpdateBarcode_enter(object sender, EventArgs e)
        {
            if (UpdateBarcode.Text == "Enter Barcode Here")
            {
                UpdateBarcode.Text = string.Empty;
            }
        }

        private void UpdateBarcode_Leave(object sender, EventArgs e)
        {
            if (UpdateBarcode.Text == string.Empty)
            {
                UpdateBarcode.Text = Barcode;
            }
        }

        private void UpdateBrand_Enter(object sender, EventArgs e)
        {
            if (UpdateBrand.Text == "Enter Brand Here")
            {
                UpdateBrand.Text = string.Empty;
            }
        }

        private void UpdateBrand_Leave(object sender, EventArgs e)
        {

            if (UpdateBrand.Text == string.Empty)
            {
                UpdateBrand.Text = Brand;
            }
        }

        private void UpdateDescription_Enter(object sender, EventArgs e)
        {
            if (UpdateDescription.Text == "Enter Description Here")
            {
                UpdateDescription.Text = string.Empty;
            }
        }

        private void UpdateDescription_Leave(object sender, EventArgs e)
        {
            if (UpdateDescription.Text == string.Empty)
            {
                UpdateDescription.Text = Description;
            }
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

      

        private void NewCategory_Click(object sender, EventArgs e)
        {
            checkCategory(CategoriesBox.Text.ToString());
            CategoriesBox.Items.Clear();
            LoadCategories();
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
            if (StockText.Text == "0")
            {
                StockText.Text = string.Empty;
            }
        }

        private void StockText_Leave(object sender, EventArgs e)
        {
            if (StockText.Text == string.Empty)
            {
                StockText.Text = "0";
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
                    PriceText.Text = Price;
                }
                else
                {
                    PriceText.Text = String.Format("{0:0.00}", double.Parse(PriceText.Text));
                }
            }
            catch
            {
                MessageBox.Show("Invalid Input", "Error");
                PriceText.Text = "0.00";
            }


        }


    }
    }

