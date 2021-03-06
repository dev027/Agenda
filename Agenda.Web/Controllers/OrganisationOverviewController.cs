﻿// <copyright file="OrganisationOverviewController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Service;
using Agenda.Utilities.Logging;
using Agenda.Utilities.Models.Whos;
using Agenda.Web.ViewModels.OrganisationOverview;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Agenda.Web.Controllers
{
    /// <summary>
    /// Organisation Overview.
    /// </summary>
    /// <seealso cref="Controller" />
    public class OrganisationOverviewController : ControllerBase
    {
        private readonly ILogger<OrganisationController> logger;
        private readonly IAgendaService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganisationOverviewController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="service">The agenda service.</param>
        /// <param name="isTestMode">Is Test Mode.</param>
        public OrganisationOverviewController(
            ILogger<OrganisationController> logger,
            IAgendaService service,
            bool isTestMode = false)
        : base(typeof(OrganisationOverviewController), isTestMode)
        {
            this.logger = logger;
            this.service = service;
        }

        /// <summary>
        /// Display all Organisations.
        /// </summary>
        /// <returns>View.</returns>
        public async Task<IActionResult> Index()
        {
            IWho who = this.Who();

            this.logger.ReportEntry(who);

            IList<IOrganisation> organisations = (await this.service
                .GetAllOrganisationsAsync(who)
                .ConfigureAwait(false))
                .OrderBy(o => o.Code)
                .ToList();

            IndexViewModel model = IndexViewModel.Create(organisations);

            ViewResult view = this.View(model);

            this.logger.ReportExitView(
                who,
                view.ViewName,
                view.Model,
                view.StatusCode);

            return view;
        }
    }
}