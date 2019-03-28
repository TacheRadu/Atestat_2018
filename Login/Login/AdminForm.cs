using System;
using System.Data;
using System.Net.Http;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

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
            grid.Dock = DockStyle.Fill;
            var source = JsonConvert.DeserializeObject(await client.GetStringAsync("http://localhost:3000/gettable?table=" + s));
            grid.DataSource = source;
            return grid;
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
