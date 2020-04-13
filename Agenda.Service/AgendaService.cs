// <copyright file="AgendaService.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Data.Crud;
using Agenda.Domain.DomainObjects.Meetings;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Domain.ValueObjects.SetupStatii;
using Agenda.Service.Resources;
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
                LoggerResources.___EntryBy___,
                nameof(this.GetRecentMeetingsMostRecentFirstAsync),
                who);

            return await this.data.GetRecentMeetingsMostRecentFirstAsync(
                    who: who,
                    timeSpan: timeSpan ?? TimeSpan.FromDays(365),
                    maxNumberOfMeetings: maxNumberOfMeetings ?? 20)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<ISetupStatus> GetSetupStatusAsync(IWho who)
        {
            this.logger.LogTrace(
                LoggerResources.___EntryBy___,
                nameof(this.GetSetupStatusAsync),
                who);

            bool haveOrganisations = await this.data.HaveOrganisationsAsync(who).ConfigureAwait(false);

            if (!haveOrganisations)
            {
                return new SetupStatus(
                    haveOrganisations: false,
                    haveCommittees: false);
            }

            bool haveCommittees = await this.data.HaveCommitteesAsync(who).ConfigureAwait(false);

            return new SetupStatus(
                haveOrganisations: true,
                haveCommittees: haveCommittees);
        }

        /// <inheritdoc/>
        public async Task<bool> CreateOrganisationAsync(
            IWho who,
            IOrganisation organisation)
        {
            this.logger.LogTrace(
                LoggerResources.___EntryBy___,
                nameof(this.CreateOrganisationAsync),
                who);

            await this.data.CreateOrganisationAsync(who, organisation).ConfigureAwait(false);

            return true;
        }

        /// <param name="who"></param>
        /// <inheritdoc/>
        public async Task<IList<IOrganisation>> GetAllOrganisationsAsync(IWho who)
        {
            this.logger.LogTrace(
                LoggerResources.___EntryBy___,
                nameof(this.GetAllOrganisationsAsync),
                who);

            return await this.data.GetAllOrganisationsAsync(who)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IOrganisation> GetOrganisationByIdAsync(IWho who, Guid organisationId)
        {
            this.logger.LogTrace(
                LoggerResources.___EntryBy___,
                nameof(this.GetOrganisationByIdAsync),
                who);

            return await this.data.GetOrganisationByIdAsync(
                    who: who,
                    organisationId: organisationId)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IOrganisationWithCommittees> GetOrganisationByIdWithCommitteesAsync(
            IWho who,
            Guid organisationId)
        {
            this.logger.LogTrace(
                LoggerResources.___EntryBy___,
                nameof(this.GetOrganisationByIdWithCommitteesAsync),
                who);

            return await this.data.GetOrganisationByIdWithCommitteesAsync(
                    who: who,
                    organisationId: organisationId)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateOrganisationAsync(IWho who, IOrganisation organisation)
        {
            this.logger.LogTrace(
                LoggerResources.___EntryBy___,
                nameof(this.UpdateOrganisationAsync),
                who);

            await this.data.UpdateOrganisationAsync(
                    who: who,
                    organisation: organisation)
                .ConfigureAwait(false);

            return true;
        }
    }
}