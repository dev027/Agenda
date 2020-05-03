// <copyright file="ValidWhat3WordsPartAttribute.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Agenda.Domain.DomainObjects.Locations;

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
    public sealed class ValidWhat3WordsPartAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets or sets the culture.
        /// </summary>
        public string Culture { get; set; } = "en-GB";

        /// <summary>
        /// Gets or sets a value indicating whether [allow null].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow null]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowNull { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

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
                return this.AllowNull;
            }

            return Regex.IsMatch(value.ToString() !, DomainMetadata.What3Words.PartRegEx);
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
            string propertyName = this.Name ?? name;
            return $"{propertyName} is not a valid what3words address";
        }
    }
}