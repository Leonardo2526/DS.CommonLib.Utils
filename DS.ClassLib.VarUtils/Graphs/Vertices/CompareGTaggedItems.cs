using QuickGraph;
using System.Collections.Generic;

namespace DS.ClassLib.VarUtils.Graphs
{
    /// <summary>
    /// 
    /// </summary>
    public class CompareGTaggedVertex : IEqualityComparer<TaggedGVertex<int>>
    {
        public bool Equals(TaggedGVertex<int> x, TaggedGVertex<int> y)
        {
            return x.Tag == y.Tag;
        }

        public int GetHashCode(TaggedGVertex<int> obj)
        {
            return obj.Tag.GetHashCode();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CompareGTaggedEdge : IEqualityComparer<TaggedEdge<IVertex, int>>
    {
        public bool Equals(TaggedEdge<IVertex, int> x, TaggedEdge<IVertex, int> y)
        {
            var equalVerex = (x.Source.Id == y.Source.Id && x.Target.Id == y.Target.Id)
                || (x.Target.Id == y.Source.Id && x.Source.Id == y.Target.Id);
            return (x.Tag == y.Tag && equalVerex);
        }

        public int GetHashCode(TaggedEdge<IVertex, int> obj)
        {
            return obj.Tag.GetHashCode();
        }
    }
}
