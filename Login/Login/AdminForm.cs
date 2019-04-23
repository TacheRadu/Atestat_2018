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

namespace Login
{
    public partial class AdminForm : Form
    {
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
            form.onReturn();
        }


        private void updateBtn_Click(object sender, EventArgs e)
        {

        }




        private async Task<DataGridView> GetGrid(string s)
        {
            DataGridView grid = new DataGridView();
            grid.UserDeletingRow += Grid_UserDeletingRow;
            grid.UserAddedRow += Grid_UserAddedRow;
            grid.CellValueChanged += Grid_CellValueChanged;
            grid.Dock = DockStyle.Fill;
            var source = JsonConvert.DeserializeObject<DataTable>(await client.GetStringAsync("http://localhost:3000/gettable?table=" + s));
            grid.DataSource = source;
            return grid;
        }

        private void Grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if(dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor != Color.LightGreen)
            {
                dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Khaki;
            }
        }

        private void Grid_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.DataGridView.Rows[e.Row.DataGridView.NewRowIndex - 1].DefaultCellStyle.BackColor = Color.LightGreen;
        }

        private void Grid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
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
