// <copyright file="IAgendaService.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.DomainObjects.Meetings;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Domain.ValueObjects.SetupStatii;
using Agenda.Utilities.Models.Whos;

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
        /// <param name="who">Who Details.</param>
        /// <param name="timeSpan">The time span.</param>
        /// <param name="maxNumberOfMeetings">The maximum number of meetings.</param>
        /// <returns>List of recent meetings.</returns>
        Task<IList<IMeeting>> GetRecentMeetingsMostRecentFirstAsync(
            IWho who,
            TimeSpan? timeSpan = null,
            int? maxNumberOfMeetings = null);

        /// <summary>
        /// Gets the setup status.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <returns>Setup status.</returns>
        Task<ISetupStatus> GetSetupStatusAsync(
            IWho who);

        /// <summary>
        /// Creates the organisation.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="organisation">Organisation.</param>
        /// <returns>Nothing.</returns>
        Task CreateOrganisationAsync(
            IWho who,
            IOrganisation organisation);

        /// <summary>
        /// Gets all Organisations.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <returns>List of Organisations.</returns>
        Task<IList<IOrganisation>> GetAllOrganisationsAsync(
            IWho who);

        /// <summary>
        /// Gets the Organisation by Id.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="organisationId">Organisation Id.</param>
        /// <returns>Organisation.</returns>
        Task<IOrganisation> GetOrganisationByIdAsync(
            IWho who,
            Guid organisationId);

        /// <summary>
        /// Gets the Organisation by Id with its Committees.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="organisationId">Organisation Id.</param>
        /// <returns>Organisation.</returns>
        Task<IOrganisationWithCommittees> GetOrganisationByIdWithCommitteesAsync(
            IWho who,
            Guid organisationId);

        /// <summary>
        /// Updates the Organisation.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="organisation">Organisation to update.</param>
        /// <returns>Nothing.</returns>
        Task UpdateOrganisationAsync(
            IWho who,
            IOrganisation organisation);

        /// <summary>
        /// Creates the committee.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="committee">Committee.</param>
        /// <returns>Nothing.</returns>
        Task CreateCommitteeAsync(
            IWho who,
            ICommittee committee);

        /// <summary>
        /// Gets the Committee by Id.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="committeeId">Committee Id.</param>
        /// <returns>Committee.</returns>
        Task<ICommittee> GetCommitteeByIdAsync(
            IWho who,
            Guid committeeId);

        /// <summary>
        /// Gets the Committee by Id with its Meetings.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="committeeId">Committee Id.</param>
        /// <returns>Committee with Meetings.</returns>
        Task<ICommitteeWithMeetings> GetCommitteeByIdWithMeetingsAsync(
            IWho who,
            Guid committeeId);

        /// <summary>
        /// Updates the Committee.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="committee">Committee to update.</param>
        /// <returns>Nothing.</returns>
        Task UpdateCommitteeAsync(
            IWho who,
            ICommittee committee);

        /// <summary>
        /// Creates the Meeting.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="meeting">Meeting.</param>
        /// <returns>Nothing.</returns>
        Task CreateMeetingAsync(
            IWho who,
            IMeeting meeting);

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
        /// Updates the Meeting.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="meeting">Meeting.</param>
        /// <returns>Nothing.</returns>
        Task UpdateMeetingAsync(
            IWho who,
            IMeeting meeting);
    }
}