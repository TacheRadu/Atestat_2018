using System;
using System.Data;
using System.Net.Http;
using System.Diagnostics;
using System.Windows.Forms;

namespace Login
{
    public partial class LoginForm : Form
    {

        public LoginForm()
        {
            InitializeComponent();

        }

        private static readonly HttpClient client = new HttpClient();

        private async void submission(object sender, EventArgs e)
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
                try
                {
                    string res = await client.GetStringAsync("http://localhost:3000/login?name=" + user.Text + "&password=" + pass.Text);
                    if(Int32.Parse(res) >= 0)
                    {
                        Hide();
                        if (res == "1")
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
                    else
                    {
                        MessageBox.Show("Wrong password or username.");
                    }
                }
                catch
                {
                    MessageBox.Show("Server is down.");
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
        public void onReturn()
        {
            user.Text = "Username";
            pass.Text = "Password";
            Show();
        }
    }
}
