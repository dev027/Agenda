// <copyright file="OrganisationOverviewController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Service;
using Agenda.Web.ViewModels.OrganisationOverview;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Agenda.Web.Controllers
{
    /// <summary>
    /// Organisation Overview.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class OrganisationOverviewController : Controller
    {
        private readonly ILogger<OrganisationController> logger;
        private readonly IAgendaService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganisationOverviewController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="service">The agenda service.</param>
        public OrganisationOverviewController(
            ILogger<OrganisationController> logger,
            IAgendaService service)
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
            IList<IOrganisation> organisations = (await this.service
                .GetAllOrganisationsAsync()
                .ConfigureAwait(false))
                .OrderBy(o => o.Code)
                .ToList();

            IndexViewModel model = IndexViewModel.Create(organisations);

            return this.View(model);
        }
    }
}