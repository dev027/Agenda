// <copyright file="DomainMetadata.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

namespace Agenda.Domain.DomainObjects.Committees
{
    /// <summary>
    /// Metadata for the <see cref="ICommittee"/>.
    /// </summary>
    public static class DomainMetadata
    {
        /// <summary>
        /// Metadata for the <see cref="ICommittee.Name"></see> property.
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
        /// Metadata for the <see cref="ICommittee.Description"></see> property.
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