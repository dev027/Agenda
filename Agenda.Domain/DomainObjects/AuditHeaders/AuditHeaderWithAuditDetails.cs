// <copyright file="AuditHeaderWithAuditDetails.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Agenda.Domain.DomainObjects.AuditDetails;
using Agenda.Domain.ValueObjects.Enums;

namespace Agenda.Domain.DomainObjects.AuditHeaders
{
    /// <summary>
    /// Audit Header with Audit Details.
    /// </summary>
    /// <seealso cref="AuditHeader" />
    /// <seealso cref="IAuditHeaderWithAuditDetails" />
    public class AuditHeaderWithAuditDetails : AuditHeader, IAuditHeaderWithAuditDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditHeaderWithAuditDetails"/> class.
        /// </summary>
        /// <param name="id">Audit Header Id.</param>
        /// <param name="auditEvent">Audit Event.</param>
        /// <param name="timeStamp">Time Stamp.</param>
        /// <param name="auditDetails">Audit Details.</param>
        /// <param name="username">Username.</param>
        /// <param name="correlationId">Correlation Id.</param>
        public AuditHeaderWithAuditDetails(
            Guid id,
            AuditEvent auditEvent,
            DateTime timeStamp,
            IList<IAuditDetail> auditDetails,
            string username,
            Guid correlationId)
        : base(id, auditEvent, timeStamp, username, correlationId)
        {
            this.AuditDetails = auditDetails ?? throw new ArgumentNullException(nameof(auditDetails));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditHeaderWithAuditDetails"/> class.
        /// </summary>
        /// <param name="auditEvent">Audit Event.</param>
        /// <param name="username">Username.</param>
        /// <param name="correlationId">Correlation Id.</param>
        public AuditHeaderWithAuditDetails(
            AuditEvent auditEvent,
            string username,
            Guid correlationId)
        : base(auditEvent, username, correlationId)
        {
            this.AuditDetails = new List<IAuditDetail>();
        }

        /// <summary>
        /// Gets the Audit Details.
        /// </summary>
        public IList<IAuditDetail> AuditDetails { get; }
    }
}