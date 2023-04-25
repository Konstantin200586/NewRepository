using System.Text.RegularExpressions;

namespace SMBTools.Contract.Extensions
{
    public static class StringExtensions
    {
        public static string ConvertFromPascalCaseToCamelCase(this string pascalCase)
        {
            var camelCase = Char.ToLowerInvariant(pascalCase[0]) + pascalCase.Substring(1);

            return camelCase;
        }

        public static string ConvertFromPascalCaseToWords(this string pascalCase)
        {
            var wordsArray = Regex.Split(pascalCase, @"(?<!^)(?=[A-Z])");

            var words = wordsArray[0];

            for (int i = 1; i < wordsArray.Length; i++)
            {
                words += " " + wordsArray[i];
            }

            return words;
        }
    }
}