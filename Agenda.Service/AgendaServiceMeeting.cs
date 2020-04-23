// <copyright file="AgendaServiceMeeting.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Meetings;
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
            IMeeting meeting)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, meeting) {@who} {@meeting}",
                nameof(this.CreateMeetingAsync),
                who,
                meeting);

            await this.data
                .CreateMeetingAsync(
                    who: who,
                    meeting: meeting)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.CreateMeetingAsync),
                who);
        }

        #endregion Create

        #region Read

        /// <inheritdoc/>
        public async Task<IMeeting> GetMeetingByIdAsync(
            IWho who,
            Guid meetingId)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, meetingId) {@who} {meetingId}",
                nameof(this.GetMeetingByIdAsync),
                who,
                meetingId);

            IMeeting meeting = await this.data
                .GetMeetingByIdAsync(
                    who: who,
                    meetingId: meetingId)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who, meeting) {@who} {@meeting}",
                nameof(this.GetMeetingByIdAsync),
                who,
                meeting);

            return meeting;
        }

        /// <inheritdoc/>
        public async Task<IList<IMeeting>> GetRecentMeetingsMostRecentFirstAsync(
            IWho who,
            TimeSpan? timeSpan = null,
            int? maxNumberOfMeetings = null)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, timeSpan, maxNumberOfMeetings) {@who} {timeSpan} {maxNumberOfMeetings}",
                nameof(this.GetRecentMeetingsMostRecentFirstAsync),
                who,
                timeSpan,
                maxNumberOfMeetings);

            IList<IMeeting> meetings = await this.data
                .GetRecentMeetingsMostRecentFirstAsync(
                    who: who,
                    timeSpan: timeSpan ?? TimeSpan.FromDays(365),
                    maxNumberOfMeetings: maxNumberOfMeetings ?? 20)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who, meetings) {@who} {@meetings}",
                nameof(this.GetRecentMeetingsMostRecentFirstAsync),
                who,
                meetings);

            return meetings;
        }

        #endregion Read

        #region Update

        /// <inheritdoc/>
        public async Task UpdateMeetingAsync(
            IWho who,
            IMeeting meeting)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, meeting) {@who} {@organisation}",
                nameof(this.UpdateMeetingAsync),
                who,
                meeting);

            await this.data
                .UpdateMeetingAsync(
                    who: who,
                    meeting: meeting)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.UpdateMeetingAsync),
                who);
        }

        #endregion Update
    }
}