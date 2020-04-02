// <copyright file="HomeController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using Agenda.Domain.DomainObjects.Meetings;
using Agenda.Domain.ValueObjects.SetupStatii;
using Agenda.Service;
using Agenda.Utilities.DependencyInjection;
using Agenda.Web.Areas.Api.Models.Home;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Web.Controllers
{
    /// <summary>
    /// Home Controller.
    /// </summary>
    /// <seealso cref="Controller" />
    public class HomeController : Controller
    {
        /// <summary>
        /// Display the default view.
        /// </summary>
        /// <returns>Default View.</returns>
        [HttpGet("/")]
        [HttpGet("/[controller]")]
        [HttpGet("/[controller]/index")]
        public IActionResult Index()
        {
            IAgendaService service = InstanceFactory.GetInstance<IAgendaService>();

            IList<IMeeting> meetings = service.GetRecentMeetingsMostRecentFirst();

            ISetupStatus setupStatus = meetings.Any()
                ? new SetupStatus(haveOrganisations: true, haveCommittees: true)
                : service.GetSetupStatus();

            IndexViewModel model = IndexViewModel.Create(meetings, setupStatus);
            return this.View(model);
        }
    }
}