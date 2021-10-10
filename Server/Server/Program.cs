using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        private static Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static List<Player> players = new List<Player>();
        
        static void Main(string[] args)
        {
            StartServer();
        }

        private static void StartServer()
        {
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, 69));
            serverSocket.Listen(10);
            Parallel.Invoke(
                () => serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null),
                () => serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null));
            Console.WriteLine("Server started... to close it, press any key...");

            while (players.Count != 2) { }

            Console.WriteLine("Two players connected! Starting the game...");
            Game gameInstance = Game.getInstance(players);
            gameInstance.StartGame();
            Console.ReadLine();
        }

        private static void AcceptCallback(IAsyncResult AR)
        {
            // Receive incoming connection
            Socket clientSocket = serverSocket.EndAccept(AR);
            Console.WriteLine("Received a connection!");

            // Ask for username
            clientSocket.Send(Encoding.ASCII.GetBytes("(action needed) Please enter your username:"));

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
}
