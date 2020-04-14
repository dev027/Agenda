// <copyright file="ICommitteeWithMeetings.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Agenda.Domain.DomainObjects.Meetings;

namespace Agenda.Domain.DomainObjects.Committees
{
    /// <summary>
    /// Committee with Meetings.
    /// </summary>
    public interface ICommitteeWithMeetings : ICommittee
    {
        /// <summary>
        /// Gets Meetings.
        /// </summary>
        IList<IMeeting> Meetings { get; }
    }
}