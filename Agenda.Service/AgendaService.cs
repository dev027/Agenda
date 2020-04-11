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
using Agenda.Utilities.Models.Whos;

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
            IWho who,
            TimeSpan? timeSpan = null,
            int? maxNumberOfMeetings = null)
        {
            using (IAgendaData data = InstanceFactory.GetInstance<IAgendaData>())
            {
                return await data.GetRecentMeetingsMostRecentFirstAsync(
                        who: who,
                        timeSpan: timeSpan ?? TimeSpan.FromDays(365),
                        maxNumberOfMeetings: maxNumberOfMeetings ?? 20)
                    .ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task<ISetupStatus> GetSetupStatusAsync(IWho who)
        {
            using (IAgendaData data = InstanceFactory.GetInstance<IAgendaData>())
            {
                bool haveOrganisations = await data.HaveOrganisationsAsync(who).ConfigureAwait(false);

                if (!haveOrganisations)
                {
                    return new SetupStatus(
                        haveOrganisations: false,
                        haveCommittees: false);
                }

                bool haveCommittees = await data.HaveCommitteesAsync(who).ConfigureAwait(false);

                return new SetupStatus(
                    haveOrganisations: true,
                    haveCommittees: haveCommittees);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> CreateOrganisationAsync(
            IWho who,
            IOrganisation organisation)
        {
            using (IAgendaData data = InstanceFactory.GetInstance<IAgendaData>())
            {
                await data.CreateOrganisationAsync(who, organisation).ConfigureAwait(false);

                return true;
            }
        }

        /// <param name="who"></param>
        /// <inheritdoc/>
        public async Task<IList<IOrganisation>> GetAllOrganisationsAsync(IWho who)
        {
            using (IAgendaData data = InstanceFactory.GetInstance<IAgendaData>())
            {
                return await data.GetAllOrganisationsAsync(who)
                    .ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task<IOrganisation> GetOrganisationByIdAsync(IWho who, Guid organisationId)
        {
            using (IAgendaData data = InstanceFactory.GetInstance<IAgendaData>())
            {
                return await data.GetOrganisationByIdAsync(
                        who: who,
                        organisationId: organisationId)
                    .ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateOrganisationAsync(IWho who, IOrganisation organisation)
        {
            using (IAgendaData data = InstanceFactory.GetInstance<IAgendaData>())
            {
                await data.UpdateOrganisationAsync(
                        who: who,
                        organisation: organisation)
                    .ConfigureAwait(false);

                return true;
            }
        }
    }
}