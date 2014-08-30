Introduction
===
obd2NET is a .NET libarary for querying the vehicles OBD system. Real time data and diagnostic codes can be retrieved by performing simple serial requests. Basically, querying works just as simple: You cast the query mode (i.e. `0x01` for the current data) and specify a PID (parameter ID) such as `0x0D` for the vehicle speed to get your information back.

However, when using this library, you don't need to worry on how to communicate with the vehicle, just plug in the OBD connector into your vehicle's OBD port and get the other end of the cable to communicate with your device (USB/serial interfaces).

Examples
===
Opening the connection is as easy as it gets:

    IOBDConnection conn = new SerialConnection("COM5"); // you may need to replace the COM port
    
Getting the current (real-time) speed of the vehicle given in km/h:

    uint speed = Vehicle.Speed(conn);
    
    
Obtaining available DTCs (Diagnostic Trouble Codes)


    var networkTroubleCodes = Vehicle.DiagnosticTroubleCodes(conn).Where(code => code.ErrorLocation == DiagnosticTroubleCode.Location.Network);

    foreach (var code in networkTroubleCodes)
        Console.WriteLine(code.TextRepresentation);
    
The given code will query all network trouble codes and print its text representation to the screen.
           

Compatibility
===
Of course, not all vehicles around are implementing the ISO 9141 standard so this library might not work for your vehicle. Before OBD2, interfaces were manufacturer specific implemented so you'd rather look into your manufacturers implementation details and extend the library by yourself.


License
===
  
    The MIT License (MIT)
    
    Copyright (c) 2014 
    
    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:
    
    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.
    
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
