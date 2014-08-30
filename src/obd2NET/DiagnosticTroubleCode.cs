using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obd2NET
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
        public Byte[] Code { get; set; }

        /// <summary>
        /// Human readable text representation
        /// </summary>
        /// <remarks>
        /// <see cref="http://en.wikipedia.org/wiki/OBD-II_PIDs#Mode_3_.28no_PID_required.29"/> for information regarding bit shifting and bit mapping </remarks>
        public string TextRepresentation
        {
            get
            {
                string representation = "";
                switch(ErrorLocation)
                {
                    case Location.Powertrain: { representation = "P"; break; }
                    case Location.Chassis: { representation = "C"; break; }
                    case Location.Body: { representation = "B"; break; }
                    case Location.Network: { representation = "U"; break; }
                }

                Byte firstByte = Code.First();
                representation += (firstByte >> 4) & 3;
                representation += Convert.ToInt32(((firstByte >> 0) & 15).ToString(), 16);

                Byte secondByte = Code.ElementAt(1);
                representation += Convert.ToInt32(((secondByte >> 4) & 15).ToString(), 16);
                representation += Convert.ToInt32(((secondByte >> 0) & 15).ToString(), 16); 

                return representation;
            }
        }

        /// <summary>
        /// Location of failure
        /// </summary>
        public Location ErrorLocation
        {
            get
            {
                Byte firstByte = Code.First();
                return (Location) ((firstByte >> 6) & 3);
            }
        }

        public DiagnosticTroubleCode(string code)
        {
            Code = System.Text.Encoding.Default.GetBytes(code);
        }

        public DiagnosticTroubleCode(Byte[] code)
        {
            Code = code;
        }
    }
}
