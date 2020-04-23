// <copyright file="AgendaService.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using Agenda.Data.Crud;
using Agenda.Domain.ValueObjects.SetupStatii;
using Agenda.Utilities.Models.Whos;
using Microsoft.Extensions.Logging;

namespace Agenda.Service
{
    /// <summary>
    /// Service Layer - General.
    /// </summary>
    /// <seealso cref="IAgendaService" />
    public partial class AgendaService : IAgendaService
    {
        #region Private Members

        private readonly ILogger<AgendaService> logger;
        private readonly IAgendaData data;

        #endregion Private Members

        #region Constructors

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

        #endregion Constructors

        #region General

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

        #endregion General
    }
}