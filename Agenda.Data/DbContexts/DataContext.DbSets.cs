// <copyright file="DataContext.DbSets.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using Agenda.Data.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Data.DbContexts
{
    /// <summary>
    /// Database Context - DB Sets.
    /// </summary>
    /// <seealso cref="DbContext" />
    public partial class DataContext
    {
        /// <summary>
        /// Gets or sets the Audit Details.
        /// </summary>
        public DbSet<AuditDetailDto> AuditDetails { get; set; } = null!;

        /// <summary>
        /// Gets or sets the Audit Headers.
        /// </summary>
        public DbSet<AuditHeaderDto> AuditHeaders { get; set; } = null!;

        /// <summary>
        /// Gets or sets the Committees.
        /// </summary>
        public DbSet<CommitteeDto> Committees { get; set; } = null!;

        /// <summary>
        /// Gets or sets the Locations.
        /// </summary>
        public DbSet<LocationDto> Locations { get; set; } = null!;

        /// <summary>
        /// Gets or sets the Meetings.
        /// </summary>
        public DbSet<MeetingDto> Meetings { get; set; } = null!;

        /// <summary>
        /// Gets or sets the Organisations.
        /// </summary>
        public DbSet<OrganisationDto> Organisations { get; set; } = null!;
    }
}