// <copyright file="IAgendaServiceCommittee.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.ValueObjects.Enums;
using Agenda.Utilities.Models.Whos;

namespace Agenda.Service
{
    /// <summary>
    /// Service Layer - Committee.
    /// </summary>
    public partial interface IAgendaService
    {
        #region Create

        /// <summary>
        /// Creates the committee.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="auditEvent">Audit Event.</param>
        /// <param name="committee">Committee.</param>
        /// <returns>Nothing.</returns>
        Task CreateCommitteeAsync(
            IWho who,
            AuditEvent auditEvent,
            ICommittee committee);

        #endregion Create

        #region Read

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

        #endregion Read

        #region Update

        /// <summary>
        /// Updates the Committee.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="committee">Committee to update.</param>
        /// <returns>Nothing.</returns>
        Task UpdateCommitteeAsync(
            IWho who,
            ICommittee committee);

        #endregion Update
    }
}