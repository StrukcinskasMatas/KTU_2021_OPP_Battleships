using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.StateFlyweightProxy
{
    public class Context
    {
        private GameState _state = null;

        public Context(GameState state)
        {
            this.TransitionTo(state);
        }

        public void TransitionTo(GameState state)
        {
            Console.WriteLine($"Context: Transition to {state.GetType().Name}.");
            this._state = state;
            this._state.SetContext(this);
        }

        public void RequestToChangeGameState()
        {
            this._state.ChangeGameState();
        }

        public String GetState() {
            return this._state.GetType().Name;
        }
    }
}
