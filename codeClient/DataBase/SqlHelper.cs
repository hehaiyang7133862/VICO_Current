using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace nsVicoClient
{
    /// <summary>
    ///针对sql server数据库操作的通用类
    /// </summary>
    public class SqlHelper
    {
        private static SqlHelper instance;

        //链接数据库字符串
        private string connectionString = Properties.Settings.Default.connectionString;

        private SqlHelper()
        { }

        public static SqlHelper getInstance()
        {
            if (instance == null)
            {
                instance = new SqlHelper();
            }

            return instance;
        }

        public DataTable RunCommandDt(string commandText)
        {
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds.Tables[0];
            }
        }

        public int RunCommandCount(string commandText)
        {
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(commandText, connection);
                    connection.Open();

                    IAsyncResult result = command.BeginExecuteNonQuery();

                    return command.EndExecuteNonQuery(result);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);

                    return 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);

                    return 0;
                }
            }
        }
    }
}
