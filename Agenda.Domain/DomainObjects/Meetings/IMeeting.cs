// <copyright file="IMeeting.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.DomainObjects.Locations;

namespace Agenda.Domain.DomainObjects.Meetings
{
    /// <summary>
    /// Meeting.
    /// </summary>
    public interface IMeeting
    {
        /// <summary>
        /// Gets the Meeting Id.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the committee.
        /// </summary>
        ICommittee Committee { get; }

        /// <summary>
        /// Gets the Location.
        /// </summary>
        ILocation Location { get; }

        /// <summary>
        /// Gets the Meeting date and time.
        /// </summary>
        DateTime MeetingDateTime { get; }
    }
}