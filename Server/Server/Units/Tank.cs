using Server.Memento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Units
{
    public class Tank : Unit
    {
        private int xLenght;
        private int yLenght;
        public Shield Shield;
        public Tank(int x, int y, Shield shield)
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
            return "Tank";
        }

        public override int GetLenght()
        {
            return xLenght;
        }

        public override char GetUnitTypeSymbol()
        {
            if (this.Shield.type == "Titan")
            {
                return 'T';
            } else
            {
                return 'P';
            }
        }
        public override Tank Clone()
        {
            return (Tank)this.MemberwiseClone();
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
            String config = GetUnitType() + " config: parts " + this.body + " | weapons " + this.weapon + " | Perks: " + this.AddPerk();
            Console.WriteLine(config);
            return config;
        }
        public override string Operation()
        {
            return "hitted tank";
        }

        public override object DeepClone()
        {
            Tank clone = (Tank)this.MemberwiseClone();
            clone.Shield = new Shield(Shield.type);
            return clone;
        }

        public override void ChangeShield(string type)
        {
            this.Shield.ChangeType(type);
        }

        public override MementoClass SaveSchieldMemento()
        {
            return new MementoClass(this.Shield.type);
        }

        public override void RestoreShiledMemeto(MementoClass memento)
        {
            this.Shield.type = memento.shieldType;
        }
    }
}
