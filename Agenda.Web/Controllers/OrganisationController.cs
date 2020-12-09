// <copyright file="OrganisationController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Domain.ValueObjects.Enums;
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
        /// <param name="isTestMode">Is Test Mode.</param>
        public OrganisationController(
            ILogger<OrganisationController> logger,
            IAgendaService service,
            bool isTestMode = false)
        : base(typeof(OrganisationController), isTestMode)
        {
            this.logger = logger;
            this.service = service;
        }

        /// <summary>
        /// Display the specified Organisation.
        /// </summary>
        /// <param name="organisationId">Organisation Id.</param>
        /// <returns>View.</returns>
        public async Task<IActionResult> Index(Guid organisationId)
        {
            IWho who = this.Who(nameof(this.Index));

            this.logger.LogDebug(
                "ENTRY {Action}(who, organisationId) {@who} {organisationId}",
                who.ActionName,
                who,
                organisationId);

            IOrganisationWithCommittees organisation = await this.service
                .GetOrganisationByIdWithCommitteesAsync(
                    who: who,
                    organisationId: organisationId)
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
        public Task<IActionResult> Add(AddViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return Internal();

            async Task<IActionResult> Internal()
            {
                IWho who = this.Who(nameof(this.Add));

                this.logger.LogDebug(
                    "ENTRY {Action}(who, model) {@who} {@model}",
                    who.ActionName,
                    who,
                    model);

                if (model.FormState == FormState.Initial)
                {
                    model = new AddViewModel(
                        formState: FormState.Initial,
                        code: string.Empty,
                        name: string.Empty,
                        bgColour: string.Empty);
                    this.ModelState.Clear();
                }
                else
                {
                    if (this.ModelState.IsValid)
                    {
                        await this.InsertRecordAsync(who, model).ConfigureAwait(false);

                        return this.ExitRedirectToAction(
                            this.logger,
                            this.RedirectToAction("Index", "OrganisationOverview"));
                    }
                }

                return this.View(model);
            }
        }

        /// <summary>
        /// Starts the edit.
        /// </summary>
        /// <param name="organisationId">The Organisation id.</param>
        /// <param name="ajaxMode">AJAX mode(0=No; 1=Yes).</param>
        /// <returns>View.</returns>
        public async Task<IActionResult> StartEdit(
            Guid organisationId,
            int ajaxMode)
        {
            IWho who = this.Who(nameof(this.StartEdit));

            this.logger.LogDebug(
                "ENTRY {Action}(who, organisationId, ajaxMode) {@who} {organisationId} {ajaxMode}",
                who.ActionName,
                who,
                organisationId,
                ajaxMode);

            IOrganisation organisation = await this.service
                .GetOrganisationByIdAsync(who, organisationId)
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
        public Task<IActionResult> Edit(EditViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return Internal();

            async Task<IActionResult> Internal()
            {
                IWho who = this.Who(nameof(this.Edit));

                this.logger.LogDebug(
                    "ENTRY {Action}(who, model) {@who} {model}",
                    who.ActionName,
                    who,
                    model);

                if (this.ModelState.IsValid)
                {
                    await this.UpdateRecordAsync(who, model).ConfigureAwait(false);

                    return this.ExitRedirectToAction(
                        this.logger,
                        this.RedirectToAction(
                            "Index",
                            "Organisation",
                            new { organisationId = model.OrganisationId }));
                }

                return this.View(model);
            }
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
                .CreateOrganisationAsync(who, AuditEvent.OrganisationMaintenance, organisation)
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
                .UpdateOrganisationAsync(
                    who: who,
                    auditEvent: AuditEvent.OrganisationMaintenance,
                    organisation: organisation)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.UpdateRecordAsync),
                who);
        }
    }
}