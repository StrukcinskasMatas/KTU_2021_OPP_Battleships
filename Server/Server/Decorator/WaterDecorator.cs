using Server.Memento;
using Server.Units;
using Server.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Decorator
{
    class WaterDecorator : Decorator

    {
        public WaterDecorator(Unit comp) : base(comp)
        {
        }

        public override void ChangeShield(string type)
        {
            throw new NotImplementedException();
        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override object DeepClone()
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
            return $"Water Attack {base.Operation()}";
        }

        public override MementoClass SaveSchieldMemento()
        {
            throw new NotImplementedException();
        }
        public override void RestoreShiledMemeto(MementoClass memento)
        {
            throw new NotImplementedException();
        }

        public override double calculateMiss(HighMissChance miss)
        {
            throw new NotImplementedException();
        }

        public override double calculateMiss(AvarageMissChance miss)
        {
            throw new NotImplementedException();
        }

        public override double calculateMiss(LowMissChance miss)
        {
            throw new NotImplementedException();
        }

        public override void setPerk()
        {
            throw new NotImplementedException();
        }

        public override void setMissChance(double chance)
        {
            throw new NotImplementedException();
        }
    }

}
