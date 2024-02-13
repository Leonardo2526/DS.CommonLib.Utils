using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DS.ClassLib.VarUtils.Iterators
{
    public static class ValidationResultsConverter
    {
        /// <summary>
        /// Get all <see cref="ValidationResult"/>s with <see cref="StringBuilder"/>.
        /// </summary>
        /// <returns>
        /// <see cref="StringBuilder"/> results if any error messages have been occured.
        /// <para>
        /// Otherwise <see langword="null"/>.
        /// </para>
        /// </returns>
        public static string ToString(IEnumerable<ValidationResult> validationResults)
        {
            if (validationResults.Count() == 0) { return null; }

            var messageBuilder = new StringBuilder();
            if (validationResults.Count() == 1)
            {
                messageBuilder.AppendLine(validationResults.First().ErrorMessage);
                return messageBuilder.ToString();
            }

            for (int i = 0; i < validationResults.Count(); i++)
            {
                var r = validationResults.ElementAt(i);
                messageBuilder.AppendLine($"Ошибка {i + 1}. {r.ErrorMessage}");
                messageBuilder.AppendLine("---------");
            }

            return messageBuilder.ToString();
        }
    }
}
