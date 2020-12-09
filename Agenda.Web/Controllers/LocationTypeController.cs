// <copyright file="LocationTypeController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.LocationTypes;
using Agenda.Domain.ValueObjects.Enums;
using Agenda.Service;
using Agenda.Utilities.Models.Whos;
using Agenda.Web.Models;
using Agenda.Web.ViewModels.LocationType;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Agenda.Web.Controllers
{
    /// <summary>
    /// SOMETHING.
    /// </summary>
    /// <seealso cref="Controller" />
    public class LocationTypeController : ControllerBase
    {
        private readonly ILogger<LocationTypeController> logger;
        private readonly IAgendaService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationTypeController"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="service">Agenda Service.</param>
        /// <param name="isTestMode">Is Test Mode.</param>
        public LocationTypeController(
            ILogger<LocationTypeController> logger,
            IAgendaService service,
            bool isTestMode = false)
        : base(typeof(LocationTypeController), isTestMode)
        {
            this.logger = logger;
            this.service = service;
        }

        /// <summary>
        /// Display the specified Location Type.
        /// </summary>
        /// <param name="locationTypeId">Location Type Id.</param>
        /// <returns>View.</returns>
        public async Task<IActionResult> Index(Guid locationTypeId)
        {
            IWho who = this.Who(nameof(this.Index));

            this.logger.LogDebug(
                "ENTRY {Action}(who, locationTypeId) {@Who} {LocationTypeId}",
                who.ActionName,
                who,
                locationTypeId);

            ILocationType locationType = await this.service
                .GetLocationTypeByIdAsync(
                    who: who,
                    locationTypeId: locationTypeId)
                .ConfigureAwait(false);

            IndexViewModel model = IndexViewModel.Create(locationType);

            return this.ExitView(this.logger, this.View(model));
        }

        /// <summary>
        /// Adds this Location Type.
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
                    "ENTRY {Action}(who, model) {@Who} {@Model}",
                    who.ActionName,
                    who,
                    model);

                if (model.FormState == FormState.Initial)
                {
                    this.ModelState.Clear();
                }
                else
                {
                    if (this.ModelState.IsValid)
                    {
                        await this.InsertRecordAsync(who, model).ConfigureAwait(false);

                        return this.ExitRedirectToAction(
                            this.logger,
                            this.RedirectToAction("Index", "LocationTypeOverview"));
                    }
                }

                return this.View(model);
            }
        }

        /// <summary>
        /// Starts the edit.
        /// </summary>
        /// <param name="locationTypeId">The Location Type Id.</param>
        /// <param name="ajaxMode">AJAX mode(0=No; 1=Yes).</param>
        /// <returns>View.</returns>
        public async Task<IActionResult> StartEdit(
            Guid locationTypeId,
            int ajaxMode)
        {
            IWho who = this.Who(nameof(this.StartEdit));

            this.logger.LogDebug(
                "ENTRY {Action}(who, locationTypeId, ajaxMode) {@Who} {LocationTypeId} {ajaxMode}",
                who.ActionName,
                who,
                locationTypeId,
                ajaxMode);

            ILocationType locationType = await this.service
                .GetLocationTypeByIdAsync(who, locationTypeId)
                .ConfigureAwait(false);

            EditViewModel model = EditViewModel.Create(locationType);

            switch (ajaxMode)
            {
                case 0:
                    return this.ExitView(this.logger, this.View("Edit", model));

                default:
                    throw new NotImplementedException($"AjaxMode {ajaxMode} not implemented yet.");
            }
        }

        /// <summary>
        /// Edits the specified location type.
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
                    "ENTRY {Action}(who, model) {@Who} {Model}",
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
                            "LocationType",
                            new { locationTypeId = model.LocationTypeId }));
                }

                return this.View(model);
            }
        }

        /// <summary>
        /// Insert Location Type.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="model">Add view model.</param>
        /// <returns>Nothing.</returns>
        private async Task InsertRecordAsync(
            IWho who,
            AddViewModel model)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, model) {@Who} {Model}",
                nameof(this.InsertRecordAsync),
                who,
                model);

            ILocationType locationType = model.ToDomain();

            await this.service
                .CreateLocationTypeAsync(who, AuditEvent.LocationTypeMaintenance, locationType)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.InsertRecordAsync),
                who);
        }

        /// <summary>
        /// Update Location Type.
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

            ILocationType locationType = model.ToDomain();

            await this.service
                .UpdateLocationTypeAsync(
                    who: who,
                    auditEvent: AuditEvent.LocationTypeMaintenance,
                    locationType: locationType)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@Who}",
                nameof(this.UpdateRecordAsync),
                who);
        }
    }
}