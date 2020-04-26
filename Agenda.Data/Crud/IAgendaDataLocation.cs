// <copyright file="IAgendaDataLocation.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Locations;
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
        /// Creates the Location.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="location">Location.</param>
        /// <returns>Nothing.</returns>
        Task CreateLocationAsync(
            IWho who,
            ILocation location);

        #endregion

        #region Read

        /// <summary>
        /// Gets the Location by Id.
        /// </summary>
        /// <param name="who">Who details.</param>
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
        /// <param name="who">Who details.</param>
        /// <param name="location">The Location.</param>
        /// <returns>Nothing.</returns>
        Task UpdateLocationAsync(
            IWho who,
            ILocation location);

        #endregion
    }
}