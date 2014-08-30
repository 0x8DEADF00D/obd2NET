using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obd2NET
{
    public class QueryException : Exception
    {
        public QueryException(string msg):
            base(msg)
        { }
    }
}
