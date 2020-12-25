// <copyright file="AgendaController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.Data;
using Agenda.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Agenda.Web.Controllers
{
    /// <summary>
    /// Agenda Controller.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    public class AgendaController : ControllerBase
    {
        private readonly ILogger<AgendaController> logger;
        private readonly IAgendaService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgendaController"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="service">Agenda Service.</param>
        /// <param name="isTestMode">Is Test Mode.</param>
        public AgendaController(
            ILogger<AgendaController> logger,
            IAgendaService service,
            bool isTestMode = false)
            : base(typeof(AgendaController), isTestMode)
        {
            this.logger = logger;
            this.service = service;
        }

        /// <summary>
        /// SOMETHING.
        /// </summary>
        /// <returns>Action  Result.</returns>
        public IActionResult Index()
        {
            throw new NoNullAllowedException();
            ////return this.View();
        }
    }
}