// <copyright file="Organisations.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Data.Dtos;
using Agenda.Domain.DomainObjects.Organisations;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Data.Crud
{
    /// <summary>
    /// CRUD operations for Organisations.
    /// </summary>
    public partial class AgendaData
    {
        /// <inheritdoc/>
        public async Task<bool> HaveOrganisationsAsync()
        {
            return await this.context.Organisations.AnyAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task CreateOrganisationAsync(IOrganisation organisation)
        {
            OrganisationDto dto = OrganisationDto.ToDto(organisation);

            this.context.Organisations.Add(dto);
            await this.context.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IList<IOrganisation>> GetAllOrganisationsAsync()
        {
            IList<OrganisationDto> dtos = await this.context.Organisations
                .ToListAsync()
                .ConfigureAwait(false);

            return dtos.Select(o => o.ToDomain())
                .ToList();
        }
    }
}