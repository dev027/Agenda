// <copyright file="IAgendaServiceLocation.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Locations;
using Agenda.Domain.ValueObjects.Enums;
using Agenda.Utilities.Models.Whos;

namespace Agenda.Service
{
    /// <summary>
    /// Service Layer - Location.
    /// </summary>
    public partial interface IAgendaService
    {
        #region Create

        /// <summary>
        /// Creates the Location.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="auditEvent">Audit Event.</param>
        /// <param name="location">Location.</param>
        /// <returns>Nothing.</returns>
        Task CreateLocationAsync(
            IWho who,
            AuditEvent auditEvent,
            ILocation location);

        #endregion Create

        #region Read

        /// <summary>
        /// Gets the Location by Id.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="locationId">Location Id.</param>
        /// <returns>Location.</returns>
        Task<ILocation> GetLocationByIdAsync(
            IWho who,
            Guid locationId);

        #endregion Read

        #region Update

        /// <summary>
        /// Updates the Location.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="location">Location to update.</param>
        /// <returns>Nothing.</returns>
        Task UpdateLocationAsync(
            IWho who,
            ILocation location);

        #endregion Update
    }
}