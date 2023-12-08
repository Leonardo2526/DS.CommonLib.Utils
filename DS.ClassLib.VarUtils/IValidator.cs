using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// The interface represents validator for <typeparamref name="T"/> objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidator<T>
    {
        /// <summary>
        /// A collection that holds failed-validation information.
        /// </summary>
        IEnumerable<ValidationResult> ValidationResults { get; }

        /// <summary>
        /// Specifies if <paramref name="value"/> is valid.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool IsValid(T value);
    }
}
