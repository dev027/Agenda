// <copyright file="ReflectionExtensions.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Agenda.Data.Attributes;

namespace Agenda.Data.Extensions
{
    /// <summary>
    /// Reflection Extension methods.
    /// </summary>
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Determines whether this instance is a collection.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified instance is collection; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCollection(this PropertyInfo instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            // If it's not IEnumerable then it cannot be a collection
            if (instance.PropertyType.GetInterface(nameof(IEnumerable)) == null)
            {
                return false;
            }

            // Strings are IEnumerable but they don't count as collections
            return instance.PropertyType != typeof(string);
        }

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <param name="instance">The Instance.</param>
        /// <returns>Property Attributes.</returns>
        public static PropertyAttributes GetAttributes(this PropertyDescriptor instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return new PropertyAttributes(
                (AuditIgnoreAttribute)instance.Attributes[typeof(AuditIgnoreAttribute)],
                (ForeignKeyAttribute)instance.Attributes[typeof(ForeignKeyAttribute)],
                (KeyAttribute)instance.Attributes[typeof(KeyAttribute)],
                (NotMappedAttribute)instance.Attributes[typeof(NotMappedAttribute)],
                (RangeAttribute)instance.Attributes[typeof(RangeAttribute)]);
        }

        /// <summary>
        /// Class detailing the known property attributes.
        /// </summary>
        public class PropertyAttributes
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="PropertyAttributes"/> class.
            /// </summary>
            /// <param name="auditIgnore">Audit Ignore attribute.</param>
            /// <param name="foreignKey">Foreign Key attribute.</param>
            /// <param name="key">Key attribute.</param>
            /// <param name="notMapped">Not Mapped attribute.</param>
            /// <param name="range">Range attribute.</param>
            public PropertyAttributes(
                AuditIgnoreAttribute? auditIgnore,
                ForeignKeyAttribute? foreignKey,
                KeyAttribute? key,
                NotMappedAttribute? notMapped,
                RangeAttribute? range)
            {
                this.AuditIgnore = auditIgnore;
                this.ForeignKey = foreignKey;
                this.Key = key;
                this.NotMapped = notMapped;
                this.Range = range;
            }

            /// <summary>
            /// Gets the Audit Ignore attribute.
            /// </summary>
            public AuditIgnoreAttribute? AuditIgnore { get; }

            /// <summary>
            /// Gets the Foreign Key attribute.
            /// </summary>
            public ForeignKeyAttribute? ForeignKey { get; }

            /// <summary>
            /// Gets the key attribute.
            /// </summary>
            public KeyAttribute? Key { get; }

            /// <summary>
            /// Gets the Not Mapped attribute.
            /// </summary>
            public NotMappedAttribute? NotMapped { get; }

            /// <summary>
            /// Gets the range attribute.
            /// </summary>
            public RangeAttribute? Range { get; }
        }
    }
}