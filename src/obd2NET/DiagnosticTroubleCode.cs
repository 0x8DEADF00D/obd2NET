using System;
using System.Linq;
using System.Text;

namespace Obd2NET
{
    /// <summary>
    /// Represents a Diagnostic trouble code which is used for reporting errors within the vehicle. 
    /// Usually the different controlling units report them by providing 5 bits containing the <see cref="Location">Location</see> and additional values
    /// </summary>
    public class DiagnosticTroubleCode
    {
        /// <summary>
        /// First 2 bits returned by the controller, indicates the location of failure
        /// </summary>
        public enum Location
        {
            Powertrain = 0x00,
            Chassis = 0x01,
            Body = 0x02,
            Network = 0x03
        }

        /// <summary>
        /// Raw and complete trouble code
        /// </summary>
        public byte[] Code { get; set; }

        /// <summary>
        /// Human readable text representation
        /// </summary>
        /// <remarks>
        /// <see cref="http://en.wikipedia.org/wiki/OBD-II_PIDs#Mode_3_.28no_PID_required.29"/> for information regarding bit shifting and bit mapping </remarks>
        public string TextRepresentation
        {
            get
            {
                var sb = new StringBuilder();
                switch (ErrorLocation)
                {
                    case Location.Powertrain: { sb.Append("P"); break; }
                    case Location.Chassis: { sb.Append("C"); break; }
                    case Location.Body: { sb.Append("B"); break; }
                    case Location.Network: { sb.Append("U"); break; }
                }

                byte firstByte = Code.First();
                sb.Append((firstByte >> 4) & 3);
                sb.Append(Convert.ToInt32(((firstByte >> 0) & 15).ToString(), 16));

                byte secondByte = Code.ElementAt(1);
                sb.Append(Convert.ToInt32(((secondByte >> 4) & 15).ToString(), 16));
                sb.Append(Convert.ToInt32(((secondByte >> 0) & 15).ToString(), 16));

                return sb.ToString();
            }
        }

        /// <summary>
        /// Location of failure
        /// </summary>
        public Location ErrorLocation
        {
            get
            {
                return (Location)((Code.First() >> 6) & 3);
            }
        }

        public DiagnosticTroubleCode(string code)
        {
            Code = Encoding.Default.GetBytes(code);
        }

        public DiagnosticTroubleCode(byte[] code)
        {
            Code = code;
        }
    }
}
