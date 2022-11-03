using System.Threading.Tasks;
using System.Windows;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// Interface to create task events.
    /// </summary>
    public interface ITaskEvent
    {
        /// <summary>
        /// Create a new task event.
        /// </summary>
        /// <returns></returns>
        public Task Create();
    }

    /// <summary>
    /// Interface to create task events with window's state checker.
    /// </summary>
    public interface IWindowTaskEvent : ITaskEvent
    {
        /// <summary>
        /// Check if window is closed.
        /// </summary>
        public bool WindowClosed { get; }
    }
}
