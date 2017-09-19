using System;

namespace Obd2NET
{
    public class QueryException : Exception
    {
        public QueryException(string message) : base(message)
        {
        }
    }
}
