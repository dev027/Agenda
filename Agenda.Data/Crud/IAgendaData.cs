// <copyright file="IAgendaData.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

#nullable enable
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.AuditHeaders;
using Agenda.Domain.ValueObjects.Enums;
using Agenda.Utilities.Models.Whos;

namespace Agenda.Data.Crud
{
    /// <summary>
    /// Data Access Layer - Transactions.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public partial interface IAgendaData
    {
        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <param name="auditEvent">Audit Event.</param>
        /// <param name="who">Who details.</param>
        /// <returns>Audit Header.</returns>
        public IAuditHeaderWithAuditDetails BeginTransaction(
            AuditEvent auditEvent,
            IWho who);

        /// <summary>
        /// Commits the transaction.
        /// </summary>
        /// <param name="auditHeader">Audit Header.</param>
        /// <returns>Nothing.</returns>
        public Task CommitTransactionAsync(IAuditHeaderWithAuditDetails? auditHeader);

        /// <summary>
        /// Rollbacks the Transaction.
        /// </summary>
        public void RollbackTransaction();
    }
}