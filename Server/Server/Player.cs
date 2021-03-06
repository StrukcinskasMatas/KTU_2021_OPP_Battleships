using Newtonsoft.Json;
using Server.StrategyObserverBuilder;
using System;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public class Player: IObserver
    {
        private string username;
        private Socket socket;
        public string role;

        public Player(string username, Socket socket, string role)
        {
            this.username = username;
            this.socket = socket;
            this.role = role;
        }

        public string GetUsername()
        {
            return this.username;
        }

        public Socket GetSocket()
        {
            return this.socket;
        }

        public void SendMessage(string msg, bool clear, bool action)
        {
            System.Threading.Thread.Sleep(100);
            Message message = new Message();
            message.setMessage(msg);
            message.actionNeeded(action);
            message.clearConsole(clear);
            string jsonData = JsonConvert.SerializeObject(message);
            byte[] dataBytes = Encoding.Default.GetBytes(jsonData);
            this.socket.Send(dataBytes);
        }

        public string ReceiveMessage()
        {
            byte[] buffer = new byte[1024 * 4];
            this.socket.Receive(buffer);
            string readData = Encoding.Default.GetString(buffer);
            Message response = JsonConvert.DeserializeObject<Message>(readData);
            return response.message;
        }
        public void Update(Cell[,] mapGrid)
        {
            Console.WriteLine("Be aware player spawned unit at random location in the map!!");
            SendMessage("Be aware player spawned unit at random location in the map!!", false, false);
        }
    }
}
