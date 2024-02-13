using Serilog;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DS.ClassLib.VarUtils.Iterators
{
    /// <summary>
    /// Instansiate a general validator for <paramref name="validators"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="validators"></param>
    public class ValidatorIterator<T>(IEnumerable<IValidator<T>> validators) : IValidator<T>
    {
        private readonly List<ValidationResult> _validationResults = new();
        //private readonly IEnumerable<IValidator<T>> _validators;
        private readonly IEnumerable<IValidator<T>> _validators = validators;

        /// <summary>
        /// The core Serilog, used for writing log events.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <inheritdoc/>
        public IEnumerable<ValidationResult> ValidationResults => _validationResults;

        /// <summary>
        /// Stop validation on first failed validator.
        /// </summary>
        public bool StopOnFirst { get; set; }

        /// <inheritdoc/>
        public bool IsValid(T value)
        {
            _validationResults.Clear();
            //var predicate = GetPredicate(value);
            int i = 0;
            foreach (var validator in _validators)
            {
                i++;
                Logger?.Information($"Validator {i} '{validator.GetType().Name}' is inititated.");
                var isValid = validator.IsValid(value);
                var validationResults = validator.ValidationResults;
                _validationResults.AddRange(validationResults);
                if (validationResults.Count() > 0)
                {
                    Logger?.Warning($"Validator {i} '{validator.GetType().Name}'  " +
                    $"errors count is {validationResults.Count()}.");
                }
                else
                { Logger?.Information($"Validator {i} '{validator.GetType().Name}' has no errors."); }
                if (!isValid && StopOnFirst) { return false; }
            }

            return _validationResults.Count == 0;
        }

        
    }
}
