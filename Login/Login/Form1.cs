using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Login{
    public partial class Form1 : Form
    {
        private Button submit = new Button();
        private Button support = new Button();
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
            if (user.Text == "")
            {
                MessageBox.Show("Insert the username, please.");
            }
            else if (pass.Text == "")
            {
                MessageBox.Show("Insert the password, please.");
            }
            else
            {
                ConnectDB orcus = new ConnectDB("localhost", "test", "root", "rootpassword");
                if (orcus.getConnection().State == ConnectionState.Open)
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT password FROM users WHERE username=@val", orcus.getConnection());
                    cmd.Parameters.AddWithValue("@val", user.Text);
                    cmd.Prepare();
                    MySqlDataReader res = cmd.ExecuteReader();
                    if (res.Read())
                    {
                        string password = (string)res["password"];
                        if (password == pass.Text)
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
        }
        private void goToSupport(object sender, EventArgs e)
        {
            Process.Start("http://orcus.cf");
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
            support.Size = new Size(100, 100);
            support.Left = this.Width / 2 - 50;
            support.Top = 200;
            support.Text = "Support";
            support.Click += new EventHandler(goToSupport);
            //submit.ForeColor = Color.Blue;
            submit.BackColor = Color.RoyalBlue;
            submit.Show();
            user.Click += new EventHandler(buttonPress);
            pass.Click += new EventHandler(buttonPress);
            submit.Click += new EventHandler(submission);
            this.Controls.Add(user);
            this.Controls.Add(pass);
            this.Controls.Add(submit);
            this.Controls.Add(support);
            this.BackColor = Color.FromArgb(255, 48, 48, 48);
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
            try
            {
                server = serv;
                database = db;
                user = usr;
                password = pwd;
                string connectionString = "SERVER=" + server + "; DATABASE=" + database + "; USER=" + user + "; PASSWORD=" + password + "; SslMode=none;";
                conn = new MySqlConnection(connectionString);
                conn.Open();
            }
            catch (MySqlException ex)
            {
                if(ex.Number == 1042)
                {
                    MessageBox.Show("Server is down...");
                }
            }
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
