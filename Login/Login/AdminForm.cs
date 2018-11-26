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
        public static bool AreTablesTheSame(DataTable tbl1, DataTable tbl2)
        {
            if (tbl1.Rows.Count != tbl2.Rows.Count || tbl1.Columns.Count != tbl2.Columns.Count)
                return false;

            try
            {
                for (int i = 0; i < tbl1.Rows.Count; i++)
                {
                    for (int c = 0; c < tbl1.Columns.Count; c++)
                    {
                        if (!Equals(tbl1.Rows[i][c], tbl2.Rows[i][c]))
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        private void loadGridView()
        {
            ConnectDB orcus = new ConnectDB("localhost", "test", "root", "rootpassword");
            DataTable source = new DataTable();
            orcus.fillDataTable("SELECT * FROM users", source);
            dataGridView1.DataSource = source;
        }
        private void onClose(object sender, EventArgs e)
        {
            ConnectDB orcus = new ConnectDB("localhost", "test", "root", "rootpassword");
            if (!AreTablesTheSame((DataTable)dataGridView1.DataSource, orcus.getDataTable("SELECT * FROM users")))
            {
                MessageBox.Show("Changes were made");
            }
            form.Show();
        }
    }
}
