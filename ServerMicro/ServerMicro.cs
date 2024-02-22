using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Microservices
{
    public class ServerMicro
    {
        public static void Start()
        {
            TcpListener server = new TcpListener(IPAddress.Any, 8888); //создаю сервер порт 8888
            server.Start();
            Console.WriteLine("Server started...");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient(); //принимаю подключение от клиента
                Console.WriteLine("Client connected...");

                NetworkStream stream = client.GetStream();

                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received message from client: {message}");

                string responseMessage = "Message received!";
                byte[] responseBuffer = Encoding.ASCII.GetBytes(responseMessage);
                stream.Write(responseBuffer, 0, responseBuffer.Length);

                client.Close();
            }
        }
    }
}