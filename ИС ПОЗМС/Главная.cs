﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ИС_ПОЗМС
{
    public partial class Главная : Form
    {
        DB db = new DB();


        public Главная()
        {
            InitializeComponent();
        }

        private void Главная_Load(object sender, EventArgs e)
        {
            db.openConnection();

            Min_materials(); //Проверка материала на количество, если меньше то выдаст сообщение

            Materials(); //Материалы

            Organizations(); //Поставщики

            Departments(); //Подразделения

            Users(); //Сотрудники
        }

        public void Materials()
        {
            try
            {
                string Sqlreq = "SELECT materials.articul, materials.name, materials.count, materials.min_count, organizations.name " +
                    "FROM materials " +
                    "JOIN organizations ON materials.organization = organizations.id " +
                    "WHERE materials.name like '%" + textBox1.Text.Trim() + "%' or materials.articul like '%" + textBox1.Text.Trim() + "%';";

                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                SqlCommand command = new SqlCommand(Sqlreq, db.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);

                dataGridView1.DataSource = table;

                dataGridView1.Columns[0].HeaderText = "Артикул";
                dataGridView1.Columns[1].HeaderText = "Наименование";
                dataGridView1.Columns[2].HeaderText = "Количество";
                dataGridView1.Columns[3].HeaderText = "Мин.количество";
                dataGridView1.Columns[4].HeaderText = "Название организации";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Min_materials()
        {
            try
            {
                string Sqlreq = "SELECT * FROM materials where count < min_count;";
                SqlCommand command = new SqlCommand(Sqlreq, db.GetConnection());
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();

                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    MessageBox.Show("Одного или нескольких материалов не хватает!");
                    button1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Organizations()
        {
            try
            {
                string Sqlreq = "SELECT name, phone FROM organizations WHERE name like '%" + textBox2.Text.Trim() + "%';";

                SqlCommand command = new SqlCommand(Sqlreq, db.GetConnection());
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                dataGridView2.DataSource = table;

                dataGridView2.Columns[0].HeaderText = "Название организации";
                dataGridView2.Columns[1].HeaderText = "Номер телефона";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Departments()
        {
            try
            {
                string Sqlreq = "SELECT code, name, phone " +
                    "FROM departments " +
                    "WHERE code like '%" + textBox4.Text.Trim() + "%' OR name like '%" + textBox4.Text.Trim() + "%'";

                SqlCommand command = new SqlCommand(Sqlreq, db.GetConnection());
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();

                adapter.SelectCommand = command;
                adapter.Fill(table);

                dataGridView4.DataSource = table;

                dataGridView4.Columns[0].HeaderText = "Код";
                dataGridView4.Columns[1].HeaderText = "Название";
                dataGridView4.Columns[2].HeaderText = "Номер телефона";
                dataGridView4.Columns[0].Width = 50;
                dataGridView4.Columns[1].Width = 250;
                dataGridView4.Columns[2].Width = 75;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Records()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            try
            {
                if (comboBox1.Text == "Пришло")
                {
                    string Sqlreq3 = "SELECT records.id, materials.name, organizations.name, records.date_time, records.in_out_count, records.in_out " +
                        "FROM records" +
                        "JOIN materials ON records.materials = materials.id" +
                        "JOIN organizations ON records.org_in = organizations.id" +
                        "WHERE m.name LIKE '%" + textBox3.Text.Trim() + "%' " +
                        "ORDER BY r.id DESC;";

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
                    dataGridView3.Columns[0].Width = 50;
                }

                if (comboBox1.Text == "Ушло")
                {
                    string Sqlreq3 = "SELECT records.id, materials.name, users.fio, departments.code, records.date_time, records.in_out_count, records.in_out " +
                        "FROM records, materials, departments, users" +
                        "JOIN materials ON records.materials = materials.id" +
                        "JOIN departments ON records.dep_to = departments.id" +
                        "JOIN users ON records.id_users = users.id" +
                        "WHERE m.name LIKE '%" + textBox3.Text.Trim() + "%';";

                    SqlCommand command = new SqlCommand(Sqlreq3, db.GetConnection());

                    adapter.SelectCommand = command;
                    adapter.Fill(table);
                    dataGridView3.DataSource = table;

                    dataGridView3.Columns[0].HeaderText = "Номер";
                    dataGridView3.Columns[1].HeaderText = "Название предмета";
                    dataGridView3.Columns[2].HeaderText = "Сотрудник";
                    dataGridView3.Columns[3].HeaderText = "Код подразделения";
                    dataGridView3.Columns[4].HeaderText = "Дата и время";
                    dataGridView3.Columns[5].HeaderText = "Количество";
                    dataGridView3.Columns[6].HeaderText = "Действие";
                    dataGridView3.Columns[0].Width = 60;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Users()
        {
            try
            {
                string Sqlreq = "SELECT users.fio, departments.name, users.post, users.phone " +
                    "FROM users" +
                    "JOIN departments ON users.department = departments.id" +
                    "WHERE users.fio LIKE '%" + textBox5.Text.Trim() + "%'  OR departments.name LIKE '%" + textBox5.Text.Trim() + "%';";

                SqlCommand command = new SqlCommand(Sqlreq, db.GetConnection());
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                dataGridView5.DataSource = table;

                dataGridView5.Columns[0].HeaderText = "Фамилия Имя Отчество";
                dataGridView5.Columns[1].HeaderText = "Отдел";
                dataGridView5.Columns[2].HeaderText = "Должность";
                dataGridView5.Columns[3].HeaderText = "Номер телефона";
                dataGridView5.Columns[0].Width = 220;
                dataGridView5.Columns[1].Width = 240;
                dataGridView5.Columns[3].Width = 145;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Exit()
        {
            Авторизация авторизация = (Авторизация)this.Owner;
            db.closeConnection();
            авторизация.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string Sqlreq = "SELECT materials.articul, materials.name, materials.count, materials.min_count, organizations.name " +
                    "FROM materials" +
                    "JOIN organizations ON materials.organization = organizations.id" +
                    "WHERE count < min_count;";

                SqlCommand command = new SqlCommand(Sqlreq, db.GetConnection());
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                dataGridView1.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Organizations();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Records();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Добавление_поставщиков добавление = new Добавление_поставщиков();
            добавление.Owner = this;
            добавление.Show();
        }

        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            Добавление_записи добавление = new Добавление_записи();
            добавление.Owner = this;
            добавление.Show();
        }

        private void btnUpdateRecord_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                Редактирование_записи редактирование = new Редактирование_записи();
                редактирование.Owner = this;
                редактирование.Show();
            }
            else { MessageBox.Show("Пожалуйста укажите действие"); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Добавление_материалов добавление_Материалов = new Добавление_материалов();
            добавление_Материалов.Owner = this;
            добавление_Материалов.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Materials();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            Records();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            Departments();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            Users();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                db.openConnection();

                if (comboBox1.Text == "Пришло")
                {
                    if (e.ColumnIndex != -1 && e.RowIndex != -1)
                    {
                        int row, id;
                        row = dataGridView3.SelectedCells[0].RowIndex;
                        id = Convert.ToInt32(dataGridView3.Rows[row].Cells[0].Value.ToString());
                        string query = $"SELECT records.id, materials.name, organizations.name, records.date_time, records.in_out_count, records.in_out " +
                            $"FROM records" +
                            $"JOIN materials ON records.materials = materials.id" +
                            $"JOIN organizations ON records.org_in = organizations.id" +
                            $"records.id = '{id}'; ";
                        SqlCommand command = new SqlCommand(query, db.GetConnection());
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            DataBank.Номер = Convert.ToInt32(reader[0]);
                            DataBank.Название_предмета = reader[1].ToString();
                            DataBank.Поставщик = reader[2].ToString();
                            DataBank.Дата_и_время = reader[3].ToString();
                            DataBank.Количество = Convert.ToInt32(reader[4]);
                            DataBank.Действие = reader[5].ToString();
                        }
                        reader.Close();
                    }
                }
                if (comboBox1.Text == "Ушло")
                {
                    if (e.ColumnIndex != -1 && e.RowIndex != -1)
                    {
                        int row, id;
                        row = dataGridView3.SelectedCells[0].RowIndex;
                        id = Convert.ToInt32(dataGridView3.Rows[row].Cells[0].Value.ToString());
                        string query = $"SELECT records.id, materials.name, users.fio, departments.name, records.date_time, records.in_out_count, records.in_out " +
                            $"FROM records" +
                            $"JOIN materials ON records.materials = materials.id" +
                            $"JOIN departments ON records.dep_to = departments.id" +
                            $"JOIN users ON records.id_users = users.id" +
                            $"WHERE r.id = '{id}'; ";
                        SqlCommand command = new SqlCommand(query, db.GetConnection());
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            DataBank.Номер = Convert.ToInt32(reader[0]);
                            DataBank.Название_предмета = reader[1].ToString();
                            DataBank.ФИО = reader[2].ToString();
                            DataBank.Подразделение = reader[3].ToString();
                            DataBank.Дата_и_время = reader[4].ToString();
                            DataBank.Количество = Convert.ToInt32(reader[5]);
                            DataBank.Действие = reader[6].ToString();
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnUpdate1_Click(object sender, EventArgs e)
        {
            Materials();
        }

        private void btnUpdate2_Click(object sender, EventArgs e)
        {
            Organizations();
        }

        private void btnUpdate3_Click(object sender, EventArgs e)
        {
            Records();
        }

        private void btnUpdate4_Click(object sender, EventArgs e)
        {
            Departments();
        }

        private void btnUpdate5_Click(object sender, EventArgs e)
        {
            Users();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnExit1_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnExit2_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnExit3_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnExit4_Click(object sender, EventArgs e)
        {
            Exit();
        }
    }
}