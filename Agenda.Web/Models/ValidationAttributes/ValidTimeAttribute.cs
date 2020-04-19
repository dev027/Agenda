// <copyright file="ValidTimeAttribute.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Agenda.Web.Models.ValidationAttributes
{
    /// <summary>
    /// Validation attribute for Id.
    /// </summary>
    /// <seealso cref="ValidationAttribute" />
    [AttributeUsage(
        AttributeTargets.Property |
        AttributeTargets.Field,
        AllowMultiple = false)]
    public sealed class ValidTimeAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets or sets the culture.
        /// </summary>
        public string Culture { get; set; } = "en-GB";

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>
        ///   <see langword="true" /> if the specified value is valid; otherwise, <see langword="false" />.
        /// </returns>
        public override bool IsValid(object value)
        {
            return value != null &&
                   Regex.IsMatch(value.ToString() !, "([01]?[0-9]|2[0-3]):[0-5][0-9]");
        }

        /// <summary>
        /// Applies formatting to an error message, based on the data field where the error occurred.
        /// </summary>
        /// <param name="name">The name to include in the formatted message.</param>
        /// <returns>
        /// An instance of the formatted error message.
        /// </returns>
        public override string FormatErrorMessage(string name)
        {
            return $"{name} is not a valid time";
        }
    }
}