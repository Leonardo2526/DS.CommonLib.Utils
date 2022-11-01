using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// An object to create a new task event for <see cref="Window"/>.
    /// </summary>
    public class WindowTaskEvent : ITaskEvent
    {
        private readonly Window _window;
        private readonly List<Button> _buttons;

        /// <summary>
        /// Create a new instance of object to build a new task events
        /// from <paramref name="window"/> and <paramref name="buttons"/>.
        /// </summary>
        /// <param name="window"><see cref="Window"/> to create task event.</param>
        /// <param name="buttons"><see cref="List{Button}"/> 
        /// of <see cref="Button"/> elements to create task events.</param>
        public WindowTaskEvent(Window window, List<Button> buttons = null)
        {
            _window = window;
            _buttons = buttons;
        }

        /// <summary>
        /// Check if <see cref="_window"/> is closed.
        /// </summary>
        public bool WindowClosed { get; private set; }

        /// <summary>
        /// Create a new task event.
        /// </summary>
        /// <returns></returns>
        public Task Create()
        {
            var tcs = new TaskCompletionSource<object>();
            void windhandler(object s, EventArgs e)
            {
                WindowClosed = true;
                tcs.TrySetResult(null);
            };
            _window.Closed += windhandler;

            void buttonHandler(object s, EventArgs e) => tcs.TrySetResult(null);
            if (_buttons.Any())
            {
                _buttons.ForEach(button => button.Click += buttonHandler);
            }

            return tcs.Task.ContinueWith(_ =>
            {
                _window.Closed -= windhandler;

                if (_buttons.Any())
                {
                    _buttons.ForEach(button => button.Click -= buttonHandler);
                }
            });
        }
    }
}
