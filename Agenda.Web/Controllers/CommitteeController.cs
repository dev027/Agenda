// <copyright file="CommitteeController.cs" company="Do It Wright">
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
    public class CommitteeController : ControllerBase
    {
        private readonly ILogger<CommitteeController> logger;
        private readonly IAgendaService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommitteeController"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="service">Agenda Service.</param>
        public CommitteeController(
            ILogger<CommitteeController> logger,
            IAgendaService service)
            : base(typeof(CommitteeController))
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

            this.Entry(this.logger);

            throw new NotImplementedException();
        }
    }
}