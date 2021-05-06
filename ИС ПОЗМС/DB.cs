using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ИС_ПОЗМС
{
    class DB
    {
        SqlConnection connection = new SqlConnection("Data Source=LAPTOP-2J26AE8V; Initial Catalog=Склад; Integrated Security=True"); //MS SQL Server
        public void openConnection()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void closeConnection()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public SqlConnection GetConnection()
        {
            return connection;
        }
    }
}
