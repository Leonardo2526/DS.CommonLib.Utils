using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils
{
    public static class OperationCreator
    {
        public static string LongOperation(CancellationTokenSource innerTokenSource, 
            CancellationTokenSource totalTokenSource = null)
        {
            string s = null;
            for (int i = 0; i < 30000000; i++)
            {
                innerTokenSource.Token.ThrowIfCancellationRequested();
                if (totalTokenSource is not null)
                {
                    totalTokenSource.Token.ThrowIfCancellationRequested();
                }
                s = i.ToString();
            }

            return s;
        }
    }
}
