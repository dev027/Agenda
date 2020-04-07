// <copyright file="Committees.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Data.Crud
{
    /// <summary>
    /// CRUD operations for Committees.
    /// </summary>
    public partial class AgendaData
    {
        /// <inheritdoc/>
        public async Task<bool> HaveCommitteesAsync()
        {
            return await this.context.Committees.AnyAsync().ConfigureAwait(false);
        }
    }
}