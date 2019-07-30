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
    public partial class Login : Form
    {
        Timer timer1 = new Timer();
        Timer timer2 = new Timer();
        Label logreg = new Label();

        

        public string usernames;
        Database auth;
        
        

        public Login()
        {
            InitializeComponent();

            this.Size = new Size(320, 480);
            pictureBox2.Image = Properties.Resources.locked;
            pictureBox2.Enabled = false;


        }

      

        // code for checking if login details are correct
        public void AuthLogin(string username, string password)
        {
            auth = new Database();
            auth.getconnection();

            using (SQLiteConnection con = new SQLiteConnection(auth.connectionstring))
            {
                con.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                string query = @"SELECT * FROM Logins WHERE Username='" + username + "' and Password='" + password + "'";

                int count = 0;
                cmd.CommandText = query;
                cmd.Connection = con;

                SQLiteDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    count++;
                }

                if (count == 1)
                {
                    MessageBox.Show("Login Successful");

                    con.Close();


                    this.Hide();
                    Form1 form1 = new Form1();
                    form1.Mainusername = username; 
                    form1.ShowDialog();
                    updateuser(username);

                    
                    
                   

                    


                }
                else
                {
                    MessageBox.Show("Details Error", "Error");
                    con.Close();
                    return;
                }
            }


        }

        //update user name
        public void updateuser(string username)
        {
            usernames = username;
        }

        // Main Login Button
        private void BunifuThinButton21_Click(object sender, EventArgs e)
        {


            AuthLogin(textBox1.Text, textBox2.Text);
        

         
            
        }

  

      

        private void Login_Load(object sender, EventArgs e)
        {
            timer1.Tick += new EventHandler(timer1_go);
            timer2.Tick += new EventHandler(timer2_go);
            timer1.Interval = 10;
            timer2.Interval = 5;
            timer1.Start(); 
        }
        void timer1_go(object sender, EventArgs e)
        {
            Logoposition(); 
        }

        //Code for whenever you click on Login or Register to trigger SLides
        void timer2_go(object sender, EventArgs e)
        {
            if (logreg.Text == "Register"){
                reg();
               
            }
            else if (logreg.Text == "Login")
            {
                log();
            }
        }

        int go = 1;
        int go1 = 6;
        void Logoposition()
        {
            if(panel1.Left == 0)
            {
                
                pictureBox1.Top += go;
                if(pictureBox1.Top > 40)
                {
                    timer1.Stop();
                    colorchng();
                }
            }
            if(panel2.Left <= 0)
            {
                
                pictureBox1.Top -= go1;
                if(pictureBox1.Top < 20)
                {
                    timer1.Stop();
                    colorchng();
                }
                
            }

        }

        //color change in register
        void colorchng()
        {

            if (panel1.Left >= 0)
            {
                bunifuSeparator1.LineColor = Color.Tomato;
                pictureBox1.Image = Properties.Resources.BpLogo;
                panel2.BackColor = Color.Salmon;
                textBox3.BackColor = Color.Salmon;
                textBox4.BackColor = Color.Salmon;
                textBox5.BackColor = Color.Salmon;
                textBox6.BackColor = Color.Salmon;
                bunifuThinButton22.BackColor = Color.Salmon;
                bunifuThinButton22.IdleFillColor = Color.Salmon;
            }
            if (panel2.Left <= 0)
            {

                bunifuSeparator1.LineColor = Color.SeaGreen;
                pictureBox1.Image = Properties.Resources.BpLogogreen;
                panel2.BackColor = Color.SeaGreen;
                textBox3.BackColor = Color.SeaGreen;
                textBox4.BackColor = Color.SeaGreen;
                textBox5.BackColor = Color.SeaGreen;
                textBox6.BackColor = Color.SeaGreen;
                bunifuThinButton22.BackColor = Color.SeaGreen;
                bunifuThinButton22.IdleFillColor = Color.SeaGreen;
            }
           

        }

        //line animation
        void line()
        {
            if (panel1.Left >= 0)
            {
                bunifuSeparator1.LineColor = Color.Tomato;
            }
            if (panel2.Left <= 0)
            {

                bunifuSeparator1.LineColor = Color.SeaGreen;
            }
        }


        int move_speed = 20;
        void reg()
        {
            if (panel2.Left > 0)
            {
                
                timer1.Start();

                panel1.Left -= move_speed;
                panel2.Left -= move_speed;
                if (panel2.Left == 0)
                {
                    timer2.Stop();
                }
            }
        }
        void log()
        {
            if (panel1.Left < 0)
            {
                timer1.Start();
              

                panel2.Left += move_speed;
                panel1.Left += move_speed;
                if (panel1.Left == 0)
                {
                    timer2.Stop();
                }
            }
        }

        //image of key and triggers the Textbox.passwordchar function
        void unlock()
        {
            if (textBox2.PasswordChar == '*')
            {
                textBox2.PasswordChar = '\0';
                pictureBox2.Image = Properties.Resources.unlocked;
            }
            else
            {
                textBox2.PasswordChar = '*';
                pictureBox2.Image = Properties.Resources.locked;
            }
        }

       
        //When you click on Username
        private void Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Username") {
                textBox1.Text = "";
                    }
        }

        //When you leave Username without typing anything
        private void Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Username";
            }
        }

        private void Enter2(object sender, EventArgs e)
        {
            if (textBox2.Text == "Password")
            {
                textBox2.PasswordChar = '*';
                pictureBox2.Enabled = true;
                textBox2.Text = string.Empty;
            }
        }

        private void Leave2(object sender, EventArgs e)
        {
            if (textBox2.Text == string.Empty)
            {
                textBox2.PasswordChar = '\0';
                pictureBox2.Enabled = false;
                textBox2.Text = "Password";
            }


        }


        //Forgot Password Click
        private void Label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Kindly Contact The Administrator", "Password");
        }




        //When Registering, Check if Admin Code is correct then triggers the CheckAccount Method which checks if the username exists or not
        public void CheckAdminCode(string code)
        {
            auth = new Database();
            auth.getconnection();

            using (SQLiteConnection con = new SQLiteConnection(auth.connectionstring))
            {
                con.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                string query = @"SELECT * FROM Admincode WHERE code='" + code + "'";

                int count = 0;
                cmd.CommandText = query;
                cmd.Connection = con;

                SQLiteDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    count++;
                }

                if (count == 1)
                {

                    checkAccount(textBox4.Text);
                    con.Close();

                }
                else
                {
                    MessageBox.Show("Wrond Code!", "Admin Code");
                    con.Close();

                }
            }
        }


        //Register Button
        private void BunifuThinButton22_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != string.Empty && textBox4.Text != string.Empty && textBox5.Text != string.Empty && textBox6.Text != string.Empty)
            {
                if(textBox6.Text == textBox3.Text)
                {
                    CheckAdminCode(textBox5.Text);
                    
                }
                else
                {
                    MessageBox.Show("Passwords do not Match!", "Incorrect Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //Checks if username already exists or not
        private void checkAccount(string username)
        {
            auth = new Database();

           
            auth.createDatabase();
            auth.getconnection();

            using(SQLiteConnection con = new SQLiteConnection(auth.connectionstring))
            {
                SQLiteCommand cmd = new SQLiteCommand();
                con.Open();

                int count = 0;
                string query = @"SELECT * FROM Logins WHERE Username='" + username + "'";
                cmd.CommandText = query;
                cmd.Connection = con;

                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    count++;
                }
                if (count != 0)
                {
                    MessageBox.Show("Username Already Exists!", "Taken",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
                else if (count == 0)
                {
                    insertData(textBox4.Text, textBox3.Text);
                    
                }

                con.Close();
            }
        }

        //inserts the username and password into the database through Register
        private void insertData(string username, string password)
        {
            auth = new Database();
            auth.getconnection();

            using(SQLiteConnection con = new SQLiteConnection(auth.connectionstring))
            {
                con.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                string query = @"INSERT INTO Logins(Username,Password) VALUES (@username,@password)";
                cmd.CommandText = query;
                cmd.Connection = con;
                cmd.Parameters.Add(new SQLiteParameter("@username", username));
                cmd.Parameters.Add(new SQLiteParameter("@password", password));
                cmd.ExecuteNonQuery();

                MessageBox.Show("Registration Complete!", "Success", MessageBoxButtons.OK);
                clearReg();
                con.Close();
            };

        }

        //After Registration is successful, this is triggered to Send the Textboxes in Registration's Text to the default
        public void clearReg()
        {
            textBox3.PasswordChar = '\0';
            textBox6.PasswordChar = '\0';
            textBox3.Text = "Password";
            textBox4.Text = "Username";
            textBox5.Text = "Admin Code";
            textBox6.Text = "Confirm Password";
        }

        private void Enter3(object sender, EventArgs e)
        {
            if (textBox4.Text == "Username")
            {
                textBox4.Text = string.Empty;
            }
        }

        private void Leave3(object sender, EventArgs e)
        {
            if (textBox4.Text == string.Empty)
            {
                textBox4.Text = "Username";
            }
        }

        private void Enter4(object sender, EventArgs e)
        {
            if (textBox3.Text == "Password")
            {
                textBox3.PasswordChar = '*';
                textBox3.Text = string.Empty;
            }
        }

        private void Leave4(object sender, EventArgs e)
        {
           
            
            if (textBox3.Text == string.Empty)
            {
                textBox3.PasswordChar = '\0';
                textBox3.Text = "Password";
            }
        }

        private void Enter5(object sender, EventArgs e)
        {
            if (textBox6.Text == "Confirm Password")
            {
                textBox6.PasswordChar = '*';
                textBox6.Text = string.Empty;
            }
        }

        private void Leave5(object sender, EventArgs e)
        {
            if (textBox6.Text == string.Empty)
            {
                textBox6.PasswordChar = '\0';
                textBox6.Text = "Confirm Password";
            }
        }

        private void Enter6(object sender, EventArgs e)
        {
            if (textBox5.Text == "Admin Code")
            {
                textBox5.Text = string.Empty;
            }
        }

        private void Leave6(object sender, EventArgs e)
        {
            if (textBox5.Text == string.Empty)
            {
                textBox5.Text = "Admin Code";
            }
        }

        //When you click on Register this is triggered and sets logreg to "Register" which triggers the slide
        private void Label2_Click(object sender, EventArgs e)
        {
            Label lr = (Label)sender;

            logreg = lr;
            timer2.Start();
        }

        //When you click on Login in the Register interface it is triggered and sets logreg to "Login" which triggers the slide
        private void Label3_Click(object sender, EventArgs e)
        {
            Label lr = (Label)sender;

            logreg = lr;
            timer2.Start();
        }

        //When clicked on triggers the TextBox.Passwordchar function
        private void PictureBox2_Click(object sender, EventArgs e)
        {
            unlock();
        }

        private void BunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
