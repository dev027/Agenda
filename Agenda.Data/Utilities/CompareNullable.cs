// <copyright file="CompareNullable.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;

namespace Agenda.Data.Utilities
{
    /// <summary>
    /// Compare routines that handles if one/both of the values are null.
    /// </summary>
    public static class CompareNullable
    {
        /// <summary>
        /// Compares two string values.
        /// </summary>
        /// <param name="a">1st value.</param>
        /// <param name="b">2nd value.</param>
        /// <returns>Returns true if the values are equal.</returns>
        public static bool AreEqual(string a, string b)
        {
            return string.IsNullOrEmpty(a) ? string.IsNullOrEmpty(b) : a == b;
        }

        /// <summary>
        /// Compares two integer values.
        /// </summary>
        /// <param name="a">1st value.</param>
        /// <param name="b">2nd value.</param>
        /// <returns>Returns true if the values are equal.</returns>
        public static bool AreEqual(int? a, int? b)
        {
            return a == null ? b == null : a == b;
        }

        /// <summary>
        /// Compares two decimal values.
        /// </summary>
        /// <param name="a">1st value.</param>
        /// <param name="b">2nd value.</param>
        /// <returns>Returns true if the values are equal.</returns>
        public static bool AreEqual(decimal? a, decimal? b)
        {
            return a == null ? b == null : a == b;
        }

        /// <summary>
        /// Compares two datetime values.
        /// </summary>
        /// <param name="a">1st value.</param>
        /// <param name="b">2nd value.</param>
        /// <returns>Returns true if the values are equal.</returns>
        public static bool AreEqual(DateTime? a, DateTime? b)
        {
            return a == null ? b == null : a == b;
        }
    }
}
