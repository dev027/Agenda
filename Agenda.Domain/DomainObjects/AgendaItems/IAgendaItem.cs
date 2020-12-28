// <copyright file="IAgendaItem.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Agenda.Domain.ValueObjects.Enums;
using Agenda.Utilities.ExtensionMethods.Integer;

namespace Agenda.Domain.DomainObjects.AgendaItems
{
    /// <summary>
    /// Agenda Item.
    /// </summary>
    public interface IAgendaItem
    {
        /// <summary>
        /// Gets the Agenda Item Id.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the Meeting Id.
        /// </summary>
        Guid MeetingId { get; }

        /// <summary>
        /// Gets the Parent Agenda Item Id.
        /// </summary>
        Guid? ParentId { get; }

        /// <summary>
        /// Gets the Text.
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Gets the Elder Sibling Id.
        /// </summary>
        Guid? ElderSiblingId { get; }

        /// <summary>
        /// Gets the Agenda Item Type.
        /// </summary>
        AgendaItemType AgendaItemType { get; }

        /// <summary>
        /// Gets the Child Numbering Type.
        /// </summary>
        NumberingType ChildNumberingType { get; }

        /// <summary>
        /// Gets the Child Agenda Items.
        /// </summary>
        IReadOnlyList<IAgendaItem> ChildAgendaItems { get; }

        /// <summary>
        /// Adds the child agenda item.
        /// </summary>
        /// <param name="child">The child agenda item.</param>
        void AddChild(IAgendaItem child);

        /// <summary>
        /// Sorts the child agenda items.
        /// </summary>
        void SortChildAgendaItems();
    }
}