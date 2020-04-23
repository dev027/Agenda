// <copyright file="IAgendaServiceOrganisation.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Organisations;
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
        /// <param name="organisation">Organisation.</param>
        /// <returns>Nothing.</returns>
        Task CreateOrganisationAsync(
            IWho who,
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