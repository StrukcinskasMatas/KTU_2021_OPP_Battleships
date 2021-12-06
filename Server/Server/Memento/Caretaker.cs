using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Memento
{
    public class Caretaker
    {
        MementoClass memento;
        public MementoClass Memento
        {
            set { memento = value; }
            get { return memento; }
        }
    }
}
