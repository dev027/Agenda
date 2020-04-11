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
            OrganisationDto dto = OrganisationDto.ToDto(organisation);

            this.context.Organisations.Add(dto);
            await this.context.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IList<IOrganisation>> GetAllOrganisationsAsync(IWho who)
        {
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
            return (await this.context.Organisations
                    .AsNoTracking()
                    .TagWith(this.Tag(who, nameof(this.GetOrganisationByIdAsync)))
                    .FirstOrDefaultAsync(o => o.Id == organisationId)
                    .ConfigureAwait(false))
                .ToDomain();
        }
    }
}