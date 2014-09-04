using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace obd2NET
{
    /// <summary>
    /// Represents the serial connection to the vehicle's interface
    /// </summary>
    public class SerialConnection : IOBDConnection
    {
        /// <summary>
        /// Port used for communicating with the vehicle's interface
        /// </summary>
        public SerialPort Port { get; set; }

        public SerialConnection():
            this(new SerialPort())
        { }

        public SerialConnection(string comPort) :
            this(new SerialPort(comPort))
        { }

        public SerialConnection(SerialPort port)
        {
            Port = port;
            Open();
        }

        ~SerialConnection()
        {
            Close();
        }

        /// <summary>
        /// Opens the connection to the interface if no connection was established yet
        /// </summary>
        public void Open()
        {
            if (!Port.IsOpen)
                Port.Open();
        }

        /// <summary>
        /// Closes the connection to the interface if the connection is established
        /// </summary>
        public void Close()
        {
            if (Port.IsOpen)
                Port.Close();
        }

        /// <summary>
        /// Queries data from the vehicle by sending a specific mode and PID
        /// </summary>
        /// <param name="parameterMode"> <c>Vehicle.Mode</c> used </param>
        /// <param name="parameterID"> <c>Vehicle.PID</c> indicating the information to query </param>
        /// <returns> <c>ControllerResponse</c> object holding the returned data from the controller unit </returns>
        /// <remarks> Blocking until a complete answer has been received </remarks>
        public ControllerResponse Query(Vehicle.Mode parameterMode, Vehicle.PID parameterID)
        {
            Send(Convert.ToUInt32(parameterMode).ToString("X2") + Convert.ToUInt32(parameterID).ToString("X2"));
            Thread.Sleep(100);

            string fullResponse = "";
            while(!fullResponse.Contains(">"))
            {
                byte[] readBuffer = Read();
                fullResponse = System.Text.Encoding.Default.GetString(readBuffer);
            }

            return new ControllerResponse(fullResponse, parameterMode, parameterID);
        }

        public void Send<T>(T message) 
        {
            Port.Write(message + "\r");
        }

        public byte[] Read()
        {
            byte[] readBuffer = new byte[1024];
            Port.Read(readBuffer, 0, 1024);
            return readBuffer;
        }

        /// <summary>
        /// Queries data from the vehicle by sending a specific mode
        /// </summary>
        /// <param name="parameterMode"> <c>Vehicle.Mode</c> used </param>
        /// <returns> <c>ControllerResponse</c> object holding the returned data from the controller unit </returns>
        /// <remarks> Blocking until a complete answer has been received </remarks>
        public ControllerResponse Query(Vehicle.Mode parameterMode)
        {
            Send(Convert.ToUInt32(parameterMode).ToString("X2"));
            Thread.Sleep(100);

            string fullResponse = "";
            while (!fullResponse.Contains(">"))
            {
                byte[] readBuffer = Read();
                fullResponse = System.Text.Encoding.Default.GetString(readBuffer);
            }

            return new ControllerResponse(fullResponse, parameterMode);
        }
    }
}
