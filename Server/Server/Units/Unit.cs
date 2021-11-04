﻿using Server.Adapter;
using Server.Bridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Units
{
    public abstract class Unit: ICloneable
    {
        StatusAlive status;
        public Perk perk;
        public abstract string GetUnitType();
        public abstract char GetUnitTypeSymbol();
        public abstract string GetSizeString();
        public abstract int GetLenght();
        public abstract object Clone();
        public abstract object DeepClone();
        public abstract string getConfiguration();
        public abstract string Operation();
        public abstract void ChangeShield(string type);
        public string ShowStatus(StatusAlive status)
        {
            this.status = status;
            return status.statusAlive();
        }
        public string AddPerk()
        {
            Random rnd = new Random();
            int num = rnd.Next(2);
            Perk newPerk;
            if(num == 0)
            {
                newPerk = new Fast();
            }
            else if(num == 1)
            {
                newPerk = new Slow();
            }
            else
            {
                newPerk = new Durable();
            }
            this.perk = newPerk;
            return this.perk.skill();
        }
    }
}
