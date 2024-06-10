using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DB;

namespace project
{
    public partial class Sign : Form
    {
        static Boolean user_root;
        Boolean is_valid = true;
        public Sign()
        {
            InitializeComponent();
            DBUtils.connect();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string rec = "SELECT * FROM Users";
            List<List<object>> data = DBUtils.request(rec);
            if (is_valid)
            {
                Exep.Text = "";
                foreach(var item in data)
                {
                    if (item[1].ToString() == textBoxName.Text && item[2].ToString()== textBoxPas.Text)
                    {
                        Exep.Text = "Вход...";
                        user_root = (bool)item[3];
                        if (user_root)
                        {
                            Form3 newForm = new Form3();
                            newForm.form_name = item[1].ToString();
                            this.Hide();
                            newForm.ShowDialog();
                        }
                        else
                        {
                            Form2 newForm = new Form2();
                            newForm.form_name = item[1].ToString();
                            this.Hide();
                            newForm.ShowDialog();
                        }
                        this.Show();
                        Application.Restart();
                        break;
                    }
                    else if(item[1].ToString() == textBoxName.Text && item[2].ToString() != textBoxPas.Text)
                    {
                        Exep.Text = "Не правильный пароль";
                        textBoxPas.Text = "";
                        break;
                    }
                    else
                    {
                        Exep.Text = "Такого аккаунта не существует";
                        
                    }
                }
                textBoxName.Text = "";
                textBoxPas.Text = "";
            }
            else
            {
                Exep.Text = "Заполните все поля";
            }

        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            textBoxName.BackColor = Color.Green;
            labelName.Text = "";
            is_valid = true;
            if (textBoxName.TextLength < 2 || textBoxName.Text.Contains(')'))
            {
                labelName.Text = "Минимальное количество символов 2";
                is_valid = false;
                textBoxName.BackColor = Color.Red;
            }
        }

        private void textBoxPas_TextChanged(object sender, EventArgs e)
        {
            textBoxPas.BackColor = Color.Green;
            labelPas.Text = "";
            if (textBoxPas.TextLength < 4 || textBoxPas.Text.Contains(')'))
            {
                labelPas.Text = "Минимальное количество символов 4";
                textBoxPas.BackColor = Color.Red;
            }
        }

        private void reg_Click(object sender, EventArgs e)
        {
            string rec = "SELECT * FROM Users";
            List<List<object>> data = DBUtils.request(rec);
            Boolean isreg = false;
            if (textBoxName.TextLength > 0 && textBoxPas.TextLength > 3)
            {
                Exep.Text = "";
                foreach (var item in data)
                {
                    if (item[1].ToString() == textBoxName.Text)
                    {
                        isreg = true;
                        Exep.Text = "Аккаунт с таким имененм уже существует";
                        textBoxName.Text = "";
                        textBoxPas.Text = "";
                        break;
                    }
                }
            }
            else
            {
                Exep.Text = "Заполните все поля";
            }
            if (!isreg)
            {
                rec = $"INSERT INTO Users (Name,Password,Root) VALUES('{textBoxName.Text}', '{textBoxPas.Text}','0') SELECT * FROM Users";
                DBUtils.request(rec);
                Exep.Text = "Вы успешно зарегестрированы! Войдите в аккаунт";
                textBoxName.Text = "";
                textBoxPas.Text = "";
            }
        }
    }
}
