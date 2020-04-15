// <copyright file="CommitteeController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Service;
using Agenda.Utilities.Models.Whos;
using Agenda.Web.Models;
using Agenda.Web.ViewModels.Committee;
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
        /// Display the specified Organisation.
        /// </summary>
        /// <param name="id">Organisation Id.</param>
        /// <returns>View.</returns>
        public async Task<IActionResult> Index(Guid id)
        {
            IWho who = this.Who(nameof(this.Index));

            this.logger.LogDebug(
                "ENTRY {ActionName}(who, id) {@who} {id}",
                who.ActionName,
                who,
                id);

            ICommitteeWithMeetings committee = await this.service
                .GetCommitteeByIdWithMeetingsAsync(
                    who: who,
                    committeeId: id)
                .ConfigureAwait(false);

            IndexViewModel model = IndexViewModel.Create(committee);

            return this.ExitView(this.logger, this.View(model));
        }

        /// <summary>
        /// Starts the add.
        /// </summary>
        /// <param name="organisationId">Organisation Id.</param>
        /// <returns>Redirect To Action.</returns>
        public async Task<IActionResult> StartAdd(Guid organisationId)
        {
            IWho who = this.Who(nameof(this.StartAdd));

            this.logger.LogDebug(
                "ENTRY {ActionName}(who, id) {@who} {organisationId}",
                who.ActionName,
                who,
                organisationId);

            IOrganisation fred = await this.service
                .GetOrganisationByIdAsync(who, organisationId)
                .ConfigureAwait(false);

            AddViewModel model = new AddViewModel(
                formState: FormState.Initial,
                organisationId: organisationId,
                name: string.Empty,
                description: string.Empty);

            return this.ExitRedirectToAction(
                this.logger,
                this.RedirectToAction("Add", model));
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
                "ENTRY {ActionName}(who, model) {@who} {@model}",
                who.ActionName,
                who,
                model);

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (model.FormState == FormState.Initial)
            {
                this.ModelState.Clear();
            }
            else
            {
                if (this.ModelState.IsValid)
                {
                    this.logger.LogDebug(
                        "{@Who} inserting model {@Model}",
                        who,
                        model);

                    await this.InsertRecordAsync(who, model).ConfigureAwait(false);

                    return this.ExitRedirectToAction(
                        this.logger,
                        this.RedirectToAction(
                            "Index",
                            "Organisation",
                            new { id = model.OrganisationId }));
                }
            }

            return this.ExitView(this.logger, this.View(model));
        }

        /// <summary>
        /// Starts the edit.
        /// </summary>
        /// <param name="id">The Committee id.</param>
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

            ICommittee committee = await this.service
                .GetCommitteeByIdAsync(who, id)
                .ConfigureAwait(false);

            EditViewModel model = EditViewModel.Create(committee);

            switch (ajaxMode)
            {
                case 0:
                    return this.ExitView(this.logger, this.View("Edit", model));

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
                        "Committee",
                        new { id = model.Id }));
            }

            return this.View(model);
        }

        /// <summary>
        /// Insert Organisation.
        /// </summary>
        /// <param name="who">Who called it.</param>
        /// <param name="model">Add view model.</param>
        /// <returns>Nothing.</returns>
        private async Task InsertRecordAsync(
            IWho who,
            AddViewModel model)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, model) {@who} {@model}",
                nameof(this.InsertRecordAsync),
                who,
                model);

            IOrganisation organisation = await this.service.GetOrganisationByIdAsync(
                    who,
                    model.OrganisationId)
                .ConfigureAwait(false);

            ICommittee committee = model.ToDomain(organisation);

            await this.service
                .CreateCommitteeAsync(who, committee)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.InsertRecordAsync),
                who);
        }

        /// <summary>
        /// Update Committee.
        /// </summary>
        /// <param name="who">Who called it.</param>
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

            ICommittee originalCommittee = await this.service
                .GetCommitteeByIdAsync(
                    who: who,
                    committeeId: model.Id)
                .ConfigureAwait(false);

            ICommittee committee = model.ToDomain(originalCommittee.Organisation);

            await this.service
                .UpdateCommitteeAsync(
                    who,
                    committee)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.UpdateRecordAsync),
                who);
        }
    }
}