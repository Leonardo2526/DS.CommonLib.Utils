using DS.ClassLib.VarUtils.BinaryOperations;
using MoreLinq;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DS.ClassLib.VarUtils.RhinoUtils
{
    /// <summary>
    /// Instansiate the object to perform base binary operations between 
    /// <see cref="BoundingBox"/> and collection of <see cref="BoundingBox"/>.
    /// </summary>
    public class BoundingBoxBinaryOperationsMany() : IBinaryOperationsMany<BoundingBox>
    {
        /// <inheritdoc/>
        public BoundingBox Difference(BoundingBox item1, IEnumerable<BoundingBox> items2)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public BoundingBox Intersection(BoundingBox item1, IEnumerable<BoundingBox> items2)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public BoundingBox SymmetricDifference(BoundingBox item1, IEnumerable<BoundingBox> items2)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public BoundingBox Union(BoundingBox item1, IEnumerable<BoundingBox> items2)
        {
            var result = new BoundingBox(item1.Min, item1.Max);
            foreach (var item2 in items2)
            { result.Union(item2); }
            return result;
        }
    }
}
