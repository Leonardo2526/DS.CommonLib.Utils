﻿namespace DS.ClassLib.VarUtils
{
    /// <summary>
    /// Factory to create <see cref="IValidator{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidatorFactory<T>
    {
        /// <summary>
        /// Create a new <see cref="IValidator{T}"/>
        /// </summary>
        /// <returns></returns>
        IValidator<T> GetValidator();
    }

    /// <summary>
    /// Factory to create <see cref="IValidator"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidatorFactory
    {
        /// <summary>
        /// Create a new <see cref="IValidator"/>
        /// </summary>
        /// <returns></returns>
        IValidator GetValidator();
    }
}
