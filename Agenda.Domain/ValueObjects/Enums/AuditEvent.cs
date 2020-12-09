// <copyright file="AuditEvent.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

namespace Agenda.Domain.ValueObjects.Enums
{
    /// <summary>
    /// Audit Events.
    /// </summary>
    public enum AuditEvent
    {
        /// <summary>
        /// Organisation Maintenance
        /// </summary>
        OrganisationMaintenance = 1,

        /// <summary>
        /// Committee Maintenance
        /// </summary>
        CommitteeMaintenance = 2,

        /// <summary>
        /// Location Maintenance
        /// </summary>
        LocationMaintenance = 3,

        /// <summary>
        /// Meeting Maintenance
        /// </summary>
        MeetingMaintenance = 4,

        /// <summary>
        /// Location Type Maintenance
        /// </summary>
        LocationTypeMaintenance = 5
    }
}