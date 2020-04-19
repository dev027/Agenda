// <copyright file="AgendaService.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Data.Crud;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.DomainObjects.Meetings;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Domain.ValueObjects.SetupStatii;
using Agenda.Utilities.Models.Whos;
using Microsoft.Extensions.Logging;

namespace Agenda.Service
{
    /// <summary>
    /// Service Layer.
    /// </summary>
    /// <seealso cref="IAgendaService" />
    public class AgendaService : IAgendaService
    {
        private readonly ILogger<AgendaService> logger;
        private readonly IAgendaData data;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgendaService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="data">The data layer.</param>
        public AgendaService(
            ILogger<AgendaService> logger,
            IAgendaData data)
        {
            this.logger = logger;
            this.data = data;
        }

        /// <inheritdoc/>
        public async Task<IList<IMeeting>> GetRecentMeetingsMostRecentFirstAsync(
            IWho who,
            TimeSpan? timeSpan = null,
            int? maxNumberOfMeetings = null)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, timeSpan, maxNumberOfMeetings) {@who} {timeSpan} {maxNumberOfMeetings}",
                nameof(this.GetRecentMeetingsMostRecentFirstAsync),
                who,
                timeSpan,
                maxNumberOfMeetings);

            IList<IMeeting> meetings = await this.data
                .GetRecentMeetingsMostRecentFirstAsync(
                    who: who,
                    timeSpan: timeSpan ?? TimeSpan.FromDays(365),
                    maxNumberOfMeetings: maxNumberOfMeetings ?? 20)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who, meetings) {@who} {@meetings}",
                nameof(this.GetRecentMeetingsMostRecentFirstAsync),
                who,
                meetings);

            return meetings;
        }

        /// <inheritdoc/>
        public async Task<ISetupStatus> GetSetupStatusAsync(IWho who)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who) {@who}",
                nameof(this.GetSetupStatusAsync),
                who);

            bool haveOrganisations = await this.data
                .HaveOrganisationsAsync(who)
                .ConfigureAwait(false);
            SetupStatus setupStatus;

            if (!haveOrganisations)
            {
                setupStatus = new SetupStatus(
                    haveOrganisations: false,
                    haveCommittees: false);
            }
            else
            {
                bool haveCommittees = await this.data
                    .HaveCommitteesAsync(who)
                    .ConfigureAwait(false);

                setupStatus = new SetupStatus(
                    haveOrganisations: true,
                    haveCommittees: haveCommittees);
            }

            this.logger.LogTrace(
                "EXIT {Method}(who, setupStatus) {@who} {@setupStatus}",
                nameof(this.GetSetupStatusAsync),
                who,
                setupStatus);

            return setupStatus;
        }

        /// <inheritdoc/>
        public async Task CreateOrganisationAsync(
            IWho who,
            IOrganisation organisation)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, organisation) {@who} {@organisation}",
                nameof(this.CreateOrganisationAsync),
                who,
                organisation);

            await this.data
                .CreateOrganisationAsync(
                    who: who,
                    organisation: organisation)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.CreateOrganisationAsync),
                who);
        }

        /// <inheritdoc/>
        public async Task<IList<IOrganisation>> GetAllOrganisationsAsync(IWho who)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who) {@who}",
                nameof(this.GetAllOrganisationsAsync),
                who);

            IList<IOrganisation> organisations = await this.data
                .GetAllOrganisationsAsync(who)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who, organisations) {@who} {@organisations}",
                nameof(this.GetAllOrganisationsAsync),
                who,
                organisations);

            return organisations;
        }

        /// <inheritdoc/>
        public async Task<IOrganisation> GetOrganisationByIdAsync(
            IWho who,
            Guid organisationId)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, organisationId) {@who} {organisationId}",
                nameof(this.GetOrganisationByIdAsync),
                who,
                organisationId);

            IOrganisation organisation = await this.data
                .GetOrganisationByIdAsync(
                    who: who,
                    organisationId: organisationId)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who, organisation) {@who} {@organisation}",
                nameof(this.GetOrganisationByIdAsync),
                who,
                organisation);

            return organisation;
        }

        /// <inheritdoc/>
        public async Task<IOrganisationWithCommittees> GetOrganisationByIdWithCommitteesAsync(
            IWho who,
            Guid organisationId)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, organisationId) {@who} {organisationId}",
                nameof(this.GetOrganisationByIdWithCommitteesAsync),
                who,
                organisationId);

            IOrganisationWithCommittees organisationWithCommittees = await this.data
                .GetOrganisationByIdWithCommitteesAsync(
                    who: who,
                    organisationId: organisationId)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who, organisationWithCommittees) {@who} {@organisationWithCommittees}",
                nameof(this.GetOrganisationByIdWithCommitteesAsync),
                who,
                organisationWithCommittees);

            return organisationWithCommittees;
        }

        /// <inheritdoc/>
        public async Task UpdateOrganisationAsync(
            IWho who,
            IOrganisation organisation)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, organisation) {@who} {@organisation}",
                nameof(this.UpdateOrganisationAsync),
                who,
                organisation);

            await this.data
                .UpdateOrganisationAsync(
                    who: who,
                    organisation: organisation)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.UpdateOrganisationAsync),
                who);
        }

        /// <inheritdoc/>
        public async Task CreateCommitteeAsync(
            IWho who,
            ICommittee committee)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, committee) {@who} {@committee}",
                nameof(this.CreateCommitteeAsync),
                who,
                committee);

            await this.data.CreateCommitteeAsync(
                    who,
                    committee)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.CreateCommitteeAsync),
                who);
        }

        /// <inheritdoc/>
        public async Task<ICommittee> GetCommitteeByIdAsync(
            IWho who,
            Guid committeeId)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, committeeId) {@who} {committeeId}",
                nameof(this.GetCommitteeByIdAsync),
                who,
                committeeId);

            ICommittee committee = await this.data
                .GetCommitteeByIdAsync(
                    who: who,
                    committeeId: committeeId)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who, committee) {@who} {@committee}",
                nameof(this.GetCommitteeByIdAsync),
                who,
                committee);

            return committee;
        }

        /// <inheritdoc/>
        public async Task<ICommitteeWithMeetings> GetCommitteeByIdWithMeetingsAsync(
            IWho who,
            Guid committeeId)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, committeeId) {@who} {committeeId}",
                nameof(this.GetCommitteeByIdWithMeetingsAsync),
                who,
                committeeId);

            ICommitteeWithMeetings committeeWithMeetings = await this.data
                .GetCommitteeByIdWithMeetingsAsync(
                    who: who,
                    committeeId: committeeId)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who, committeeWithMeetings) {@who} {@committeeWithMeetings}",
                nameof(this.GetCommitteeByIdWithMeetingsAsync),
                who,
                committeeWithMeetings);

            return committeeWithMeetings;
        }

        /// <inheritdoc/>
        public async Task UpdateCommitteeAsync(
            IWho who,
            ICommittee committee)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, committee) {@who} {@committee}",
                nameof(this.UpdateCommitteeAsync),
                who,
                committee);

            await this.data
                .UpdateCommitteeAsync(
                    who: who,
                    committee: committee)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.UpdateCommitteeAsync),
                who);
        }

        /// <inheritdoc/>
        public async Task CreateMeetingAsync(
            IWho who,
            IMeeting meeting)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, meeting) {@who} {@meeting}",
                nameof(this.CreateMeetingAsync),
                who,
                meeting);

            await this.data
                .CreateMeetingAsync(
                    who: who,
                    meeting: meeting)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.CreateMeetingAsync),
                who);
        }

        /// <inheritdoc/>
        public async Task<IMeeting> GetMeetingByIdAsync(
            IWho who,
            Guid meetingId)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, meetingId) {@who} {meetingId}",
                nameof(this.GetMeetingByIdAsync),
                who,
                meetingId);

            IMeeting meeting = await this.data
                .GetMeetingByIdAsync(
                    who: who,
                    meetingId: meetingId)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who, meeting) {@who} {@meeting}",
                nameof(this.GetMeetingByIdAsync),
                who,
                meeting);

            return meeting;
        }

        /// <inheritdoc/>
        public async Task UpdateMeetingAsync(
            IWho who,
            IMeeting meeting)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, meeting) {@who} {@organisation}",
                nameof(this.UpdateMeetingAsync),
                who,
                meeting);

            await this.data
                .UpdateMeetingAsync(
                    who: who,
                    meeting: meeting)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.UpdateMeetingAsync),
                who);
        }
    }
}