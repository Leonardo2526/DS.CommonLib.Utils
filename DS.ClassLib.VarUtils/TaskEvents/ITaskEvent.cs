using System.Threading.Tasks;

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
}
