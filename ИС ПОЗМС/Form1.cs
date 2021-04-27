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
        const string conn = "";
        int min_count;
        int count;

        private bool porog = true; // переменная для обозначения перехода минимального порога любого из материалов
        // false - Ниже минимального порога
        // true - Выше минимального порога
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                
                using (SqlCommand q = con.CreateCommand())
                {
                    q.CommandText = String.Format(
                      @"select {0}
                       from materials
                       where id = @prmid", "min_count");

                    q.Parameters.AddWithValue("@prmid", "1");

                    using (var reader = q.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // you may want to check if value is NULL: reader.IsDBNull(0)
                            Decimal min_value = Convert.ToDecimal(reader[0]);

                            min_count = min_value;
                        }
                    }
                }

                using (SqlCommand q = con.CreateCommand())
                {
                    for (int i, i < )
                    q.CommandText = String.Format(
                      @"select {0}
                       from materials
                       where id = @prmid", "count");

                    q.Parameters.AddWithValue("@prmid", "1");

                    using (var reader = q.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // you may want to check if value is NULL: reader.IsDBNull(0)
                            Decimal value = Convert.ToDecimal(reader[0]);

                            count = value;
                        }
                    }
                }

                con.Close();
            }

            if (count < min_count)
            {
                porog = false;
            }

            //Проверка на минимальный порог для любого материала и окрашивание главного меню в нужный цвет
            if (porog == true) { this.BackColor = System.Drawing.Color.LightGreen; }
            else { this.BackColor = System.Drawing.Color.Crimson; }*/

            /*string sqlreq = "SELECT MAX(id) FROM users";
            

            SqlConnection connection = new SqlConnection(conn);

            connection.Open();

            SqlCommand command = new SqlCommand(sqlreq, connection);

            using (var reader = q.ExecuteReader())
            {
                if (reader.Read())
                {
                    // you may want to check if value is NULL: reader.IsDBNull(0)
                    Decimal value = Convert.ToDecimal(reader[0]);

                    count = Convert.ToInt32(value);
                }
            }

            int[] id = new int[count];

            for (int i = 0; i < count; i++)
            {
                string sqlreq1 = "SELECT id = '" + i + "' FROM materials";

                SqlCommand command1 = new SqlCommand(sqlreq1, connection);


            }

            connection.Close();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            adapter.SelectCommand = command;
            adapter.Fill(table);*/

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
