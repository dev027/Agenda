// <copyright file="IMeeting.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Domain.DomainObjects.Committees;

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
        /// Gets the Meeting date and time.
        /// </summary>
        DateTime MeetingDateTime { get; }

        /// <summary>
        /// Gets the Location.
        /// </summary>
        string Location { get; }
    }
}