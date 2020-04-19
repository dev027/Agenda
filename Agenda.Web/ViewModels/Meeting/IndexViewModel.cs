﻿// <copyright file="IndexViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Agenda.Domain.DomainObjects.Meetings;

namespace Agenda.Web.ViewModels.Meeting
{
    /// <summary>
    /// View Meeint view model.
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexViewModel"/> class.
        /// </summary>
        /// <param name="id">Committee Id.</param>
        /// <param name="organisationName">Organisation Name.</param>
        /// <param name="committeeName">Committee Name.</param>
        /// <param name="location">Location.</param>
        /// <param name="meetingDate">Meeting Date.</param>
        /// <param name="meetingTime">Meeting Time.</param>
        public IndexViewModel(
            Guid id,
            string organisationName,
            string committeeName,
            string location,
            string meetingDate,
            string meetingTime)
        {
            this.Id = id;
            this.OrganisationName = organisationName;
            this.CommitteeName = committeeName;
            this.Location = location;
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
        /// Gets the Committee Id.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the Organisation Name.
        /// </summary>
        [Display(Name = "Organisation")]
        public string OrganisationName { get; }

        /// <summary>
        /// Gets the Committee Name.
        /// </summary>
        [Display(Name = "Committee")]
        public string CommitteeName { get; }

        /// <summary>
        /// Gets Location.
        /// </summary>
        [Display(Name = "Location")]
        public string Location { get; }

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
                id: meeting.Id,
                organisationName: meeting.Committee.Organisation.Name,
                committeeName: meeting.Committee.Name,
                location: meeting.Location,
                meetingDate: date,
                meetingTime: time);
        }
    }
}