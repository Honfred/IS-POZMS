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
    public partial class Добавление_поставщиков : Form
    {
        public Добавление_поставщиков()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            Главная главная = (Главная)this.Owner;

            db.openConnection();

            string MySqlreq = "INSERT INTO organizations (name, phone) VALUES ('" + textBox1.Text + "', '" + textBox2.Text + "');";
            SqlCommand command = new SqlCommand(MySqlreq, db.GetConnection());
            command.ExecuteNonQuery();
            
            главная.Organizations();
            this.Hide();

            db.closeConnection();
        }
    }
}
