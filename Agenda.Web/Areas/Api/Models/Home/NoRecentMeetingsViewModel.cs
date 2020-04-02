// <copyright file="NoRecentMeetingsViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using Agenda.Domain.ValueObjects.SetupStatii;

namespace Agenda.Web.Areas.Api.Models.Home
{
    /// <summary>
    /// No Recent Meetings view model.
    /// </summary>
    public class NoRecentMeetingsViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoRecentMeetingsViewModel"/> class.
        /// </summary>
        /// <param name="showAddOrganisationButton">Show Add Organisation Button.</param>
        /// <param name="showAddCommitteeButton">Show Add Committee Button.</param>
        /// <param name="showAddMeetingButton">Show Add Meeting Button.</param>
        public NoRecentMeetingsViewModel(
            bool showAddOrganisationButton,
            bool showAddCommitteeButton,
            bool showAddMeetingButton)
        {
            this.ShowAddOrganisationButton = showAddOrganisationButton;
            this.ShowAddCommitteeButton = showAddCommitteeButton;
            this.ShowAddMeetingButton = showAddMeetingButton;
        }

        /// <summary>
        /// Gets a value indicating whether [show add organisation button].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show add organisation button]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowAddOrganisationButton { get; }

        /// <summary>
        /// Gets a value indicating whether [show add committee button].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show add committee button]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowAddCommitteeButton { get; }

        /// <summary>
        /// Gets a value indicating whether [show add meeting button].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show add meeting button]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowAddMeetingButton { get; }

        /// <summary>
        /// Creates a No Recent Meetings view model.
        /// </summary>
        /// <param name="setupStatus">The setup status.</param>
        /// <returns>No Recent Meetings view model.</returns>
        public static NoRecentMeetingsViewModel Create(ISetupStatus setupStatus)
        {
            if (setupStatus == null)
            {
                return new NoRecentMeetingsViewModel(
                    showAddOrganisationButton: false,
                    showAddCommitteeButton: false,
                    showAddMeetingButton: false);
            }

            return new NoRecentMeetingsViewModel(
                showAddOrganisationButton: !setupStatus.HaveOrganisations,
                showAddCommitteeButton: setupStatus.HaveOrganisations && !setupStatus.HaveCommittees,
                showAddMeetingButton: setupStatus.HaveOrganisations && setupStatus.HaveCommittees);
        }
    }
}