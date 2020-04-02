// <copyright file="Committees.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.Linq;

namespace Agenda.Data.Crud
{
    /// <summary>
    /// CRUD operations for Committees.
    /// </summary>
    public partial class AgendaData
    {
        /// <inheritdoc/>
        public bool HaveCommittees()
        {
            return this.context.Committees.Any();
        }
    }
}