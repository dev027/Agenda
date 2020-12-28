// <copyright file="AgendaDataAgendaItem.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Data.Dtos;
using Agenda.Data.Utilities;
using Agenda.Domain.DomainObjects.AgendaItems;
using Agenda.Domain.DomainObjects.AuditHeaders;
using Agenda.Utilities.Logging;
using Agenda.Utilities.Models.Whos;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Data.Crud
{
    /// <summary>
    /// CRUD operations for Agenda Item.
    /// </summary>
    public partial class AgendaData
    {
        #region Create

        /// <inheritdoc/>
        public async Task CreateAgendaItemRecursivelyAsync(
            IWho who,
            IAuditHeaderWithAuditDetails auditHeader,
            IAgendaItem agendaItem)
        {
            AgendaItemDto dto = AgendaItemDto.ToDto(agendaItem);
            this.context.AgendaItems.Add(dto);
            Audit.AuditCreate(auditHeader, dto, dto.Id);

            foreach (IAgendaItem child in agendaItem.ChildAgendaItems)
            {
                await this.CreateAgendaItemRecursivelyAsync(
                    who,
                    auditHeader,
                    child);
            }

            if (agendaItem.ParentId == null)
            {
                this.logger.Debug(
                    who,
                    "Saving changes",
                    new { AgendaItemId = agendaItem });
                await this.context.SaveChangesAsync();
            }

            this.logger.ReportExit(who);
        }

        #endregion Create

        #region Read

        /// <inheritdoc/>
        public async Task<IAgendaItem> GetAgendaItemByIdAsync(
            IWho who,
            Guid agendaItemId)
        {
            this.logger.ReportEntry(
                who,
                new { AgendaItemId = agendaItemId });

            IAgendaItem agendaItem = (await this.context.AgendaItems
                    .AsNoTracking()
                    .TagWith(this.Tag(who))
                    .FirstOrDefaultAsync(ai => ai.Id == agendaItemId)
                    .ConfigureAwait(false))
                .ToDomain();

            this.logger.ReportExit(
                who,
                new { AgendaItem = agendaItem });

            return agendaItem;
        }

        /// <inheritdoc/>
        public async Task<IList<IAgendaItem>> GetAgendaItemsByMeetingIdAsync(
            IWho who,
            Guid meetingId)
        {
            this.logger.ReportEntry(
                who,
                new { MeetingId = meetingId });

            IList<IAgendaItem> agendaItems = (await this.context.AgendaItems
                    .AsNoTracking()
                    .TagWith(this.Tag(who))
                    .Where(ai => ai.MeetingId == meetingId)
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(ai => ai.ToDomain())
                .ToList();

            this.logger.ReportExit(
                who,
                new { AgendaItems = agendaItems });

            return agendaItems;
        }

        #endregion Read

        #region Update

        /// <inheritdoc/>
        public async Task UpdateAgendaItemAsync(
            IWho who,
            IAuditHeaderWithAuditDetails auditHeader,
            IAgendaItem agendaItem)
        {
            this.logger.ReportEntry(
                who,
                new { AgendaItem = agendaItem });

            AgendaItemDto dto = AgendaItemDto.ToDto(agendaItem);
            AgendaItemDto original = await this.context.FindAsync<AgendaItemDto>(agendaItem.Id);
            Audit.AuditUpdate(auditHeader, dto.Id, original, dto);

            this.context.Entry(original).CurrentValues.SetValues(dto);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            this.logger.ReportExit(who);
        }

        #endregion Update
    }
}