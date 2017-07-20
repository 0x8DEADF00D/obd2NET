using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace obd2NET
{
    /// <summary>
    /// Represents the response received from the controller unit.
    /// Answers are supplied with the following syntax:
    /// <c>010D\r\r41 0D 4B\r></c>
    /// <c>010D</c> being the requested PID and <c>41 0D 4B</c> being the value that the controller unit supplied. 
    /// The <c>></c> character is the stop byte for signaling that the data is complete
    /// </summary>
    public class ControllerResponse
    {
        public string Raw { get; set; }
        public Vehicle.PID RequestedPID { get; set; }
        public Vehicle.Mode RequestedMode { get; set; }

        public Byte[] Value
        {
            get
            {
                return String.Join(string.Empty, Raw
                        .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                        .Where(_ => _.Split(' ').Count(number => number.Length == 2) >= 2)
                        .Select(_ => _.Split(' ').Skip(1))
                        .SelectMany(_ => _))
                    .ToByteArray();
            }
        }

        public bool HasValidData()
        {
            return Raw.Contains("NO DATA");
        }

        public ControllerResponse(string raw, Vehicle.Mode requestedMode = Vehicle.Mode.Unknown, Vehicle.PID requestedPID = Vehicle.PID.Unknown)
        {
            Raw = raw;
            RequestedPID = requestedPID;
            RequestedMode = requestedMode;
        }
    }
}
