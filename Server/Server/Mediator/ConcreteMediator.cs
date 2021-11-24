using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Mediator
{
    class ConcreteMediator : IMediator
    {
        private FirstPlayer _component1;

        private SecondPlayer _component2;

        public ConcreteMediator(FirstPlayer component1, SecondPlayer component2)
        {
            this._component1 = component1;
            this._component1.SetMediator(this);
            this._component2 = component2;
            this._component2.SetMediator(this);
        }

        public void Notify(object sender, int id)
        {
            if (id == 1)
            {
                this._component1.PostMessage();
                this._component2.PostMessageAboutUpgrade();
            }
            if (id == 2)
            {
                this._component2.PostMessage();
                this._component1.PostMessageAboutUpgrade();
            }
        }
    }
}
