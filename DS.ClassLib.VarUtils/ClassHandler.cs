using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils
{
    public class ClassHandler
    {
        public delegate void MyHandler();
        public event MyHandler RollBackHandler;
    }
}
