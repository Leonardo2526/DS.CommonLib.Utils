using System;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// The object to run operations wtih <see cref="TimeSpan"/> 
    /// to calculate operation time.
    /// </summary>
    public static class TimeSpanRunner
    {
        /// <summary>
        /// Run <paramref name="action"/> and calculate operation time.
        /// </summary>
        /// <param name="action"></param>
        /// <returns>
        /// <see cref="TimeSpan"/> of <paramref name="action"/> perform.
        /// </returns>
        public static TimeSpan Run(Action action)
        {
            var date1 = DateTime.Now;
            action.Invoke();
            var date2 = DateTime.Now;
            return date2 - date1;
        }
    }
}
