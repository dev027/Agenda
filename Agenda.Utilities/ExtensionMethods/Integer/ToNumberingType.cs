// <copyright file="ToNumberingType.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agenda.Utilities.ExtensionMethods.Integer
{
    /// <summary>
    /// Integer Extension Methods.
    /// </summary>
    public static partial class IntegerExtensionMethods
    {
        private static readonly Dictionary<string, int> RomanMapping = new Dictionary<string, int>
        {
            { "m", 1000 },
            { "cm", 900 },
            { "d", 500 },
            { "cd", 400 },
            { "c", 100 },
            { "xc", 90 },
            { "l", 50 },
            { "xl", 40 },
            { "x", 10 },
            { "ix", 9 },
            { "v", 5 },
            { "iv", 4 },
            { "i", 1 }
        };

        /// <summary>
        /// Converts to number to the specified numbering type.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="numberingType">Numbering Type required.</param>
        /// <param name="suffix">Optional suffix. Default is ". ".</param>
        /// <param name="prefix">Optional prefix. Default is "".</param>
        /// <returns>String version of the  number in the correct numbering type.</returns>
        public static string ToNumberingType(
            this int number,
            NumberingType numberingType,
            string suffix = ". ",
            string prefix = "")
        {
            switch (numberingType)
            {
                case NumberingType.None:
                    return string.Empty;

                case NumberingType.Number:
                    return $"{prefix}{number}{suffix}";

                case NumberingType.LetterLower:
                    return $"{prefix}{number.ToLetterLower()}{suffix}";

                case NumberingType.LetterUpper:
                    return $"{prefix}{number.ToLetterLower().ToUpperInvariant()}{suffix}";

                case NumberingType.RomanLower:
                    return $"{prefix}{number.ToRomanLower()}{suffix}";

                case NumberingType.RomanUpper:
                    return $"{prefix}{number.ToRomanLower().ToUpperInvariant()}{suffix}";

                default:
                    throw new ArgumentOutOfRangeException(nameof(numberingType), numberingType, null);
            }
        }

        /// <summary>
        /// Converts number to letter lowercase.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>Letter as lower case.</returns>
        public static string ToLetterLower(this int number)
        {
            if (number < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(number),
                    number,
                    @"Number must be positive");
            }

            string value = string.Empty;

            while (number > 0)
            {
                int digitNum = number % 26;
                value = $"{Convert.ToChar(digitNum + 65 + 32)}";
                number /= 26;
            }

            return value;
        }

        /// <summary>
        /// Converts number to lowercase Roman numerals.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>The number as lowercase roman numerals.</returns>
        public static string ToRomanLower(this int number)
        {
            StringBuilder sb = new StringBuilder();

            foreach ((string key, int value) in RomanMapping)
            {
                sb.Append(Enumerable.Repeat(key, number / value));
                number %= value;
            }

            return sb.ToString();
        }
    }
}