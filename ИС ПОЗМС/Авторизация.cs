using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace ИС_ПОЗМС
{
    public partial class Авторизация : Form
    {

        public Авторизация()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (TextLogin.Text != "" && TextPass.Text != "")
            {
                string login = TextLogin.Text.Trim();
                string pass = TextPass.Text.Trim();
                string sqlreq = "SELECT * FROM users WHERE fio = '" + login + "' AND password = '" + pass + "';";

                DB db = new DB();

                db.openConnection();

                SqlCommand command = new SqlCommand(sqlreq, db.GetConnection());

                if (command.ToString() != "")
                {
                    Главная главная = new Главная();
                    db.closeConnection();
                    главная.Show();
                    this.Hide();
                }
                else { MessageBox.Show("Логин и/или пароль введены не правильно"); }
            }
        }

        private void TextLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
