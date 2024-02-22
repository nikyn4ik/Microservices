using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerMicro
{
    public class Server
    {
        private TcpListener _listener;
        private bool _isRunning;

        public Server()
        {
            _listener = new TcpListener(IPAddress.Any, 8888); //создаю сервер порт 8888
            _isRunning = false;
        }

        public async Task StartAsync()
        {
            if (!_isRunning)
            {
                _isRunning = true;
                _listener.Start();
                Console.WriteLine("Server started. Waiting for connections...");

                try
                {
                    while (_isRunning)
                    {
                        TcpClient client = await _listener.AcceptTcpClientAsync(); //принимаю подключение от клиента
                        HandleClientAsync(client); //обрабатываю подключение клиента
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Server error: {ex.Message}");
                }
                finally
                {
                    _listener.Stop();
                }
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();

                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length); //принимаю данные от клиента
                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received message from client: {message}");

                string responseMessage = "Message received!";
                byte[] responseBuffer = Encoding.ASCII.GetBytes(responseMessage);
                await stream.WriteAsync(responseBuffer, 0, responseBuffer.Length); //отправляю ответ клиенту

                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling client: {ex.Message}");
            }
        }

        public void Stop()
        {
            _isRunning = false;
        }
    }
}
