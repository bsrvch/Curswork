using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ApiHelp;
using CityDB;
using DB;
using System.Threading;
using System.Globalization;
using Gmap_Markers;


namespace project
{
    public partial class Form3 : Form
    {
        public List<List<object>> data_user = new List<List<object>>();
        public List<List<object>> data_travel = new List<List<object>>();
        public List<List<object>> data_name = new List<List<object>>();
        public List<City> city = new List<City>();
        public string form_name;
        public Boolean full = false;
        public Boolean is_valid = true;
        public double start_lat = 0;
        public double start_lng = 0;
        public double finish_lat = 0;
        public double finish_lng = 0;

        public Form3()
        {
            InitializeComponent();
            ApiH.GetAllCountries();
            ApiH.GetAllCities();
            AddColumns(dataGridView1, "Users");
            AddColumns(dataGridView2, "Travels");
            data_user = AddRows(dataGridView1, "Users");
            data_travel = AddRows(dataGridView2, "Travels");
            TravelsStartTime.CustomFormat = "dd:MM:yyyy HH:mm";
            TravelsFinishTime.CustomFormat = "dd:MM:yyyy HH:mm";
        }
        private void AddColumns(DataGridView dataGrid, string table_name)
        {
            string rec = $"SELECT COLUMN_NAME FROM Aviasales.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{table_name}'";
            data_name = DBUtils.request(rec);
            int widght = (dataGrid.Width- dataGrid.RowHeadersWidth) / data_name.Count;
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
            full = false;
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
            red.Checked = false;
            full = true;
            return data_1;
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            this.Text = form_name;
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (red.Checked && full)
            {
                string rec = "SELECT COLUMN_NAME FROM Aviasales.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Users'";
                data_name = DBUtils.request(rec);
                string value = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
                if (value == "True")
                {
                    value = "1";
                }
                else if(value=="False")
                {
                    value = "0";
                }
                rec = $"UPDATE Users SET {data_name[e.ColumnIndex][0]} = '{value}' WHERE id = {data_user[e.RowIndex][0]}";
                DBUtils.request(rec);
            }
            else if(full)
            {
                dataGridView1[e.ColumnIndex,e.RowIndex].Value = data_user[e.RowIndex][e.ColumnIndex];
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                exepp.Visible = false;
                for(int i = 0; i< dataGridView1.SelectedRows.Count; i++)
                {
                    if (dataGridView1.SelectedRows[i].Cells[0].Value != null)
                    {
                        string rec = $"DELETE Users WHERE id = {dataGridView1.SelectedRows[i].Cells[0].Value}";
                        DBUtils.request(rec);
                    }
                }
                data_user = AddRows(dataGridView1, "Users");
            }
            else
            {
                exepp.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (groupBox1.Visible)
            {
                groupBox1.Visible = false;
                exeppp.Text = "";
            }
            else
            {
                groupBox1.Visible = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (is_valid)
            {
                string rec = "SELECT COLUMN_NAME FROM Aviasales.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Users'";
                data_name = DBUtils.request(rec);
                Boolean isreg = false;
                int isroot = 0;
                if (UsersName.TextLength > 0 && UsersPassword.TextLength > 3)
                {
                    exeppp.Text = "";
                    foreach (var item in data_user)
                    {
                        if (item[1].ToString() == UsersName.Text)
                        {
                            isreg = true;
                            exeppp.Text = "Аккаунт с таким имененм уже существует";
                            UsersName.Text = "";
                            UsersPassword.Text = "";
                            break;
                        }
                    }
                }
                else
                {
                    exeppp.Text = "Заполните все поля";
                }
                if (!isreg)
                {
                    if (UsersRoot.Checked)
                    {
                        isroot = 1;
                    }
                    else
                    {
                        isroot = 0;
                    }
                    rec = "INSERT INTO Users (";
                    string rec2 = " VALUES(";
                    for(int i = 1; i < data_name.Count; i++)
                    {
                        rec += $"{data_name[i][0]},";
                        if (i + 1 == data_name.Count)
                            rec2 += $"'{isroot}',";
                        else
                            rec2 += $"'{groupBox1.Controls["Users" + data_name[i][0]].Text}',";
                        
                    }
                    rec = rec.Remove(rec.Length - 1)+")";
                    rec2 = rec2.Remove(rec2.Length - 1)+");";
                    rec = rec + rec2;
                    DBUtils.request(rec);
                    exeppp.Text = "Пользователь добавлен";
                    UsersName.Text = "";
                    UsersPassword.Text = "";
                    UsersRoot.Checked = false;
                    data_user = AddRows(dataGridView1, "Users");
                }
            }
        }

        private void textBoxName_TextChanged_1(object sender, EventArgs e)
        {
            UsersName.BackColor = Color.Green;
            is_valid = true;
            if (UsersName.TextLength < 2 || UsersName.Text.Contains(')'))
            {
                is_valid = false;
                UsersName.BackColor = Color.Red;
            }
        }

        private void textBoxPas_TextChanged_1(object sender, EventArgs e)
        {
            UsersPassword.BackColor = Color.Green;
            if (UsersPassword.TextLength < 4 || UsersPassword.Text.Contains(')'))
            {
                UsersPassword.BackColor = Color.Red;
            }
        }

        private void CountryText_TextChanged(object sender, EventArgs e)
        {
            AutoHintsCountry(listBoxCountry, CountryText);
        }
        private void TravelsStartLocation_TextChanged(object sender, EventArgs e)
        {
            AutoHintsCity(listBoxCity, TravelsStartLocation);
        }
        private void listBoxCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxCountry.SelectedIndex > -1)
            {
                city.Clear();
                CountryText.Text = listBoxCountry.Items[listBoxCountry.SelectedIndex].ToString();
                listBoxCountry.Visible = false;
                TravelsStartLocation.Visible = true;
                string code = "";
                foreach (var item in ApiH.countries)
                {
                    if (item.name == CountryText.Text)
                        code = item.code;
                }
                foreach (var item in ApiH.cities)
                {
                    if (code == item.country)
                        city.Add(item);
                }
            }
            
        }

        private void CityBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxCity.SelectedIndex > -1)
            {
                TravelsStartLocation.Text = listBoxCity.Items[listBoxCity.SelectedIndex].ToString();
                listBoxCity.Visible = false;
                foreach (var item in city)
                {
                    if (item.name == TravelsStartLocation.Text)
                    {
                        CultureInfo temp_culture = Thread.CurrentThread.CurrentCulture;
                        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                        double lat = double.Parse(item.lat);
                        double lng = double.Parse(item.lng);
                        start_lat = lat;
                        start_lng = lng;
                    }
                }
            }           
        }
        private void CountryText1_TextChanged(object sender, EventArgs e)
        {
            AutoHintsCountry(listBoxCountry1, CountryText1);
        }
        private void TravelsFinishLocation_TextChanged(object sender, EventArgs e)
        {
            AutoHintsCity(listBoxCity1, TravelsFinishLocation);
        }

        private void listBoxCountry1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxCountry1.SelectedIndex > -1)
            {
                city.Clear();
                CountryText1.Text = listBoxCountry1.Items[listBoxCountry1.SelectedIndex].ToString();
                listBoxCountry1.Visible = false;
                TravelsFinishLocation.Visible = true;
                string code = "";
                foreach (var item in ApiH.countries)
                {
                    if (item.name == CountryText1.Text)
                        code = item.code;
                }
                foreach (var item in ApiH.cities)
                {
                    if (code == item.country)
                        city.Add(item);
                }
            }
           
        }

        private void listBoxCity1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxCity1.SelectedIndex > -1)
            {
                TravelsFinishLocation.Text = listBoxCity1.Items[listBoxCity1.SelectedIndex].ToString();
                listBoxCity1.Visible = false;
                foreach (var item in city)
                {
                    if (item.name == TravelsFinishLocation.Text)
                    {
                        CultureInfo temp_culture = Thread.CurrentThread.CurrentCulture;
                        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                        double lat = double.Parse(item.lat);
                        double lng = double.Parse(item.lng);
                        finish_lat = lat;
                        finish_lng = lng;
                    }
                }
            }

            
        }
        private void AutoHintsCountry(ListBox listBox, TextBox textBox)
        {
            listBox.Visible = true;
            listBox.Items.Clear();
            foreach (var item in ApiH.countries)
            {
                Boolean y = true;
                for (int i = 0; i < textBox.Text.Length; i++)
                {
                    if (item.name.Length > i)
                    {
                        if (textBox.Text[i] != item.name[i])
                            y = false;
                    }
                    else
                    {
                        y = false;
                    }
                }
                if (y)
                {
                    listBox.Items.Add(item.name);
                }
            }
        }
        private void AutoHintsCity(ListBox listBox, TextBox textBox)
        {
            listBox.Visible = true;
            listBox.Items.Clear();
            foreach (var item in city)
            {
                Boolean y = true;
                for (int i = 0; i < textBox.Text.Length; i++)
                {
                    if (item.name.Length > i)
                    {
                        if (textBox.Text[i] != item.name[i])
                            y = false;
                    }
                    else
                    {
                        y = false;
                    }
                }
                if (y)
                {
                    listBox.Items.Add(item.name);
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Map map = new Map();
            map.lat = start_lat;
            map.lng = start_lng;
            map.ShowDialog();
            if(GMapMarkerImage.City != "")
            {
                string code = "";
                foreach(var item in ApiH.cities)
                {
                    if(item.name== GMapMarkerImage.City)
                    {
                        code = item.country;
                        CultureInfo temp_culture = Thread.CurrentThread.CurrentCulture;
                        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                        double lat = double.Parse(item.lat);
                        double lng = double.Parse(item.lng);
                        start_lat = lat;
                        start_lng = lng;
                        break;
                    }
                }
                foreach (var item in ApiH.countries)
                {
                    if (item.code == code)
                    {
                        CountryText.Text = item.name;
                        listBoxCountry.Visible = false;
                    }
                }
                TravelsStartLocation.Visible = true;
                TravelsStartLocation.Text = GMapMarkerImage.City;
                listBoxCity.Visible = false;
            }
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Map map = new Map();
            map.lat = finish_lat;
            map.lng = finish_lng;
            map.ShowDialog();
            if (GMapMarkerImage.City != "")
            {
                string code = "";
                foreach(var item in ApiH.cities)
                {
                    if(item.name== GMapMarkerImage.City)
                    {
                        code = item.country;
                        CultureInfo temp_culture = Thread.CurrentThread.CurrentCulture;
                        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                        double lat = double.Parse(item.lat);
                        double lng = double.Parse(item.lng);
                        finish_lat = lat;
                        finish_lng = lng;
                        break;
                    }
                }
                foreach(var item in ApiH.countries)
                {
                    if (item.code == code)
                    {
                        CountryText1.Text = item.name;
                        listBoxCountry1.Visible = false;
                    }
                }
                TravelsFinishLocation.Visible = true;
                TravelsFinishLocation.Text = GMapMarkerImage.City;
                listBoxCity1.Visible = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                exepp.Visible = false;
                for (int i = 0; i < dataGridView2.SelectedRows.Count; i++)
                {
                    if (dataGridView2.SelectedRows[i].Cells[0].Value != null)
                    {
                        string rec = $"DELETE Travels WHERE id = {dataGridView2.SelectedRows[i].Cells[0].Value}";
                        DBUtils.request(rec);
                    }
                }
                data_travel = AddRows(dataGridView2, "Travels");
            }
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (red1.Checked && full)
            {
                string rec = "SELECT COLUMN_NAME FROM Aviasales.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Travels'";
                data_name = DBUtils.request(rec);
                string value = dataGridView2[e.ColumnIndex, e.RowIndex].Value.ToString();
                if (value == "True")
                {
                    value = "1";
                }
                else if (value == "False")
                {
                    value = "0";
                }
                rec = $"UPDATE Travels SET {data_name[e.ColumnIndex][0]} = '{value}' WHERE id = {data_travel[e.RowIndex][0]}";
                DBUtils.request(rec);
            }
            else if (full)
            {
                dataGridView2[e.ColumnIndex, e.RowIndex].Value = data_travel[e.RowIndex][e.ColumnIndex];
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int isvip;
            if (checkBox1.Checked)
            {
                isvip = 1;
            }
            else
            {
                isvip = 0;
            }
            string rec = "SELECT COLUMN_NAME FROM Aviasales.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Travels'";
            data_name = DBUtils.request(rec);
            rec = "INSERT INTO Travels (";
            string rec2 = " VALUES(";
            for (int i = 1; i < data_name.Count; i++)
            {
                rec += $"{data_name[i][0]},";
                if (i + 1 == data_name.Count)
                    rec2 += $"'{isvip}',";
                else
                    rec2 += $"'{groupBox2.Controls["Travels" + data_name[i][0]].Text}',";

            }
            rec = rec.Remove(rec.Length - 1) + ")";
            rec2 = rec2.Remove(rec2.Length - 1) + ");";
            rec = rec + rec2;
            DBUtils.request(rec);
            data_travel = AddRows(dataGridView2, "Travels");
        }
    }
  

}
