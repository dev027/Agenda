// <copyright file="MeetingCardViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Domain.DomainObjects.Meetings;

namespace Agenda.Web.Areas.Api.Models.Home
{
    /// <summary>
    /// Meeting Card View Model.
    /// </summary>
    public class MeetingCardViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingCardViewModel"/> class.
        /// </summary>
        /// <param name="meetingId">Meeting Id.</param>
        /// <param name="organisationName">Organisation Name.</param>
        /// <param name="committeeName">Committee Name.</param>
        /// <param name="meetingDateTime">Date and Time of the Meeting.</param>
        /// <param name="location">Location.</param>
        public MeetingCardViewModel(
            Guid meetingId,
            string organisationName,
            string committeeName,
            DateTime meetingDateTime,
            string location)
        {
            this.MeetingId = meetingId;
            this.OrganisationName = organisationName;
            this.CommitteeName = committeeName;
            this.MeetingDateTime = meetingDateTime;
            this.Location = location;
        }

        /// <summary>
        /// Gets the Meeting Id.
        /// </summary>
        public Guid MeetingId { get; }

        /// <summary>
        /// Gets the Organisation Name.
        /// </summary>
        public string OrganisationName { get; }

        /// <summary>
        /// Gets the Committee. Name.
        /// </summary>
        public string CommitteeName { get; }

        /// <summary>
        /// Gets the Date and Time of the Meeting.
        /// </summary>
        public DateTime MeetingDateTime { get; }

        /// <summary>
        /// Gets the Location.
        /// </summary>
        public string Location { get; }

        /// <summary>
        /// Creates the Meeting Card View Model.
        /// </summary>
        /// <param name="meeting">Meeting.</param>
        /// <returns>Meeting Card View Model.</returns>
        public static MeetingCardViewModel Create(IMeeting meeting)
        {
            if (meeting == null)
            {
                throw new ArgumentNullException(nameof(meeting));
            }

            return new MeetingCardViewModel(
                meetingId: meeting.Id,
                organisationName: meeting.Committee.Organisation.Name,
                committeeName: meeting.Committee.Name,
                meetingDateTime: meeting.MeetingDateTime,
                location: meeting.Location.Name);
        }
    }
}
