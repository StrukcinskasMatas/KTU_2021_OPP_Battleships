using Server.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Decorator
{
    class BlueColorDecorator : Decorator
    {
        public BlueColorDecorator(Unit comp) : base(comp)
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

        // Decorators may call parent implementation of the operation, instead
        // of calling the wrapped object directly. This approach simplifies
        // extension of decorator classes.
        public override string Operation()
        {
            return $"ConcreteDecoratorBlue({base.Operation()})";
        }
    }
}
