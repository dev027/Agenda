// <copyright file="Organisations.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.Linq;

namespace Agenda.Data.Crud
{
    /// <summary>
    /// CRUD operations for Organisations.
    /// </summary>
    public partial class AgendaData
    {
        /// <inheritdoc/>
        public bool HaveOrganisations()
        {
            return this.context.Organisations.Any();
        }
    }
}