// <copyright file="AgendaDataLocationType.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Data.Dtos;
using Agenda.Data.Utilities;
using Agenda.Domain.DomainObjects.AuditHeaders;
using Agenda.Domain.DomainObjects.LocationTypes;
using Agenda.Utilities.Models.Whos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Agenda.Data.Crud
{
    /// <summary>
    /// CRUD operations for Location.
    /// </summary>
    public partial class AgendaData
    {
        #region Create

        /// <inheritdoc/>
        public async Task CreateLocationTypeAsync(
            IWho who,
            IAuditHeaderWithAuditDetails auditHeader,
            ILocationType locationType)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, locationType) {@who} {@locationType}",
                nameof(this.CreateLocationTypeAsync),
                who,
                locationType);

            LocationTypeDto dto = LocationTypeDto.ToDto(locationType);

            this.context.LocationTypes.Add(dto);
            await this.context.SaveChangesAsync().ConfigureAwait(false);
            Audit.AuditCreate(auditHeader, dto, dto.Id);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.CreateLocationTypeAsync),
                who);
        }

        #endregion Create

        #region Read

        /// <inheritdoc/>
        public async Task<ILocationType> GetLocationTypeByIdAsync(
            IWho who,
            Guid locationTypeId)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, locationTypeId) {@who} {locationTypeId}",
                nameof(this.GetLocationTypeByIdAsync),
                who,
                locationTypeId);

            ILocationType locationType = (await this.context.LocationTypes
                    .AsNoTracking()
                    .TagWith(this.Tag(who, nameof(this.GetLocationTypeByIdAsync)))
                    .FirstOrDefaultAsync(l => l.Id == locationTypeId)
                    .ConfigureAwait(false))
                .ToDomain();

            this.logger.LogTrace(
                "EXIT {Method}(who, locationType) {@who} {@locationType}",
                nameof(this.GetLocationTypeByIdAsync),
                who,
                locationType);

            return locationType;
        }

        #endregion Read

        #region Update

        /// <inheritdoc/>
        public async Task UpdateLocationTypeAsync(
            IWho who,
            IAuditHeaderWithAuditDetails auditHeader,
            ILocationType locationType)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, locationType) {@who} {@locationType}",
                nameof(this.UpdateLocationTypeAsync),
                who,
                locationType);

            LocationTypeDto dto = LocationTypeDto.ToDto(locationType);
            LocationTypeDto original = await this.context.FindAsync<LocationTypeDto>(locationType.Id)
                .ConfigureAwait(false);
            Audit.AuditUpdate(auditHeader, dto.Id, original, dto);

            this.context.Entry(original).CurrentValues.SetValues(dto);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.UpdateLocationTypeAsync),
                who);
        }

        #endregion Update
    }
}