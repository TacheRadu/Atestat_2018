using System;
using System.Data;
using System.Net.Http;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Net;
using System.IO;

namespace Login
{
    public partial class AdminForm : Form
    {
        private bool isChanged = false;
        private LoginForm form;
        private static readonly HttpClient client = new HttpClient();


        public AdminForm(LoginForm form)
        {
            InitializeComponent();
            BuildTabsNGrids();
            this.form = form;
        }


        private void onClose(object sender, FormClosingEventArgs e)
        {
            if (isChanged)
            {
                DialogResult res = MessageBox.Show("Au fost facute modificari la nivelul bazei de date. Le salvati?", "Updates", MessageBoxButtons.YesNoCancel);
                if (res == DialogResult.Cancel) {
                    e.Cancel = true;
                }
                else if(res == DialogResult.Yes)
                {
                    UpdateBtn_Click(new object(), new EventArgs());
                    form.onReturn();
                }
                else if(res == DialogResult.No)
                {
                    form.onReturn();
                }
            }
            else
            {
                form.onReturn();
            }
        }

        private void SendData(string table, DataTable rows, string action)
        {
            Console.WriteLine(JsonConvert.SerializeObject(rows));
            try
            {
                if (rows.Rows.Count > 0)
                {
                    HttpWebRequest httpReq = WebRequest.Create("http://localhost:3000/" + action + "?table=" + table) as HttpWebRequest;
                    httpReq.ContentType = "application/json";
                    httpReq.Method = "POST";
                    using (StreamWriter stream = new StreamWriter(httpReq.GetRequestStream()))
                    {
                        stream.Write(JsonConvert.SerializeObject(rows));
                        stream.Flush();
                        stream.Close();
                    }
                    HttpWebResponse response = (HttpWebResponse)httpReq.GetResponse();
                    using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        string result = streamReader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                Console.Write(ex.Message);
            }
        }
        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                DataGridView dgv = tab.Controls[0] as DataGridView;
                DataTable addedRows = ((DataTable)dgv.DataSource).Copy();
                addedRows.Rows.Clear();
                DataTable deletedRows = ((DataTable)dgv.DataSource).Copy();
                deletedRows.Rows.Clear();
                DataTable changedRows = ((DataTable)dgv.DataSource).Copy();
                changedRows.Rows.Clear();
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    // Okay, what's coming is ugly. I needed the DataGridViewRow features to get the background color.
                    // But I actually needed to add a DataRow item, so this: ((DataRowView)row.DataBoundItem).Row is for casting
                    // Also, you can't really append the same row to more than one table, so you just pass the values of the item to the Add function, for it to construct its own row..
                    if (row.DefaultCellStyle.BackColor == Color.LightCoral)
                    {
                        deletedRows.Rows.Add(((DataRowView)row.DataBoundItem).Row.ItemArray);
                    }
                    else if (row.DefaultCellStyle.BackColor == Color.LightGreen)
                    {
                        addedRows.Rows.Add(((DataRowView)row.DataBoundItem).Row.ItemArray);
                    }
                    else if (row.DefaultCellStyle.BackColor == Color.Khaki)
                    {
                        changedRows.Rows.Add(((DataRowView)row.DataBoundItem).Row.ItemArray);
                    }
                    //Sorry for this
                }
                SendData(tab.Name, addedRows, "add");
                SendData(tab.Name, deletedRows, "delete");
                SendData(tab.Name, changedRows, "change");
            }
        }




        private async Task<DataGridView> GetGrid(string s)
        {
            DataGridView grid = new DataGridView();
            grid.UserDeletingRow += Grid_UserDeletingRow;
            grid.UserAddedRow += Grid_UserAddedRow;
            grid.CellValueChanged += Grid_CellValueChanged;
            grid.Dock = DockStyle.Fill;
            var source = JsonConvert.DeserializeObject<DataTable>(await client.GetStringAsync("http://localhost:3000/gettable?table=" + s));
            source.Columns["id"].ReadOnly = true;
            try
            {
                DataTable dtCloned = source.Clone();
                dtCloned.Columns["is_admin"].DataType = typeof(Boolean);
                foreach (DataRow row in source.Rows)
                {
                    dtCloned.ImportRow(row);
                }
                grid.DataSource = dtCloned;
            }
            catch
            {
                grid.DataSource = source;
            }
            return grid;
        }

        private void Grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            isChanged = true;
            DataGridView dgv = sender as DataGridView;
            if(dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor != Color.LightGreen)
            {
                dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Khaki;
            }
        }

        private void Grid_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            isChanged = true;
            e.Row.DataGridView.Rows[e.Row.DataGridView.NewRowIndex - 1].DefaultCellStyle.BackColor = Color.LightGreen;
        }

        private void Grid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            isChanged = true;
            if (e.Row.DefaultCellStyle.BackColor != Color.LightGreen)
            {
                e.Row.DefaultCellStyle.BackColor = Color.LightCoral;
                e.Cancel = true;
            }

        }

        private async Task<TabPage> getTab(string s)
        {
            TabPage tp = new TabPage(s);
            tp.Name = s;
            tp.Controls.Add(await GetGrid(s));
            return tp;
        }


        private async void BuildTabsNGrids()
        {
            try{
                var JsonString = await client.GetStringAsync("http://localhost:3000/tables");
                var tables = JsonConvert.DeserializeObject<List<String>>(JsonString);
                foreach(var i in tables)
                {
                    tabControl1.TabPages.Add(await getTab(i));
                }
            }
            catch(HttpRequestException ex)
            {
                throw ex;
            }
        }
    }    
}
