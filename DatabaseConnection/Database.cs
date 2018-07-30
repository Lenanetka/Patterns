using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DatabaseConnection
{
    //sealed - нельзя наследовать
    public sealed class Database
    {       
        //volatile - может изменяться одновременно разными потоками
        private static volatile SqlConnection instance;
        private static object syncRoot = new object();
        private const string connectionString = "Data Source=ServerName;" +
                                                "Initial Catalog=DataBaseName;" +
                                                "User id=UserName;" +
                                                "Password=Secret;";
        private Database()
        {

        }
        public static SqlConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new SqlConnection(connectionString);
                    }
                }

                return instance;
            }
        }
        public void write(string name)
        {
            if (instance == null) throw new Exception("No instance of database exists.");
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "INSERT persons (name) VALUES ('"+ name + "')";
            cmd.Connection = instance;

            instance.Open();
            cmd.ExecuteNonQuery();
            instance.Close();
        }
        public List<string> read()
        {
            if (instance == null) throw new Exception("No instance of database exists.");
            List<string> result = new List<string>();
            using (SqlCommand command = new SqlCommand(
                "SELECT name FROM persons ORDER BY name",
                instance))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            result.Add((string)reader.GetValue(i));
                        }
                    }
                }
            }
            return result;
        }
    }
}
