using System;
using System.Data;
using System.Net.Http;
using System.Diagnostics;
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
                            Hide();
                            pass.Text = "Password";
                            user.Text = "Username";
                            if (res.GetBoolean(4))
                            {
                                AdminForm admin = new AdminForm(this);
                                admin.Show();

                            }
                            else
                            {
                                UserForm user = new UserForm(this);
                                user.Show();
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

        private void userFocus(object sender, EventArgs e)
        {
            if(user.Text == "Username")
            {
                user.Clear();
            }
        }

        private void passFocus(object sender, EventArgs e)
        {
            if(pass.Text == "Password")
            {
                pass.Clear();
                pass.PasswordChar = '*';
            }
        }

        private void userEnter(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                pass.Focus();
            }
        }

        private void passEnter(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                submit.Focus();
                submission(submit, new EventArgs());
            }
        }
    }
}
