// <copyright file="IAgendaService.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        /// <param name="who">Who called it.</param>
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
        /// <param name="who">Who called it.</param>
        /// <returns>Setup status.</returns>
        Task<ISetupStatus> GetSetupStatusAsync(
            IWho who);

        /// <summary>
        /// Creates the organisation.
        /// </summary>
        /// <param name="who">Who called it.</param>
        /// <param name="organisation">Organisation.</param>
        /// <returns>True if created.</returns>
        Task<bool> CreateOrganisationAsync(
            IWho who,
            IOrganisation organisation);

        /// <summary>
        /// Gets all Organisations.
        /// </summary>
        /// <param name="who">Who called it.</param>
        /// <returns>List of Organisations.</returns>
        Task<IList<IOrganisation>> GetAllOrganisationsAsync(
            IWho who);

        /// <summary>
        /// Gets the Organisation by Id.
        /// </summary>
        /// <param name="who">Who called it.</param>
        /// <param name="organisationId">Organisation Id.</param>
        /// <returns>Organisation.</returns>
        Task<IOrganisation> GetOrganisationByIdAsync(
            IWho who,
            Guid organisationId);

        /// <summary>
        /// Gets the Organisation by Id with it's Committees.
        /// </summary>
        /// <param name="who">Who called it.</param>
        /// <param name="organisationId">Organisation Id.</param>
        /// <returns>Organisation.</returns>
        Task<IOrganisationWithCommittees> GetOrganisationByIdWithCommitteesAsync(
            IWho who,
            Guid organisationId);

        /// <summary>
        /// Updates the Organisation.
        /// </summary>
        /// <param name="who">Who called it.</param>
        /// <param name="organisation">Organisation to update.</param>
        /// <returns>True if updated.</returns>
        Task<bool> UpdateOrganisationAsync(
            IWho who,
            IOrganisation organisation);
    }
}