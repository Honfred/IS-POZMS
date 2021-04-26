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
        const string conn = "Data Source=LAPTOP-2J26AE8V;Initial Catalog=Склад;Integrated Security=True";

        public Сотрудники()
        {
            InitializeComponent();
        }

        private void Сотрудники_Load(object sender, EventArgs e)
        {
            string sqlreq = "SELECT u.fio, d.code, u.phone FROM users AS u, departments AS d WHERE u.department = d.id;";

            SqlConnection connection = new SqlConnection(conn);

            connection.Open();

            SqlCommand command = new SqlCommand(sqlreq, connection);

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            dataGridView1.DataSource = table;

            connection.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                string text = textBox1.Text;
                //string sqlreq = "SELECT * FROM users WHERE department = '" + text + "';";
                string sqlreq1 = "SELECT u.fio, d.code, u.phone FROM users AS u, departments AS d WHERE d.code = '" + text + "' AND u.department = d.id;";

                SqlConnection connection = new SqlConnection(conn);

                connection.Open();

                SqlCommand command = new SqlCommand(sqlreq1, connection);

                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    dataGridView1.DataSource = table;
                }
                else { MessageBox.Show("Такого подразделения не существует или в нем никто не работает"); }


                connection.Close();
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
