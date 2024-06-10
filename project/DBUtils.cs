using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace DB
{
    public class DBUtils
    {
        public static SqlConnection connection;
        public static async void connect()
        {
            string connectionString = "Server=DESKTOP-8P4TD1Q\\SQLEXPRESS;Database=Aviasales;Trusted_Connection=True;";

            connection = new SqlConnection(connectionString);
            try
            {
                await connection.OpenAsync();

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static List<List<object>> request(string sqlExpression)
        {
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader Reader = command.ExecuteReader();
            List<List<object>> rezult = new List<List<object>>();
            while (Reader.Read())
            {
                List<object> tmparr = new List<object>();
                Object[] values = new Object[Reader.FieldCount];
                int fieldCount = Reader.GetValues(values);
                for (int i = 0; i < fieldCount; i++)
                    tmparr.Add(values[i]);
                rezult.Add(tmparr);
            }
            Reader.Close();
            return rezult;
        }

    }
}