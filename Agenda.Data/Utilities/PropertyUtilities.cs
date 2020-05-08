// <copyright file="PropertyUtilities.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel;
using System.Reflection;
using Agenda.Data.Extensions;

namespace Agenda.Data.Utilities
{
    /// <summary>
    /// Property Utilities.
    /// </summary>
    public static class PropertyUtilities
    {
        /// <summary>
        /// Determines whether [is auditable column] [the specified property descriptors].
        /// </summary>
        /// <param name="propertyDescriptors">Property Descriptors.</param>
        /// <param name="propertyInfo">Property Information.</param>
        /// <returns>
        ///   <c>true</c> if [is auditable column] [the specified property descriptors]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAuditableColumn(
            PropertyDescriptorCollection propertyDescriptors,
            PropertyInfo propertyInfo)
        {
            if (propertyDescriptors == null)
            {
                throw new ArgumentNullException(nameof(propertyDescriptors));
            }

            if (propertyInfo == null)
            {
                throw new ArgumentNullException(nameof(propertyInfo));
            }

            // Get property descriptor details for model property
            PropertyDescriptor propertyDescriptor = propertyDescriptors[propertyInfo.Name];

            if (propertyDescriptor == null)
            {
                return false;
            }

            ReflectionExtensions.PropertyAttributes propertyAttributes = propertyDescriptor.GetAttributes();

            // Auditable if
            // - Has a setter
            // - Not got [Key] attribute
            // - Not got [ForeignKey] attribute
            // - Not got [NotMapped] attribute
            // - Not got [AuditIgnore] attribute
            // - Not a collection
            return propertyInfo.CanWrite &&
                   propertyAttributes.Key == null &&
                   propertyAttributes.ForeignKey == null &&
                   propertyAttributes.NotMapped == null &&
                   propertyAttributes.AuditIgnore == null &&
                   !propertyInfo.IsCollection();
        }
    }
}