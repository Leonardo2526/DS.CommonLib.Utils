using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Connection
{
    public interface IConnectionService<T>
    {
        ISpecifyConnection<T> Build(
            T item1,
            T item2);
    }

    public interface ISpecifyConnection<T>
    {
        T TryConnect();
        Task<T> TryConnectAsync();
    }

    public abstract class ConnectionServiceBase<T> : IConnectionService<T>
    {
        public abstract ISpecifyConnection<T> Build(T item1, T item2);

        protected abstract class ConnectionSpecificatorBase : ISpecifyConnection<T>
        {
            protected T _item1, _item2;

            public ConnectionSpecificatorBase(T item1, T item2)
            {
                _item1 = item1;
                _item2 = item2;
            }

            public abstract T TryConnect();

            public abstract Task<T> TryConnectAsync();
        }
    }
}
