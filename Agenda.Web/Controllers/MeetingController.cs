// <copyright file="MeetingController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.DomainObjects.Meetings;
using Agenda.Domain.ValueObjects.Enums;
using Agenda.Service;
using Agenda.Utilities.Logging;
using Agenda.Utilities.Models.Whos;
using Agenda.Web.Models;
using Agenda.Web.ViewModels.Meeting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;

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
            IWho who = this.Who();

            this.logger.ReportEntry(
                who,
                new { MeetingId = meetingId });

            IMeeting meeting = await this.service
                .GetMeetingByIdAsync(
                    who: who,
                    meetingId: meetingId)
                .ConfigureAwait(false);

            IndexViewModel model = IndexViewModel.Create(meeting);

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
        /// <param name="committeeId">Committee Id.</param>
        /// <returns>Redirect To Action.</returns>
        public async Task<IActionResult> StartAdd(Guid committeeId)
        {
            IWho who = this.Who();

            this.logger.ReportExit(
                who,
                new { CommitteeId = committeeId });

            ICommittee committee = await this.service
                .GetCommitteeByIdAsync(who, committeeId)
                .ConfigureAwait(false);

            AddViewModel model = AddViewModel.Create(committee);

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
                    this.ModelState.Clear();
                }
                else
                {
                    if (this.ModelState.IsValid)
                    {
                        this.logger.Debug(
                            who,
                            "Inserting model",
                            new { Model = model });

                        await this.InsertRecordAsync(who, model).ConfigureAwait(false);

                        RedirectToActionResult redirect = this.RedirectToAction(
                            "Index",
                            "MeetingOverview",
                            new { committeeId = model.CommitteeId });

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
        /// <param name="meetingId">The Meeting Id.</param>
        /// <param name="ajaxMode">AJAX mode(0=No; 1=Yes).</param>
        /// <returns>View.</returns>
        public async Task<IActionResult> StartEdit(
            Guid meetingId,
            int ajaxMode)
        {
            IWho who = this.Who();

            this.logger.ReportEntry(
                who,
                new
                {
                    MeetingId = meetingId,
                    AjaxMode = ajaxMode
                });

            IMeeting meeting = await this.service
                .GetMeetingByIdAsync(who, meetingId)
                .ConfigureAwait(false);

            EditViewModel model = EditViewModel.Create(meeting);

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
                IWho who = this.Who();

                this.logger.ReportEntry(
                    who,
                    new { Model = model });

                if (this.ModelState.IsValid)
                {
                    await this.UpdateRecordAsync(who, model).ConfigureAwait(false);

                    RedirectToActionResult redirect = this.RedirectToAction(
                        "Index",
                        "Meeting",
                        new { meetingId = model.MeetingId });

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
        /// Insert Meeting.
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

            ICommittee committee = await this.service.GetCommitteeByIdAsync(
                    who: who,
                    committeeId: model.CommitteeId)
                .ConfigureAwait(false);

            IMeeting meeting = model.ToDomain(
                committee: committee);

            await this.service
                .CreateMeetingAsync(who, AuditEvent.MeetingMaintenance, meeting)
                .ConfigureAwait(false);

            this.logger.ReportExit(who);
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
            this.logger.ReportEntry(
                who,
                new { Model = model });

            IMeeting originalMeeting = await this.service
                .GetMeetingByIdAsync(
                    who: who,
                    meetingId: model.MeetingId)
                .ConfigureAwait(false);

            IMeeting meeting = model.ToDomain(
                committee: originalMeeting.Committee);

            await this.service
                .UpdateMeetingAsync(
                    who: who,
                    auditEvent: AuditEvent.MeetingMaintenance,
                    meeting: meeting)
                .ConfigureAwait(false);

            this.logger.ReportExit(who);
        }
    }
}