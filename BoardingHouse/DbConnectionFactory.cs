using MySql.Data.MySqlClient;

namespace BoardingHouse
{
    public static class DbConnectionFactory
    {
        public static MySqlConnection CreateConnection()
        {
            try
            {
                var connection = new MySqlConnection(AppConfig.ConnectionString);
                connection.Open();
                return connection;
            }
            catch (MySqlException mex)
            {
                throw new InvalidOperationException("Failed to establish a database connection.", mex);
            }
        }
    }
}
