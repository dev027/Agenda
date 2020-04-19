// <copyright file="IndexViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Agenda.Domain.DomainObjects.Committees;

namespace Agenda.Web.ViewModels.Committee
{
    /// <summary>
    /// View Organisation view model.
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexViewModel"/> class.
        /// </summary>
        /// <param name="id">Committee Id.</param>
        /// <param name="organisationName">Organisation Name.</param>
        /// <param name="name">Committee Name.</param>
        /// <param name="description">Committee Description.</param>
        /// <param name="meetingViewModels">Meeting view models.</param>
        public IndexViewModel(
            Guid id,
            string organisationName,
            string name,
            string description,
            IList<MeetingViewModel> meetingViewModels)
        {
            this.Id = id;
            this.OrganisationName = organisationName;
            this.Name = name;
            this.Description = description;
            this.MeetingViewModels = meetingViewModels;
        }

        /// <summary>
        /// Gets the Page Title.
        /// </summary>
        [Display(Name = "View Committee")]
        public string PageTitle { get; } = null;

        /// <summary>
        /// Gets the Card Title.
        /// </summary>
        [Display(Name = "Committee")]
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
        [Display(Name = "Name")]
        public string Name { get; }

        /// <summary>
        /// Gets Committee Description.
        /// </summary>
        [Display(Name = "Description")]
        public string Description { get; }

        /// <summary>
        /// Gets the Meeting view models.
        /// </summary>
        public IList<MeetingViewModel> MeetingViewModels { get; }

        /// <summary>
        /// Creates the Index view model.
        /// </summary>
        /// <param name="committee">Committee.</param>
        /// <returns>View model.</returns>
        public static IndexViewModel Create(
            ICommitteeWithMeetings committee)
        {
            if (committee == null)
            {
                throw new ArgumentNullException(nameof(committee));
            }

            return new IndexViewModel(
                id: committee.Id,
                organisationName: committee.Organisation.Name,
                name: committee.Name,
                description: committee.Description,
                meetingViewModels: committee.Meetings
                    .OrderByDescending(m => m.MeetingDateTime)
                    .Select(MeetingViewModel.Create)
                    .ToList());
        }
    }
}