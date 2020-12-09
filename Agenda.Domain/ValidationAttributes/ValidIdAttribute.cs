// <copyright file="ValidIdAttribute.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;

namespace Agenda.Domain.ValidationAttributes
{
    /// <summary>
    /// Validate Id.
    /// </summary>
    /// <seealso cref="ValidationAttribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ValidIdAttribute : ValidationAttribute
    {
        /// <inheritdoc />
        public override bool IsValid(object? value)
        {
            if (!(value is Guid))
            {
                return false;
            }

            Guid inputValue = (Guid)value;
            bool isValid = inputValue != Guid.Empty;
            return isValid;
        }

        /// <inheritdoc/>
        public override string FormatErrorMessage(string name)
        {
            return $"'{name}' must not be an empty Guid";
        }
    }
}