// <copyright file="HomeController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Meetings;
using Agenda.Domain.ValueObjects.SetupStatii;
using Agenda.Service;
using Agenda.Web.Areas.Api.Models.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Agenda.Web.Controllers
{
    /// <summary>
    /// Home Controller.
    /// </summary>
    /// <seealso cref="Controller" />
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IAgendaService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="service">The agenda service.</param>
        public HomeController(
            ILogger<HomeController> logger,
            IAgendaService service)
        {
            this.logger = logger;
            this.service = service;
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
            IList<IMeeting> meetings = await this.service.GetRecentMeetingsMostRecentFirstAsync()
                .ConfigureAwait(false);

            ISetupStatus setupStatus = meetings.Any()
                ? new SetupStatus(haveOrganisations: true, haveCommittees: true)
                : await this.service.GetSetupStatusAsync().ConfigureAwait(false);

            IndexViewModel model = IndexViewModel.Create(meetings, setupStatus);
            return this.View(model);
        }
    }
}