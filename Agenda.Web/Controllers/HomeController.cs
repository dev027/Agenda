// <copyright file="HomeController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Meetings;
using Agenda.Domain.ValueObjects.SetupStatii;
using Agenda.Service;
using Agenda.Utilities.Logging;
using Agenda.Utilities.Models.Whos;
using Agenda.Web.Areas.Api.Models.Home;
using Agenda.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;

namespace Agenda.Web.Controllers
{
    /// <summary>
    /// Home Controller.
    /// </summary>
    /// <seealso cref="Controller" />
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> logger;
        private readonly IAgendaService service;
        private readonly IFeatureManager featureManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="service">The agenda service.</param>
        /// <param name="featureManager">The feature manager.</param>
        /// <param name="isTestMode">Is Test Mode.</param>
        public HomeController(
            ILogger<HomeController> logger,
            IAgendaService service,
            IFeatureManager featureManager,
            bool isTestMode = false)
        : base(typeof(HomeController), isTestMode)
        {
            this.logger = logger;
            this.service = service;
            this.featureManager = featureManager;
        }

        /// <summary>
        /// Display the default view.
        /// </summary>
        /// <returns>Default View.</returns>
        [HttpGet("/")]
        [HttpGet("/[controller]")]
        [HttpGet("/[controller]/index")]
        public async Task<IActionResult> Index()
        {
            IWho who = this.Who();

            this.logger.ReportEntry(who);

            IList<IMeeting> meetings = await this.service
                .GetRecentMeetingsMostRecentFirstAsync(who)
                .ConfigureAwait(false);

            ISetupStatus setupStatus = meetings.Any()
                ? new SetupStatus(haveOrganisations: true, haveCommittees: true)
                : await this.service
                    .GetSetupStatusAsync(who)
                    .ConfigureAwait(false);

            bool featureEnabled = await this.featureManager
                .IsEnabledAsync(nameof(FeatureFlag.NewReferenceSearch))
                .ConfigureAwait(false);

            IndexViewModel model = IndexViewModel.Create(meetings, setupStatus, featureEnabled);

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