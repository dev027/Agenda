// <copyright file="ValidDateAttribute.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

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
    public sealed class ValidDateAttribute : ValidationAttribute
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
            if (value == null)
            {
                return false;
            }

            CultureInfo cultureInfo = CultureInfo.GetCultureInfo(this.Culture);

            if (DateTime.TryParse(
                value.ToString(),
                cultureInfo,
                DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeLocal,
                out DateTime outDateTime))
            {
                return (outDateTime - outDateTime.Date).Ticks == 0;
            }

            return false;
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
            return $"{name} is not a valid date";
        }
    }
}