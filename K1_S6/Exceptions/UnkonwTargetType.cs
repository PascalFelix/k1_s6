using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_S6.Exceptions
{
    class UnkonwTargetType : Exception
    {
        public UnkonwTargetType(string objectFunktion): base(objectFunktion)
        {

        }
    }
}
