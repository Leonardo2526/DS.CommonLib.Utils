﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DS.ClassLib.VarUtils.Selectors
{
    /// <summary>
    /// The interface is used to select <typeparamref name="T"/> objects and validate them.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidatableSelector<T> : ISelector<T>, IResettable
    {
        /// <summary>
        /// Validate object by all validators or only to first with not valid result.
        /// </summary>
        public bool CheckAllValidators { get; set; }

        /// <summary>
        /// Selected <typeparamref name="T"/>.
        /// </summary>
        public T SelectedItem { get; }

        /// <summary>
        /// <typeparamref name="T"/> validity.
        /// </summary>
        public bool IsValid { get; }

        /// <summary>
        /// Validators validator for <typeparamref name="T"/> objects.
        /// </summary>
        IEnumerable<IValidator<T>> Validators { get; }
    }
}
