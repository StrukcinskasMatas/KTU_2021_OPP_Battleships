using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Command
{
    class Receiver
    {
        public void SendMessage(Socket socket, string message, bool clear, bool action)
        {
            Message new_msg = new Message();
            new_msg.setMessage(message);
            new_msg.actionNeeded(action);
            new_msg.clearConsole(clear);
            string jsonData = JsonConvert.SerializeObject(new_msg);
            byte[] dataBytes = Encoding.Default.GetBytes(jsonData);
            socket.Send(dataBytes);
        }

    }
}
