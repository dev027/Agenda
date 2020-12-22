// <copyright file="IndexViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Agenda.Domain.DomainObjects.Meetings;

namespace Agenda.Web.ViewModels.Meeting
{
    /// <summary>
    /// View Meeting view model.
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexViewModel"/> class.
        /// </summary>
        /// <param name="meetingId">Meeting Id.</param>
        /// <param name="organisationId">Organisation Id.</param>
        /// <param name="organisationName">Organisation Name.</param>
        /// <param name="committeeId">Committee Id.</param>
        /// <param name="committeeName">Committee Name.</param>
        /// <param name="meetingDate">Meeting Date.</param>
        /// <param name="meetingTime">Meeting Time.</param>
        public IndexViewModel(
            Guid meetingId,
            Guid organisationId,
            string organisationName,
            Guid committeeId,
            string committeeName,
            string meetingDate,
            string meetingTime)
        {
            this.MeetingId = meetingId;
            this.OrganisationId = organisationId;
            this.OrganisationName = organisationName;
            this.CommitteeId = committeeId;
            this.CommitteeName = committeeName;
            this.MeetingDate = meetingDate;
            this.MeetingTime = meetingTime;
        }

        /// <summary>
        /// Gets the Page Title.
        /// </summary>
        [Display(Name = "View Meeting")]
        public string PageTitle { get; } = null;

        /// <summary>
        /// Gets the Card Title.
        /// </summary>
        [Display(Name = "Meeting")]
        public string CardTitle { get; } = null;

        /// <summary>
        /// Gets the Meeting Id.
        /// </summary>
        public Guid MeetingId { get; }

        /// <summary>
        /// Gets the Organisation Id.
        /// </summary>
        public Guid OrganisationId { get; }

        /// <summary>
        /// Gets the Organisation Name.
        /// </summary>
        [Display(Name = "Organisation")]
        public string OrganisationName { get; }

        /// <summary>
        /// Gets the Committee Id.
        /// </summary>
        public Guid CommitteeId { get; }

        /// <summary>
        /// Gets the Committee Name.
        /// </summary>
        [Display(Name = "Committee")]
        public string CommitteeName { get; }

        /// <summary>
        /// Gets Meeting Date.
        /// </summary>
        [Display(Name = "Date")]
        public string MeetingDate { get; }

        /// <summary>
        /// Gets Meeting Time.
        /// </summary>
        [Display(Name = "Time")]
        public string MeetingTime { get; }

        /// <summary>
        /// Creates the Index view model.
        /// </summary>
        /// <param name="meeting">Meeting.</param>
        /// <returns>View model.</returns>
        public static IndexViewModel Create(
            IMeeting meeting)
        {
            if (meeting == null)
            {
                throw new ArgumentNullException(nameof(meeting));
            }

            CultureInfo cultureInfo = CultureInfo.GetCultureInfo("en-GB");
            string date = meeting.MeetingDateTime.ToLongDateString();
            string time = meeting.MeetingDateTime.ToString("h:mm tt", cultureInfo);

            return new IndexViewModel(
                meetingId: meeting.Id,
                organisationId: meeting.Committee.Organisation.Id,
                organisationName: meeting.Committee.Organisation.Name,
                committeeId: meeting.Committee.Id,
                committeeName: meeting.Committee.Name,
                meetingDate: date,
                meetingTime: time);
        }
    }
}