// <copyright file="OrganisationController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Service;
using Agenda.Utilities.Models.Whos;
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
    public class OrganisationController : ControllerBase
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
        : base(typeof(OrganisationController))
        {
            this.logger = logger;
            this.service = service;
        }

        /// <summary>
        /// Display the specified Organisation.
        /// </summary>
        /// <param name="id">Organisation Id.</param>
        /// <returns>View.</returns>
        public async Task<IActionResult> Index(Guid id)
        {
            IWho who = this.Who(nameof(this.Index));

            this.logger.LogDebug(
                "ENTRY {Action}(who, id) {@who} {id}",
                who.ActionName,
                who,
                id);

            IOrganisationWithCommittees organisation = await this.service
                .GetOrganisationByIdWithCommitteesAsync(
                    who: who,
                    organisationId: id)
                .ConfigureAwait(false);

            IndexViewModel model = IndexViewModel.Create(organisation);

            return this.ExitView(this.logger, this.View(model));
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
            IWho who = this.Who(nameof(this.Add));

            this.logger.LogDebug(
                "ENTRY {Action}(who, model) {@who} {@model}",
                who.ActionName,
                who,
                model);

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
                    await this.InsertRecordAsync(who, model).ConfigureAwait(false);

                    return this.ExitRedirectToAction(
                        this.logger,
                        this.RedirectToAction("Index", "Home"));
                }
            }

            return this.View(model);
        }

        /// <summary>
        /// Starts the edit.
        /// </summary>
        /// <param name="id">The Organisation id.</param>
        /// <param name="ajaxMode">AJAX mode(0=No; 1=Yes).</param>
        /// <returns>View.</returns>
        public async Task<IActionResult> StartEdit(
            Guid id,
            int ajaxMode)
        {
            IWho who = this.Who(nameof(this.StartEdit));

            this.logger.LogDebug(
                "ENTRY {Action}(who, id, ajaxMode) {@who} {id} {ajaxMode}",
                who.ActionName,
                who,
                id,
                ajaxMode);

            IOrganisation organisation = await this.service
                .GetOrganisationByIdAsync(who, id)
                .ConfigureAwait(false);

            EditViewModel model = EditViewModel.Create(organisation);

            switch (ajaxMode)
            {
                case 0:
                    return this.ExitView(this.logger, this.View("Edit", model));

                default:
                    throw new NotImplementedException($"AjaxMode {ajaxMode} not implemented yet.");
            }
        }

        /// <summary>
        /// Edits the specified organisation.
        /// </summary>
        /// <param name="model">Edit view model.</param>
        /// <returns>View.</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            IWho who = this.Who(nameof(this.Edit));

            this.logger.LogDebug(
                "ENTRY {Action}(who, model) {@who} {model}",
                who.ActionName,
                who,
                model);

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (this.ModelState.IsValid)
            {
                await this.UpdateRecordAsync(who, model).ConfigureAwait(false);

                return this.ExitRedirectToAction(
                    this.logger,
                    this.RedirectToAction(
                        "Index",
                        "Organisation",
                        new { id = model.Id }));
            }

            return this.View(model);
        }

        /// <summary>
        /// Insert Organisation.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="model">Add view model.</param>
        /// <returns>Nothing.</returns>
        private async Task InsertRecordAsync(
            IWho who,
            AddViewModel model)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, model) {@who} {model}",
                nameof(this.InsertRecordAsync),
                who,
                model);

            IOrganisation organisation = model.ToDomain();

            await this.service
                .CreateOrganisationAsync(who, organisation)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.InsertRecordAsync),
                who);
        }

        /// <summary>
        /// Update Organisation.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="model">Edit view model.</param>
        /// <returns>Nothing.</returns>
        private async Task UpdateRecordAsync(
            IWho who,
            EditViewModel model)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, model) {@who} {model}",
                nameof(this.UpdateRecordAsync),
                who,
                model);

            IOrganisation organisation = model.ToDomain();

            await this.service
                .UpdateOrganisationAsync(who, organisation)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.UpdateRecordAsync),
                who);
        }
    }
}