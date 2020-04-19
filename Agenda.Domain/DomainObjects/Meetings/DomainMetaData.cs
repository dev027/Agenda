// <copyright file="DomainMetaData.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using Agenda.Domain.DomainObjects.Organisations;

namespace Agenda.Domain.DomainObjects.Meetings
{
    /// <summary>
    /// Metadata for the <see cref="IOrganisation"/>.
    /// </summary>
    public static class DomainMetadata
    {
        /// <summary>
        /// Metadata for the <see cref="IMeeting.Location"></see> property.
        /// </summary>
        public static class Location
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