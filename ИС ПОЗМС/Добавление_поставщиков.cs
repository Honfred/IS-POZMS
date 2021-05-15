using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ИС_ПОЗМС
{
    public partial class Добавление_поставщиков : Form
    {
        public Добавление_поставщиков()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            Главная главная = (Главная)this.Owner;

            try
            {
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    db.openConnection();

                    string MySqlreq = "INSERT INTO organizations (name, phone) VALUES ('" + textBox1.Text + "', '" + textBox2.Text + "');";
                    SqlCommand command = new SqlCommand(MySqlreq, db.GetConnection());
                    command.ExecuteNonQuery();

                    главная.Organizations();
                    this.Hide();

                    db.closeConnection();
                }
                else { MessageBox.Show("Не все поля заполнены, пожалуйста заполните все поля"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
