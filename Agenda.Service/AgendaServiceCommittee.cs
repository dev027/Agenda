// <copyright file="AgendaServiceCommittee.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.AuditHeaders;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.ValueObjects.Enums;
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
            this.logger.LogTrace(
                "ENTRY {Method}(who, committee) {@who} {@committee}",
                nameof(this.CreateCommitteeAsync),
                who,
                committee);

            try
            {
                IAuditHeaderWithAuditDetails auditHeader = this.data.BeginTransaction(auditEvent);

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

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.CreateCommitteeAsync),
                who);
        }

        #endregion Create

        #region Read

        /// <inheritdoc/>
        public async Task<ICommittee> GetCommitteeByIdAsync(
            IWho who,
            Guid committeeId)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, committeeId) {@who} {committeeId}",
                nameof(this.GetCommitteeByIdAsync),
                who,
                committeeId);

            ICommittee committee = await this.data
                .GetCommitteeByIdAsync(
                    who: who,
                    committeeId: committeeId)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who, committee) {@who} {@committee}",
                nameof(this.GetCommitteeByIdAsync),
                who,
                committee);

            return committee;
        }

        /// <inheritdoc/>
        public async Task<ICommitteeWithMeetings> GetCommitteeByIdWithMeetingsAsync(
            IWho who,
            Guid committeeId)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, committeeId) {@who} {committeeId}",
                nameof(this.GetCommitteeByIdWithMeetingsAsync),
                who,
                committeeId);

            ICommitteeWithMeetings committeeWithMeetings = await this.data
                .GetCommitteeByIdWithMeetingsAsync(
                    who: who,
                    committeeId: committeeId)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who, committeeWithMeetings) {@who} {@committeeWithMeetings}",
                nameof(this.GetCommitteeByIdWithMeetingsAsync),
                who,
                committeeWithMeetings);

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
            this.logger.LogTrace(
                "ENTRY {Method}(who, committee) {@who} {@committee}",
                nameof(this.UpdateCommitteeAsync),
                who,
                committee);

            try
            {
                IAuditHeaderWithAuditDetails auditHeader =
                    this.data.BeginTransaction(auditEvent);

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

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.UpdateCommitteeAsync),
                who);
        }

        #endregion Update
    }
}