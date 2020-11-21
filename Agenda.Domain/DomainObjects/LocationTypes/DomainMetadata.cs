// <copyright file="DomainMetadata.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

namespace Agenda.Domain.DomainObjects.LocationTypes
{
    /// <summary>
    /// Metadata for <see cref="ILocationType"/>.
    /// </summary>
    public static class DomainMetadata
    {
        /// <summary>
        /// Metadata for the <see cref="ILocationType.Code"></see> property.
        /// </summary>
        public static class Code
        {
            /// <summary>
            /// The minimum length.
            /// </summary>
            public const int MinLength = 1;

            /// <summary>
            /// The maximum length.
            /// </summary>
            public const int MaxLength = 10;
        }

        /// <summary>
        /// Metadata for the <see cref="ILocationType.Name"></see> property.
        /// </summary>
        public static class Name
        {
            /// <summary>
            /// The minimum length.
            /// </summary>
            public const int MinLength = 1;

            /// <summary>
            /// The maximum length.
            /// </summary>
            public const int MaxLength = 50;
        }

        /// <summary>
        /// Metadata for the <see cref="ILocationType.Description"></see> property.
        /// </summary>
        public static class Description
        {
            /// <summary>
            /// The minimum length.
            /// </summary>
            public const int MinLength = 1;

            /// <summary>
            /// The maximum length.
            /// </summary>
            public const int MaxLength = 50;
        }
    }
}