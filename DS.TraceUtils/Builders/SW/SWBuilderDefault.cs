using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.TraceUtils
{
    public class SWBuilderDefault : SWBuilder
    {
        public SWBuilderDefault(string expDirName, bool append = false) : base(expDirName, append)
        { }

        public override StreamWriter Build()
        {
            return new StreamWriter(ExpDirName, Append, Encoding.UTF8);
        }
    }
}
