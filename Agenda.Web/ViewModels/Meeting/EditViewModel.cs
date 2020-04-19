// <copyright file="EditViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.DomainObjects.Meetings;
using Agenda.Web.Models;
using Agenda.Web.Models.ValidationAttributes;
using DomainMetadata = Agenda.Domain.DomainObjects.Meetings.DomainMetadata;

namespace Agenda.Web.ViewModels.Meeting
{
    /// <summary>
    /// Add view model.
    /// </summary>
    public class EditViewModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EditViewModel"/> class.
        /// </summary>
        public EditViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditViewModel"/> class.
        /// </summary>
        /// <param name="formState">Form State.</param>
        /// <param name="id">Meeting Id.</param>
        /// <param name="organisationName">Organisation Name.</param>
        /// <param name="committeeName">Committee Name.</param>
        /// <param name="location">Location.</param>
        /// <param name="meetingDate">Meeting Date.</param>
        /// <param name="meetingTime">Meeting Time.</param>
        public EditViewModel(
            FormState formState,
            Guid id,
            string organisationName,
            string committeeName,
            string location,
            string meetingDate,
            string meetingTime)
        {
            this.FormState = formState;
            this.Id = id;
            this.OrganisationName = organisationName;
            this.CommitteeName = committeeName;
            this.Location = location;
            this.MeetingDate = meetingDate;
            this.MeetingTime = meetingTime;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the Form State.
        /// </summary>
        public FormState FormState { get; set; }

        /// <summary>
        /// Gets or sets the Meeting Id.
        /// </summary>
        [MyRequired]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Organisation Name.
        /// </summary>
        [Display(Name = "Organisation")]
        [MyRequired]
        public string OrganisationName { get; set; }

        /// <summary>
        /// Gets or sets the Committee Name.
        /// </summary>
        [Display(Name = "Committee")]
        [MyRequired]
        public string CommitteeName { get; set; }

        /// <summary>
        /// Gets or sets the Location.
        /// </summary>
        [Display(Name = "Location")]
        [MyStringLength(
            DomainMetadata.Location.MaxLength,
            DomainMetadata.Location.MinLength)]
        [MyRequired]
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the Meeting Date.
        /// </summary>
        [Display(Name = "Date")]
        [ValidDate]
        [MyRequired]
        public string MeetingDate { get; set; }

        /// <summary>
        /// Gets or sets the Meeting Time.
        /// </summary>
        [Display(Name = "Date")]
        [ValidTime]
        [MyRequired]
        public string MeetingTime { get; set; }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Creates the view model.
        /// </summary>
        /// <param name="meeting">Meeting.</param>
        /// <returns>Edit view model.</returns>
        public static EditViewModel Create(IMeeting meeting)
        {
            if (meeting == null)
            {
                throw new ArgumentNullException(nameof(meeting));
            }

            CultureInfo cultureInfo = CultureInfo.GetCultureInfo("en-GB");

            return new EditViewModel(
                formState: FormState.Initial,
                id: meeting.Id,
                organisationName: meeting.Committee.Organisation.Name,
                committeeName: meeting.Committee.Name,
                location: meeting.Location,
                meetingDate: meeting.MeetingDateTime.ToString("dd/MM/yyyy", cultureInfo),
                meetingTime: meeting.MeetingDateTime.ToString("hh:mm", cultureInfo));
        }

        /// <summary>
        /// Converts instance to Committee domain object.
        /// </summary>
        /// <param name="committee">Committee.</param>
        /// <returns>Committee domain object.</returns>
        public IMeeting ToDomain(ICommittee committee)
        {
            CultureInfo cultureInfo = new CultureInfo("en-GB");
            DateTime meetingDateTime = DateTime.Parse(this.MeetingDate, cultureInfo);
            string[] timeParts = this.MeetingTime.Split(":");
            meetingDateTime = meetingDateTime.AddHours(int.Parse(timeParts[0], cultureInfo));
            meetingDateTime = meetingDateTime.AddMinutes(int.Parse(timeParts[1], cultureInfo));

            return new Domain.DomainObjects.Meetings.Meeting(
                id: this.Id,
                committee: committee,
                meetingDateTime: meetingDateTime,
                location: this.Location);
        }

        #endregion Public Methods
    }
}