using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.StateFlyweightProxy
{
    public class ConcreteRunningGameState : GameState
    {
        public override void ChangeGameState()
        {
            Console.WriteLine("ConcreteRunningGameState handles ChangeGameState.");
            this._context.TransitionTo(new ConcretePausedGameState());
        }
    }
}
