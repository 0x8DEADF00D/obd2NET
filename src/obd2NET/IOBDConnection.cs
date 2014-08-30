using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obd2NET
{
    /// <summary>
    /// Interface to be implemented by all connections that are interacting with the OBD interface of the vehicle
    /// Backwards compatible, meaning that even OBD1 may work here. Since OBD1 is not standardized, manufacturer specific interfaces need to be implemented
    /// </summary>
    public interface IOBDConnection
    {
        void Open();
        void Close();
        ControllerResponse Query(Vehicle.Mode parameterMode, Vehicle.PID parameterID);
        ControllerResponse Query(Vehicle.Mode parameterMode);
    }
}
