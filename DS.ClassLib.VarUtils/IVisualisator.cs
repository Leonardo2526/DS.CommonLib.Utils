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

    public interface IPointVisualisator<T>
    {
        public void Show(T point);
    }
}
