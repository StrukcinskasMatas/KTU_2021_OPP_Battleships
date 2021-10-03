using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

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
                // Receive a message from the server
                byte[] messageBuffer = new byte[1024];
                serverSocket.Receive(messageBuffer);
                string message = Encoding.ASCII.GetString(messageBuffer);

                if (message.Contains("cls"))
                {
                    Console.Clear();
                }

                Console.WriteLine(message); // TODO: make nice message struct

                // Process the server's message
                if (message.Contains("action needed"))
                {
                    Console.Write("> ");
                    string command = Console.ReadLine();
                    byte[] commandBuffer = Encoding.ASCII.GetBytes(command);
                    serverSocket.Send(commandBuffer); // need try-catch, perhaps move to another 'SendCommand()' function
                }
            }
        }
    }
}
