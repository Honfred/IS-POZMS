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
    public partial class Главная : Form
    {
        public Главная()
        {
            InitializeComponent();
        }

        private void Главная_Load(object sender, EventArgs e)
        {
            DB db = new DB();

            string MySqlreq = "SELECT * FROM `materials` where count < min_count;";
            string Sqlreq1 = "SELECT m.articul, m.name, m.count, m.min_count, o.name FROM materials AS m, organizations AS o WHERE m.organization = o.id;";

            db.openConnection();

            //Проверка материала на количество, если меньше то выдаст сообщение
            MySqlCommand command = new MySqlCommand(MySqlreq, db.GetConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
                MessageBox.Show("Одного или нескольких материалов не хватает!");

            //заполнение datagridwview для отображения таблицы материалы
            DataTable table1 = new DataTable();
            MySqlCommand command1 = new MySqlCommand(Sqlreq1, db.GetConnection());
            adapter.SelectCommand = command1;
            adapter.Fill(table1);

            dataGridView1.DataSource = table1;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                string MySqlreq = "SELECT u.fio, d.code, u.phone FROM users AS u, departments AS d WHERE d.code = '" + textBox1.Text + "' AND u.department = d.id;";

                

                DB db = new DB();

                MySqlCommand command = new MySqlCommand(MySqlreq, db.GetConnection());

                MySqlDataAdapter adapter = new MySqlDataAdapter();
                DataTable table = new DataTable();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                    dataGridView1.DataSource = table;
                else 
                    MessageBox.Show("Такого подразделения не существует или в нем никто не работает");
            }
        }
    }
}
