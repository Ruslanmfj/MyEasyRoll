using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace MyEasyRoll
{
    class database
    {
        MySqlConnection connection = new MySqlConnection("server=127.0.0.1;port = 3306; user=dba;password=dbaPass;database=MyEasyRoll");
    
    public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public MySqlConnection getConnection()
        {
            return connection;
        }
    }
}
