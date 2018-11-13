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

namespace Login
{
    public partial class AdminForm : Form
    {
        private Form1 form;
        public AdminForm(Form1 form)
        {
            InitializeComponent();
            loadGridView();
            this.form = form;
        }

        private void loadGridView()
        {
            ConnectDB orcus = new ConnectDB("localhost", "test", 
"root", "rootpassword");
            MySqlConnection conn = orcus.getConnection();
            MySqlCommand cmd = new MySqlCommand("SHOW TABLES;", conn);
            MySqlDataReader dr = cmd.ExecuteReader();
            dataGridView1.DataSource = dr;
            conn.Close();
        }
        private void onClose(object sender, EventArgs e)
        {
            form.Show();
        }
    }
}
