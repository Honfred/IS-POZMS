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
        //const string conn = "Data Source=LAPTOP-2J26AE8V;Initial Catalog=Склад;Integrated Security=True";

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

                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
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
