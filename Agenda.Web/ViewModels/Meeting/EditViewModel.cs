// <copyright file="EditViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.DomainObjects.Locations;
using Agenda.Domain.DomainObjects.Meetings;
using Agenda.Web.Models;
using Agenda.Web.Models.ValidationAttributes;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        /// <param name="meetingId">Meeting Id.</param>
        /// <param name="organisationName">Organisation Name.</param>
        /// <param name="committeeName">Committee Name.</param>
        /// <param name="locationId">Location Id.</param>
        /// <param name="meetingDate">Meeting Date.</param>
        /// <param name="meetingTime">Meeting Time.</param>
        public EditViewModel(
            FormState formState,
            Guid meetingId,
            string organisationName,
            string committeeName,
            Guid locationId,
            string meetingDate,
            string meetingTime)
        {
            this.FormState = formState;
            this.MeetingId = meetingId;
            this.OrganisationName = organisationName;
            this.CommitteeName = committeeName;
            this.LocationId = locationId;
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
        public Guid MeetingId { get; set; }

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
        /// Gets or sets the Location Id.
        /// </summary>
        [Display(Name = "Location")]
        [MyRequired]
        public Guid LocationId { get; set; }

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

        /// <summary>
        /// Gets or sets the Location options.
        /// </summary>
        public SelectList LocationOptions { get; set; }

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
                meetingId: meeting.Id,
                organisationName: meeting.Committee.Organisation.Name,
                committeeName: meeting.Committee.Name,
                locationId: meeting.Location.Id,
                meetingDate: meeting.MeetingDateTime.ToString("dd/MM/yyyy", cultureInfo),
                meetingTime: meeting.MeetingDateTime.ToString("hh:mm", cultureInfo));
        }

        /// <summary>
        /// Refreshes the dropdown lists.
        /// </summary>
        /// <param name="locations">Locations.</param>
        public void RefreshDropdowns(IList<ILocation> locations)
        {
            if (locations == null)
            {
                throw new ArgumentNullException(nameof(locations));
            }

            this.LocationOptions = LocationDropdownOptions(locations);
        }

        /// <summary>
        /// Converts instance to Committee domain object.
        /// </summary>
        /// <param name="committee">Committee.</param>
        /// <param name="location">Location.</param>
        /// <returns>Committee domain object.</returns>
        public IMeeting ToDomain(
            ICommittee committee,
            ILocation location)
        {
            CultureInfo cultureInfo = new CultureInfo("en-GB");
            DateTime meetingDateTime = DateTime.Parse(this.MeetingDate, cultureInfo);
            string[] timeParts = this.MeetingTime.Split(":");
            meetingDateTime = meetingDateTime.AddHours(int.Parse(timeParts[0], cultureInfo));
            meetingDateTime = meetingDateTime.AddMinutes(int.Parse(timeParts[1], cultureInfo));

            return new Domain.DomainObjects.Meetings.Meeting(
                id: this.MeetingId,
                committee: committee,
                location: location,
                meetingDateTime: meetingDateTime);
        }

        #endregion Public Methods

        #region Private Methods

        private static SelectList LocationDropdownOptions(IList<ILocation> locations)
        {
            return new SelectList(
                locations, nameof(ILocation.Id), nameof(ILocation.Name));
        }

        #endregion Private Methods
    }
}