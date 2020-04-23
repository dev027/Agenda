// <copyright file="IAgendaServiceMeeting.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Meetings;
using Agenda.Utilities.Models.Whos;

namespace Agenda.Service
{
    /// <summary>
    /// Service Layer - Meeting.
    /// </summary>
    public partial interface IAgendaService
    {
        #region Create

        /// <summary>
        /// Creates the Meeting.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="meeting">Meeting.</param>
        /// <returns>Nothing.</returns>
        Task CreateMeetingAsync(
            IWho who,
            IMeeting meeting);

        #endregion Create

        #region Read

        /// <summary>
        /// Gets the Meeting by Id.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="meetingId">Meeting Id.</param>
        /// <returns>Meeting.</returns>
        Task<IMeeting> GetMeetingByIdAsync(
            IWho who,
            Guid meetingId);

        /// <summary>
        /// Gets the recent meetings most recent first.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="timeSpan">The time span.</param>
        /// <param name="maxNumberOfMeetings">The maximum number of meetings.</param>
        /// <returns>List of recent meetings.</returns>
        Task<IList<IMeeting>> GetRecentMeetingsMostRecentFirstAsync(
            IWho who,
            TimeSpan? timeSpan = null,
            int? maxNumberOfMeetings = null);

        #endregion Read

        #region Update

        /// <summary>
        /// Updates the Meeting.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="meeting">Meeting.</param>
        /// <returns>Nothing.</returns>
        Task UpdateMeetingAsync(
            IWho who,
            IMeeting meeting);

        #endregion Update
    }
}