using System.Data.SqlClient;

namespace ИС_ПОЗМС
{
    class DB
    {
        //MySqlConnection connection = new MySqlConnection("server=192.168.1.41;port=3306;username=student;password=123;database=student_storage"); //PHP MY Admin
        SqlConnection connection = new SqlConnection("Data Source=LAPTOP-2J26AE8V; Initial Catalog=Склад; Integrated Security=True"); //MS SQL Server
        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public SqlConnection GetConnection()
        {
            return connection;
        }
    }
}
