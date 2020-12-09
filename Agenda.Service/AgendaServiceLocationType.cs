// <copyright file="AgendaServiceLocationType.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.AuditHeaders;
using Agenda.Domain.DomainObjects.LocationTypes;
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
        public async Task CreateLocationTypeAsync(
            IWho who,
            AuditEvent auditEvent,
            ILocationType locationType)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, locationType) {@who} {@locationType}",
                nameof(this.CreateLocationTypeAsync),
                who,
                locationType);

            try
            {
                IAuditHeaderWithAuditDetails auditHeader = this.data.BeginTransaction(auditEvent, who);

                await this.data
                    .CreateLocationTypeAsync(
                        who: who,
                        auditHeader: auditHeader,
                        locationType: locationType)
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

            ILocationType locationType = await this.data
                .GetLocationTypeByIdAsync(
                    who: who,
                    locationTypeId: locationTypeId)
                .ConfigureAwait(false);

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
            AuditEvent auditEvent,
            ILocationType locationType)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, locationType) {@who} {@locationType}",
                nameof(this.UpdateLocationTypeAsync),
                who,
                locationType);

            try
            {
                IAuditHeaderWithAuditDetails auditHeader = this.data.BeginTransaction(auditEvent, who);

                await this.data
                    .UpdateLocationTypeAsync(
                        who: who,
                        auditHeader: auditHeader,
                        locationType: locationType)
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
                nameof(this.UpdateLocationTypeAsync),
                who);
        }

        #endregion Update
    }
}