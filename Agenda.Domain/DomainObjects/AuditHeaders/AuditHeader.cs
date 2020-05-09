// <copyright file="AuditHeader.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Domain.ValueObjects.Enums;

namespace Agenda.Domain.DomainObjects.AuditHeaders
{
    /// <summary>
    /// Audit Header.
    /// </summary>
    /// <seealso cref="IAuditHeader" />
    public class AuditHeader : IAuditHeader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditHeader"/> class.
        /// </summary>
        /// <param name="id">Audit Header Id.</param>
        /// <param name="auditEvent">Audit Event.</param>
        /// <param name="timeStamp">Time Stamp.</param>
        /// <param name="username">Username.</param>
        /// <param name="correlationId">Correlation Id.</param>
        public AuditHeader(
            Guid id,
            AuditEvent auditEvent,
            DateTime timeStamp,
            string username,
            Guid correlationId)
        {
            this.Id = id;
            this.AuditEvent = auditEvent;
            this.TimeStamp = timeStamp;
            this.Username = username;
            this.CorrelationId = correlationId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditHeader"/> class.
        /// </summary>
        /// <param name="auditEvent">Audit Event.</param>
        /// <param name="username">Username.</param>
        /// <param name="correlationId">Correlation Id.</param>
        public AuditHeader(
            AuditEvent auditEvent,
            string username,
            Guid correlationId)
        {
            this.Id = Guid.NewGuid();
            this.AuditEvent = auditEvent;
            this.TimeStamp = DateTime.Now;
            this.Username = username;
            this.CorrelationId = correlationId;
        }

        /// <summary>
        /// Gets Audit Header Id.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the Audit Event.
        /// </summary>
        public AuditEvent AuditEvent { get; }

        /// <summary>
        /// Gets the Time Stamp.
        /// </summary>
        public DateTime TimeStamp { get; }

        /// <summary>
        /// Gets the Username.
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// Gets the Correlation Id.
        /// </summary>
        public Guid CorrelationId { get; }
    }
}