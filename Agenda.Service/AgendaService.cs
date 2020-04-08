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
using Agenda.Utilities.DependencyInjection;

namespace Agenda.Service
{
    /// <summary>
    /// Service Layer.
    /// </summary>
    /// <seealso cref="Agenda.Service.IAgendaService" />
    public class AgendaService : IAgendaService
    {
        /// <inheritdoc/>
        public async Task<IList<IMeeting>> GetRecentMeetingsMostRecentFirstAsync(
            TimeSpan? timeSpan = null,
            int? maxNumberOfMeetings = null)
        {
            using (IAgendaData data = InstanceFactory.GetInstance<IAgendaData>())
            {
                return await data.GetRecentMeetingsMostRecentFirstAsync(
                    timeSpan ?? TimeSpan.FromDays(365),
                    maxNumberOfMeetings ?? 20)
                    .ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task<ISetupStatus> GetSetupStatusAsync()
        {
            using (IAgendaData data = InstanceFactory.GetInstance<IAgendaData>())
            {
                bool haveOrganisations = await data.HaveOrganisationsAsync().ConfigureAwait(false);

                if (!haveOrganisations)
                {
                    return new SetupStatus(
                        haveOrganisations: false,
                        haveCommittees: false);
                }

                bool haveCommittees = await data.HaveCommitteesAsync().ConfigureAwait(false);

                return new SetupStatus(
                    haveOrganisations: true,
                    haveCommittees: haveCommittees);
            }
        }

        /// <inheritdoc/>
        public async Task<IOrganisation> CreateOrganisationAsync(string code, string name)
        {
            using (IAgendaData data = InstanceFactory.GetInstance<IAgendaData>())
            {
                IOrganisation organisation = new Organisation(
                    id: Guid.NewGuid(),
                    code: code,
                    name: name);

                await data.CreateOrganisationAsync(organisation).ConfigureAwait(false);

                return organisation;
            }
        }

        /// <inheritdoc/>
        public async Task<IList<IOrganisation>> GetAllOrganisationsAsync()
        {
            using (IAgendaData data = InstanceFactory.GetInstance<IAgendaData>())
            {
                return await data.GetAllOrganisationsAsync()
                    .ConfigureAwait(false);
            }
        }
    }
}