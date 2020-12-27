// <copyright file="IAgendaDataAgendaItem.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.AgendaItems;
using Agenda.Domain.DomainObjects.AuditHeaders;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Utilities.Models.Whos;

namespace Agenda.Data.Crud
{
    /// <summary>
    /// Data access - Agenda Item.
    /// </summary>
    public partial interface IAgendaData
    {
        #region Create

        /// <summary>
        /// Creates the Agenda Item.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="auditHeader">Audit Header.</param>
        /// <param name="agendaItem">Agenda Item.</param>
        /// <returns>Nothing.</returns>
        Task CreateAgendaItemRecursivelyAsync(
            IWho who,
            IAuditHeaderWithAuditDetails auditHeader,
            IAgendaItem agendaItem);

        #endregion Create

        #region Read

        /// <summary>
        /// Gets the Agenda Item by Id.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="agendaItemId">Agenda Item Id.</param>
        /// <returns>Agenda Item (Null=Not Found).</returns>
        Task<IAgendaItem> GetAgendaItemByIdAsync(
            IWho who,
            Guid agendaItemId);

        /// <summary>
        /// Gets the Agenda Items for a Meeting Id.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="meetingId">Meeting Id.</param>
        /// <returns>Agenda Items.</returns>
        Task<IList<IAgendaItem>> GetAgendaItemsByMeetingIdAsync(
            IWho who,
            Guid meetingId);

        #endregion Read

        #region Update

        /// <summary>
        /// Updates the Agenda Item.
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="auditHeader">Audit Header.</param>
        /// <param name="agendaItem">Agenda Item.</param>
        /// <returns>Nothing.</returns>
        Task UpdateAgendaItemAsync(
            IWho who,
            IAuditHeaderWithAuditDetails auditHeader,
            IAgendaItem agendaItem);

        #endregion Update
    }
}