using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ИС_ПОЗМС
{
    public partial class Добавление_материалов : Form
    {
        DB db = new DB();

        public Добавление_материалов()
        {
            InitializeComponent();
        }

        private void Добавление_материалов_Load(object sender, EventArgs e)
        {
            db.openConnection();

            Поставщики();
        }

        private void Поставщики()
        {
            try
            {
                string Sqlreq = "SELECT name FROM organizations";

                SqlCommand command = new SqlCommand(Sqlreq, db.GetConnection());
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    comboBox3.Items.Add(String.Format("{0}", reader[0]));
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (textBox1.Text != "" && textBox2.Text != "" && comboBox3.Text != "" && numericUpDown1.Value > 0)
                {
                    string SqlReqAdd = "INSERT INTO materials " +
                                                              "(articul, name, count, min_count, organization) " +
                                                             $"VALUES ('{textBox1.Text.Trim()}', " +
                                                             $"'{textBox2.Text.Trim()}', " +
                                                              "'0', " +
                                                             $"'{numericUpDown1.Value}', " +
                                                             $"(SELECT id FROM organizations WHERE name = '{comboBox3.Text}'));";

                    SqlCommand command = new SqlCommand(SqlReqAdd, db.GetConnection());
                    command.ExecuteNonQuery();

                    Главная главная = (Главная)this.Owner;
                    главная.Materials();

                    this.Close();

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
