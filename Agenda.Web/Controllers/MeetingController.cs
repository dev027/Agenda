// <copyright file="MeetingController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Service;
using Agenda.Utilities.Models.Whos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Agenda.Web.Controllers
{
    /// <summary>
    /// SOMETHING.
    /// </summary>
    /// <seealso cref="Controller" />
    public class MeetingController : ControllerBase
    {
        private readonly ILogger<MeetingController> logger;
        private readonly IAgendaService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingController"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="service">Agenda Service.</param>
        public MeetingController(
            ILogger<MeetingController> logger,
            IAgendaService service)
            : base(typeof(MeetingController))
        {
            this.logger = logger;
            this.service = service;
        }

        /// <summary>
        /// Adds this instance.
        /// </summary>
        /// <returns>View.</returns>
        public IActionResult Add()
        {
            IWho who = this.Who(nameof(this.Add));

            this.logger.LogDebug(
                "ENTRY {Action}(who) {@who}",
                who.ActionName,
                who);
            throw new NotImplementedException();
        }
    }
}