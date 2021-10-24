using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Units
{
    public class Utility : Unit
    {
        private int xLenght;
        private int yLenght;
        public Shield Shield;

        public Utility(int x, int y, Shield shield)
        {
            xLenght = x;
            yLenght = y;
            this.Shield = shield;
        }

        public override string GetSizeString()
        {
            return String.Format("{0}x{1}", xLenght, yLenght);
        }

        public override string GetUnitType()
        {
            return "Utility";
        }

        public override int GetLenght()
        {
            return xLenght;
        }

        public override char GetUnitTypeSymbol()
        {
            return 'U';
        }
        public override Utility Clone()
        {
            return (Utility)this.MemberwiseClone();
        }
        public override Utility DeepClone()
        {
            Utility clone = (Utility)this.MemberwiseClone();
            clone.Shield = new Shield(Shield.type);
            return clone;
        }
        private string body;
        private string weapon;

        public string getBody()
        {
            return body;
        }
        public void setBody(string body)
        {
            this.body = body;
        }

        public string getWeapon()
        {
            return weapon;
        }
        public void setWeapon(string weapon)
        {
            this.weapon = weapon;
        }

        public override string getConfiguration()
        {
            String config = GetUnitType() + " config: parts " + this.body + "| weapons " + this.weapon;
            Console.WriteLine(config);
            return config;
        }
        public override string Operation()
        {
            return "ConcreteComponent";
        }

        public override string GetUnitInfo()
        {
          return "Utility with : " + this.Shield.type;
        }
    }
}
