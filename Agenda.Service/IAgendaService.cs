// <copyright file="IAgendaService.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Agenda.Domain.DomainObjects.Meetings;
using Agenda.Domain.ValueObjects.SetupStatii;

namespace Agenda.Service
{
    /// <summary>
    /// Service Layer.
    /// </summary>
    public interface IAgendaService
    {
        /// <summary>
        /// Gets the recent meetings most recent first.
        /// </summary>
        /// <param name="timeSpan">The time span.</param>
        /// <param name="maxNumberOfMeetings">The maximum number of meetings.</param>
        /// <returns>List of recent meetings.</returns>
        IList<IMeeting> GetRecentMeetingsMostRecentFirst(
                    TimeSpan? timeSpan = null,
                    int? maxNumberOfMeetings = null);

        /// <summary>
        /// Gets the setup status.
        /// </summary>
        /// <returns>Setup status.</returns>
        ISetupStatus GetSetupStatus();
    }
}