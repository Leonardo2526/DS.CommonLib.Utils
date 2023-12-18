using DS.ClassLib.VarUtils;
using DS.ClassLib.VarUtils.Extensions.Tuples;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Windows.Controls.Primitives;

namespace DS.ClassLib.VarUtils.Selectors
{
    /// <summary>
    /// The object to select <typeparamref name="T"/> and validate it.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValidatableSelector<T> : IValidatableSelector<T>
    {
        private T _selectedItem;
        private bool _isValid;
        private readonly Queue<Func<T>> _selectorsQueue = new();
        private readonly IEnumerable<Func<T>> _selectors;

        /// <summary>
        /// Instansiate the object to select <typeparamref name="T"/> with <paramref name="selectors"/> and validate it.
        /// </summary>
        /// <param name="selectors"></param>
        public ValidatableSelector(IEnumerable<Func<T>> selectors)
        {
            if (selectors is null)
            {
                throw new ArgumentNullException(nameof(selectors));
            }
            _selectors = selectors;
        }

        #region Properties

        /// <inheritdoc/>
        public IEnumerable<IValidator<T>> Validators { get; set; } =
          new List<IValidator<T>>();

        /// <inheritdoc/>
        public T SelectedItem => _selectedItem;

        /// <inheritdoc/>
        public bool IsValid => _isValid;

        /// <summary>
        /// The core Serilog, used for writing log events.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Messenger to show errors.
        /// </summary>
        public IWindowMessenger Messenger { get; set; }

        /// <inheritdoc/>
        public bool CheckAllValidators { get; set; }

        #endregion


        /// <inheritdoc/>
        public T Select()
        {
            Reset();
            while ((_selectedItem == null || _selectedItem.IsTupleNull()) && _selectorsQueue.Count > 0)
            {
                try
                {
                    _selectedItem = _selectorsQueue.Dequeue().Invoke();
                }
                catch (Exception ex)
                {
                    Logger?.Information(ex.Message);
                }
            }

            if (_selectedItem == null || _selectedItem.IsTupleNull())
            {
                Logger?.Warning("Selected item is null.");
                return default;
            }

            if (CheckAllValidators)
            {
                var results = new List<ValidationResult>();
                foreach (var validator in Validators)
                {
                    if (!validator.IsValid(_selectedItem))
                    {
                        results.AddRange(validator.ValidationResults);
                    }
                }
                _isValid = results.Count == 0;
            }
            else
            {
                _isValid = Validators.ToList().TrueForAll(v => v.IsValid(_selectedItem));
            }

            if (!_isValid)
            { Logger?.Warning("Selected item is not valid."); }

            if (Messenger != null)
            { ShowResults(); }

            return _selectedItem;
        }

        /// <summary>
        /// Get all <see cref="ValidationResult"/>s with <see cref="StringBuilder"/>.
        /// </summary>
        /// <returns>
        /// <see cref="StringBuilder"/> results if any error messages have been occured.
        /// <para>
        /// Otherwise <see langword="null"/>.
        /// </para>
        /// </returns>
        protected virtual StringBuilder GetValidationResults()
        {
            var results = new List<ValidationResult>();
            Validators.ToList().ForEach(v => results.AddRange(v.ValidationResults));
            var messageBuilder = new StringBuilder();
            if (results.Count == 1)
            { messageBuilder.AppendLine(results.First().ErrorMessage); }
            else if (results.Count > 1)
                for (int i = 0; i < results.Count; i++)
                {
                    var r = results[i];
                    messageBuilder.AppendLine($"Ошибка {i + 1}. {r.ErrorMessage}");
                    messageBuilder.AppendLine("---------");
                }

            return messageBuilder;
        }

        /// <summary>
        /// Show validation results with <see cref="Messenger"/>.
        /// </summary>
        private void ShowResults()
        {
            var messageBuilder = GetValidationResults();
            if (messageBuilder.Length > 0)
            { Messenger.Show(messageBuilder.ToString(), "Ошибка"); }
        }

        /// <inheritdoc/>
        public bool TryReset()
        {
            _selectors.ToList().
                Select(s => s.Target).
                OfType<IResettable>().ToList().
                ForEach(s => s.TryReset());
            return true;
        }

        /// <inheritdoc/>
        public void Reset()
        {
            _selectedItem = default;
            _isValid = false;
            _selectorsQueue.Clear();
            _selectors.ToList().ForEach(_selectorsQueue.Enqueue);
        }
    }
}