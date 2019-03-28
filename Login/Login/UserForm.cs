using System;
using System.Data;
using System.Net.Http;
using System.Diagnostics;
using System.Windows.Forms;

namespace Login
{
    public partial class UserForm : Form
    {
        private LoginForm form;
        public UserForm(LoginForm form)
        {
            InitializeComponent();
            this.form = form;
        }
        private void onClose(object sender, FormClosingEventArgs e)
        {
            form.onReturn();
        }
    }
}
