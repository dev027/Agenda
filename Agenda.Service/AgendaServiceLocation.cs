// <copyright file="AgendaServiceLocation.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.AuditHeaders;
using Agenda.Domain.DomainObjects.Locations;
using Agenda.Domain.ValueObjects.Enums;
using Agenda.Utilities.Models.Whos;
using Microsoft.Extensions.Logging;

namespace Agenda.Service
{
    /// <summary>
    /// Service Layer - Location.
    /// </summary>
    /// <seealso cref="IAgendaService" />
    public partial class AgendaService
    {
        #region Create

        /// <inheritdoc />
        public async Task CreateLocationAsync(
            IWho who,
            AuditEvent auditEvent,
            ILocation location)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, location) {@who} {@location}",
                nameof(this.CreateLocationAsync),
                who,
                location);

            try
            {
                IAuditHeaderWithAuditDetails auditHeader = this.data.BeginTransaction(auditEvent, who);

                await this.data
                    .CreateLocationAsync(
                        who: who,
                        auditHeader: auditHeader,
                        location: location)
                    .ConfigureAwait(false);

                await this.data.CommitTransactionAsync(auditHeader)
                    .ConfigureAwait(false);
            }
            catch (Exception)
            {
                this.data.RollbackTransaction();
                throw;
            }

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

            ILocation location = await this.data
                .GetLocationByIdAsync(
                    who: who,
                    locationId: locationId)
                .ConfigureAwait(false);

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
            AuditEvent auditEvent,
            ILocation location)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, location) {@who} {@location}",
                nameof(this.UpdateLocationAsync),
                who,
                location);

            try
            {
                IAuditHeaderWithAuditDetails auditHeader = this.data.BeginTransaction(auditEvent, who);

                await this.data
                    .UpdateLocationAsync(
                        who: who,
                        auditHeader: auditHeader,
                        location: location)
                    .ConfigureAwait(false);

                await this.data.CommitTransactionAsync(auditHeader)
                    .ConfigureAwait(false);
            }
            catch (Exception)
            {
                this.data.RollbackTransaction();
                throw;
            }

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.UpdateLocationAsync),
                who);
        }

        #endregion Update
    }
}