using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ИС_ПОЗМС
{
    public partial class Редактирование_записи : Form
    {
        DB db = new DB();
        int diff = 0;
        public Редактирование_записи()
        {
            try
            {
                InitializeComponent();

                db.openConnection();

                Materials(); // Заполнения вариантами поля материалы
                Users();

                numericUpDown1.Value = DataBank.Количество;
            }
            catch (System.ArgumentOutOfRangeException)
            {
                MessageBox.Show("Пожалуйста выберите запись");
            }
        }

        private void Materials()
        {
            try
            {
                string Sqlreq = "SELECT name from materials";

                SqlCommand command = new SqlCommand(Sqlreq, db.GetConnection());
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    comboBox2.Items.Add(String.Format("{0}", reader[0]));
                }
                reader.Close();

                comboBox1.Text = DataBank.Действие;
                comboBox2.Text = DataBank.Название_предмета;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Users()
        {
            try
            {
                string Sqlreq = "SELECT fio FROM users";

                SqlCommand command = new SqlCommand(Sqlreq, db.GetConnection());
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    comboBox4.Items.Add(String.Format("{0}", reader[0]));
                }
                reader.Close();

                comboBox4.Text = DataBank.ФИО;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == "Пришло")
                {
                    string SqlReqAdd = $"UPDATE records " +
                        $"SET materials = (SELECT id FROM materials where name = '{comboBox2.Text}'), " +
                        $"org_in = (SELECT id FROM organizations where name = '{comboBox3.Text}'), " +
                        $"in_out_count = '{numericUpDown1.Value}' WHERE id = {DataBank.Номер};";

                    diff = Convert.ToInt32(numericUpDown1.Value);
                    diff -= DataBank.Количество;
                    string SqlReqUpdate = $"UPDATE materials " +
                        $"SET count += {diff} " +
                        $"WHERE name = '{comboBox2.Text}' AND " +
                        $"organization = (SELECT id FROM organizations where name = '{comboBox3.Text}');";

                    SqlCommand command = new SqlCommand(SqlReqAdd, db.GetConnection());
                    command.ExecuteNonQuery();

                    SqlCommand command1 = new SqlCommand(SqlReqUpdate, db.GetConnection());
                    command1.ExecuteNonQuery();
                }

                if (comboBox1.Text == "Ушло")
                {
                    string SqlReqAdd = $"UPDATE records " +
                        $"SET materials = (SELECT id FROM materials where name = '{comboBox2.Text}'), " +
                        $"id_users = (SELECT id FROM users where fio = '{comboBox4.Text}'), " +
                        $"dep_to = (SELECT id FROM departments where name = '{comboBox3.Text}'), " +
                        $"in_out_count = '{numericUpDown1.Value}' WHERE id = {DataBank.Номер};";

                    diff = Convert.ToInt32(numericUpDown1.Value);
                    diff -= DataBank.Количество;
                    string SqlReqUpdate = $"UPDATE materials " +
                        $"SET count -= {diff} " +
                        $"WHERE name = '{comboBox2.Text}';";

                    SqlCommand command = new SqlCommand(SqlReqAdd, db.GetConnection());
                    command.ExecuteNonQuery();

                    SqlCommand command1 = new SqlCommand(SqlReqUpdate, db.GetConnection());
                    command1.ExecuteNonQuery();
                }
                db.closeConnection();
                Главная главная = (Главная)this.Owner;
                главная.Records();
                главная.Materials();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == "Пришло")
                {
                    comboBox3.Items.Clear();

                    string Sqlreq = "SELECT name FROM organizations";

                    SqlCommand command = new SqlCommand(Sqlreq, db.GetConnection());
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        comboBox3.Items.Add(String.Format("{0}", reader[0]));
                    }
                    reader.Close();

                    comboBox3.Text = DataBank.Поставщик;
                }

                if (comboBox1.Text == "Ушло")
                {
                    comboBox3.Items.Clear();
                    string Sqlreq = "SELECT name FROM departments";

                    SqlCommand command = new SqlCommand(Sqlreq, db.GetConnection());
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        comboBox3.Items.Add(String.Format("{0}", reader[0]));
                    }
                    reader.Close();

                    comboBox3.Text = DataBank.Подразделение;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (comboBox1.Text != "")
            {
                string s = comboBox4.Text.ToString();
                comboBox4.Items.Clear();
                comboBox4.Text = s;
            }
            Users();
        }
    }
}