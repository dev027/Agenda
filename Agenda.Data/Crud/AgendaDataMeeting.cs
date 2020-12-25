// <copyright file="AgendaDataMeeting.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Data.Dtos;
using Agenda.Data.Utilities;
using Agenda.Domain.DomainObjects.AuditHeaders;
using Agenda.Domain.DomainObjects.Meetings;
using Agenda.Utilities.Logging;
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
            this.logger.ReportEntry(
                who,
                new { Meeting = meeting });

            MeetingDto dto = MeetingDto.ToDto(meeting);

            this.context.Meetings.Add(dto);
            await this.context.SaveChangesAsync().ConfigureAwait(false);
            Audit.AuditCreate(auditHeader, dto, dto.Id);

            this.logger.ReportExit(who);
        }

        #endregion Create

        #region Read

        /// <inheritdoc />
        public async Task<IList<IMeeting>> GetRecentMeetingsMostRecentFirstAsync(
            IWho who,
            TimeSpan timeSpan,
            int maxNumberOfMeetings)
        {
            this.logger.ReportEntry(
                who,
                new
                {
                    TimeSpan = timeSpan,
                    MaxNumberOfMeetings = maxNumberOfMeetings
                });

            DateTime earliestDate = DateTime.Now.Subtract(timeSpan);
            IList<MeetingDto> meetingDtos = await this.context.Meetings
                .AsNoTracking()
                .TagWith(this.Tag(who, nameof(this.GetRecentMeetingsMostRecentFirstAsync)))
                .Take(maxNumberOfMeetings)
                .Include(m => m.Committee)
                .ThenInclude(c => c!.Organisation)
                .Where(m => m.MeetingDateTime >= earliestDate)
                .ToListAsync()
                .ConfigureAwait(false);

            IList<IMeeting> meetings = meetingDtos.Select(m => m.ToDomain()).ToList();

            this.logger.ReportExit(
                who,
                new { Meetings = meetings });

            return meetings;
        }

        /// <inheritdoc />
        public async Task<IMeeting> GetMeetingByIdAsync(
            IWho who,
            Guid meetingId)
        {
            this.logger.ReportEntry(
                who,
                new { MeetingId = meetingId });

            IMeeting meeting = (await this.context.Meetings
                    .AsNoTracking()
                    .TagWith(this.Tag(who, nameof(this.GetMeetingByIdAsync)))
                    .Include(m => m.Committee)
                    .ThenInclude(c => c!.Organisation)
                    .FirstOrDefaultAsync(m => m.Id == meetingId)
                    .ConfigureAwait(false))
                .ToDomain();

            this.logger.ReportExit(
                who,
                new { Meeting = meeting });

            return meeting;
        }

        #endregion Read

        #region Update

        /// <inheritdoc/>
        public async Task UpdateMeetingAsync(
            IWho who,
            IAuditHeaderWithAuditDetails auditHeader,
            IMeeting meeting)
        {
            this.logger.ReportEntry(
                who,
                new { Meeting = meeting });

            MeetingDto dto = MeetingDto.ToDto(meeting);
            MeetingDto original = await this.context.FindAsync<MeetingDto>(meeting.Id);
            Audit.AuditUpdate(auditHeader, dto.Id, original, dto);

            this.context.Entry(original).CurrentValues.SetValues(dto);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            this.logger.ReportExit(who);
        }

        #endregion Update
    }
}