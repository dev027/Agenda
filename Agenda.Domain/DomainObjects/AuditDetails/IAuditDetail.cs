// <copyright file="IAuditDetail.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Domain.DomainObjects.AuditHeaders;
using Agenda.Domain.ValueObjects.Enums;

namespace Agenda.Domain.DomainObjects.AuditDetails
{
    /// <summary>
    /// Audit Detail.
    /// </summary>
    public interface IAuditDetail
    {
        #region Properties

        /// <summary>
        /// Gets the Audit Detail Id.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the Table Name.
        /// </summary>
        string TableName { get; }

        /// <summary>
        /// Gets the Column Name.
        /// </summary>
        string ColumnName { get; }

        /// <summary>
        /// Gets the Record Id.
        /// </summary>
        Guid RecordId { get; }

        /// <summary>
        /// Gets the Old Value.
        /// </summary>
        string OldValue { get; }

        /// <summary>
        /// Gets the New Value.
        /// </summary>
        string NewValue { get; }

        /// <summary>
        /// Gets the Database Action.
        /// </summary>
        DatabaseAction DatabaseAction { get; }

        #endregion

        #region Parent Properties

        /// <summary>
        /// Gets the Audit Header.
        /// </summary>
        IAuditHeader AuditHeader { get; }

        #endregion
    }
}