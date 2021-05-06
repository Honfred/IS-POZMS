using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ИС_ПОЗМС
{
    public partial class Авторизация : Form
    {
        DB db = new DB();

        public Авторизация()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
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
                        Главная главная = new Главная { Owner = this };
                        db.closeConnection();
                        главная.Show();
                        this.Hide();
                    }
                    else { MessageBox.Show("Логин и/или пароль введены не правильно"); }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
