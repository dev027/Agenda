// <copyright file="Meeting.cs" company="Do It Wright">
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
    /// <seealso cref="IMeeting" />
    public class Meeting : IMeeting
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Meeting"/> class.
        /// </summary>
        /// <param name="id">Meeting Id.</param>
        /// <param name="committee">Committee.</param>
        /// <param name="location">Location.</param>
        /// <param name="meetingDateTime">Date and Time of Meeting.</param>
        public Meeting(
            Guid id,
            ICommittee committee,
            ILocation location,
            DateTime meetingDateTime)
        {
            this.Id = id;
            this.Committee = committee ?? throw new ArgumentNullException(nameof(committee));
            this.Location = location ?? throw new ArgumentNullException(nameof(location));
            this.MeetingDateTime = meetingDateTime;
        }

        /// <inheritdoc/>
        public Guid Id { get; }

        /// <inheritdoc/>
        public ICommittee Committee { get; }

        /// <inheritdoc/>
        public ILocation Location { get; }

        /// <inheritdoc/>
        public DateTime MeetingDateTime { get; }
    }
}