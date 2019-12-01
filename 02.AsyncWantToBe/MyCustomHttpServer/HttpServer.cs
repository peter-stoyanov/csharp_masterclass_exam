namespace MyCustomHttpServer
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class HttpServer : IHttpServer
    {
        private bool isWorking;

        public HttpServer()
        {
        }

        public void Start()
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 80);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            this.isWorking = true;

            Console.WriteLine("Started.");

            try
            {
                // Bind the socket to the local endpoint and listen for incoming connections.  
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (this.isWorking)
                {
                    var client = listener.Accept();
                    Task.Run(async () => await ProcessClient(client));
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Stop()
        {
            this.isWorking = false;
        }

        private static async Task ProcessClient(Socket socket)
        {
            using (socket)
            {
                using (var stream = new NetworkStream(socket))
                {
                    // Console.WriteLine($"thread: {Thread.CurrentThread.ManagedThreadId}");

                    var buffer = new byte[1024];
                    var readLength = await stream.ReadAsync(buffer, 0, buffer.Length);
                    var requestText = Encoding.UTF8.GetString(buffer, 0, readLength);

                    Console.WriteLine(new string('=', 60));
                    Console.WriteLine(requestText);

                    var responseText = await File.ReadAllTextAsync("index.html");
                    var responseBytes = Encoding.UTF8.GetBytes(
                        "HTTP/1.0 200 Not Found" + Environment.NewLine +
                        "Content-Length: " + responseText.Length + Environment.NewLine + Environment.NewLine +
                        responseText);

                    await socket.SendAsync(responseBytes, SocketFlags.None);
                }
            }
        }
    }
}