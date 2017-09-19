namespace Obd2NET
{
    public class Vehicle
    {
        /// <summary>
        /// List of supported PIDs
        /// <see cref="http://en.wikipedia.org/wiki/OBD-II_PIDs#Mode_1_PID_03"/>
        /// </summary>
        public enum PID
        {
            Unknown = 0x0,
            MIL = 0x01,
            DTCCount = 0x01,
            Speed = 0x0D,
            EngineTemperature = 0x05,
            RPM = 0x0C,
            ThrottlePosition = 0x11,
            CalculatedEngineLoadValue = 0x04,
            FuelPressure = 0x0A
        };

        /// <summary>
        /// List of supported modes
        /// <see cref="http://en.wikipedia.org/wiki/OBD-II_PIDs#Mode_1_PID_03"/>
        /// </summary>
        public enum Mode
        {
            Unknown = 0x00,
            CurrentData = 0x01,
            FreezeFrameData = 0x02,
            DiagnosticTroubleCodes = 0x03
        }
    }
}
