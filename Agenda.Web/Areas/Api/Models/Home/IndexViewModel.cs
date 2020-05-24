// <copyright file="IndexViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using Agenda.Domain.DomainObjects.Meetings;
using Agenda.Domain.ValueObjects.SetupStatii;
using Agenda.Web.Controllers;

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
        /// <param name="noRecentMeetingsViewModel">No Recent Meetings view model.</param>
        /// <param name="meetingCardViewModels">Meeting Card view models.</param>
        /// <param name="featureEnabled">Feature Enabled.</param>
        public IndexViewModel(
            NoRecentMeetingsViewModel noRecentMeetingsViewModel,
            IList<MeetingCardViewModel> meetingCardViewModels,
            bool featureEnabled)
        {
            this.NoRecentMeetingsViewModel = noRecentMeetingsViewModel;
            this.MeetingCardViewModels = meetingCardViewModels;
            this.FeatureEnabled = featureEnabled;
        }

        /// <summary>
        /// Gets the no recent meetings view model.
        /// </summary>
        public NoRecentMeetingsViewModel NoRecentMeetingsViewModel { get; }

        /// <summary>
        /// Gets the meeting card view models.
        /// </summary>
        public IList<MeetingCardViewModel> MeetingCardViewModels { get; }

        /// <summary>
        /// Gets a value indicating whether [feature enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [feature enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool FeatureEnabled { get; }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <param name="recentMeetings">Recent meetings.</param>
        /// <param name="setupStatus">Setup status.</param>
        /// <param name="featureEnabled">Is Feature Enabled.</param>
        /// <returns>Index view model.</returns>
        public static IndexViewModel Create(
            IList<IMeeting> recentMeetings,
            ISetupStatus setupStatus,
            bool featureEnabled)
        {
            return new IndexViewModel(
                noRecentMeetingsViewModel: NoRecentMeetingsViewModel.Create(setupStatus),
                meetingCardViewModels: recentMeetings
                    .Select(MeetingCardViewModel.Create)
                    .ToList(),
                featureEnabled: featureEnabled);
        }
    }
}