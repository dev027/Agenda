// <copyright file="IAgendaService.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Meetings;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Domain.ValueObjects.SetupStatii;

namespace Agenda.Service
{
    /// <summary>
    /// Service Layer.
    /// </summary>
    public interface IAgendaService
    {
        /// <summary>
        /// Gets the recent meetings most recent first.
        /// </summary>
        /// <param name="timeSpan">The time span.</param>
        /// <param name="maxNumberOfMeetings">The maximum number of meetings.</param>
        /// <returns>List of recent meetings.</returns>
        Task<IList<IMeeting>> GetRecentMeetingsMostRecentFirstAsync(
            TimeSpan? timeSpan = null,
            int? maxNumberOfMeetings = null);

        /// <summary>
        /// Gets the setup status.
        /// </summary>
        /// <returns>Setup status.</returns>
        Task<ISetupStatus> GetSetupStatusAsync();

        /// <summary>
        /// Creates the organisation.
        /// </summary>
        /// <param name="code">Organisation Code.</param>
        /// <param name="name">Organisation Name.</param>
        /// <returns>Organisation.</returns>
        Task<IOrganisation> CreateOrganisationAsync(
            string code,
            string name);
    }
}