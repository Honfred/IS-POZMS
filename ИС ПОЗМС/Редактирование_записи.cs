using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ИС_ПОЗМС
{
    public partial class Редактирование_записи : Form
    {
        DB db = new DB();
        int diff = 0;
        public Редактирование_записи()
        {
            InitializeComponent();

            db.openConnection();

            Materials(); // Заполнения вариантами поля материалы

            numericUpDown1.Value = DataBank.Количество;
        }

        private void Materials()
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Пришло")
            {
                string SqlReqAdd = $"UPDATE records SET materials = (SELECT id FROM materials where name = '{comboBox2.Text}'), org_in = (SELECT id FROM organizations where name = '{comboBox3.Text}'), in_out_count = '{numericUpDown1.Value}' WHERE id = {DataBank.Номер};";
                diff = Convert.ToInt32(numericUpDown1.Value);
                diff = diff - DataBank.Количество;
                string SqlReqUpdate = $"UPDATE materials SET count = count + {diff} WHERE name = '{comboBox2.Text}' AND organization = (SELECT id FROM organizations where name = '{comboBox3.Text}');";

                SqlCommand command = new SqlCommand(SqlReqAdd, db.GetConnection());
                command.ExecuteNonQuery();

                SqlCommand command1 = new SqlCommand(SqlReqUpdate, db.GetConnection());
                command1.ExecuteNonQuery();

                this.Close();

                db.closeConnection();
            }

            if (comboBox1.Text == "Ушло")
            {
                string SqlReqAdd = $"UPDATE records SET materials = (SELECT id FROM materials where name = '{comboBox2.Text}'), dep_to = (SELECT id FROM departments where name = '{comboBox3.Text}'), in_out_count = '{numericUpDown1.Value}' WHERE id = {DataBank.Номер};";
                diff = Convert.ToInt32(numericUpDown1.Value);
                diff = DataBank.Количество - diff;
                string SqlReqUpdate = $"UPDATE materials SET count = count - {diff} WHERE name = '{comboBox2.Text}' AND organization = (SELECT id FROM departments where name = '{comboBox3.Text}');";

                SqlCommand command = new SqlCommand(SqlReqAdd, db.GetConnection());
                command.ExecuteNonQuery();

                SqlCommand command1 = new SqlCommand(SqlReqUpdate, db.GetConnection());
                command1.ExecuteNonQuery();

                this.Close();

                db.closeConnection();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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
    }
}
//разница в значениях при редактировании и после в update