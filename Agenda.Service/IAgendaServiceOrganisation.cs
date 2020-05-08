// <copyright file="IAgendaServiceOrganisation.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Domain.ValueObjects.Enums;
using Agenda.Utilities.Models.Whos;

namespace Agenda.Service
{
    /// <summary>
    /// Service Layer - Organisation.
    /// </summary>
    public partial interface IAgendaService
    {
        #region Create

        /// <summary>
        /// Creates the organisation.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="auditEvent">Audit Event.</param>
        /// <param name="organisation">Organisation.</param>
        /// <returns>Nothing.</returns>
        Task CreateOrganisationAsync(
            IWho who,
            AuditEvent auditEvent,
            IOrganisation organisation);

        #endregion Create

        #region Read

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
        /// Gets the Organisation by Id with its Locations.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="organisationId">Organisation Id.</param>
        /// <returns>Organisation.</returns>
        Task<IOrganisationWithLocations> GetOrganisationByIdWithLocationsAsync(
            IWho who,
            Guid organisationId);

        /// <summary>
        /// Gets the Organisation by Committee Id with Locations.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="committeeId">Committee Id.</param>
        /// <returns>Organisation with Locations.</returns>
        Task<IOrganisationWithLocations> GetOrganisationByCommitteeIdWithLocationsAsync(
            IWho who,
            Guid committeeId);

        /// <summary>
        /// Gets the Organisation by Meeting Id with Locations.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="meetingId">Meeting Id.</param>
        /// <returns>Organisation with Locations.</returns>
        Task<IOrganisationWithLocations> GetOrganisationByMeetingIdWithLocationsAsync(
            IWho who,
            Guid meetingId);

        #endregion Read

        #region Update

        /// <summary>
        /// Updates the Organisation.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="organisation">Organisation to update.</param>
        /// <returns>Nothing.</returns>
        Task UpdateOrganisationAsync(
            IWho who,
            IOrganisation organisation);

        #endregion Update
    }
}