﻿using System;
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
                //TODO: This is a temporary fix to fetch all DTC's.
                if (RequestedMode == Vehicle.Mode.DiagnosticTroubleCodes) {
                    return String.Join(string.Empty, Raw
                            .Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries)
                            .Where(_ => _.Split(' ').Count(number => number.Length == 2) >= 2)
                            .Select(_ => _.Split(' ').Skip(1))
                            .SelectMany(_ => _))
                        .ToByteArray();
                }
                if(RequestedPID != Vehicle.PID.Unknown && RequestedMode != Vehicle.Mode.Unknown)
                {
                    Match matchedPattern = Regex.Match(Raw, @"\n([0-9a-fA-F ]{5})([0-9a-fA-F ]+)\r\n>");
                    return (matchedPattern.Groups.Count > 2) ? matchedPattern.Groups[2].Value.Replace(" ", "").ToByteArray() : Raw.ToByteArray();
                }
                else if (RequestedPID == Vehicle.PID.Unknown)
                {
                    Match matchedPattern = Regex.Match(Raw, @"\n([0-9a-fA-F]{2})([0-9a-fA-F ]+)\r\n>");
                    return (matchedPattern.Groups.Count > 2) ? matchedPattern.Groups[2].Value.Replace(" ", "").ToByteArray() : Raw.ToByteArray();
                }
                else
                {
                    Match matchedPattern = Regex.Match(Raw, @"\n([0-9a-fA-F ]+)\r\n>");
                    return (matchedPattern.Groups.Count > 1) ? matchedPattern.Groups[1].Value.Replace(" ", "").ToByteArray() : Raw.ToByteArray();
                }
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
