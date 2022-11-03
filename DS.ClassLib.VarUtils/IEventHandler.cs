using System;

namespace DS.ClassLib.VarUtils
{
    public interface IEventHandler
    {
        public event EventHandler RollBackHandler;
        public event EventHandler CloseWindowHandler;
    }

    public interface IEventHandler<T>
    {
        public event EventHandler<T> RollBackHandler;
        public event EventHandler<T> CloseWindowHandler;
    }
}
