using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obd2NET
{
    public class InitializationException : Exception
    {
        public InitializationException(string msg) :
            base(msg)
        { }
    }
}
