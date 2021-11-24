using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.COR
{
    class CopyHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if (request.ToString() == "C")
            {
                return $"Select ship to copy: (example input: \"1 2\")";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }
}
