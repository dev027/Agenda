// <copyright file="Organisations.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Data.Dtos;
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

        /// <inheritdoc />
        public async Task CreateOrganisationAsync(
            IWho who,
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

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.CreateOrganisationAsync),
                who);
        }

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

            this.context.Organisations.Update(dto);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.UpdateOrganisationAsync),
                who);
        }
    }
}