using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using demo.ethm.Infraestrutura.ExtensionLogger.Model;

namespace demo.ethm.Infraestrutura.ExtensionLogger.Repositories
{
    public class LogEventoRepository
    {
        private string ConnectionString { get; set; }

        public LogEventoRepository(string connection)
        {
            ConnectionString = connection;
        }

        private bool ExecuteNonQuery(string commandStr, List<MySqlParameter> paramList)
        {
            var result = false;
            using (var conn = new MySqlConnection(ConnectionString))
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }

                using (var command = new MySqlCommand(commandStr, conn))
                {
                    command.Parameters.AddRange(paramList.ToArray());
                    var count = command.ExecuteNonQuery();
                    result = count > 0;
                }
            }

            return result;
        }

        public bool InsertLog(LogEvento log)
        {
            var command = $@"INSERT INTO [dbo].[EventLog] ([EventID],[LogLevel],[Message],[CreatedTime]) VALUES (@EventID, @LogLevel, @Message, @CreatedTime)";
            var paramList = new List<MySqlParameter>
            {
                new MySqlParameter("EventID", log.EventId),
                new MySqlParameter("LogLevel", log.LogLevel),
                new MySqlParameter("Message", log.Message),
                new MySqlParameter("CreatedTime", log.CreatedTime)
            };

            return ExecuteNonQuery(command, paramList);
        }
    }
}
