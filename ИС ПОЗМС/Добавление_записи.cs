using MySql.Data.MySqlClient;
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
    public partial class Добавление_записи : Form
    {
        DataTable table = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter();
        DB db = new DB();


        public Добавление_записи()
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Пришло")
            {
                string Sqlreq = "insert into records (materials, org_in, date_time, in_out_count, in_out) values ((SELECT id FROM materials where name = '" + comboBox2.Text + "'), (SELECT id FROM organizations where name = '" + comboBox3.Text + "'), GETDATE(), 25, 'Пришло')";
                
                SqlCommand command = new SqlCommand(Sqlreq, db.GetConnection());
                command.ExecuteNonQuery();

                this.Hide();

                db.closeConnection();
            }

            if (comboBox1.Text == "Ушло")
            {
                string Sqlreq = "insert into records (materials, dep_to, date_time, in_out_count, in_out) values ((SELECT id FROM materials where name = '" + comboBox2.Text + "'), (SELECT id FROM departments where name = '" + comboBox3.Text + "'), GETDATE(), 25, 'Ушло')";
                
                SqlCommand command = new SqlCommand(Sqlreq, db.GetConnection());
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
            }
        }

        
    }
}
//comboBox1.Items.Add(String.Format("{0}", reader[0]));

//разница в значениях при редактировании и после в update
