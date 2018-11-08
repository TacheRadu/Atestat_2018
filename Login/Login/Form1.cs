using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Login
{
    public partial class Form1 : Form
    {
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
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
