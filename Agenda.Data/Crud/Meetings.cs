// <copyright file="Meetings.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Data.Dtos;
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
    }
}