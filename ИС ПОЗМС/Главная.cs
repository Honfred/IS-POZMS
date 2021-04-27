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
    public partial class Главная : Form
    {
        public Главная()
        {
            InitializeComponent();
        }

        private void Главная_Load(object sender, EventArgs e)
        {
            //Материалы


            DB db = new DB();

            bool min_value = false;
            string Sqlreq = "SELECT * FROM materials where count < min_count;";
            string Sqlreq1 = "SELECT m.articul, m.name, m.count, m.min_count, o.name FROM materials AS m, organizations AS o WHERE m.organization = o.id;";

            db.openConnection();

            //Проверка материала на количество, если меньше то выдаст сообщение
            SqlCommand command = new SqlCommand(Sqlreq, db.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Одного или нескольких материалов не хватает!");
                min_value = true;
            }
                
                

            //заполнение datagridwview для отображения таблицы материалы
            DataTable table1 = new DataTable();
            SqlCommand command1 = new SqlCommand(Sqlreq1, db.GetConnection());
            adapter.SelectCommand = command1;
            adapter.Fill(table1);

            dataGridView1.DataSource = table1;

            if (min_value == true)
                button1.Visible = true;
            else
                button1.Visible = false;

            dataGridView1.Columns[0].HeaderText = "Артикул";
            dataGridView1.Columns[1].HeaderText = "Наименование";
            dataGridView1.Columns[2].HeaderText = "Количество";
            dataGridView1.Columns[3].HeaderText = "Мин.количество";
            dataGridView1.Columns[4].HeaderText = "Название организации";
            /////////////////////////////////////////////////////////////////////////////////////////////////
            ///


            //Поставщики
            

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                string Sqlreq = "SELECT u.fio, d.code, u.phone FROM users AS u, departments AS d WHERE d.code = '" + textBox1.Text + "' AND u.department = d.id;";

                DB db = new DB();

                SqlCommand command = new SqlCommand(Sqlreq, db.GetConnection());

                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                    dataGridView1.DataSource = table;
                else 
                    MessageBox.Show("Такого подразделения не существует или в нем никто не работает");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Sqlreq = "SELECT m.articul, m.name, m.count, m.min_count, o.name FROM materials AS m, organizations AS o WHERE m.organization = o.id and count < min_count;";

            DB db = new DB();

            SqlCommand command = new SqlCommand(Sqlreq, db.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            dataGridView1.DataSource = table;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string Sqlreq = "SELECT m.articul, m.name, m.count, m.min_count, o.name FROM materials AS m, organizations AS o WHERE m.organization = o.id and m.name like '%" + textBox1.Text + "%';";

            DB db = new DB();

            SqlCommand command = new SqlCommand(Sqlreq, db.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            dataGridView1.DataSource = table;
        }
    }
}
