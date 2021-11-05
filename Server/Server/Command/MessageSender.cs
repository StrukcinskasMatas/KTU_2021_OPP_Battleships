using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Command
{
    class ComplexCommand : ICommand
    {
        private Receiver _receiver;

        // Context data, required for launching the receiver's methods.
        private string _message;
        private bool _clear;
        private bool _action;
        private Socket _socket;

        // Complex commands can accept one or several receiver objects along
        // with any context data via the constructor.
        public ComplexCommand(Receiver receiver, Socket socket, string message, bool clear, bool action)
        {
            this._receiver = receiver;
            this._message = message;
            this._clear = clear;
            this._action = action;
            this._socket = socket;
        }

        // Commands can delegate to any methods of a receiver.
        public void Execute()
        {
            this._receiver.SendMessage(this._socket, this._message, this._clear, this._action);
        }
    }
}
