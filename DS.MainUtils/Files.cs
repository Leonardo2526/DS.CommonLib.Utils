using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.MainUtils
{
    public static class Files
    {
        /// <summary>
        /// Check if file is empty;
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns>Return true if file by fullPath is empty. Return null if it isn't.</returns>
        public static bool IsFileEmpty(string fullPath)
        {
            long fileLength = new FileInfo(fullPath).Length;

            if (fileLength == 0 || (fileLength == 3 && File.ReadAllBytes(fullPath).SequenceEqual(new byte[] { 239, 187, 191 })))
            {
                return true;
            }
            return false;
        }
    }
}
