using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.TraceUtils
{
    public abstract class SWBuilder
    {
        public SWBuilder(string expDirName, bool append)
        {
            ExpDirName = expDirName;
            Append = append;
        }

        protected string ExpDirName { get; }
        protected bool Append { get; }

        public abstract StreamWriter Build();
    }
}
