// <copyright file="MeetingViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Agenda.Domain.DomainObjects.Meetings;

namespace Agenda.Web.ViewModels.MeetingOverview
{
    /// <summary>
    /// Committee view model.
    /// </summary>
    public class MeetingViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingViewModel"/> class.
        /// </summary>
        /// <param name="meetingId">Meeting Id.</param>
        /// <param name="meetingDate">Meeting Date.</param>
        /// <param name="meetingTime">Meeting Time.</param>
        public MeetingViewModel(
            Guid meetingId,
            string meetingDate,
            string meetingTime)
        {
            this.MeetingId = meetingId;
            this.MeetingDate = meetingDate;
            this.MeetingTime = meetingTime;
        }

        /// <summary>
        /// Gets the view action button text.
        /// </summary>
        [Display(Name = "View")]
        public string ViewActionButtonText { get; } = null;

        /// <summary>
        /// Gets the Meeting Id.
        /// </summary>
        public Guid MeetingId { get; }

        /// <summary>
        /// Gets the Meeting Date.
        /// </summary>
        [DisplayName("Date")]
        public string MeetingDate { get; }

        /// <summary>
        /// Gets the Meeting Time.
        /// </summary>
        [DisplayName("Time")]
        public string MeetingTime { get; }

        /// <summary>
        /// Creates the view model.
        /// </summary>
        /// <param name="meeting">Meeting.</param>
        /// <returns>View model.</returns>
        public static MeetingViewModel Create(
            IMeeting meeting)
        {
            if (meeting == null)
            {
                throw new ArgumentNullException(nameof(meeting));
            }

            CultureInfo cultureInfo = CultureInfo.GetCultureInfo("en-GB");

            string date = meeting.MeetingDateTime.Date.ToLongDateString();
            string time = meeting.MeetingDateTime.ToString("h:mm tt", cultureInfo);

            return new MeetingViewModel(
                meetingId: meeting.Id,
                meetingDate: date,
                meetingTime: time);
        }
    }
}