// <copyright file="Organisations.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Data.Dtos;
using Agenda.Data.Resources;
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
                LoggerResources.___EntryBy___,
                nameof(this.HaveOrganisationsAsync),
                who);

            return await this.context.Organisations
                .TagWith(this.Tag(who, nameof(this.HaveOrganisationsAsync)))
                .AnyAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task CreateOrganisationAsync(
            IWho who,
            IOrganisation organisation)
        {
            this.logger.LogTrace(
                LoggerResources.___EntryBy___,
                nameof(this.CreateOrganisationAsync),
                who);

            OrganisationDto dto = OrganisationDto.ToDto(organisation);

            this.context.Organisations.Add(dto);
            await this.context.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IList<IOrganisation>> GetAllOrganisationsAsync(IWho who)
        {
            this.logger.LogTrace(
                LoggerResources.___EntryBy___,
                nameof(this.GetOrganisationByIdAsync),
                who);

            IList<OrganisationDto> dtos = await this.context.Organisations
                .AsNoTracking()
                .TagWith(this.Tag(who, nameof(this.GetAllOrganisationsAsync)))
                .ToListAsync()
                .ConfigureAwait(false);

            return dtos.Select(o => o.ToDomain())
                .ToList();
        }

        /// <inheritdoc />
        public async Task<IOrganisation> GetOrganisationByIdAsync(
            IWho who,
            Guid organisationId)
        {
            this.logger.LogTrace(
                LoggerResources.___EntryBy___,
                nameof(this.GetOrganisationByIdAsync),
                who);

            return (await this.context.Organisations
                    .AsNoTracking()
                    .TagWith(this.Tag(who, nameof(this.GetOrganisationByIdAsync)))
                    .FirstOrDefaultAsync(o => o.Id == organisationId)
                    .ConfigureAwait(false))
                .ToDomain();
        }

        /// <inheritdoc/>
        public async Task<IOrganisationWithCommittees> GetOrganisationByIdWithCommitteesAsync(
            IWho who,
            Guid organisationId)
        {
            this.logger.LogTrace(
                LoggerResources.___EntryBy___,
                nameof(this.GetOrganisationByIdAsync),
                who);

            return (await this.context.Organisations
                    .AsNoTracking()
                    .TagWith(this.Tag(who, nameof(this.GetOrganisationByIdWithCommitteesAsync)))
                    .Include(o => o.Committees)
                    .FirstOrDefaultAsync(o => o.Id == organisationId)
                    .ConfigureAwait(false))
                .ToDomainWithCommittees();
        }

        /// <inheritdoc/>
        public async Task UpdateOrganisationAsync(
            IWho who,
            IOrganisation organisation)
        {
            this.logger.LogTrace(
                LoggerResources.___EntryBy___,
                nameof(this.UpdateOrganisationAsync),
                who);

            OrganisationDto dto = OrganisationDto.ToDto(organisation);

            this.context.Organisations.Update(dto);
            await this.context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}