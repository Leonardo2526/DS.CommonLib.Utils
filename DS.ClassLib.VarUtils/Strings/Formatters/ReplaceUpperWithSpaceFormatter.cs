using System;
using System.Text;
using System.Threading;

namespace DS.ClassLib.VarUtils.Strings.Formatters
{
    /// <summary>
    /// Format <see cref="string"/> by replacing upper case symbols with space.
    /// <para>
    /// The formatting starts from symbol at index 1.
    /// </para>
    /// </summary>
    public sealed class ReplaceUpperWithSpaceFormatter : IFormatProvider, ICustomFormatter
    {
        /// <summary>
        /// Determine whether to preserve acronyms.
        /// </summary>
        public bool PreserveAcronyms { get; set; }

        /// <summary>
        /// Determine whether to convert upper case symbols to lower case.
        /// </summary>
        public bool ToLowerCase { get; set; }

        /// <inheritdoc/>
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter)) return this;
            return Thread.CurrentThread.CurrentCulture.GetFormat(formatType);
        }

        /// <inheritdoc/>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            string result;
            if (arg is not IFormattable formattable) result = arg.ToString();
            else result = formattable.ToString(format, formatProvider);

            if (string.IsNullOrWhiteSpace(result))
                return string.Empty;
            var newText = new StringBuilder(result.Length * 2);
            newText.Append(result[0]);
            for (int i = 1; i < result.Length; i++)
            {
                if (char.IsUpper(result[i]))

                    if ((result[i - 1] != ' ' && !char.IsUpper(result[i - 1])) ||
                        (PreserveAcronyms && char.IsUpper(result[i - 1]) &&
                         i < result.Length - 1 && !char.IsUpper(result[i + 1])))
                        newText.Append(' ');
                char c = result[i];
                if (ToLowerCase)
                { c = Convert.ToChar(c.ToString().ToLower()); }
                newText.Append(c);
            }
            result = newText.ToString();
            return result;
        }
    }
}
