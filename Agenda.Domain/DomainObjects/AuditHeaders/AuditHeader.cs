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
        public AuditHeader(
            Guid id,
            AuditEvent auditEvent,
            DateTime timeStamp)
        {
            this.Id = id;
            this.AuditEvent = auditEvent;
            this.TimeStamp = timeStamp;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditHeader"/> class.
        /// </summary>
        /// <param name="auditEvent">Audit Event.</param>
        public AuditHeader(
            AuditEvent auditEvent)
        {
            this.Id = Guid.NewGuid();
            this.AuditEvent = auditEvent;
            this.TimeStamp = DateTime.Now;
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
    }
}