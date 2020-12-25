// <copyright file="MeetingOverviewController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Service;
using Agenda.Utilities.Logging;
using Agenda.Utilities.Models.Whos;
using Agenda.Web.ViewModels.MeetingOverview;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Agenda.Web.Controllers
{
    /// <summary>
    /// Meeting Overview controller.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    public class MeetingOverviewController : ControllerBase
    {
        private readonly ILogger<MeetingOverviewController> logger;
        private readonly IAgendaService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingOverviewController"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="service">Agenda Service.</param>
        /// <param name="isTestMode">Is Test Mode.</param>
        public MeetingOverviewController(
            ILogger<MeetingOverviewController> logger,
            IAgendaService service,
            bool isTestMode = false)
            : base(typeof(CommitteeOverviewController), isTestMode)
        {
            this.logger = logger;
            this.service = service;
        }

        /// <summary>
        /// Display meetings for the specified committee.
        /// </summary>
        /// <param name="committeeId">CommitteeId.</param>
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
    }
}