// <copyright file="IAgendaData.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.DomainObjects.Meetings;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Utilities.Models.Whos;

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
        /// <param name="who">Who details.</param>
        /// <returns>True if Committees exist.</returns>
        Task<bool> HaveCommitteesAsync(
            IWho who);

        /// <summary>
        /// Creates the Committee.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="committee">Committee.</param>
        /// <returns>Nothing.</returns>
        Task CreateCommitteeAsync(IWho who, ICommittee committee);

        /// <summary>
        /// Gets the Committee by Id.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="committeeId">Committee Id.</param>
        /// <returns>Committee.</returns>
        Task<ICommittee> GetCommitteeByIdAsync(
            IWho who,
            Guid committeeId);

        /// <summary>
        /// Gets the Committee by Id with its meetings.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="committeeId">Committee id.</param>
        /// <returns>Committee with Meetings.</returns>
        Task<ICommitteeWithMeetings> GetCommitteeByIdWithMeetingsAsync(
            IWho who,
            Guid committeeId);

        /// <summary>
        /// Updates the Committee asynchronous.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="committee">The committee.</param>
        /// <returns>Nothing.</returns>
        Task UpdateCommitteeAsync(IWho who, ICommittee committee);

        #endregion Committees

        #region Meetings

        /// <summary>
        /// Creates the Meeting.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="meeting">Meeting.</param>
        /// <returns>Nothing.</returns>
        Task CreateMeetingAsync(
            IWho who,
            IMeeting meeting);

        /// <summary>
        /// Gets the recent meetings with the most recent first.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="timeSpan">The time span define what is recent.</param>
        /// <param name="maxNumberOfMeetings">The maximum number of meetings to return.</param>
        /// <returns>List of Meetings.</returns>
        Task<IList<IMeeting>> GetRecentMeetingsMostRecentFirstAsync(
            IWho who,
            TimeSpan timeSpan,
            int maxNumberOfMeetings);

        #endregion Meetings

        #region Organisations

        /// <summary>
        /// Creates the Organisation.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="organisation">Organisation.</param>
        /// <returns>Nothing.</returns>
        Task CreateOrganisationAsync(
            IWho who,
            IOrganisation organisation);

        /// <summary>
        /// Gets all organisations.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <returns>List of Organisations.</returns>
        Task<IList<IOrganisation>> GetAllOrganisationsAsync(IWho who);

        /// <summary>
        /// Gets the Organisation by Id.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="organisationId">Organisation Id.</param>
        /// <returns>Organisation (Null=Not Found).</returns>
        Task<IOrganisation> GetOrganisationByIdAsync(
            IWho who,
            Guid organisationId);

        /// <summary>
        /// Gets the Organisation by Id with its Committees.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="organisationId">Organisation Id.</param>
        /// <returns>Organisation (Null=Not Found).</returns>
        Task<IOrganisationWithCommittees> GetOrganisationByIdWithCommitteesAsync(
            IWho who,
            Guid organisationId);

        /// <summary>
        /// Checks if we have Organisations.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <returns>True if Organisations exist.</returns>
        Task<bool> HaveOrganisationsAsync(IWho who);

        /// <summary>
        /// Updates the Organisation.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="organisation">Organisation.</param>
        /// <returns>Nothing.</returns>
        Task UpdateOrganisationAsync(
            IWho who,
            IOrganisation organisation);

        #endregion Organisations
    }
}