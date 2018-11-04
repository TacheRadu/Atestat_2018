using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Login{
    public partial class Form1 : Form
    {
        private Button submit = new Button();
        private TextBox user = new TextBox();
        private TextBox pass = new TextBox();
        private void buttonPress(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box.Text == "username" || box.Text == "password") {
                box.Clear();
            }
        }
        private void submission(object sender, EventArgs e)
        {
            if(user.Text == "")
            {
                MessageBox.Show("Insert the username, please.");
            }
            else if(pass.Text == "")
            {
                MessageBox.Show("Insert the password, please.");
            }
            else
            {
                ConnectDB orcus = new ConnectDB("localhost", "orcus", "username", "password");
                MySqlCommand cmd = new MySqlCommand("SELECT password FROM users WHERE username=@val", orcus.getConnection());
                cmd.Parameters.AddWithValue("@val", user.Text);
                cmd.Prepare();
                MySqlDataReader res = cmd.ExecuteReader();
                if (res.Read())
                {
                    string password = (string)res["password"];
                    if(password == pass.Text)
                    {
                        MessageBox.Show("Login Successful!");
                    }
                    else
                    {
                        MessageBox.Show("Wrong password.");
                    }
                }
                else
                {
                    MessageBox.Show("No there is no user named " + user.Text + ".");
                }
                orcus.closeConnection();
            }
        }
        public Form1()
        {
            InitializeComponent();
            user.Text = "username";
            pass.Text = "password";
            user.Size = new System.Drawing.Size(70, 30);
            user.Left = 50;
            user.Top = 50;
            pass.Size = new System.Drawing.Size(70, 30);
            pass.Left = (this.Width - 100 - pass.Width) / 2;
            pass.Top = 50;
            pass.PasswordChar = '*';
            submit.Size = new Size(70, 30);
            submit.Top = 50;
            submit.Left = this.Width - 100;
            submit.Text = "Login";
            submit.Show();
            user.Click += new EventHandler(buttonPress);
            pass.Click += new EventHandler(buttonPress);
            submit.Click += new EventHandler(submission);
            this.Controls.Add(user);
            this.Controls.Add(pass);
            this.Controls.Add(submit);
        }
    }
    public class ConnectDB
    {
        private MySqlConnection conn;
        private string server;
        private string database;
        private string user;
        private string password;
        public ConnectDB(string serv, string db, string usr, string pwd)
        {
            server = serv;
            database = db;
            user = usr;
            password = pwd;
            string connectionString = "SERVER=" + server + "; DATABASE=" + database + "; USER=" + user + "; PASSWORD=" + password + ";";
            conn = new MySqlConnection(connectionString);
            conn.Open();
        }
        public MySqlConnection getConnection()
        {
            return conn;
        }
        public void closeConnection()
        {
            conn.Close();
        }
    }
}
