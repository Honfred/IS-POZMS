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
            DB db = new DB();

            if (TextLogin.Text != "" && TextPass.Text != "")
            {
                string login = TextLogin.Text.Trim();
                string pass = TextPass.Text.Trim();
                string Sqlreq = "SELECT id FROM users WHERE fio = '" + login + "' AND password = ('" + pass + "');";

                db.openConnection();

                SqlCommand command = new SqlCommand(Sqlreq, db.GetConnection());
                int id = 0;

                id = Convert.ToInt32(command.ExecuteScalar());



                if (id != 0)
                {
                    Главная главная = new Главная();
                    db.closeConnection();
                    главная.Show();
                    this.Close();
                }
                else { MessageBox.Show("Логин и/или пароль введены не правильно"); }
            }
        }

        private void TextLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
