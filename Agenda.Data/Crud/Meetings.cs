// <copyright file="Meetings.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using Agenda.Data.Dtos;
using Agenda.Domain.DomainObjects.Meetings;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Data.Crud
{
    /// <summary>
    /// CRUD operations for Meetings.
    /// </summary>
    public partial class AgendaData
    {
        /// <inheritdoc />
        public IList<IMeeting> GetRecentMeetingsMostRecentFirst(
            TimeSpan timeSpan,
            int maxNumberOfMeetings)
        {
            DateTime earliestDate = DateTime.Now.Subtract(timeSpan);
            IList<MeetingDto> meetingDtos = this.context.Meetings
                .AsNoTracking()
                .Take(maxNumberOfMeetings)
                .Include(m => m.Committee)
                .ThenInclude(c => c!.Organisation)
                .Where(m => m.MeetingDateTime >= earliestDate)
                .ToList();

            return meetingDtos.Select(m => m.ToDomain()).ToList();
        }
    }
}