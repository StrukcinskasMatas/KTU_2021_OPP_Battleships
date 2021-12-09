using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.StateFlyweightProxy
{
    public class ConcretePausedGameState : GameState
    {
        public override void ChangeGameState()
        {
            Console.WriteLine("ConcretePausedGameState handles ChangeGameState.");
            this._context.TransitionTo(new ConcreteRunningGameState());
        }
    }
}
