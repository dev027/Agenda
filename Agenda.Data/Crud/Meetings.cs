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
    /// <seealso cref="System.IDisposable" />
    public partial class AgendaData
    {
        /// <summary>
        /// Gets the recent meetings with the most recent first.
        /// </summary>
        /// <param name="timeSpan">The time span define what is recent.</param>
        /// <param name="maxNumberOfMeetings">The maximum number of meetings to return.</param>
        /// <returns>List of Meetings.</returns>
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