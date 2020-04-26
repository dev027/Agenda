﻿// <copyright file="LocationController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Locations;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Service;
using Agenda.Utilities.Models.Whos;
using Agenda.Web.Models;
using Agenda.Web.ViewModels.Location;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Agenda.Web.Controllers
{
    /// <summary>
    /// Meetings.
    /// </summary>
    /// <seealso cref="Controller" />
    public class LocationController : ControllerBase
    {
        private readonly ILogger<LocationController> logger;
        private readonly IAgendaService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationController"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="service">Agenda Service.</param>
        /// <param name="isTestMode">Is Test Mode.</param>
        public LocationController(
            ILogger<LocationController> logger,
            IAgendaService service,
            bool isTestMode = false)
            : base(typeof(LocationController), isTestMode)
        {
            this.logger = logger;
            this.service = service;
        }

        /// <summary>
        /// Display the specified Location.
        /// </summary>
        /// <param name="locationId">Location Id.</param>
        /// <returns>View.</returns>
        public async Task<IActionResult> Index(Guid locationId)
        {
            IWho who = this.Who(nameof(this.Index));

            this.logger.LogDebug(
                "ENTRY {ActionName}(who, locationId) {@who} {locationId}",
                who.ActionName,
                who,
                locationId);

            ILocation location = await this.service
                .GetLocationByIdAsync(
                    who: who,
                    locationId: locationId)
                .ConfigureAwait(false);

            IndexViewModel model = IndexViewModel.Create(location);

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
                "ENTRY {ActionName}(who, organisationId) {@who} {organisationId}",
                who.ActionName,
                who,
                organisationId);

            IOrganisation organisation = await this.service
                .GetOrganisationByIdAsync(who, organisationId)
                .ConfigureAwait(false);

            AddViewModel model = AddViewModel.Create(organisation);

            return this.ExitRedirectToAction(
                this.logger,
                this.RedirectToAction("Add", model));
        }

        /// <summary>
        /// Adds Location.
        /// </summary>
        /// <param name="model">Add view model.</param>
        /// <returns>View.</returns>
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
                            "LocationOverview",
                            new { organisationId = model.OrganisationId }));
                }
            }

            return this.ExitView(this.logger, this.View(model));
        }

        /// <summary>
        /// Starts the edit.
        /// </summary>
        /// <param name="locationId">The Location Id.</param>
        /// <param name="ajaxMode">AJAX mode(0=No; 1=Yes).</param>
        /// <returns>View.</returns>
        public async Task<IActionResult> StartEdit(
            Guid locationId,
            int ajaxMode)
        {
            IWho who = this.Who(nameof(this.StartEdit));

            this.logger.LogDebug(
                "ENTRY {Action}(who, locationId, ajaxMode) {@who} {locationId} {ajaxMode}",
                who.ActionName,
                who,
                locationId,
                ajaxMode);

            ILocation location = await this.service
                .GetLocationByIdAsync(who, locationId)
                .ConfigureAwait(false);

            EditViewModel model = EditViewModel.Create(location);

            switch (ajaxMode)
            {
                case 0:
                    return this.ExitView(this.logger, this.View("Edit", model));

                default:
                    throw new NotImplementedException($"AjaxMode {ajaxMode} not implemented yet.");
            }
        }

        /// <summary>
        /// Edits the specified Location.
        /// </summary>
        /// <param name="model">Edit view model.</param>
        /// <returns>View.</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            IWho who = this.Who(nameof(this.Edit));

            this.logger.LogDebug(
                "ENTRY {Action}(who, model) {@who} {@model}",
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
                        "Location",
                        new { locationId = model.LocationId }));
            }

            return this.View(model);
        }

        /// <summary>
        /// Insert Location.
        /// </summary>
        /// <param name="who">Who Details.</param>
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

            ILocation location = model.ToDomain(organisation);

            await this.service
                .CreateLocationAsync(who, location)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.InsertRecordAsync),
                who);
        }

        /// <summary>
        /// Update Location.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <param name="model">Edit view model.</param>
        /// <returns>Nothing.</returns>
        private async Task UpdateRecordAsync(
            IWho who,
            EditViewModel model)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, model) {@who} {@model}",
                nameof(this.UpdateRecordAsync),
                who,
                model);

            ILocation originalLocation = await this.service
                .GetLocationByIdAsync(
                    who: who,
                    locationId: model.LocationId)
                .ConfigureAwait(false);

            ILocation location = model.ToDomain(originalLocation.Organisation);

            await this.service
                .UpdateLocationAsync(
                    who,
                    location)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.UpdateRecordAsync),
                who);
        }
    }
}