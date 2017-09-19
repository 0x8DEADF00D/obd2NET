using System;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Obd2NET
{
    /// <summary>
    /// Represents the serial connection to the vehicle's interface
    /// </summary>
    public class SerialConnection : IOBDConnection, IDisposable
    {
        public SerialConnection()
            : this(new SerialPort())
        {

        }

        public SerialConnection(string comPort)
            : this(new SerialPort(comPort))
        {

        }

        public SerialConnection(SerialPort port)
        {
            Port = port;
        }

        /// <summary>
        /// Port used for communicating with the vehicle's interface
        /// </summary>
        public SerialPort Port { get; set; }

        private bool disposed;

        /// <summary>
        /// Closes the connection to the interface if the connection is established
        /// </summary>
        public void Close()
        {
            if (Port.IsOpen)
                Port.Close();
        }

        /// <summary>
        /// Opens the connection to the interface if no connection was established yet
        /// </summary>
        public void Open()
        {
            if (!Port.IsOpen)
                Port.Open();
        }

        ~SerialConnection()
        {
            Close();
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
            Port.Write($"{Convert.ToUInt32(parameterMode).ToString("X2")}{Convert.ToUInt32(parameterID).ToString("X2")}{"\r"}");
            Thread.Sleep(100);

            string fullResponse = "";
            while (!fullResponse.Contains(">"))
            {
                byte[] readBuffer = new byte[1024];
                Port.Read(readBuffer, 0, 1024);
                fullResponse = Encoding.Default.GetString(readBuffer);
            }

            return new ControllerResponse(fullResponse, parameterMode, parameterID);
        }

        public async ValueTask<ControllerResponse> QueryAsync(Vehicle.Mode parameterMode, Vehicle.PID parameterID)
        {
            Port.Write($"{Convert.ToUInt32(parameterMode).ToString("X2")}{Convert.ToUInt32(parameterID).ToString("X2")}{"\r"}");
            await Task.Delay(100);

            string fullResponse = "";
            while (!fullResponse.Contains(">"))
            {
                byte[] readBuffer = new byte[1024];
                Port.Read(readBuffer, 0, 1024);
                fullResponse = Encoding.Default.GetString(readBuffer);
            }

            return new ControllerResponse(fullResponse, parameterMode, parameterID);
        }

        /// <summary>
        /// Queries data from the vehicle by sending a specific mode
        /// </summary>
        /// <param name="parameterMode"> <c>Vehicle.Mode</c> used </param>
        /// <returns> <c>ControllerResponse</c> object holding the returned data from the controller unit </returns>
        /// <remarks> Blocking until a complete answer has been received </remarks>
        public async ValueTask<ControllerResponse> QueryAsync(Vehicle.Mode parameterMode)
        {
            Port.Write(Convert.ToUInt32(parameterMode).ToString("X2") + "\r");
            await Task.Delay(100);

            string fullResponse = "";
            while (!fullResponse.Contains(">"))
            {
                byte[] readBuffer = new byte[1024];
                Port.Read(readBuffer, 0, 1024);
                fullResponse = Encoding.Default.GetString(readBuffer);
            }

            return new ControllerResponse(fullResponse, parameterMode);
        }

        /// <summary>
        /// Queries data from the vehicle by sending a specific mode
        /// </summary>
        /// <param name="parameterMode"> <c>Vehicle.Mode</c> used </param>
        /// <returns> <c>ControllerResponse</c> object holding the returned data from the controller unit </returns>
        /// <remarks> Blocking until a complete answer has been received </remarks>
        public ControllerResponse Query(Vehicle.Mode parameterMode)
        {
            Port.Write(Convert.ToUInt32(parameterMode).ToString("X2") + "\r");
            Thread.Sleep(100);

            string fullResponse = "";
            while (!fullResponse.Contains(">"))
            {
                byte[] readBuffer = new byte[1024];
                Port.Read(readBuffer, 0, 1024);
                fullResponse = System.Text.Encoding.Default.GetString(readBuffer);
            }

            return new ControllerResponse(fullResponse, parameterMode);
        }

        private void Dispose(bool disposed)
        {
            if (!disposed)
            {
                Close();
            }
        }

        public void Dispose()
        {
            Dispose(disposed);
            disposed = true;
        }
    }
}
