using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Adapter
{
    public class StatusAlive
    {
        public virtual string statusAlive()
        {
            string msg = "*Status-alive*";
            return msg;
        }
    }
}
