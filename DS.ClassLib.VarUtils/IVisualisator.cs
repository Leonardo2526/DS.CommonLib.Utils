namespace DS.ClassLib.VarUtils
{
    public interface IVisualisator
    {
        public void Show();
    }

    public interface IObjectVisualisator
    {
        public void Show(object objectToShow);
    }

    /// <summary>
    /// The interface is used to show <typeparamref name="T"/> poins. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPointVisualisator<T>
    {
        /// <summary>
        /// Show <typeparamref name="T"/> poins.
        /// </summary>
        /// <param name="point"></param>
        public void Show(T point);
    }
}
