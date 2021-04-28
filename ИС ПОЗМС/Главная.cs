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
                button1.Visible = true;
            }



            //Материалы
            DataTable table1 = new DataTable();
            SqlCommand command1 = new SqlCommand(Sqlreq1, db.GetConnection());
            adapter.SelectCommand = command1;
            adapter.Fill(table1);

            dataGridView1.DataSource = table1;

            dataGridView1.Columns[0].HeaderText = "Артикул";
            dataGridView1.Columns[1].HeaderText = "Наименование";
            dataGridView1.Columns[2].HeaderText = "Количество";
            dataGridView1.Columns[3].HeaderText = "Мин.количество";
            dataGridView1.Columns[4].HeaderText = "Название организации";



            /////////////////////////////////////////////////////////////////////////////////////////////////



            //Поставщики
            string Sqlreq2 = "SELECT name, phone FROM organizations";
            DataTable table2 = new DataTable();
            SqlCommand command2 = new SqlCommand(Sqlreq2, db.GetConnection());
            adapter.SelectCommand = command2;
            adapter.Fill(table2);
            dataGridView2.DataSource = table2;

            dataGridView2.Columns[0].HeaderText = "Название организации";
            dataGridView2.Columns[1].HeaderText = "Номер телефона";



            /////////////////////////////////////////////////////////////////////////////////////////////////



            //Подразделения
            string Sqlreq3 = "SELECT code, name, phone FROM departments";
            DataTable table3 = new DataTable();
            SqlCommand command3 = new SqlCommand(Sqlreq3, db.GetConnection());
            adapter.SelectCommand = command3;
            adapter.Fill(table3);
            dataGridView4.DataSource = table3;
            dataGridView4.Columns[0].HeaderText = "Код";
            dataGridView4.Columns[1].HeaderText = "Название";
            dataGridView4.Columns[2].HeaderText = "Номер телефона";
            dataGridView4.Columns[0].Width = 50;
            dataGridView4.Columns[1].Width = 225;
            dataGridView4.Columns[2].Width = 75;



            /////////////////////////////////////////////////////////////////////////////////////////////////



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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            string Sqlreq = "SELECT name, phone FROM organizations WHERE name like '%" + textBox2.Text + "%';";

            DB db = new DB();

            SqlCommand command = new SqlCommand(Sqlreq, db.GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            dataGridView2.DataSource = table;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            DB db = new DB();
            
            if (comboBox1.Text == "Пришло")
            {
                string Sqlreq = "SELECT r.id, m.name, o.name, r.date_time, r.in_out_count, r.in_out FROM records AS r, materials AS m, organizations AS o WHERE r.materials = m.id AND r.org_in = o.id AND m.name LIKE '%" + textBox3.Text + "%';";

                SqlCommand command = new SqlCommand(Sqlreq, db.GetConnection());
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();

                adapter.SelectCommand = command;
                adapter.Fill(table);
                dataGridView3.DataSource = table;

                dataGridView3.Columns[0].HeaderText = "Номер";
                dataGridView3.Columns[1].HeaderText = "Название предмета";
                dataGridView3.Columns[2].HeaderText = "Поставщик";
                dataGridView3.Columns[3].HeaderText = "Дата и время";
                dataGridView3.Columns[4].HeaderText = "Количество";
                dataGridView3.Columns[5].HeaderText = "Действие";
                
            }

            if (comboBox1.Text == "Ушло")
            {
                string Sqlreq = "SELECT r.id, m.name, d.code, r.date_time, r.in_out_count, r.in_out FROM records AS r, materials AS m, departments AS d WHERE r.materials = m.id AND r.dep_to = d.id AND m.name LIKE '%" + textBox3.Text + "%';";

                SqlCommand command = new SqlCommand(Sqlreq, db.GetConnection());
                
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);
                dataGridView3.DataSource = table;

                dataGridView3.Columns[0].HeaderText = "Номер";
                dataGridView3.Columns[1].HeaderText = "Название предмета";
                dataGridView3.Columns[2].HeaderText = "Код подразделения";
                dataGridView3.Columns[3].HeaderText = "Дата и время";
                dataGridView3.Columns[4].HeaderText = "Количество";
                dataGridView3.Columns[5].HeaderText = "Действие";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DB db = new DB();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            if (comboBox1.Text == "Пришло")
            {
                string Sqlreq3 = "SELECT r.id, m.name, o.name, r.date_time, r.in_out_count, r.in_out FROM records AS r, materials AS m, organizations AS o WHERE r.materials = m.id AND r.org_in = o.id;";

                SqlCommand command = new SqlCommand(Sqlreq3, db.GetConnection());

                adapter.SelectCommand = command;
                adapter.Fill(table);
                dataGridView3.DataSource = table;

                dataGridView3.Columns[0].HeaderText = "Номер";
                dataGridView3.Columns[1].HeaderText = "Название предмета";
                dataGridView3.Columns[2].HeaderText = "Поставщик";
                dataGridView3.Columns[3].HeaderText = "Дата и время";
                dataGridView3.Columns[4].HeaderText = "Количество";
                dataGridView3.Columns[5].HeaderText = "Действие";
            }

            if (comboBox1.Text == "Ушло")
            {
                string Sqlreq3 = "SELECT r.id, m.name, d.code, r.date_time, r.in_out_count, r.in_out FROM records AS r, materials AS m, departments AS d WHERE r.materials = m.id AND r.dep_to = d.id;";

                SqlCommand command = new SqlCommand(Sqlreq3, db.GetConnection());

                adapter.SelectCommand = command;
                adapter.Fill(table);
                dataGridView3.DataSource = table;

                dataGridView3.Columns[0].HeaderText = "Номер";
                dataGridView3.Columns[1].HeaderText = "Название предмета";
                dataGridView3.Columns[2].HeaderText = "Код подразделения";
                dataGridView3.Columns[3].HeaderText = "Дата и время";
                dataGridView3.Columns[4].HeaderText = "Количество";
                dataGridView3.Columns[5].HeaderText = "Действие";
            }
        }
    }
}
