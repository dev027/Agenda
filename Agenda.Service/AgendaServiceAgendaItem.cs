// <copyright file="AgendaServiceAgendaItem.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.AgendaItems;
using Agenda.Domain.DomainObjects.AuditHeaders;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.ValueObjects.Enums;
using Agenda.Service.Constants;
using Agenda.Utilities.Logging;
using Agenda.Utilities.Models.Whos;
using Microsoft.Extensions.Logging;

namespace Agenda.Service
{
    /// <summary>
    /// Service Layer - Committee.
    /// </summary>
    /// <seealso cref="IAgendaService" />
    public partial class AgendaService
    {
        #region Create

        /// <inheritdoc/>
        public Task<IAgendaItem> CreateSkeletonAgenda(
            IWho who,
            AuditEvent auditEvent,
            SkeletonAgendaType skeletonAgendaType)
        {
            throw new NotImplementedException();
        }

        #endregion Create

        #region Read

        /// <summary>
        /// Gets the Agenda Items for a Meeting in a tree structure.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="meetingId">Meeting Id.</param>
        /// <returns>Committee.</returns>
        public Task<IAgendaItem> GetAgendaItemsByMeetingIdAsTreeAsync(
            IWho who,
            Guid meetingId)
        {
            throw new NotImplementedException();
        }

        #endregion Read

        #region Update

        // Placeholder
        #endregion Update
    }
}