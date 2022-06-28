using System.Text.RegularExpressions;

namespace DS.MainUtils.Strings
{
    public static class StringExtensions
    {
        /// <summary>
        /// Check if string contains special characters.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Return true if string contains only numbers or letters. Return false if it isn't.</returns>
        public static bool IsNoSpecialSymbol(this string input)
        {
            string pattern = PatternSelector.Select(RegexPatterns.NoSpecialChar);
            return Regex.IsMatch(input, pattern);
        }

        /// <summary>
        /// Check if string is a number.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Return true if string contains only numbers. Return false if it isn't.</returns>
        public static bool IsNumber(this string input)
        {
            string pattern = PatternSelector.Select(RegexPatterns.Number);
            return Regex.IsMatch(input, pattern);
        }

        /// <summary>
        /// Check if string is a natural number.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Return true if string contains only natural numbers. Return false if it isn't.</returns>
        public static bool IsNaturalNumber(this string input)
        {
            string pattern = PatternSelector.Select(RegexPatterns.NaturalNumber);
            return Regex.IsMatch(input, pattern);
        }

        /// <summary>
        /// Check if string is a integer number.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Return true if string a integer number. Return false if it isn't.</returns>
        public static bool IsInt(this string input)
        {
            var isNumeric = int.TryParse(input, out int result);
            return isNumeric;
        }

    }
}
