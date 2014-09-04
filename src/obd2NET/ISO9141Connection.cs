using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace obd2NET
{
    public class ISO9141Connection : SerialConnection, IStandardizedConnection
    {
        public ISO9141Connection(string comPort) :
            this(new SerialPort(comPort))
        { }

        public ISO9141Connection(SerialPort port)
            : base(port)
        { }

        /// <summary>
        /// Negotiates the connection protocol with the vehicle
        /// </summary>
        public void Negotiate()
        {
            Port.BaudRate = 5;
            Send("0x33");
            Thread.Sleep(300);
            byte[] buffer = Read();
            byte synchronizationByte = buffer.FirstOrDefault();

            if (synchronizationByte != 0x55) throw new InitializationException("ISO 9141 initialization of the remote endpoint failed, synchronization byte is invalid or was not transmitted");
            Port.BaudRate = 10400;
            Thread.Sleep(100);
            buffer = Read();

            byte[] keyBytes = buffer.Take(2).ToArray();
            Thread.Sleep(50);
            Send(~keyBytes.ElementAt(2));

            Thread.Sleep(50);
            buffer = Read();
            byte communicationReadyByte = Read().FirstOrDefault();
            if (communicationReadyByte != ~synchronizationByte) throw new InitializationException("ISO 9141 initialization of the remote endpoint failed, vehicle is not ready to communicate");
        }
    }
}
