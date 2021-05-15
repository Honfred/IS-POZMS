using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ИС_ПОЗМС
{
    public partial class Добавление_записи : Form
    {
        DB db = new DB();
        int diff = 0;
        public Добавление_записи()
        {
            InitializeComponent();

            db.openConnection();

            Materials(); // Заполнения вариантами поля материалы
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
                
                comboBox4.Select(comboBox1.Text.Length, 0);

                string Sqlreq = $"SELECT fio FROM users WHERE fio LIKE '%{comboBox4.Text}%'";

                SqlCommand command = new SqlCommand(Sqlreq, db.GetConnection());
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    comboBox4.Items.Add(String.Format("{0}", reader[0]));
                }
                reader.Close();
                
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
                if ( comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && numericUpDown1.Value > 0)
                {
                    if (comboBox1.Text == "Пришло")
                    {
                        string SqlReqAdd = "insert into records " +
                                                                "(materials, org_in, date_time, in_out_count, in_out) " +
                                                                "values (" +
                                                                "(SELECT id FROM materials where name = '" + comboBox2.Text + "'), " +
                                                                "(SELECT id FROM organizations where name = '" + comboBox3.Text + "'), " +
                                                                "GETDATE(), '" + numericUpDown1.Value + "', 'Пришло');";

                        diff = Convert.ToInt32(numericUpDown1.Value);

                        string SqlReqUpdate = "UPDATE materials " +
                                                               $"SET count = count + {diff} " +
                                                               $"WHERE name = '{comboBox2.Text}' AND " +
                                                               $"organization = (SELECT id FROM organizations where name = '{comboBox3.Text}');";

                        SqlCommand command = new SqlCommand(SqlReqAdd, db.GetConnection());
                        command.ExecuteNonQuery();

                        SqlCommand command1 = new SqlCommand(SqlReqUpdate, db.GetConnection());
                        command1.ExecuteNonQuery();
                    }

                    if (comboBox1.Text == "Ушло")
                    {
                        diff = Convert.ToInt32(numericUpDown1.Value);

                        int tmp = 0;
                        string req = $"select count from materials where name = '{comboBox2.Text}'";
                        SqlCommand command = new SqlCommand(req, db.GetConnection());
                        tmp = Convert.ToInt32(command.ExecuteScalar());
                        if (numericUpDown1.Value <= tmp )
                        {
                            string SqlReqAdd = "insert into records " +
                                                                    "(materials, id_users, dep_to, date_time, in_out_count, in_out) " +
                                                                    "values (" +
                                                                    "(SELECT id FROM materials where name = '" + comboBox2.Text + "'), " +
                                                                    "(SELECT id FROM users WHERE fio = '" + comboBox4.Text + "'), " +
                                                                    "(SELECT id FROM departments where name = '" + comboBox3.Text + "'), " +
                                                                    "GETDATE(), '" + numericUpDown1.Value + "', 'Ушло');";



                            string SqlReqUpdate = "UPDATE materials " +
                                                                   $"SET count -= {diff} " +
                                                                   $"WHERE name = '{comboBox2.Text}' AND " +
                                                                   $"organization = (SELECT id FROM departments where name = '{comboBox3.Text}');";

                            SqlCommand command1 = new SqlCommand(SqlReqAdd, db.GetConnection());
                            command1.ExecuteNonQuery();

                            SqlCommand command2 = new SqlCommand(SqlReqUpdate, db.GetConnection());
                            command2.ExecuteNonQuery();
                        }         
                        else { MessageBox.Show("Вы пытаетесь затребовать со склада больше, чем есть на данный момент!"); }
                    }
                }
                else { MessageBox.Show("Не все поля заполнены, пожалуйста заполните все поля"); }

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

                    Users(); //Заполнения вариантами поля сортудник
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (comboBox1.Text == "Ушло")
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
}
