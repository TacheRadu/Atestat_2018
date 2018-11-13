using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class UserForm : Form
    {
        private Form1 form;
        public UserForm(Form1 form)
        {
            InitializeComponent();
            this.form = form;
        }
        private void onClose(object sender, FormClosingEventArgs e)
        {
            form.Show();
        }
    }
}
