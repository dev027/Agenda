// <copyright file="AgendaServiceAgendaItem.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.AgendaItems;
using Agenda.Domain.DomainObjects.AuditHeaders;
using Agenda.Domain.ValueObjects.Enums;
using Agenda.Service.Constants;
using Agenda.Service.Factories;
using Agenda.Service.Utilities;
using Agenda.Utilities.Logging;
using Agenda.Utilities.Models.Whos;

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
        public async Task<IAgendaItem> CreateSkeletonAgendaAsync(
            IWho who,
            AuditEvent auditEvent,
            Guid meetingId,
            SkeletonAgendaType skeletonAgendaType)
        {
            this.logger.ReportEntry(
                who,
                new
                {
                    AuditEvenit = auditEvent,
                    MeetingId = meetingId,
                    SkeletonAgendaType = skeletonAgendaType
                });

            SkeletonAgendaFactory factory = new SkeletonAgendaFactory();
            IAgendaItem agendaItem = factory.Create(meetingId, skeletonAgendaType);

            try
            {
                IAuditHeaderWithAuditDetails auditHeader = this.data.BeginTransaction(auditEvent, who);

                await this.data.CreateAgendaItemRecursivelyAsync(
                    who,
                    auditHeader,
                    agendaItem).ConfigureAwait(false);

                await this.data.CommitTransactionAsync(auditHeader)
                    .ConfigureAwait(false);
            }
            catch (Exception)
            {
                this.data.RollbackTransaction();
                throw;
            }

            this.logger.ReportExit(
                    who,
                    new { AgendaItem = agendaItem });

            return agendaItem;
        }

        #endregion Create

        #region Read

        /// <summary>
        /// Gets the Agenda Items for a Meeting in a tree structure.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="meetingId">Meeting Id.</param>
        /// <returns>Committee.</returns>
        public async Task<IAgendaItem?> GetAgendaItemsByMeetingIdAsTreeAsync(
            IWho who,
            Guid meetingId)
        {
            this.logger.ReportEntry(
                who,
                new { MeetingId = meetingId });

            IList<IAgendaItem> agendaItems = await this.data
                .GetAgendaItemsByMeetingIdAsync(
                    who: who,
                    meetingId: meetingId)
                .ConfigureAwait(false);

            IAgendaItem? agendaItem = agendaItems.Any()
                ? AgendaItemUtilities.CreateTreeStructure(agendaItems)
                : null;

            this.logger.ReportExit(
                who,
                new { AgendaItem = agendaItem });

            return agendaItem;
        }

        #endregion Read

        #region Update

        // Placeholder
        #endregion Update
    }
}