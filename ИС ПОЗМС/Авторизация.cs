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

namespace ИС_ПОЗМС
{
    public partial class Авторизация : Form
    {
        const string conn = "Data Source=LAPTOP-2J26AE8V;Initial Catalog=Склад;Integrated Security=True";

        public Авторизация()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (TextLogin.Text != "" && TextPass.Text != "")
            {
                string login = TextLogin.Text;
                string pass = TextPass.Text;
                string sqlreq = "SELECT * FROM users WHERE id = '" + login + "' AND password = '" + pass + "';";

                SqlConnection connection = new SqlConnection(conn);

                connection.Open();

                SqlCommand command = new SqlCommand(sqlreq, connection);

                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    Form1 form1 = new Form1();
                    connection.Close();
                    form1.Show();
                    this.Hide();
                }
                else { MessageBox.Show("Логин и/или пароль введены не правильно"); }
                               
            }
        }
    }
}
