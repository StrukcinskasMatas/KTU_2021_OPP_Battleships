using Server.Bridge;
using Server.Memento;
using Server.Visitor;
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
        public char color;
        public string perk;
        public double missChance;
        public Tank(int x, int y, Shield shield, char color)
        {
            xLenght = x;
            yLenght = y;
            this.Shield = shield;
            this.color = color;
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

        public char getColor() 
        {
            return color;
        }
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
        public override void setPerk()
        {
            perk = AddPerk();
        }
        public override void setMissChance(double chance)
        {
            missChance = chance;
        }
        public override string getConfiguration()
        {
            String config = GetUnitType() + " config: parts " + this.body + " | weapons " + this.weapon + " | perk: " + perk + " | color: " + getColor() + " | miss chance: " + missChance + "%";
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
        public override double calculateMiss(HighMissChance miss)
        {
            return miss.chance() + 2.5;
        }
        public override double calculateMiss(AvarageMissChance miss)
        {
            return miss.chance();
        }
        public override double calculateMiss(LowMissChance miss)
        {
            return miss.chance() - 2.5;
        }
    }
}
