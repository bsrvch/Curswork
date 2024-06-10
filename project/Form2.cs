using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ApiHelp;
using DB;
using System.Threading;
using System.Globalization;

namespace project
{
    public partial class Form2 : Form
    {
        public List<List<object>> data_travel = new List<List<object>>();
        public List<List<object>> data_name = new List<List<object>>();
        public double start_lat = 0;
        public double start_lng = 0;
        public string form_name;
        public Form2()
        {
            InitializeComponent();
            ApiH.GetAllCountries();
            ApiH.GetAllCities();
            AddColumns(dataGridView2, "Travels");
            data_travel = AddRows(dataGridView2, "Travels");
        }
        private void AddColumns(DataGridView dataGrid, string table_name)
        {
            string rec = $"SELECT COLUMN_NAME FROM Aviasales.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{table_name}'";
            data_name = DBUtils.request(rec);
            int widght = (dataGrid.Width - dataGrid.RowHeadersWidth) / data_name.Count;
            for (int i = 0; i < data_name.Count; i++)
            {
                if (i + 1 == data_name.Count)
                {
                    DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
                    column.Name = "Col" + i.ToString();
                    column.HeaderText = data_name[i][0].ToString();
                    column.Width = widght;
                    column.Tag = i;

                    dataGrid.Columns.Add(column);
                }
                else
                {
                    DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                    column.Name = "Col" + i.ToString();
                    column.HeaderText = data_name[i][0].ToString();
                    column.Width = widght;
                    column.Tag = i;
                    dataGrid.Columns.Add(column);
                }
            }
        }
        private List<List<object>> AddRows(DataGridView dataGrid, string tabel_name)
        {
            dataGrid.Rows.Clear();
            List<List<object>> data_1 = new List<List<object>>();
            string rec = $"SELECT * FROM {tabel_name}";
            data_1 = DBUtils.request(rec);
            foreach (var item in data_1)
            {
                string[] values = new string[item.Count];

                for (int i = 0; i < dataGrid.ColumnCount; i++)
                {
                    values[i] = item[i].ToString();
                }
                dataGrid.Rows.Add(values);
            }
            return data_1;
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex==1 || e.ColumnIndex==2)
            {
                Map map = new Map();
                
                foreach (var item in ApiH.cities)
                {

                    if (item.name == dataGridView2[e.ColumnIndex, e.RowIndex].Value.ToString())
                    {
                        CultureInfo temp_culture = Thread.CurrentThread.CurrentCulture;
                        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                        double lat = double.Parse(item.lat);
                        double lng = double.Parse(item.lng);
                        start_lat = lat;
                        start_lng = lng;
                        break;
                    }
                }
                map.vis = false;
                map.lat = start_lat;
                map.lng = start_lng;
                map.ShowDialog();
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            this.Text = form_name;
        }
    }
}
