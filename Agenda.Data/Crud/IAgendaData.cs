// <copyright file="IAgendaData.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Meetings;
using Agenda.Domain.DomainObjects.Organisations;

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
        Task<bool> HaveCommitteesAsync();

        #endregion Committees

        #region Meetings

        /// <summary>
        /// Gets the recent meetings with the most recent first.
        /// </summary>
        /// <param name="timeSpan">The time span define what is recent.</param>
        /// <param name="maxNumberOfMeetings">The maximum number of meetings to return.</param>
        /// <returns>List of Meetings.</returns>
        Task<IList<IMeeting>> GetRecentMeetingsMostRecentFirstAsync(
            TimeSpan timeSpan,
            int maxNumberOfMeetings);

        #endregion Meetings

        #region Organisations

        /// <summary>
        /// Checks if we have Organisations.
        /// </summary>
        /// <returns>True if Organisations exist.</returns>
        Task<bool> HaveOrganisationsAsync();

        /// <summary>
        /// Creates the Organisation.
        /// </summary>
        /// <param name="organisation">Organisation.</param>
        /// <returns>Nothing.</returns>
        Task CreateOrganisationAsync(IOrganisation organisation);

        /// <summary>
        /// Gets all organisations.
        /// </summary>
        /// <returns>List of Organisations.</returns>
        Task<IList<IOrganisation>> GetAllOrganisationsAsync();

        #endregion Organisations

    }
}