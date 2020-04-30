// <copyright file="CommitteeOverviewController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Service;
using Agenda.Utilities.Models.Whos;
using Agenda.Web.ViewModels.CommitteeOverview;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Agenda.Web.Controllers
{
    /// <summary>
    /// Committee Overview controller.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    public class CommitteeOverviewController : ControllerBase
    {
        private readonly ILogger<CommitteeOverviewController> logger;
        private readonly IAgendaService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommitteeOverviewController"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="service">Agenda Service.</param>
        /// <param name="isTestMode">Is Test Mode.</param>
        public CommitteeOverviewController(
            ILogger<CommitteeOverviewController> logger,
            IAgendaService service,
            bool isTestMode = false)
            : base(typeof(CommitteeOverviewController), isTestMode)
        {
            this.logger = logger;
            this.service = service;
        }

        /// <summary>
        /// Display committees for the specified organisation.
        /// </summary>
        /// <param name="organisationId">The organisation identifier.</param>
        /// <returns>View.</returns>
        public async Task<IActionResult> Index(Guid organisationId)
        {
            IWho who = this.Who(nameof(this.Index));

            this.logger.LogDebug(
                "ENTRY {ActionName}(who, organisationId) {@who} {organisationId}",
                who.ActionName,
                who,
                organisationId);

            IOrganisationWithCommittees organisation = await this.service
                .GetOrganisationByIdWithCommitteesAsync(
                    who: who,
                    organisationId: organisationId)
                .ConfigureAwait(false);

            IndexViewModel model = IndexViewModel.Create(organisation);

            return this.ExitView(this.logger, this.View(model));
        }
    }
}