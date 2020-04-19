// <copyright file="IAgendaDataOrganisation.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Organisations;
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
        /// Creates the Organisation.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="organisation">Organisation.</param>
        /// <returns>Nothing.</returns>
        Task CreateOrganisationAsync(
            IWho who,
            IOrganisation organisation);

        #endregion Create

        #region Read

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

        #endregion Read

        #region Update

        /// <summary>
        /// Updates the Organisation.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="organisation">Organisation.</param>
        /// <returns>Nothing.</returns>
        Task UpdateOrganisationAsync(
            IWho who,
            IOrganisation organisation);

        #endregion Update
    }
}