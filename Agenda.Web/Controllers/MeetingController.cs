// <copyright file="MeetingController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.DomainObjects.Locations;
using Agenda.Domain.DomainObjects.Meetings;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Domain.ValueObjects.Enums;
using Agenda.Service;
using Agenda.Utilities.Models.Whos;
using Agenda.Web.Models;
using Agenda.Web.ViewModels.Meeting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Agenda.Web.Controllers
{
    /// <summary>
    /// Meetings.
    /// </summary>
    /// <seealso cref="Controller" />
    public class MeetingController : ControllerBase
    {
        private readonly ILogger<MeetingController> logger;
        private readonly IAgendaService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingController"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="service">Agenda Service.</param>
        /// <param name="isTestMode">Is Test Mode.</param>
        public MeetingController(
            ILogger<MeetingController> logger,
            IAgendaService service,
            bool isTestMode = false)
            : base(typeof(MeetingController), isTestMode)
        {
            this.logger = logger;
            this.service = service;
        }

        /// <summary>
        /// Display the specified Meeting.
        /// </summary>
        /// <param name="meetingId">Meeting Id.</param>
        /// <returns>View.</returns>
        public async Task<IActionResult> Index(Guid meetingId)
        {
            IWho who = this.Who(nameof(this.Index));

            this.logger.LogDebug(
                "ENTRY {ActionName}(who, meetingId) {@who} {meetingId}",
                who.ActionName,
                who,
                meetingId);

            IMeeting meeting = await this.service
                .GetMeetingByIdAsync(
                    who: who,
                    meetingId: meetingId)
                .ConfigureAwait(false);

            IndexViewModel model = IndexViewModel.Create(meeting);

            return this.ExitView(this.logger, this.View(model));
        }

        /// <summary>
        /// Starts the add.
        /// </summary>
        /// <param name="committeeId">Committee Id.</param>
        /// <returns>Redirect To Action.</returns>
        public async Task<IActionResult> StartAdd(Guid committeeId)
        {
            IWho who = this.Who(nameof(this.StartAdd));

            this.logger.LogDebug(
                "ENTRY {ActionName}(who, committeeId) {@who} {committeeId}",
                who.ActionName,
                who,
                committeeId);

            ICommittee committee = await this.service
                .GetCommitteeByIdAsync(who, committeeId)
                .ConfigureAwait(false);

            AddViewModel model = AddViewModel.Create(committee);

            return this.ExitRedirectToAction(
                this.logger,
                this.RedirectToAction("Add", model));
        }

        /// <summary>
        /// Adds this instance.
        /// </summary>
        /// <param name="model">Add view model.</param>
        /// <returns>View.</returns>
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
                    "ENTRY {ActionName}(who, model) {@who} {@model}",
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
                        this.logger.LogDebug(
                            "{@who} inserting model {@Model}",
                            who,
                            model);

                        await this.InsertRecordAsync(who, model).ConfigureAwait(false);

                        return this.ExitRedirectToAction(
                            this.logger,
                            this.RedirectToAction(
                                "Index",
                                "MeetingOverview",
                                new { committeeId = model.CommitteeId }));
                    }
                }

                IOrganisationWithLocations organisation = await this.service
                    .GetOrganisationByCommitteeIdWithLocationsAsync(who, model.CommitteeId)
                    .ConfigureAwait(false);

                model.RefreshDropdowns(organisation.Locations);

                return this.ExitView(this.logger, this.View(model));
            }
        }

        /// <summary>
        /// Starts the edit.
        /// </summary>
        /// <param name="meetingId">The Meeting Id.</param>
        /// <param name="ajaxMode">AJAX mode(0=No; 1=Yes).</param>
        /// <returns>View.</returns>
        public async Task<IActionResult> StartEdit(
            Guid meetingId,
            int ajaxMode)
        {
            IWho who = this.Who(nameof(this.StartEdit));

            this.logger.LogDebug(
                "ENTRY {Action}(who, meetingId, ajaxMode) {@who} {meetingId} {ajaxMode}",
                who.ActionName,
                who,
                meetingId,
                ajaxMode);

            IMeeting meeting = await this.service
                .GetMeetingByIdAsync(who, meetingId)
                .ConfigureAwait(false);

            EditViewModel model = EditViewModel.Create(meeting);

            IOrganisationWithLocations organisation = await this.service
                .GetOrganisationByMeetingIdWithLocationsAsync(
                    who: who,
                    meetingId: model.MeetingId)
                .ConfigureAwait(false);

            model.RefreshDropdowns(organisation.Locations);

            switch (ajaxMode)
            {
                case 0:
                    return this.ExitView(this.logger, this.View("Edit", model));

                default:
                    throw new NotImplementedException($"AjaxMode {ajaxMode} not implemented yet.");
            }
        }

        /// <summary>
        /// Edits the specified Meeting.
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
                    "ENTRY {Action}(who, model) {@who} {@model}",
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
                            "Meeting",
                            new { meetingId = model.MeetingId }));
                }

                IOrganisationWithLocations organisation = await this.service
                    .GetOrganisationByMeetingIdWithLocationsAsync(
                        who: who,
                        meetingId: model.MeetingId)
                    .ConfigureAwait(false);

                model.RefreshDropdowns(organisation.Locations);

                return this.View(model);
            }
        }

        /// <summary>
        /// Insert Meeting.
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

            ICommittee committee = await this.service.GetCommitteeByIdAsync(
                    who: who,
                    committeeId: model.CommitteeId)
                .ConfigureAwait(false);

            ILocation location = await this.service.GetLocationByIdAsync(
                    who: who,
                    locationId: model.LocationId)
                .ConfigureAwait(false);

            IMeeting meeting = model.ToDomain(
                committee: committee,
                location: location);

            await this.service
                .CreateMeetingAsync(who, AuditEvent.MeetingMaintenance, meeting)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.InsertRecordAsync),
                who);
        }

        /// <summary>
        /// Update Meeting.
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

            IMeeting originalMeeting = await this.service
                .GetMeetingByIdAsync(
                    who: who,
                    meetingId: model.MeetingId)
                .ConfigureAwait(false);

            ILocation location = await this.service
                .GetLocationByIdAsync(
                    who: who,
                    locationId: model.LocationId)
                .ConfigureAwait(false);

            IMeeting meeting = model.ToDomain(
                committee: originalMeeting.Committee,
                location: location);

            await this.service
                .UpdateMeetingAsync(
                    who: who,
                    auditEvent: AuditEvent.MeetingMaintenance,
                    meeting: meeting)
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.UpdateRecordAsync),
                who);
        }
    }
}