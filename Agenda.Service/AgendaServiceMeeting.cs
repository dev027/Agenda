// <copyright file="AgendaServiceMeeting.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.AuditHeaders;
using Agenda.Domain.DomainObjects.Meetings;
using Agenda.Domain.ValueObjects.Enums;
using Agenda.Utilities.Logging;
using Agenda.Utilities.Models.Whos;
using Microsoft.Extensions.Logging;

namespace Agenda.Service
{
    /// <summary>
    /// Service Layer - Meeting.
    /// </summary>
    /// <seealso cref="IAgendaService" />
    public partial class AgendaService
    {
        #region Create

        /// <inheritdoc/>
        public async Task CreateMeetingAsync(
            IWho who,
            AuditEvent auditEvent,
            IMeeting meeting)
        {
            this.logger.ReportEntry(
                who,
                new { Meeting = meeting });

            try
            {
                IAuditHeaderWithAuditDetails auditHeader = this.data.BeginTransaction(auditEvent, who);

                await this.data
                    .CreateMeetingAsync(
                        who: who,
                        auditHeader: auditHeader,
                        meeting: meeting)
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
        public async Task<IMeeting> GetMeetingByIdAsync(
            IWho who,
            Guid meetingId)
        {
            this.logger.ReportEntry(
                who,
                new { MeetingId = meetingId });

            IMeeting meeting = await this.data
                .GetMeetingByIdAsync(
                    who: who,
                    meetingId: meetingId)
                .ConfigureAwait(false);

            this.logger.ReportExit(
                who,
                new { Meeting = meeting });

            return meeting;
        }

        /// <inheritdoc/>
        public async Task<IList<IMeeting>> GetRecentMeetingsMostRecentFirstAsync(
            IWho who,
            TimeSpan? timeSpan = null,
            int? maxNumberOfMeetings = null)
        {
            this.logger.ReportEntry(
                who,
                new
                {
                    TimeSpan = timeSpan,
                    MaxNumberOfMeeeting = maxNumberOfMeetings
                });

            IList<IMeeting> meetings = await this.data
                .GetRecentMeetingsMostRecentFirstAsync(
                    who: who,
                    timeSpan: timeSpan ?? TimeSpan.FromDays(365),
                    maxNumberOfMeetings: maxNumberOfMeetings ?? 20)
                .ConfigureAwait(false);

            this.logger.ReportExit(
                who,
                new { Meetings = meetings });

            return meetings;
        }

        #endregion Read

        #region Update

        /// <inheritdoc/>
        public async Task UpdateMeetingAsync(
            IWho who,
            AuditEvent auditEvent,
            IMeeting meeting)
        {
            this.logger.ReportEntry(
                who,
                new { Meeting = meeting });

            try
            {
                IAuditHeaderWithAuditDetails auditHeader = this.data.BeginTransaction(auditEvent, who);

                await this.data
                    .UpdateMeetingAsync(
                        who: who,
                        auditHeader: auditHeader,
                        meeting: meeting)
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