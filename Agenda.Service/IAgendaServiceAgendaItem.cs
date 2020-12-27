// <copyright file="IAgendaServiceAgendaItem.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.AgendaItems;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.ValueObjects.Enums;
using Agenda.Service.Constants;
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
        /// <param name="skeletonAgendaType">Skeleton Agenda Type.</param>
        /// <returns>Nothing.</returns>
        Task<IAgendaItem> CreateSkeletonAgenda(
            IWho who,
            AuditEvent auditEvent,
            SkeletonAgendaType skeletonAgendaType);

        #endregion Create

        #region Read

        /// <summary>
        /// Gets the Agenda Items for a Meeting in a tree structure.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="meetingId">Meeting Id.</param>
        /// <returns>Committee.</returns>
        Task<IAgendaItem> GetAgendaItemsByMeetingIdAsTreeAsync(
            IWho who,
            Guid meetingId);

        #endregion Read

        #region Update

        // Place Holder
        #endregion Update
    }
}