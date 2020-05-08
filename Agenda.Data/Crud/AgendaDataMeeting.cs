// <copyright file="AgendaDataMeeting.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Data.Dtos;
using Agenda.Domain.DomainObjects.AuditHeaders;
using Agenda.Domain.DomainObjects.Meetings;
using Agenda.Utilities.Models.Whos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Agenda.Data.Crud
{
    /// <summary>
    /// CRUD operations for Meetings.
    /// </summary>
    public partial class AgendaData
    {
        #region Create

        /// <inheritdoc/>
        public async Task CreateMeetingAsync(
            IWho who,
            IAuditHeaderWithAuditDetails auditHeader,
            IMeeting meeting)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, meeting) {@who} {@meeting}",
                nameof(this.CreateMeetingAsync),
                who,
                meeting);

            MeetingDto dto = MeetingDto.ToDto(meeting);

            this.context.Meetings.Add(dto);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.CreateMeetingAsync),
                who);
        }

        #endregion Create

        #region Read

        /// <inheritdoc />
        public async Task<IList<IMeeting>> GetRecentMeetingsMostRecentFirstAsync(
            IWho who,
            TimeSpan timeSpan,
            int maxNumberOfMeetings)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, timeSpan, maxNumberOfMeetings) {@who} {timeSpan} {maxNumberOfMeetings}",
                nameof(this.GetRecentMeetingsMostRecentFirstAsync),
                who,
                timeSpan,
                maxNumberOfMeetings);

            DateTime earliestDate = DateTime.Now.Subtract(timeSpan);
            IList<MeetingDto> meetingDtos = await this.context.Meetings
                .AsNoTracking()
                .TagWith(this.Tag(who, nameof(this.GetRecentMeetingsMostRecentFirstAsync)))
                .Take(maxNumberOfMeetings)
                .Include(m => m.Committee)
                .ThenInclude(c => c!.Organisation)
                .Include(m => m.Location)
                .ThenInclude(l => l!.Organisation)
                .Where(m => m.MeetingDateTime >= earliestDate)
                .ToListAsync()
                .ConfigureAwait(false);

            IList<IMeeting> meetings = meetingDtos.Select(m => m.ToDomain()).ToList();

            this.logger.LogTrace(
                "EXIT {Method}(who, meetings) {@who} {@meetings}",
                nameof(this.GetRecentMeetingsMostRecentFirstAsync),
                who,
                meetings);

            return meetings;
        }

        /// <inheritdoc />
        public async Task<IMeeting> GetMeetingByIdAsync(IWho who, Guid meetingId)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, meetingId) {@who} {meetingId}",
                nameof(this.GetMeetingByIdAsync),
                who,
                meetingId);

            IMeeting meeting = (await this.context.Meetings
                    .AsNoTracking()
                    .TagWith(this.Tag(who, nameof(this.GetMeetingByIdAsync)))
                    .Include(m => m.Committee)
                    .ThenInclude(c => c!.Organisation)
                    .Include(m => m.Location)
                    .ThenInclude(l => l!.Organisation)
                    .FirstOrDefaultAsync(m => m.Id == meetingId)
                    .ConfigureAwait(false))
                .ToDomain();

            this.logger.LogTrace(
                "EXIT {Method}(who, meeting) {@who} {@meeting}",
                nameof(this.GetMeetingByIdAsync),
                who,
                meeting);

            return meeting;
        }

        #endregion Read

        #region Update

        /// <inheritdoc/>
        public async Task UpdateMeetingAsync(
            IWho who,
            IMeeting meeting)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, meeting) {@who} {@meeting}",
                nameof(this.UpdateMeetingAsync),
                who,
                meeting);

            MeetingDto dto = MeetingDto.ToDto(meeting);

            MeetingDto original = await this.context.FindAsync<MeetingDto>(meeting.Id);

            this.context.Entry(original).CurrentValues.SetValues(dto);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.UpdateMeetingAsync),
                who);
        }

        #endregion Update
    }
}