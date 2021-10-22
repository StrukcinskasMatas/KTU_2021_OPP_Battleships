using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket serverSocket = ConnectToServer(69);
            StartCommunication(serverSocket);
        }

        private static Socket ConnectToServer(int port)
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int attempts = 0;
            while (!serverSocket.Connected && attempts < 420)
            {
                try
                {
                    attempts++;
                    serverSocket.Connect(IPAddress.Loopback, port);

                    if (serverSocket.Connected)
                    {
                        Console.WriteLine("Successfully connected to server!");
                        break;
                    }
                }
                catch (SocketException)
                {
                    Console.WriteLine("Failed to connect to server after " + attempts.ToString() + " attempts. :(");
                    Console.ReadKey();
                    Environment.Exit(1);
                }
            }

            return serverSocket;
        }

        private static void StartCommunication(Socket serverSocket)
        {
            while (serverSocket.Connected)
            {
                Message response = new Message();
                Message message = new Message();
                response.recieve(serverSocket);
                if (response.clear_console)
                {
                    Console.Clear();
                }
                Console.WriteLine(response.message);

                // Process the server's message
                if (response.action_needed)
                {
                    Console.Write("> ");
                    string command = Console.ReadLine();
                    message.setMessage(command);
                    message.send(serverSocket);
                }
            }
        }
    }
}
