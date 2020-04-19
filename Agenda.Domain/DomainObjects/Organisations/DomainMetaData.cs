// <copyright file="DomainMetaData.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

namespace Agenda.Domain.DomainObjects.Organisations
{
    /// <summary>
    /// Metadata for the <see cref="IOrganisation"/>.
    /// </summary>
    public static class DomainMetadata
    {
        /// <summary>
        /// Metadata for the <see cref="IOrganisation.Code"></see> property.
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
        /// Metadata for the <see cref="IOrganisation.Name"></see> property.
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
    }
}