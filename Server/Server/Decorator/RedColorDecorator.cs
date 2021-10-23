using Server.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Decorator
{
    class RedColorDecorator : Decorator
    {
        public RedColorDecorator(Unit comp) : base(comp)
        {
        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override string getConfiguration()
        {
            throw new NotImplementedException();
        }

        public override int GetLenght()
        {
            throw new NotImplementedException();
        }

        public override string GetSizeString()
        {
            throw new NotImplementedException();
        }

        public override string GetUnitType()
        {
            throw new NotImplementedException();
        }

        public override char GetUnitTypeSymbol()
        {
            throw new NotImplementedException();
        }

        public override string Operation()
        {
            return $"ConcreteDecoratorRed({base.Operation()})";
        }
    }
}
