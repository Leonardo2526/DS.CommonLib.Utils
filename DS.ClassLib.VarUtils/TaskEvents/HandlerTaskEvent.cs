using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace DS.ClassLib.VarUtils.TaskEvents
{
    /// <summary>
    /// An object to create a new task event for <see cref="IEventHandler"/>.
    /// </summary>
    public class HandlerTaskEvent : IWindowTaskEvent
    {
        private readonly IEventHandler _objectHandler;

        /// <summary>
        /// Create a new instance of object to build a new task events with the <paramref name="objectHandler"/>.
        /// </summary>
        /// <param name="objectHandler"></param>
        public HandlerTaskEvent(IEventHandler objectHandler)
        {
            _objectHandler = objectHandler;
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
            void handler(object s, EventArgs e)
            {
                tcs.TrySetResult(null);
            };

            void windhandler(object s, EventArgs e)
            {
                WindowClosed = true;
                tcs.TrySetResult(null);
            };
            _objectHandler.RollBackHandler += handler;
            _objectHandler.CloseWindowHandler += windhandler;


            return tcs.Task.ContinueWith(_ =>
            {
                _objectHandler.RollBackHandler -= handler;
                _objectHandler.CloseWindowHandler -= windhandler;
            });
        }
    }
}
