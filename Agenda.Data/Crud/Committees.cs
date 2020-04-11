// <copyright file="Committees.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using Agenda.Utilities.Models.Whos;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Data.Crud
{
    /// <summary>
    /// CRUD operations for Committees.
    /// </summary>
    public partial class AgendaData
    {
        /// <param name="who"></param>
        /// <inheritdoc/>
        public async Task<bool> HaveCommitteesAsync(IWho who)
        {
            return await this.context.Committees
                .TagWith(this.Tag(who, nameof(this.HaveCommitteesAsync)))
                .AnyAsync()
                .ConfigureAwait(false);
        }
    }
}