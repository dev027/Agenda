// <copyright file="NumberingType.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

namespace Agenda.Utilities.ExtensionMethods.Integer
{
    /// <summary>
    /// Numbering Type.
    /// </summary>
    public enum NumberingType
    {
        /// <summary>
        /// None
        /// </summary>
        None,

        /// <summary>
        /// Number
        /// </summary>
        /// <example>1, 2, 3, ...</example>
        Number,

        /// <summary>
        /// Letter Lowercase
        /// </summary>
        /// <example>a, b, c, ...</example>
        LetterLower,

        /// <summary>
        /// Letter Uppercase
        /// </summary>
        /// <example>A, B, C, ...</example>
        LetterUpper,

        /// <summary>
        /// Roman Numerals Lowercase
        /// </summary>
        /// <example>i, ii, iii, iv, ...</example>
        RomanLower,

        /// <summary>
        /// Roman Numerals Uppercase
        /// </summary>
        /// <example>I, II, III, IV, ...</example>
        RomanUpper
    }
}