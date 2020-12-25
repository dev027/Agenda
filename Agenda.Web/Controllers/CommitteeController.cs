// <copyright file="CommitteeController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Domain.ValueObjects.Enums;
using Agenda.Service;
using Agenda.Utilities.Logging;
using Agenda.Utilities.Models.Whos;
using Agenda.Web.Models;
using Agenda.Web.ViewModels.Committee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Agenda.Web.Controllers
{
    /// <summary>
    /// Committee Controller.
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
        /// <param name="isTestMode">Is Test Mode.</param>
        public CommitteeController(
            ILogger<CommitteeController> logger,
            IAgendaService service,
            bool isTestMode = false)
            : base(typeof(CommitteeController), isTestMode)
        {
            this.logger = logger;
            this.service = service;
        }

        /// <summary>
        /// Display the specified Committee.
        /// </summary>
        /// <param name="committeeId">Committee Id.</param>
        /// <returns>View.</returns>
        public async Task<IActionResult> Index(Guid committeeId)
        {
            IWho who = this.Who();

            this.logger.ReportEntry(
                who,
                new { CommitteeId = committeeId });

            ICommitteeWithMeetings committee = await this.service
                .GetCommitteeByIdWithMeetingsAsync(
                    who: who,
                    committeeId: committeeId)
                .ConfigureAwait(false);

            IndexViewModel model = IndexViewModel.Create(committee);

            ViewResult view = this.View(model);

            this.logger.ReportExitView(
                who,
                view.ViewName,
                view.Model,
                view.StatusCode);

            return view;
        }

        /// <summary>
        /// Starts the add.
        /// </summary>
        /// <param name="organisationId">Organisation Id.</param>
        /// <returns>Redirect To Action.</returns>
        public async Task<IActionResult> StartAdd(Guid organisationId)
        {
            IWho who = this.Who();

            this.logger.ReportEntry(
                who,
                new { OrganisationId = organisationId });

            IOrganisation organisation = await this.service
                .GetOrganisationByIdAsync(who, organisationId)
                .ConfigureAwait(false);

            AddViewModel model = AddViewModel.Create(organisation);

            RedirectToActionResult redirect = this.RedirectToAction("Add", model);

            this.logger.ReportExitRedirectToAction(
                who,
                redirect.ControllerName,
                redirect.ActionName,
                redirect.RouteValues);

            return redirect;
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

            return InternalAsync();

            async Task<IActionResult> InternalAsync()
            {
                IWho who = this.Who();

                this.logger.ReportEntry(
                    who,
                    new { Model = model });

                if (model.FormState == FormState.Initial)
                {
                    this.ModelState.Clear();
                }
                else
                {
                    if (this.ModelState.IsValid)
                    {
                        this.logger.Debug(
                            who,
                            "Inserting record",
                            new { Model = model });

                        await this.InsertRecordAsync(who, model).ConfigureAwait(false);

                        RedirectToActionResult redirect = this.RedirectToAction(
                            "Index",
                            "CommitteeOverview",
                            new { organisationId = model.OrganisationId });

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
        /// <param name="committeeId">The Committee id.</param>
        /// <param name="ajaxMode">AJAX mode(0=No; 1=Yes).</param>
        /// <returns>View.</returns>
        public async Task<IActionResult> StartEdit(
            Guid committeeId,
            int ajaxMode)
        {
            IWho who = this.Who();

            this.logger.ReportEntry(
                who,
                new
                {
                    CommitteeId = committeeId,
                    AjaxMode = ajaxMode
                });

            ICommittee committee = await this.service
                .GetCommitteeByIdAsync(who, committeeId)
                .ConfigureAwait(false);

            EditViewModel model = EditViewModel.Create(committee);

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
        /// Edits the specified committee.
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

            return InternalAsync();

            async Task<IActionResult> InternalAsync()
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
                        "Committee",
                        new { committeeId = model.CommitteeId });

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
        /// Insert Committee.
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

            IOrganisation organisation = await this.service.GetOrganisationByIdAsync(
                    who,
                    model.OrganisationId)
                .ConfigureAwait(false);

            ICommittee committee = model.ToDomain(organisation);

            await this.service
                .CreateCommitteeAsync(who, AuditEvent.CommitteeMaintenance, committee)
                .ConfigureAwait(false);

            this.logger.ReportExit(who);
        }

        /// <summary>
        /// Update Committee.
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

            ICommittee originalCommittee = await this.service
                .GetCommitteeByIdAsync(
                    who: who,
                    committeeId: model.CommitteeId)
                .ConfigureAwait(false);

            ICommittee committee = model.ToDomain(originalCommittee.Organisation);

            await this.service
                .UpdateCommitteeAsync(
                    who: who,
                    auditEvent: AuditEvent.CommitteeMaintenance,
                    committee: committee)
                .ConfigureAwait(false);

            this.logger.ReportExit(who);
        }
    }
}