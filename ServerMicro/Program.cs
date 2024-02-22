using System;

namespace ServerMicro
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ServerMicro is running...");
            var server = new ServerMicro.Server();
            server.StartAsync().Wait();
            Console.ReadLine();
        }
    }
}
