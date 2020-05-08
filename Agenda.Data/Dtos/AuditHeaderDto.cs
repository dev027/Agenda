// <copyright file="AuditHeaderDto.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using Agenda.Domain.DomainObjects.AuditHeaders;
using Agenda.Domain.ValueObjects.Enums;

namespace Agenda.Data.Dtos
{
    /// <summary>
    /// Audit Header DTO.
    /// </summary>
    public class AuditHeaderDto
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditHeaderDto"/> class.
        /// </summary>
        public AuditHeaderDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditHeaderDto"/> class.
        /// </summary>
        /// <param name="id">Audit Header Id.</param>
        /// <param name="auditEvent">Audit Event.</param>
        /// <param name="timeStamp">Time Stamp.</param>
        public AuditHeaderDto(
            Guid id,
            AuditEvent auditEvent,
            DateTime timeStamp)
        {
            this.Id = id;
            this.AuditEvent = auditEvent;
            this.TimeStamp = timeStamp;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditHeaderDto"/> class.
        /// </summary>
        /// <param name="id">Audit Header Id.</param>
        /// <param name="auditEvent">Audit Event.</param>
        /// <param name="timeStamp">Time Stamp.</param>
        /// <param name="auditDetails">Audit Details.</param>
        public AuditHeaderDto(
            Guid id,
            AuditEvent auditEvent,
            DateTime timeStamp,
            IList<AuditDetailDto> auditDetails)
        {
            this.Id = id;
            this.AuditEvent = auditEvent;
            this.TimeStamp = timeStamp;
            this.AuditDetails = auditDetails;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditHeaderDto"/> class.
        /// </summary>
        /// <param name="auditEvent">Audit Event.</param>
        public AuditHeaderDto(
            AuditEvent auditEvent)
        {
            this.Id = Guid.NewGuid();
            this.AuditEvent = auditEvent;
            this.TimeStamp = DateTime.Now;
            this.AuditDetails = new List<AuditDetailDto>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets Audit Header Id.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the Audit Event.
        /// </summary>
        public AuditEvent AuditEvent { get; private set; }

        /// <summary>
        /// Gets the Time Stamp.
        /// </summary>
        public DateTime TimeStamp { get; private set; }

        #endregion

        #region Child Properties

        /// <summary>
        /// Gets the Audit Details.
        /// </summary>
        public IList<AuditDetailDto> AuditDetails { get; private set; } = null!;

        #endregion

        #region Public Methids

        /// <summary>
        /// Converts to DTO with AuditDetails.
        /// </summary>
        /// <param name="auditHeader">Audit Header.</param>
        /// <returns>Audit Header DTO with Audit Details.</returns>
        public static AuditHeaderDto ToDtoWithAuditDetails(
            IAuditHeaderWithAuditDetails auditHeader)
        {
            if (auditHeader == null)
            {
                throw new ArgumentNullException(nameof(auditHeader));
            }

            return new AuditHeaderDto(
                auditHeader.Id,
                auditHeader.AuditEvent,
                auditHeader.TimeStamp,
                auditHeader.AuditDetails.Select(AuditDetailDto.ToDto).ToList());
        }

        #endregion
    }
}