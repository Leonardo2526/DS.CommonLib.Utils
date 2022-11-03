using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.TaskEvents
{
    /// <summary>
    /// An object to create a new task event for <see cref="IEventHandler"/>.
    /// </summary>
    public class THandlerTaskEvent<T> : IWindowTaskEvent
    {
        private readonly IEventHandler<T> _eventHandler;

        /// <summary>
        /// Create a new instance of object to build a new task events with the <paramref name="eventHandler"/>.
        /// </summary>
        /// <param name="eventHandler"></param>
        public THandlerTaskEvent(IEventHandler<T> eventHandler)
        {
            _eventHandler = eventHandler;
        }

        ///<inheritdoc/>
        public bool WindowClosed { get; private set; }


        /// <summary>
        /// Create a new task event.
        /// </summary>
        /// <returns></returns>
        public Task Create()
        {
            var tcs = new TaskCompletionSource<object>();
            void handler(object s, T e)
            {
                tcs.TrySetResult(null);
                //MessageBox.Show($"handler called with object: {s.GetType().Name}, arg: {e}");
            };

            void windhandler(object s, T e)
            {
                WindowClosed = true;
                tcs.TrySetResult(null);
                //MessageBox.Show("windhandler called with arg: " + e.ToString());
            };
            _eventHandler.RollBackHandler += handler;
            _eventHandler.CloseWindowHandler += windhandler;


            return tcs.Task.ContinueWith(_ =>
            {
                _eventHandler.RollBackHandler -= handler;
                _eventHandler.CloseWindowHandler -= windhandler;
            });
        }
    }
}
