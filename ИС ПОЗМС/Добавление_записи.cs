using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ИС_ПОЗМС
{
    public partial class Добавление_записи : Form
    {
        DataTable table = new DataTable();
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        DB db = new DB();


        public Добавление_записи()
        {
            InitializeComponent();

            db.openConnection();

            Materials(); // Заполнения вариантами поля материалы

            
        }

        private void Materials()
        {
            string MySqlreq = "SELECT name from materials";

            MySqlCommand command = new MySqlCommand(MySqlreq, db.GetConnection());
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                comboBox2.Items.Add(String.Format("{0}", reader[0]));
            }
            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Пришло")
            {
                string Sqlreq = "insert into records (materials, org_in, date_time, in_out_count, in_out) values ((SELECT id FROM materials where name = '" + comboBox2.Text + "'), (SELECT id FROM organizations where name = '" + comboBox3.Text + "'), now(), 25, 'Пришло')";
                
                MySqlCommand command = new MySqlCommand(Sqlreq, db.GetConnection());
                command.ExecuteNonQuery();

                this.Hide();

                db.closeConnection();
            }

            if (comboBox1.Text == "Ушло")
            {
                string Sqlreq = "insert into records (materials, dep_to, date_time, in_out_count, in_out) values ((SELECT id FROM materials where name = '" + comboBox2.Text + "'), (SELECT id FROM departments where name = '" + comboBox3.Text + "'), now(), 25, 'Ушло')";
                
                MySqlCommand command = new MySqlCommand(Sqlreq, db.GetConnection());
                command.ExecuteNonQuery();

                this.Hide();

                db.closeConnection();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Пришло")
            {
                comboBox3.Items.Clear();

                string MySqlreq = "SELECT name FROM organizations";

                MySqlCommand command = new MySqlCommand(MySqlreq, db.GetConnection());
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    comboBox3.Items.Add(String.Format("{0}", reader[0]));
                }
                reader.Close();
            }

            if (comboBox1.Text == "Ушло")
            {
                comboBox3.Items.Clear();
                string MySqlreq = "SELECT name FROM departments";

                MySqlCommand command = new MySqlCommand(MySqlreq, db.GetConnection());
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    comboBox3.Items.Add(String.Format("{0}", reader[0]));
                }
                reader.Close();
            }
        }

        
    }
}
//comboBox1.Items.Add(String.Format("{0}", reader[0]));

//разница в значениях при редактировании и после в update
