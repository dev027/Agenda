// <copyright file="AgendaService.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Agenda.Data.Crud;
using Agenda.Domain.DomainObjects.Meetings;
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
        public IList<IMeeting> GetRecentMeetingsMostRecentFirst(
            TimeSpan? timeSpan = null,
            int? maxNumberOfMeetings = null)
        {
            using (IAgendaData data = InstanceFactory.GetInstance<IAgendaData>())
            {
                return data.GetRecentMeetingsMostRecentFirst(
                    timeSpan ?? TimeSpan.FromDays(365),
                    maxNumberOfMeetings ?? 20);
            }
        }

        /// <inheritdoc/>
        public ISetupStatus GetSetupStatus()
        {
            using (IAgendaData data = InstanceFactory.GetInstance<IAgendaData>())
            {
                bool haveOrganisations = data.HaveOrganisations();

                if (!haveOrganisations)
                {
                    return new SetupStatus(
                        haveOrganisations: false,
                        haveCommittees: false);
                }

                bool haveCommittees = data.HaveCommittees();

                return new SetupStatus(
                    haveOrganisations: true,
                    haveCommittees: haveCommittees);
            }
        }
    }
}