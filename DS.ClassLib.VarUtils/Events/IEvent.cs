using System;

namespace DS.ClassLib.VarUtils
{
    public interface IEvent<T>
    {
        public event EventHandler<T> Event;
    }
}
