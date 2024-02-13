
namespace DS.ClassLib.VarUtils.Intersections
{
    public interface IIntersectionVisualizator<TItem1, TItem2>
    {
        void Show((TItem1, TItem2) intersection);
    }
}