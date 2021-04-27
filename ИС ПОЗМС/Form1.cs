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
    public partial class Form1 : Form
    {
        //const string conn = "";
        int min_count;
        int count;

        private bool porog = true; // переменная для обозначения перехода минимального порога любого из материалов
        // false - Ниже минимального порога
        // true - Выше минимального порога
        public Form1()
        {
            InitializeComponent();

            DB db = new DB();

            string MySqlreq = "SELECT * FROM `materials` where count < min_count;";


            MySqlCommand command = new MySqlCommand(MySqlreq, db.GetConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                porog = false;
                MessageBox.Show("Одного или нескольких материалов не хватает!");
                db.closeConnection();

            }


            //Проверка на минимальный порог для любого материала и окрашивание главного меню в нужный цвет
            if (porog == true) { this.BackColor = System.Drawing.Color.LightGreen; }
            else { this.BackColor = System.Drawing.Color.Crimson; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e) // Сотрудники
        {
            Сотрудники сотрудники = new Сотрудники();
            сотрудники.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e) // Поставщики
        {

        }

        private void button3_Click(object sender, EventArgs e) // Материалы
        {

        }

        private void button4_Click(object sender, EventArgs e) // Заявки
        {

        }

        private void button5_Click(object sender, EventArgs e) // Подразделения
        {

        }

        private void button6_Click(object sender, EventArgs e) // Отчеты
        {

        }
    }
}
