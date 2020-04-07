// <copyright file="OrganisationController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Service;
using Agenda.Web.Models;
using Agenda.Web.ViewModels.Organisation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Agenda.Web.Controllers
{
    /// <summary>
    /// SOMETHING.
    /// </summary>
    /// <seealso cref="Controller" />
    public class OrganisationController : Controller
    {
        private readonly ILogger<OrganisationController> logger;
        private readonly IAgendaService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganisationController"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="service">Agenda Service.</param>
        public OrganisationController(
            ILogger<OrganisationController> logger,
            IAgendaService service)
        {
            this.logger = logger;
            this.service = service;
        }

        /// <summary>
        /// Adds this instance.
        /// </summary>
        /// <param name="model">Add view model.</param>
        /// <returns>View.</returns>
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (model.FormState == FormState.Initial)
            {
                model = new AddViewModel(
                    formState: FormState.Initial,
                    code: string.Empty,
                    name: string.Empty);
                this.ModelState.Clear();
            }
            else
            {
                if (this.ModelState.IsValid)
                {
                    if (await this.InsertRecordAsync(model).ConfigureAwait(false))
                    {
                        return this.RedirectToAction("Index", "Home");
                    }

                    this.ModelState.AddModelError("Save", "File save failure");
                }
            }

            return this.View(model);
        }

        /// <summary>
        /// Insert Organisation.
        /// </summary>
        /// <param name="model">Add view model.</param>
        /// <returns>True if inserted.</returns>
        private async Task<bool> InsertRecordAsync(AddViewModel model)
        {
            IOrganisation organisation = await this.service.CreateOrganisationAsync(
                model.Code,
                model.Name).ConfigureAwait(false);

            return organisation != null;
        }
    }
}