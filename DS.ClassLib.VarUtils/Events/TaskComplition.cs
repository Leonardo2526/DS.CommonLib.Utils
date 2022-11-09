using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Events
{
    public class TaskComplition
    {
        private readonly IEvent<EventType> _eventObject;

        /// <summary>
        /// Create a new instance of object to build a new task events with the <paramref name="eventObject"/>.
        /// </summary>
        /// <param name="eventObject"></param>
        public TaskComplition(IEvent<EventType> eventObject)
        {
            _eventObject = eventObject;
        }

        public EventType EventType { get; private set; }

        /// <summary>
        /// Create a new completion task and subscribe it's result setting to <see cref="_eventObject"/> event.
        /// </summary>
        /// <returns>Returns completion task with unsubscribing after event invoking.</returns>
        public Task Create()
        {
            var tcs = new TaskCompletionSource<object>();
            void handler(object s, EventType commandType)
            {
                EventType = commandType;
                tcs.TrySetResult(null);
            };

            _eventObject.Event += handler;

            return tcs.Task.ContinueWith(_ =>
            {
                _eventObject.Event -= handler;
            });
        }
    }
}
