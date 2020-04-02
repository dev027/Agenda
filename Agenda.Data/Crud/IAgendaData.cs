// <copyright file="IAgendaData.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Agenda.Domain.DomainObjects.Meetings;

namespace Agenda.Data.Crud
{
    /// <summary>
    /// Data access.
    /// </summary>
    public interface IAgendaData : IDisposable
    {
        #region Committees

        /// <summary>
        /// Checks if we have Committees.
        /// </summary>
        /// <returns>True if Committees exist.</returns>
        bool HaveCommittees();

        #endregion Committees

        #region Meetings

        /// <summary>
        /// Gets the recent meetings with the most recent first.
        /// </summary>
        /// <param name="timeSpan">The time span define what is recent.</param>
        /// <param name="maxNumberOfMeetings">The maximum number of meetings to return.</param>
        /// <returns>List of Meetings.</returns>
        IList<IMeeting> GetRecentMeetingsMostRecentFirst(
            TimeSpan timeSpan,
            int maxNumberOfMeetings);

        #endregion Meetings

        #region Organisations

        /// <summary>
        /// Checks if we have Organisations.
        /// </summary>
        /// <returns>True if Organisations exist.</returns>
        bool HaveOrganisations();

        #endregion Organisations
    }
}