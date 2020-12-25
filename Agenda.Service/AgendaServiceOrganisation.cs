// <copyright file="AgendaServiceOrganisation.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.AuditHeaders;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Domain.ValueObjects.Enums;
using Agenda.Utilities.Logging;
using Agenda.Utilities.Models.Whos;
using Microsoft.Extensions.Logging;

namespace Agenda.Service
{
    /// <summary>
    /// Service Layer - Organisation.
    /// </summary>
    /// <seealso cref="IAgendaService" />
    public partial class AgendaService
    {
        #region Create

        /// <inheritdoc/>
        public async Task CreateOrganisationAsync(
            IWho who,
            AuditEvent auditEvent,
            IOrganisation organisation)
        {
            this.logger.ReportEntry(
                who,
                new { Organisation = organisation });

            try
            {
                IAuditHeaderWithAuditDetails auditHeader = this.data.BeginTransaction(auditEvent, who);

                await this.data
                    .CreateOrganisationAsync(
                        who: who,
                        auditHeader: auditHeader,
                        organisation: organisation)
                    .ConfigureAwait(false);

                await this.data.CommitTransactionAsync(auditHeader)
                    .ConfigureAwait(false);
            }
            catch (Exception)
            {
                this.data.RollbackTransaction();
                throw;
            }

            this.logger.ReportExit(who);
        }

        #endregion Create

        #region Read

        /// <inheritdoc/>
        public async Task<IList<IOrganisation>> GetAllOrganisationsAsync(
            IWho who)
        {
            this.logger.ReportEntry(who);

            IList<IOrganisation> organisations = await this.data
                .GetAllOrganisationsAsync(who)
                .ConfigureAwait(false);

            this.logger.ReportExit(
                who,
                new { Organisations = organisations });

            return organisations;
        }

        /// <inheritdoc/>
        public async Task<IOrganisation> GetOrganisationByIdAsync(
            IWho who,
            Guid organisationId)
        {
            this.logger.ReportEntry(
                who,
                new { OrganisationId = organisationId });

            IOrganisation organisation = await this.data
                .GetOrganisationByIdAsync(
                    who: who,
                    organisationId: organisationId)
                .ConfigureAwait(false);

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

            IOrganisationWithCommittees organisationWithCommittees = await this.data
                .GetOrganisationByIdWithCommitteesAsync(
                    who: who,
                    organisationId: organisationId)
                .ConfigureAwait(false);

            this.logger.ReportExit(
                who,
                new { OrganisationWithCommittees = organisationWithCommittees });

            return organisationWithCommittees;
        }

        #endregion Read

        #region Update

        /// <inheritdoc/>
        public async Task UpdateOrganisationAsync(
            IWho who,
            AuditEvent auditEvent,
            IOrganisation organisation)
        {
            this.logger.ReportEntry(
                who,
                new { Organisation = organisation });

            try
            {
                IAuditHeaderWithAuditDetails auditHeader = this.data.BeginTransaction(auditEvent, who);

                await this.data
                    .UpdateOrganisationAsync(
                        who: who,
                        auditHeader: auditHeader,
                        organisation: organisation)
                    .ConfigureAwait(false);

                await this.data.CommitTransactionAsync(auditHeader)
                    .ConfigureAwait(false);
            }
            catch (Exception)
            {
                this.data.RollbackTransaction();
                throw;
            }

            this.logger.ReportExit(who);
        }

        #endregion Update
    }
}