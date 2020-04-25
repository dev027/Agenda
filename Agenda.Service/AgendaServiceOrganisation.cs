// <copyright file="AgendaServiceOrganisation.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Utilities.Models.Whos;
using Microsoft.Extensions.Logging;

namespace Agenda.Service
{
    /// <summary>
    /// Service Layer - Organisation.
    /// </summary>
    /// <seealso cref="IAgendaService" />
    public partial class AgendaService
    {
        #region Create

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

        #endregion Create

        #region Read

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
        public async Task<IOrganisationWithLocations> GetOrganisationByIdWithLocationsAsync(IWho who, Guid organisationId)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, organisationId) {@who} {organisationId}",
                nameof(this.GetOrganisationByIdWithLocationsAsync),
                who,
                organisationId);

            IOrganisationWithLocations organisationWithLocations = await this.data
                .GetOrganisationByIdWithLocationsAsync(
                    who: who,
                    organisationId: organisationId)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who, organisationWithLocations) {@who} {@organisationWithLocations}",
                nameof(this.GetOrganisationByIdWithLocationsAsync),
                who,
                organisationWithLocations);

            return organisationWithLocations;
        }

        #endregion Read

        #region Update

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

        #endregion Update
    }
}