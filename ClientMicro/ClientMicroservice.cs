using System;

namespace ClientMicroservice
{
    public class ClientMicroservice
    {
        public static async Task StartAsync()
        {
            Client client = new Client();
            await client.ConnectAsync(); //подключаюсь к серверу

            string message = "Hello from client!";
            await client.SendMessageAsync(message); //отправляю сообщение серверу

            string response = await client.ReceiveMessageAsync(); //получаю ответ от сервера
            Console.WriteLine($"Received response from server: {response}");

            client.Disconnect();
        }
    }
}
