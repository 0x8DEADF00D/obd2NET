using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obd2NET
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Also known as KWP and ISO 14230
    /// </remarks>
    public class KeywordConnection : SerialConnection, IStandardizedConnection
    {
        public KeywordConnection(string comPort) :
            this(new SerialPort(comPort))
        { }

        public KeywordConnection(SerialPort port)
            : base(port)
        { }

        public void Negotiate()
        {
            throw new NotImplementedException();
        }
    }
}
