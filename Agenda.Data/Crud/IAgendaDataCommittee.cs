// <copyright file="IAgendaDataCommittee.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Committees;
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
        /// Creates the Committee.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="committee">Committee.</param>
        /// <returns>Nothing.</returns>
        Task CreateCommitteeAsync(IWho who, ICommittee committee);

        #endregion Create

        #region Read

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
        /// Checks if we have Committees.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <returns>True if Committees exist.</returns>
        Task<bool> HaveCommitteesAsync(
            IWho who);

        #endregion Read

        #region Update

        /// <summary>
        /// Updates the Committee asynchronous.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="committee">The committee.</param>
        /// <returns>Nothing.</returns>
        Task UpdateCommitteeAsync(IWho who, ICommittee committee);

        #endregion Update
    }
}