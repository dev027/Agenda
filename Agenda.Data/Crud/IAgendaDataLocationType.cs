// <copyright file="IAgendaDataLocationType.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.AuditHeaders;
using Agenda.Domain.DomainObjects.LocationTypes;
using Agenda.Utilities.Models.Whos;

namespace Agenda.Data.Crud
{
    /// <summary>
    /// Data access.
    /// </summary>
    public partial interface IAgendaData
    {
        #region Create

        /// <summary>
        /// Creates the Location Type.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="auditHeader">Audit Header.</param>
        /// <param name="locationType">Location Type.</param>
        /// <returns>Nothing.</returns>
        Task CreateLocationTypeAsync(
            IWho who,
            IAuditHeaderWithAuditDetails auditHeader,
            ILocationType locationType);

        #endregion

        #region Read

        /// <summary>
        /// Gets the Location Type by Id.
        /// </summary>
        /// <param name="who">Who details.</param>
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
        /// <param name="who">Who details.</param>
        /// <param name="auditHeader">Audit Header.</param>
        /// <param name="locationType">The Location Type.</param>
        /// <returns>Nothing.</returns>
        Task UpdateLocationTypeAsync(
            IWho who,
            IAuditHeaderWithAuditDetails auditHeader,
            ILocationType locationType);

        #endregion
    }
}