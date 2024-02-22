using System;

namespace ClientMicroservice
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ClientMicros is running...");

            //класс client и подключаюсь к серверу
            var client = new Client();
            client.ConnectAsync().Wait();

            //отправл¤ю сообщение серверу
            client.SendMessageAsync("Hello from client!").Wait();

            //получаю ответ от сервера
            string response = client.ReceiveMessageAsync().Result;
            Console.WriteLine($"Received response from server: {response}");

            //отключаюсь от сервера
            client.Disconnect();

            Console.ReadLine();
        }
    }
}