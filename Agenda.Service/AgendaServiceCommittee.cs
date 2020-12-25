// <copyright file="AgendaServiceCommittee.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.AuditHeaders;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.ValueObjects.Enums;
using Agenda.Utilities.Logging;
using Agenda.Utilities.Models.Whos;
using Microsoft.Extensions.Logging;

namespace Agenda.Service
{
    /// <summary>
    /// Service Layer - Committee.
    /// </summary>
    /// <seealso cref="IAgendaService" />
    public partial class AgendaService
    {
        #region Create

        /// <inheritdoc/>
        public async Task CreateCommitteeAsync(
            IWho who,
            AuditEvent auditEvent,
            ICommittee committee)
        {
            this.logger.ReportEntry(
                who,
                new { Committee = committee });

            try
            {
                IAuditHeaderWithAuditDetails auditHeader = this.data.BeginTransaction(auditEvent, who);

                await this.data.CreateCommitteeAsync(
                        who: who,
                        auditHeader: auditHeader,
                        committee: committee)
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
        public async Task<ICommittee> GetCommitteeByIdAsync(
            IWho who,
            Guid committeeId)
        {
            this.logger.ReportEntry(
                who,
                new { CommitteeId = committeeId });

            ICommittee committee = await this.data
                .GetCommitteeByIdAsync(
                    who: who,
                    committeeId: committeeId)
                .ConfigureAwait(false);

            this.logger.ReportExit(
                who,
                new { Committee = committee });

            return committee;
        }

        /// <inheritdoc/>
        public async Task<ICommitteeWithMeetings> GetCommitteeByIdWithMeetingsAsync(
            IWho who,
            Guid committeeId)
        {
            this.logger.ReportEntry(
                who,
                new { CommitteeId = committeeId });

            ICommitteeWithMeetings committeeWithMeetings = await this.data
                .GetCommitteeByIdWithMeetingsAsync(
                    who: who,
                    committeeId: committeeId)
                .ConfigureAwait(false);

            this.logger.ReportExit(
                who,
                new { CommitteeWithMeetings = committeeWithMeetings });

            return committeeWithMeetings;
        }

        #endregion Read

        #region Update

        /// <inheritdoc/>
        public async Task UpdateCommitteeAsync(
            IWho who,
            AuditEvent auditEvent,
            ICommittee committee)
        {
            this.logger.ReportEntry(
                who,
                new { Committee = committee });

            try
            {
                IAuditHeaderWithAuditDetails auditHeader =
                    this.data.BeginTransaction(auditEvent, who);

                await this.data
                    .UpdateCommitteeAsync(
                        who: who,
                        auditHeader: auditHeader,
                        committee: committee)
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