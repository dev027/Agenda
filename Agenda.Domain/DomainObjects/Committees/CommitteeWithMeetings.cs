// <copyright file="CommitteeWithMeetings.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Agenda.Domain.DomainObjects.Meetings;
using Agenda.Domain.DomainObjects.Organisations;

namespace Agenda.Domain.DomainObjects.Committees
{
    /// <summary>
    ///  Committee with Meetings.
    /// </summary>
    /// <seealso cref="Committee" />
    /// <seealso cref="ICommitteeWithMeetings" />
    public class CommitteeWithMeetings : Committee, ICommitteeWithMeetings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommitteeWithMeetings"/> class.
        /// </summary>
        /// <param name="id">Committee Id.</param>
        /// <param name="organisation">Organisation.</param>
        /// <param name="name">Committee Name.</param>
        /// <param name="description">Committee Description.</param>
        /// <param name="meetings">Meetings.</param>
        public CommitteeWithMeetings(
            Guid id,
            IOrganisation organisation,
            string name,
            string description,
            IList<IMeeting> meetings)
            : base(
                id, organisation, name, description)
        {
            this.Meetings = meetings ?? throw new ArgumentNullException(nameof(organisation));
        }

        /// <inheritdoc/>
        public IList<IMeeting> Meetings { get; }
    }
}