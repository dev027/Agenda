// <copyright file="IndexViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using Agenda.Domain.DomainObjects.Committees;

namespace Agenda.Web.ViewModels.MeetingOverview
{
    /// <summary>
    /// Index view model.
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexViewModel"/> class.
        /// </summary>
        /// <param name="committeeId">Committee Id.</param>
        /// <param name="committeeName">Committee Name.</param>
        /// <param name="organisationId">Organisation Id.</param>
        /// <param name="organisationName">Organisation Name.</param>
        /// <param name="meetingViewModels">Meeting view models.</param>
        public IndexViewModel(
            Guid committeeId,
            string committeeName,
            Guid organisationId,
            string organisationName,
            IList<MeetingViewModel> meetingViewModels)
        {
            this.CommitteeId = committeeId;
            this.CommitteeName = committeeName;
            this.OrganisationId = organisationId;
            this.OrganisationName = organisationName;
            this.MeetingViewModels = meetingViewModels;
        }

        /// <summary>
        /// Gets the Committee Id.
        /// </summary>
        public Guid CommitteeId { get; }

        /// <summary>
        /// Gets the Committee Name.
        /// </summary>
        public string CommitteeName { get; }

        /// <summary>
        /// Gets the Organisation Id.
        /// </summary>
        public Guid OrganisationId { get; }

        /// <summary>
        /// Gets the Organisation Name.
        /// </summary>
        public string OrganisationName { get; }

        /// <summary>
        /// Gets the Meeting view models.
        /// </summary>
        public IList<MeetingViewModel> MeetingViewModels { get; }

        /// <summary>
        /// Creates the Index view model.
        /// </summary>
        /// <param name="committee">The committee.</param>
        /// <returns>Index view model.</returns>
        public static IndexViewModel Create(ICommitteeWithMeetings committee)
        {
            if (committee == null)
            {
                throw new ArgumentNullException(nameof(committee));
            }

            return new IndexViewModel(
                committeeId: committee.Id,
                committeeName: committee.Name,
                organisationId: committee.Organisation.Id,
                organisationName: committee.Organisation.Name,
                meetingViewModels: committee.Meetings.Select(MeetingViewModel.Create).ToList());
        }
    }
}