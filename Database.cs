using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_software
{
    class Database
    {
        public SQLiteConnection myconnection;
       
       

        public string connectionstring { get; set; }
        string connection;

        public void getconnection()
        {
            connection = @"Data Source=Database.db ; Version=3";
            connectionstring = connection;
        }

        public Database()
        {
            ///myconnection = new SQLiteConnection("Data Source = Database.db;version=3");

            if (!File.Exists("./Database.db"))
            {
                SQLiteConnection.CreateFile("Database.db");

                getconnection();
                using (SQLiteConnection con = new SQLiteConnection(connection))
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    con.Open();

                    string query = @"CREATE TABLE Logins (Username Text(25), Password Text(25))";
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();

                    string query1 = @"CREATE TABLE Admincode (code Text(25))";
                    cmd.CommandText = query1;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();

                    string query4 = @"CREATE TABLE Categories (code Text(25))";
                    cmd.CommandText = query4;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();

                    string query3 = @"CREATE TABLE Inventory (Barcode Text(30) primary key, Brand Text(30), Description Text(40), Stock integer, Category Text(30), Price Decimal)";
                    cmd.CommandText = query3;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();


                    string query2 = @"INSERT INTO Admincode (code) VALUES (@code)";
                    cmd.CommandText = query2;
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SQLiteParameter("@code", "14AD568Y"));
                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
            else
            {
                return;
            }
        }

        //create a new Database.db file with Logins, AdminCode tables and insert one AdminCode
        public void createDatabase()
        {
            if (!File.Exists("./Database.db"))
            {
                SQLiteConnection.CreateFile("Database.db");

                getconnection();
                using (SQLiteConnection con = new SQLiteConnection(connection))
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    con.Open();

                    string query = @"CREATE TABLE Logins (Username Text(25), Password Text(25))";
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();

                    string query1 = @"CREATE TABLE Admincode (code Text(25))";
                    cmd.CommandText = query1;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();

                    string query2 = @"INSERT INTO Admincode (code) VALUES (@code)";
                    cmd.CommandText = query2;
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SQLiteParameter("@code", "14AD568Y"));

                    con.Close();
                }
            }
            else
            {
                return;
            }
        }
    

        public void openconnection()
        {
            if(myconnection.State != System.Data.ConnectionState.Open)
            {
                myconnection.Open();
            }
        }

        public void closeconnection()
        {
            if (myconnection.State == System.Data.ConnectionState.Open)
            {
                myconnection.Close();
            }
        }
    }

}
