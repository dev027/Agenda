// <copyright file="MigrationContext.DbSets.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using Agenda.Data.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Migration.DbContexts
{
    /// <summary>
    /// Migration Context - DB Sets.
    /// </summary>
    /// <seealso cref="DbContext" />
    public partial class MigrationContext
    {
        /// <summary>
        /// Gets or sets the Agenda Items.
        /// </summary>
        public DbSet<AgendaItemDto> AgendaItems { get; set; } = null!;

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
        /// Gets or sets the Meetings.
        /// </summary>
        public DbSet<MeetingDto> Meetings { get; set; } = null!;

        /// <summary>
        /// Gets or sets the Organisations.
        /// </summary>
        public DbSet<OrganisationDto> Organisations { get; set; } = null!;
    }
}