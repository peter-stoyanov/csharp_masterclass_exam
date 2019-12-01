namespace MyCustomHttpServer
{
    public static class StartUp
    {
        public static void Main(string[] args)
        {
            var server = new HttpServer();
            server.Start();
        }
    }
}