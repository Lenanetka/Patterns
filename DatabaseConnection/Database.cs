using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DatabaseConnection
{
    //sealed - нельзя наследовать
    public sealed class Database
    {       
        //volatile - может изменяться одновременно разными потоками
        private static volatile Database instance;
        private static volatile SqlConnection connection;
        private static object syncRoot = new object();
        private static string connectionString;
        private Database()
        {
            if (connection == null)
                connection = new SqlConnection(connectionString);
        }
        public static void setConnectionSettings(string serverName, string dataBaseName, string userName, string secret)
        {
            connectionString = "Data Source=" + serverName + ";" +
                               "Initial Catalog=" + dataBaseName + ";" +
                               "User id=" + userName + ";" +
                               "Password=" + secret + ";";
        }
        public static Database getInstanse()
        {
            if (connectionString == null) throw new Exception("Connection settings must be set!");
            if (instance == null)
            {
                lock (syncRoot)
                {
                    instance = new Database();
                }
            }

            return instance;
        }
        public void create_table(string table, string fields)
        {
            if (instance == null) throw new Exception("No instance of database exists.");
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "CREATE TABLE "+ table + " ("+ fields + ");";
            cmd.Connection = connection;

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public void write(string table, string fields, string values)
        {
            if (instance == null) throw new Exception("No instance of database exists.");
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "INSERT persons ("+ fields + ") VALUES ('"+ values + "')";
            cmd.Connection = connection;

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public List<string> read(string table, string values)
        {
            if (instance == null) throw new Exception("No instance of database exists.");
            List<string> result = new List<string>();
            using (SqlCommand command = new SqlCommand(
                "SELECT "+ values + " FROM " + table,
                connection))
            {
                connection.Open();
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
                connection.Close();
            }
            return result;
        }
    }
}
