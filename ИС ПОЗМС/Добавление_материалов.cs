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

        private void Поставщики ()
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
            string SqlReqAdd = $"insert into materials (articul, name, count, min_count, organization) values ('{textBox1.Text.Trim()}', '{textBox2.Text.Trim()}', '0', '{numericUpDown1.Value}', (select id from organizations where name = '{comboBox3.Text}'));";
            
            SqlCommand command = new SqlCommand(SqlReqAdd, db.GetConnection());
            command.ExecuteNonQuery();

            this.Close();
            db.closeConnection();
        }
    }
}
