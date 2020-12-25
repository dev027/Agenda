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
using Agenda.Utilities.Logging;
using Agenda.Utilities.Models.Whos;
using Microsoft.EntityFrameworkCore;

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
            this.logger.ReportEntry(
                who,
                new { Organisation = organisation });

            OrganisationDto dto = OrganisationDto.ToDto(organisation);

            this.context.Organisations.Add(dto);
            await this.context.SaveChangesAsync().ConfigureAwait(false);
            Audit.AuditCreate(auditHeader, dto, dto.Id);

            this.logger.ReportExit(who);
        }

        #endregion Create

        #region Read

        /// <inheritdoc />
        public async Task<IList<IOrganisation>> GetAllOrganisationsAsync(IWho who)
        {
            this.logger.ReportEntry(who);

            IList<OrganisationDto> dtos = await this.context.Organisations
                .AsNoTracking()
                .TagWith(this.Tag(who, nameof(this.GetAllOrganisationsAsync)))
                .ToListAsync()
                .ConfigureAwait(false);

            IList<IOrganisation> organisations = dtos.Select(o => o.ToDomain())
                .ToList();

            this.logger.ReportExit(
                who,
                new { Organisations = organisations });

            return organisations;
        }

        /// <inheritdoc />
        public async Task<IOrganisation> GetOrganisationByIdAsync(
            IWho who,
            Guid organisationId)
        {
            this.logger.ReportEntry(
                who,
                new { OrganisationId = organisationId });

            IOrganisation organisation = (await this.context.Organisations
                    .AsNoTracking()
                    .TagWith(this.Tag(who, nameof(this.GetOrganisationByIdAsync)))
                    .FirstOrDefaultAsync(o => o.Id == organisationId)
                    .ConfigureAwait(false))
                .ToDomain();

            this.logger.ReportExit(
                who,
                new { Organisation = organisation });

            return organisation;
        }

        /// <inheritdoc/>
        public async Task<IOrganisationWithCommittees> GetOrganisationByIdWithCommitteesAsync(
            IWho who,
            Guid organisationId)
        {
            this.logger.ReportEntry(
                who,
                new { OrganisationId = organisationId });

            IOrganisationWithCommittees organisationWithCommittees = (await this.context.Organisations
                    .AsNoTracking()
                    .TagWith(this.Tag(who, nameof(this.GetOrganisationByIdWithCommitteesAsync)))
                    .Include(o => o.Committees)
                    .FirstOrDefaultAsync(o => o.Id == organisationId)
                    .ConfigureAwait(false))
                .ToDomainWithCommittees();

            this.logger.ReportExit(
                who,
                new
                {
                    OrganisationWithCommittees = organisationWithCommittees
                });

            return organisationWithCommittees;
        }

        /// <inheritdoc/>
        public async Task<bool> HaveOrganisationsAsync(IWho who)
        {
            this.logger.ReportEntry(who);

            bool haveOrganisations = await this.context.Organisations
                .TagWith(this.Tag(who, nameof(this.HaveOrganisationsAsync)))
                .AnyAsync()
                .ConfigureAwait(false);

            this.logger.ReportExit(
                who,
                new { HaveOrganisations = haveOrganisations });

            return haveOrganisations;
        }

        #endregion Read

        #region Update

        /// <inheritdoc/>
        public async Task UpdateOrganisationAsync(
            IWho who,
            IAuditHeaderWithAuditDetails auditHeader,
            IOrganisation organisation)
        {
            this.logger.ReportEntry(
                who,
                new { Organisation = organisation });

            OrganisationDto dto = OrganisationDto.ToDto(organisation);
            OrganisationDto original = await this.context.FindAsync<OrganisationDto>(organisation.Id);
            Audit.AuditUpdate(auditHeader, dto.Id, original, dto);

            this.context.Entry(original).CurrentValues.SetValues(dto);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            this.logger.ReportExit(who);
        }

        #endregion Update
    }
}