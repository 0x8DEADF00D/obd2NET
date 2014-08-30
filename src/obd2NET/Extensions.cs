using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obd2NET
{
    public static class Extensions
    {
        /// <summary>
        /// Converts a hex string into a byte array
        /// </summary>
        /// <param name="hex"> String to convert </param>
        /// <returns> Converted byte array </returns>
        public static byte[] ToByteArray(this string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }
}
