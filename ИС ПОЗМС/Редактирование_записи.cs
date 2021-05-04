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

        public Редактирование_записи()
        {
            InitializeComponent();

            db.openConnection();

            Materials(); // Заполнения вариантами поля материалы
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

                comboBox3.Text = DataBank.Код_подразделения;
            }
        }
    }
}
//разница в значениях при редактировании и после в update