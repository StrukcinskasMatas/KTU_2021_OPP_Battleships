using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.StrategyObserverBuilder
{
    public abstract class Subject : ISubject
    {
        public abstract void Attach(IObserver observer);
        public abstract void Detach(IObserver observer);
        public abstract void Notify();
    }
}
