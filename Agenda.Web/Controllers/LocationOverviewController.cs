// <copyright file="LocationOverviewController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Service;
using Agenda.Utilities.Models.Whos;
using Agenda.Web.ViewModels.LocationOverview;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Agenda.Web.Controllers
{
    /// <summary>
    /// SOMETHING.
    /// </summary>
    /// <seealso cref="Controller" />
    public class LocationOverviewController : ControllerBase
    {
        private readonly ILogger<LocationOverviewController> logger;
        private readonly IAgendaService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationOverviewController"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="service">Agenda Service.</param>
        /// <param name="isTestMode">Is Test Mode.</param>
        public LocationOverviewController(
            ILogger<LocationOverviewController> logger,
            IAgendaService service,
            bool isTestMode = false)
            : base(typeof(LocationOverviewController), isTestMode)
        {
            this.logger = logger;
            this.service = service;
        }

        /// <summary>
        /// Display the specified Organisation with lts Locations.
        /// </summary>
        /// <param name="organisationId">Organisation Id.</param>
        /// <returns>View.</returns>
        public async Task<IActionResult> Index(Guid organisationId)
        {
            IWho who = this.Who(nameof(this.Index));

            this.logger.LogDebug(
                "ENTRY {ActionName}(who, organisationId) {@who} {organisationId}",
                who.ActionName,
                who,
                organisationId);

            IOrganisationWithLocations organisation = await this.service
                .GetOrganisationByIdWithLocationsAsync(
                    who: who,
                    organisationId: organisationId)
                .ConfigureAwait(false);

            IndexViewModel model = IndexViewModel.Create(organisation);

            return this.ExitView(this.logger, this.View(model));
        }
    }
}