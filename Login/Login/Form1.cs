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

        public Form1()
        {
            InitializeComponent();

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
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM users WHERE username=@val AND password = @val2", orcus.getConnection());
                    cmd.Parameters.AddWithValue("@val", user.Text);
                    cmd.Parameters.AddWithValue("@val2", pass.Text);
                    cmd.Prepare();
                    MySqlDataReader res = cmd.ExecuteReader();
                    if (res.Read())
                    {
                        string password = (string)res["password"];
                        if (password == pass.Text)
                        {
                            MessageBox.Show("Login Successful!");
                            this.Hide();
                            if (res.GetBoolean(4))
                            {
                                Form2 admin = new Form2();
                                admin.Show();

                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Wrong password or username.");
                    }
                    orcus.closeConnection();
                }
            }
        }

        private void Support_Click(object sender, EventArgs e)
        {
            Process.Start("http://orcus.cf");
        }

        private void user_Click(object sender, EventArgs e)
        {
            if(user.Text == "Username")
            {
                user.Clear();
            }
        }

        private void pass_Click(object sender, EventArgs e)
        {
            if(pass.Text == "Password")
            {
                pass.Clear();
                pass.PasswordChar = '*';
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pass_TextChanged(object sender, EventArgs e)
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
