using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Message
    {
        public string message;
        public bool action_needed;
        public bool clear_console;

        public Message GetMessage()
        {
            Message msg = new Message
            {
                message = this.message,
                action_needed = this.action_needed,
                clear_console = this.clear_console
            };
            return msg;
        }
        public void setMessage(string message)
        {
            this.message = message;
        }
        public void actionNeeded()
        {
            this.action_needed = true;
        }
        public void clearConsole()
        {
            this.clear_console = true;
        }
        public void reset()
        {
            this.message = "";
            this.action_needed = false;
            this.clear_console = false;
        }
        public void send(Socket socket)
        {
            Message message = GetMessage();
            string jsonData = JsonConvert.SerializeObject(message);
            byte[] dataBytes = Encoding.Default.GetBytes(jsonData);
            socket.Send(dataBytes);
            reset();
        }
        public void recieve(Socket socket)
        {
            byte[] buffer = new byte[1024 * 4];
            socket.Receive(buffer);
            string readData = Encoding.Default.GetString(buffer);
            Message response = JsonConvert.DeserializeObject<Message>(readData);
            this.message = response.message;
            this.action_needed = response.action_needed;
            this.clear_console = response.clear_console;
        }
    }
}
