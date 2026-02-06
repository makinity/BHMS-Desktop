namespace BoardingHouse
{
    public static class AppConfig
    {
        public static string Server => "localhost";
        public static string Database => "board";
        public static string User => "root";
        public static string Password => "root";

        public static string ConnectionString =>
            $"Server={Server};Database={Database};User Id={User};Password={Password};SslMode=Preferred;";
    }
}
