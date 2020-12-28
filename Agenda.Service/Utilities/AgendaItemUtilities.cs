// <copyright file="AgendaItemUtilities.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using Agenda.Domain.DomainObjects.AgendaItems;

namespace Agenda.Service.Utilities
{
    /// <summary>
    /// Agenda Item utilities.
    /// </summary>
    public static class AgendaItemUtilities
    {
        /// <summary>
        /// Create an agenda item tree structure from a list of agenda items.
        /// </summary>
        /// <param name="agendaItems">List of agenda items.</param>
        /// <returns>Agenda Item tree structure.</returns>
        public static IAgendaItem CreateTreeStructure(IList<IAgendaItem> agendaItems)
        {
            IAgendaItem root = agendaItems.Single(ai => ai.ParentId == null);

            foreach (IAgendaItem agendaItem in agendaItems.Where(ai => ai.Id != root.Id))
            {
                IAgendaItem parent = agendaItems
                    .Single(ai => ai.Id == agendaItem.ParentId);

                parent.AddChild(agendaItem);
            }

            foreach (IAgendaItem agendaItem in agendaItems)
            {
                agendaItem.SortChildAgendaItems();
            }

            return root;
        }
    }
}