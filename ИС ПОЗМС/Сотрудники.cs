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
    public partial class Сотрудники : Form
    {
        //const string conn = "Data Source=LAPTOP-2J26AE8V;Initial Catalog=Склад;Integrated Security=True";

        public Сотрудники()
        {
            InitializeComponent();
        }

        private void Сотрудники_Load(object sender, EventArgs e)
        {
            string Sqlreq = "SELECT u.fio, d.code, u.phone FROM users AS u, departments AS d WHERE u.department = d.id;";

            DB db = new DB();

            //MySqlConnection connection = new MySqlConnection(db.GetConnection);

            db.openConnection();

            MySqlCommand command = new MySqlCommand(Sqlreq, db.GetConnection());

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            dataGridView1.DataSource = table;

            db.closeConnection();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                string text = textBox1.Text;
                //string MySqlreq = "SELECT * FROM users WHERE department = '" + text + "';";
                string MySqlreq1 = "SELECT u.fio, d.code, u.phone FROM users AS u, departments AS d WHERE d.code = '" + text + "' AND u.department = d.id;";
                DB db = new DB();

                db.openConnection();

                MySqlCommand command = new MySqlCommand(MySqlreq1, db.GetConnection());

                MySqlDataAdapter adapter = new MySqlDataAdapter();
                DataTable table = new DataTable();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    dataGridView1.DataSource = table;
                }
                else { MessageBox.Show("Такого подразделения не существует или в нем никто не работает"); }


                db.closeConnection();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }
    }
}
