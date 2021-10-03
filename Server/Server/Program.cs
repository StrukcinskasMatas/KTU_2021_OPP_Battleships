using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        private static Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static List<Player> players = new List<Player>();
        private static byte[] _buffer = new byte[1024];
        
        static void Main(string[] args)
        {
            StartServer();
        }

        private static void StartServer()
        {
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, 69));
            serverSocket.Listen(10);
            serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);

            Console.WriteLine("Server started... to close it, press any key...");

            while (players.Count != 2) { }

            Console.WriteLine("Two players connected! Starting the game...");

            // TODO: start game
            foreach (var player in players)
            {
                player.getSocket().Send(Encoding.ASCII.GetBytes("(cls)"));
                player.getSocket().Send(Encoding.ASCII.GetBytes(PrintGrid()));
            }
            // TODO: iskelti i nauja funckija ^^

            Console.ReadLine();
        }

        private static string PrintGrid()
        {
            return "00000\n00000\n00000\n00000\n00000\n";
        }

        private static void AcceptCallback(IAsyncResult AR)
        {
            // Receive incoming connection
            Socket clientSocket = serverSocket.EndAccept(AR);
            Console.WriteLine("Received a connection!");

            // Ask for username
            byte[] promptBuffer = Encoding.ASCII.GetBytes("(action needed) Please enter your username:");
            clientSocket.Send(promptBuffer);

            // Get username
            byte[] responseBuffer = new byte[1024];
            clientSocket.Receive(responseBuffer);
            string username = Encoding.ASCII.GetString(responseBuffer);
            Console.WriteLine("Connection identified: " + username);

            // Add the connection to players list
            players.Add(new Player(username, clientSocket));
            if (players.Count != 2)
            {
                clientSocket.Send(Encoding.ASCII.GetBytes("Waiting for the second player to join..."));
            }
        }
    }

    public class Player
    {
        private string username;
        private Socket socket;

        public Player(string username, Socket socket)
        {
            this.username = username;
            this.socket = socket;
        }

        public string getUsername()
        {
            return this.username;
        }

        public Socket getSocket()
        {
            return this.socket;
        }
    }
}
