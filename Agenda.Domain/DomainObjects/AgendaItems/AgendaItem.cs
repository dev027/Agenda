// <copyright file="AgendaItem.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using Agenda.Domain.ValueObjects.Enums;
using Agenda.Utilities.ExtensionMethods.Integer;

namespace Agenda.Domain.DomainObjects.AgendaItems
{
    /// <summary>
    /// Agenda Item.
    /// </summary>
    /// <seealso cref="Agenda.Domain.DomainObjects.AgendaItems.IAgendaItem" />
    public class AgendaItem : IAgendaItem
    {
        private IList<IAgendaItem> childAgendaItems = new List<IAgendaItem>();

        /// <summary>
        /// Initializes a new instance of the <see cref="AgendaItem"/> class.
        /// </summary>
        /// <param name="id">Agenda Item Id.</param>
        /// <param name="meetingId">Meeting Item Id.</param>
        /// <param name="parentId">Parent Agenda Item Id.</param>
        /// <param name="text">The text.</param>
        /// <param name="elderSiblingId">Elder Sibling Agenda Item Id.</param>
        /// <param name="agendaItemType">Agenda Item type.</param>
        /// <param name="childNumberingType">Child Agenda Item Numbering Type.</param>
        public AgendaItem(
            Guid id,
            Guid meetingId,
            Guid? parentId,
            string text,
            Guid? elderSiblingId,
            AgendaItemType agendaItemType,
            NumberingType childNumberingType)
        {
            this.Id = id;
            this.MeetingId = meetingId;
            this.ParentId = parentId;
            this.Text = text;
            this.ElderSiblingId = elderSiblingId;
            this.AgendaItemType = agendaItemType;
            this.ChildNumberingType = childNumberingType;
        }

        /// <inheritdoc/>
        public Guid Id { get; }

        /// <inheritdoc/>
        public Guid MeetingId { get; }

        /// <inheritdoc/>
        public Guid? ParentId { get; }

        /// <inheritdoc/>
        public string Text { get; }

        /// <inheritdoc/>
        public Guid? ElderSiblingId { get; }

        /// <inheritdoc/>
        public AgendaItemType AgendaItemType { get; }

        /// <inheritdoc/>
        public NumberingType ChildNumberingType { get; }

        /// <inheritdoc/>
        public IReadOnlyList<IAgendaItem> ChildAgendaItems =>
            (IReadOnlyList<IAgendaItem>)this.childAgendaItems;

        /// <inheritdoc/>
        public IAgendaItem BuildAgenda(IList<IAgendaItem> agendaItems)
        {
            foreach (IAgendaItem agendaItem in agendaItems)
            {
                if (agendaItem.ParentId == null)
                {
                    continue;
                }

                IAgendaItem parent = agendaItems
                    .Single(ai => ai.Id == agendaItem.ParentId);

                parent.AddChild(agendaItem);
            }

            foreach (IAgendaItem agendaItem in agendaItems)
            {
                agendaItem.SortChildAgendaItems();
            }

            return agendaItems.Single(ai => ai.ParentId == null);
        }

        /// <inheritdoc/>
        public void AddChild(IAgendaItem child)
        {
            this.childAgendaItems.Add(child);
        }

        /// <inheritdoc/>
        public void SortChildAgendaItems()
        {
            if (this.ChildAgendaItems.Count < 2)
            {
                return;
            }

            IList<IAgendaItem> agendaItems = new List<IAgendaItem>();

            Guid? guid = null;
            do
            {
                IAgendaItem? agendaItem = this.ChildAgendaItems
                    .SingleOrDefault(cai => cai.ElderSiblingId == guid);

                if (agendaItem == null)
                {
                    guid = null;
                }
                else
                {
                    agendaItems.Add(agendaItem);
                    guid = agendaItem.Id;
                }
            }
            while (guid != null);

            this.childAgendaItems = agendaItems;
        }
    }
}