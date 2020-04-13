// <copyright file="Committees.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Data.Resources;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Utilities.Models.Whos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
            this.logger.LogTrace(
                LoggerResources.___EntryBy___,
                nameof(this.HaveCommitteesAsync),
                who);

            return await this.context.Committees
                .TagWith(this.Tag(who, nameof(this.HaveCommitteesAsync)))
                .AnyAsync()
                .ConfigureAwait(false);
        }
    }
}