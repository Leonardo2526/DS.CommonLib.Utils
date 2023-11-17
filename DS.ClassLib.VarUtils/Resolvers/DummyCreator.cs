using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Resolvers
{
    public class DummyCreator : IEnumerator<IResolveTask>
    {
        public IResolveTask Current { get; private set; }

        object IEnumerator.Current => this.Current;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            Current = new DummyTask();
            return true;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
