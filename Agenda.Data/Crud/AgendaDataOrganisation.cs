// <copyright file="AgendaDataOrganisation.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Data.Dtos;
using Agenda.Data.Utilities;
using Agenda.Domain.DomainObjects.AuditHeaders;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Utilities.Models.Whos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Agenda.Data.Crud
{
    /// <summary>
    /// CRUD operations for Organisations.
    /// </summary>
    public partial class AgendaData
    {
        #region Create

        /// <inheritdoc />
        public async Task CreateOrganisationAsync(
            IWho who,
            IAuditHeaderWithAuditDetails auditHeader,
            IOrganisation organisation)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, organisation) {@who} {@organisation}",
                nameof(this.CreateOrganisationAsync),
                who,
                organisation);

            OrganisationDto dto = OrganisationDto.ToDto(organisation);

            this.context.Organisations.Add(dto);
            await this.context.SaveChangesAsync().ConfigureAwait(false);
            Audit.AuditCreate(auditHeader, dto, dto.Id);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.CreateOrganisationAsync),
                who);
        }

        #endregion Create

        #region Read

        /// <inheritdoc />
        public async Task<IList<IOrganisation>> GetAllOrganisationsAsync(IWho who)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who) {@who}",
                nameof(this.GetOrganisationByIdAsync),
                who);

            IList<OrganisationDto> dtos = await this.context.Organisations
                .AsNoTracking()
                .TagWith(this.Tag(who, nameof(this.GetAllOrganisationsAsync)))
                .ToListAsync()
                .ConfigureAwait(false);

            IList<IOrganisation> organisations = dtos.Select(o => o.ToDomain())
                .ToList();

            this.logger.LogTrace(
                "EXIT {Method}(who, organisations) {@who} {@organisations}",
                nameof(this.GetOrganisationByIdAsync),
                who,
                organisations);

            return organisations;
        }

        /// <inheritdoc />
        public async Task<IOrganisation> GetOrganisationByIdAsync(
            IWho who,
            Guid organisationId)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, organisationId) {@who} {organisationId}",
                nameof(this.GetOrganisationByIdAsync),
                who,
                organisationId);

            IOrganisation organisation = (await this.context.Organisations
                    .AsNoTracking()
                    .TagWith(this.Tag(who, nameof(this.GetOrganisationByIdAsync)))
                    .FirstOrDefaultAsync(o => o.Id == organisationId)
                    .ConfigureAwait(false))
                .ToDomain();

            this.logger.LogTrace(
                "EXIT {Method}(who, organisation) {@who} {@organisation}",
                nameof(this.GetOrganisationByIdAsync),
                who,
                organisation);

            return organisation;
        }

        /// <inheritdoc/>
        public async Task<IOrganisationWithCommittees> GetOrganisationByIdWithCommitteesAsync(
            IWho who,
            Guid organisationId)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, organisationId) {@who} {organisationId}",
                nameof(this.GetOrganisationByIdAsync),
                who,
                organisationId);

            IOrganisationWithCommittees organisationWithCommittees = (await this.context.Organisations
                    .AsNoTracking()
                    .TagWith(this.Tag(who, nameof(this.GetOrganisationByIdWithCommitteesAsync)))
                    .Include(o => o.Committees)
                    .FirstOrDefaultAsync(o => o.Id == organisationId)
                    .ConfigureAwait(false))
                .ToDomainWithCommittees();

            this.logger.LogTrace(
                "EXIT {Method}(who, organisationWithCommittees) {@who} {@organisationWithCommittees}",
                nameof(this.GetOrganisationByIdAsync),
                who,
                organisationWithCommittees);

            return organisationWithCommittees;
        }

        /// <inheritdoc/>
        public async Task<IOrganisationWithLocations> GetOrganisationByIdWithLocationsAsync(
            IWho who,
            Guid organisationId)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, organisationId) {@who} {organisationId}",
                nameof(this.GetOrganisationByIdWithLocationsAsync),
                who,
                organisationId);

            IOrganisationWithLocations organisationWithLocations = (await this.context.Organisations
                    .AsNoTracking()
                    .TagWith(this.Tag(who, nameof(this.GetOrganisationByIdWithLocationsAsync)))
                    .Include(o => o.Locations)
                    .FirstOrDefaultAsync(o => o.Id == organisationId)
                    .ConfigureAwait(false))
                .ToDomainWithLocations();

            this.logger.LogTrace(
                "EXIT {Method}(who, organisationWithLocations) {@who} {@organisationWithLocations}",
                nameof(this.GetOrganisationByIdWithLocationsAsync),
                who,
                organisationWithLocations);

            return organisationWithLocations;
        }

        /// <inheritdoc/>
        public async Task<IOrganisationWithLocations> GetOrganisationByCommitteeIdWithLocationsAsync(
            IWho who,
            Guid committeeId)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, committeeId) {@who} {committeeId}",
                nameof(this.GetOrganisationByCommitteeIdWithLocationsAsync),
                who,
                committeeId);

            IOrganisationWithLocations organisationWithLocations = (await this.context.Organisations
                    .AsNoTracking()
                    .TagWith(this.Tag(who, nameof(this.GetOrganisationByCommitteeIdWithLocationsAsync)))
                    .Include(o => o.Locations)
                    .FirstOrDefaultAsync(o => o.Committees.Any(c => c.Id == committeeId))
                    .ConfigureAwait(false))
                .ToDomainWithLocations();

            this.logger.LogTrace(
                "EXIT {Method}(who, organisationWithLocations) {@who} {@organisationWithLocations}",
                nameof(this.GetOrganisationByCommitteeIdWithLocationsAsync),
                who,
                organisationWithLocations);

            return organisationWithLocations;
        }

        /// <inheritdoc/>
        public async Task<IOrganisationWithLocations> GetOrganisationByMeetingIdWithLocationsAsync(
            IWho who,
            Guid meetingId)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, meetingId) {@who} {meetingId}",
                nameof(this.GetOrganisationByMeetingIdWithLocationsAsync),
                who,
                meetingId);

            IOrganisationWithLocations organisationWithLocations = (await this.context.Organisations
                    .AsNoTracking()
                    .TagWith(this.Tag(who, nameof(this.GetOrganisationByMeetingIdWithLocationsAsync)))
                    .Include(o => o.Locations)
                    .FirstOrDefaultAsync(o =>
                        o.Committees.Any(c => c.Meetings.Any(m =>
                            m.Id == meetingId)))
                    .ConfigureAwait(false))
                .ToDomainWithLocations();

            this.logger.LogTrace(
                "EXIT {Method}(who, organisationWithLocations) {@who} {@organisationWithLocations}",
                nameof(this.GetOrganisationByMeetingIdWithLocationsAsync),
                who,
                organisationWithLocations);

            return organisationWithLocations;
        }

        /// <inheritdoc/>
        public async Task<bool> HaveOrganisationsAsync(IWho who)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who) {@who}",
                nameof(this.HaveOrganisationsAsync),
                who);

            bool haveOrganisations = await this.context.Organisations
                .TagWith(this.Tag(who, nameof(this.HaveOrganisationsAsync)))
                .AnyAsync()
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who, haveOrganisations) {@who} {haveOrganisations}",
                nameof(this.HaveOrganisationsAsync),
                who,
                haveOrganisations);

            return haveOrganisations;
        }

        #endregion Read

        #region Update

        /// <inheritdoc/>
        public async Task UpdateOrganisationAsync(
            IWho who,
            IOrganisation organisation)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, organisation) {@who} {@organisation}",
                nameof(this.UpdateOrganisationAsync),
                who,
                organisation);

            OrganisationDto dto = OrganisationDto.ToDto(organisation);

            OrganisationDto original = await this.context.FindAsync<OrganisationDto>(organisation.Id);

            this.context.Entry(original).CurrentValues.SetValues(dto);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.UpdateOrganisationAsync),
                who);
        }

        #endregion Update
    }
}