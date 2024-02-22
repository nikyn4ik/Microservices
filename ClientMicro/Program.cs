using System;

namespace ClientMicroservice
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ClientMicros is running...");

            //����� client � ����������� � �������
            var client = new Client();
            client.ConnectAsync().Wait();

            //�������� ��������� �������
            client.SendMessageAsync("Hello from client!").Wait();

            //������� ����� �� �������
            string response = client.ReceiveMessageAsync().Result;
            Console.WriteLine($"Received response from server: {response}");

            //���������� �� �������
            client.Disconnect();

            Console.ReadLine();
        }
    }
}