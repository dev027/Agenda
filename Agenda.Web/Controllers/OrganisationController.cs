// <copyright file="OrganisationController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Domain.ValueObjects.Enums;
using Agenda.Service;
using Agenda.Utilities.Logging;
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
            IWho who = this.Who();

            this.logger.ReportEntry(
                who,
                new { OrganisationId = organisationId });

            IOrganisationWithCommittees organisation = await this.service
                .GetOrganisationByIdWithCommitteesAsync(
                    who: who,
                    organisationId: organisationId)
                .ConfigureAwait(false);

            IndexViewModel model = IndexViewModel.Create(organisation);

            ViewResult view = this.View(model);

            this.logger.ReportExitView(
                who,
                view.ViewName,
                view.Model,
                view.StatusCode);

            return view;
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
                IWho who = this.Who();

                this.logger.ReportEntry(
                    who,
                    new { Model = model });

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

                        RedirectToActionResult redirect = this.RedirectToAction("Index", "OrganisationOverview");

                        this.logger.ReportExitRedirectToAction(
                            who,
                            redirect.ControllerName,
                            redirect.ActionName,
                            redirect.RouteValues);

                        return redirect;
                    }
                }

                ViewResult view = this.View(model);

                this.logger.ReportExitView(
                    who,
                    view.ViewName,
                    view.Model,
                    view.StatusCode);

                return view;
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
            IWho who = this.Who();

            this.logger.ReportEntry(
                who,
                new
                {
                    Organisationid = organisationId,
                    AjaxMode = ajaxMode
                });

            IOrganisation organisation = await this.service
                .GetOrganisationByIdAsync(who, organisationId)
                .ConfigureAwait(false);

            EditViewModel model = EditViewModel.Create(organisation);

            switch (ajaxMode)
            {
                case 0:
                    ViewResult view = this.View("Edit", model);

                    this.logger.ReportExitView(
                        who,
                        view.ViewName,
                        view.Model,
                        view.StatusCode);

                    return view;

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
                IWho who = this.Who();

                this.logger.ReportEntry(
                    who,
                    new { Model = model });

                if (this.ModelState.IsValid)
                {
                    await this.UpdateRecordAsync(who, model).ConfigureAwait(false);

                    RedirectToActionResult redirect = this.RedirectToAction(
                        "Index",
                        "Organisation",
                        new { organisationId = model.OrganisationId });

                    this.logger.ReportExitRedirectToAction(
                        who,
                        redirect.ControllerName,
                        redirect.ActionName,
                        redirect.RouteValues);

                    return redirect;
                }

                ViewResult view = this.View(model);

                this.logger.ReportExitView(
                    who,
                    view.ViewName,
                    view.Model,
                    view.StatusCode);

                return view;
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
            this.logger.ReportEntry(
                who,
                new { Model = model });

            IOrganisation organisation = model.ToDomain();

            await this.service
                .CreateOrganisationAsync(who, AuditEvent.OrganisationMaintenance, organisation)
                .ConfigureAwait(false);

            this.logger.ReportExit(who);
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
            this.logger.ReportEntry(
                who,
                new { Model = model });

            IOrganisation organisation = model.ToDomain();

            await this.service
                .UpdateOrganisationAsync(
                    who: who,
                    auditEvent: AuditEvent.OrganisationMaintenance,
                    organisation: organisation)
                .ConfigureAwait(false);

            this.logger.ReportExit(who);
        }
    }
}