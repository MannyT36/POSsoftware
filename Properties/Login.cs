using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_software.Properties
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            Database Databaseobj1 = new Database();


//string query = "INSERT INTO Login ('Username','Password') VALUES (@Username,@Password)";
          //  SQLiteCommand mycommand = new SQLiteCommand(query, Databaseobj1.myconnection);
           // Databaseobj1.openconnection();
          //  mycommand.Parameters.AddWithValue("@Username", "Kofi1");
          //  mycommand.Parameters.AddWithValue("@Password", "Kofipass1");
          //  mycommand.ExecuteNonQuery();
          //  Databaseobj1.closeconnection();


        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
