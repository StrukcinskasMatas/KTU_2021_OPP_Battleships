﻿using System;
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

        public Tank(int x, int y)
        {
            xLenght = x;
            yLenght = y;
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
            return 'T';
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
            String config = GetUnitType() + " config: parts " + this.body + "| weapons " + this.weapon;
            Console.WriteLine(config);
            return config;
        }
    }
}
