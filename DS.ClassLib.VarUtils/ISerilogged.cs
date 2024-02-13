using Serilog;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// The core Serilog logging API, used for writing log events.
    /// </summary>
    public interface ISerilogged
    {
        /// <summary>
        /// Logger used for writing log events.
        /// </summary>
        public ILogger Logger { get; set; }
    }
}
