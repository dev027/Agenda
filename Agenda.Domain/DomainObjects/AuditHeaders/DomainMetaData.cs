// <copyright file="DomainMetadata.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

namespace Agenda.Domain.DomainObjects.AuditHeaders
{
    /// <summary>
    /// Metadata for the <see cref="IAuditHeader"/>.
    /// </summary>
    public static class DomainMetadata
    {
        /// <summary>
        /// Metadata for the <see cref="IAuditHeader.Username"></see> property.
        /// </summary>
        public static class Username
        {
            /// <summary>
            /// The minimum length.
            /// </summary>
            public const int MinLength = 1;

            /// <summary>
            /// The maximum length.
            /// </summary>
            public const int MaxLength = 100;
        }
    }
}