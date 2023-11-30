using DS.ClassLib.VarUtils;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.RevitLib.Utils.Various
{
    /// <summary>
    /// The interface is used to select <typeparamref name="T"/> objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISelector<T>
    {
        /// <summary>
        /// Select <typeparamref name="T"/> object.
        /// </summary>
        /// <returns>
        /// Selected <typeparamref name="T"/> object.
        /// </returns>
        T Select();
    }


    /// <summary>
    /// The interface is used to select <typeparamref name="T"/> objects and validate them.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidatableSelector<T> : ISelector<T>
    {
        /// <summary>
        /// Validators validator for <typeparamref name="T"/> objects.
        /// </summary>
        IEnumerable<IValidator<T>> Validators { get; }
    }
}
