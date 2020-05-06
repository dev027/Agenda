// <copyright file="AgendaDataLocation.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Data.Dtos;
using Agenda.Domain.DomainObjects.Locations;
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
        public async Task CreateLocationAsync(
            IWho who,
            ILocation location)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, location) {@who} {@location}",
                nameof(this.CreateLocationAsync),
                who,
                location);

            LocationDto dto = LocationDto.ToDto(location);

            this.context.Locations.Add(dto);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.CreateLocationAsync),
                who);
        }

        #endregion Create

        #region Read

        /// <inheritdoc/>
        public async Task<ILocation> GetLocationByIdAsync(
            IWho who,
            Guid locationId)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, locationId) {@who} {locationId}",
                nameof(this.GetLocationByIdAsync),
                who,
                locationId);

            ILocation location = (await this.context.Locations
                    .AsNoTracking()
                    .TagWith(this.Tag(who, nameof(this.GetLocationByIdAsync)))
                    .Include(l => l.Organisation)
                    .FirstOrDefaultAsync(l => l.Id == locationId)
                    .ConfigureAwait(false))
                .ToDomain();

            this.logger.LogTrace(
                "EXIT {Method}(who, location) {@who} {@location}",
                nameof(this.GetLocationByIdAsync),
                who,
                location);

            return location;
        }

        #endregion Read

        #region Update

        /// <inheritdoc/>
        public async Task UpdateLocationAsync(
            IWho who,
            ILocation location)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, location) {@who} {@location}",
                nameof(this.UpdateLocationAsync),
                who,
                location);

            LocationDto dto = LocationDto.ToDto(location);

            LocationDto original = await this.context.FindAsync<LocationDto>(location.Id);

            this.context.Entry(original).CurrentValues.SetValues(dto);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.UpdateLocationAsync),
                who);
        }

        #endregion Update
    }
}