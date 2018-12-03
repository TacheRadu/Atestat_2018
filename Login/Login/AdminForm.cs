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
        private DataTable source;
        private DataSet ds;
        private string query;
        public AdminForm(Form1 form)
        {
            query = "SELECT * FROM users";
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
            source = new DataTable();
            source.TableName = "users";
            orcus.fillDataTable(query, source);
            dataGridView1.DataSource = source;
        }
        private void onClose(object sender, FormClosingEventArgs e)
        {
            ConnectDB orcus = new ConnectDB("localhost", "test", "root", "rootpassword");
            if (!AreTablesTheSame(source, orcus.getDataTable("SELECT * FROM users")))
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to update the table?", "Changes were made!", MessageBoxButtons.YesNo);
                if(dialogResult == DialogResult.Yes)
                {
                    ds = new DataSet();
                    ds.Tables.Add(source);
                    orcus.remoteUpdate(query, ds);
                }
                else if(dialogResult == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            form.Show();
        }

        private void update(object sender, EventArgs e)
        {
            ConnectDB orcus = new ConnectDB("localhost", "test", "root", "rootpassword");
            ds = new DataSet();
            ds.Tables.Add(source);
            orcus.remoteUpdate(query, ds);
        }
    }
}
