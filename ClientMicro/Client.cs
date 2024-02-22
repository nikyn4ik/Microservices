using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientMicroservice
{
    public class Client
    {
        private TcpClient _client;

        public Client()
        {
            _client = new TcpClient();
        }

        public async Task ConnectAsync()
        {
            try
            {
                await _client.ConnectAsync("127.0.0.1", 8888); //подключаюсь к серверу на лм порт 8888
                Console.WriteLine("Connected to server...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to server: {ex.Message}");
            }
        }

        public async Task SendMessageAsync(string message)
        {
            try
            {
                NetworkStream stream = _client.GetStream();
                byte[] buffer = Encoding.ASCII.GetBytes(message);
                await stream.WriteAsync(buffer, 0, buffer.Length); //отправляю сообщение серверу
                Console.WriteLine($"Sent message to server: {message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
            }
        }

        public async Task<string> ReceiveMessageAsync()
        {
            try
            {
                NetworkStream stream = _client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length); //получаю ответ от сервера
                string responseMessage = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received response from server: {responseMessage}");
                return responseMessage;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error receiving message: {ex.Message}");
                return null;
            }
        }

        public void Disconnect()
        {
            _client.Close();
        }
    }
}