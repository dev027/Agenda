// <copyright file="IAgendaServiceLocationType.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.LocationTypes;
using Agenda.Domain.ValueObjects.Enums;
using Agenda.Utilities.Models.Whos;

namespace Agenda.Service
{
    /// <summary>
    /// Service Layer - Location Type.
    /// </summary>
    public partial interface IAgendaService
    {
        #region Create

        /// <summary>
        /// Creates the Location Type.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="auditEvent">Audit Event.</param>
        /// <param name="locationType">Location Type.</param>
        /// <returns>Nothing.</returns>
        Task CreateLocationTypeAsync(
            IWho who,
            AuditEvent auditEvent,
            ILocationType locationType);

        #endregion Create

        #region Read

        /// <summary>
        /// Gets the Location Type by Id.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="locationTypeId">Location Type Id.</param>
        /// <returns>Location.</returns>
        Task<ILocationType> GetLocationTypeByIdAsync(
            IWho who,
            Guid locationTypeId);

        #endregion Read

        #region Update

        /// <summary>
        /// Updates the Location Type.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="auditEvent">Audit Event.</param>
        /// <param name="locationType">Location Type to update.</param>
        /// <returns>Nothing.</returns>
        Task UpdateLocationTypeAsync(
            IWho who,
            AuditEvent auditEvent,
            ILocationType locationType);

        #endregion Update
    }
}