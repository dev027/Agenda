// <copyright file="IndexViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using Agenda.Domain.DomainObjects.Meetings;
using Agenda.Domain.ValueObjects.SetupStatii;
using Agenda.Web.Controllers;
using Agenda.Web.ViewModels.Shared;

namespace Agenda.Web.Areas.Api.Models.Home
{
    /// <summary>
    /// View model for <see cref="HomeController.Index"/>.
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexViewModel"/> class.
        /// </summary>
        /// <param name="headerViewModel">Header view model.</param>
        /// <param name="noRecentMeetingsViewModel">No Recent Meetings view model.</param>
        /// <param name="meetingCardViewModels">Meeting Card view models.</param>
        public IndexViewModel(
            HeaderViewModel headerViewModel,
            NoRecentMeetingsViewModel noRecentMeetingsViewModel,
            IList<MeetingCardViewModel> meetingCardViewModels)
        {
            this.HeaderViewModel = headerViewModel;
            this.NoRecentMeetingsViewModel = noRecentMeetingsViewModel;
            this.MeetingCardViewModels = meetingCardViewModels;
        }

        /// <summary>
        /// Gets the header view model.
        /// </summary>
        public HeaderViewModel HeaderViewModel { get; }

        /// <summary>
        /// Gets the no recent meetings view model.
        /// </summary>
        public NoRecentMeetingsViewModel NoRecentMeetingsViewModel { get; }

        /// <summary>
        /// Gets the meeting card view models.
        /// </summary>
        public IList<MeetingCardViewModel> MeetingCardViewModels { get; }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <param name="recentMeetings">Recent meetings.</param>
        /// <param name="setupStatus">Setup status.</param>
        /// <returns>Index view model.</returns>
        public static IndexViewModel Create(
            IList<IMeeting> recentMeetings,
            ISetupStatus setupStatus)
        {
            return new IndexViewModel(
                headerViewModel: HeaderViewModel.Create(),
                noRecentMeetingsViewModel: NoRecentMeetingsViewModel.Create(setupStatus),
                meetingCardViewModels: recentMeetings
                    .Select(MeetingCardViewModel.Create)
                    .ToList());
        }
    }
}