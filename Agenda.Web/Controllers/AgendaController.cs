// <copyright file="AgendaController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.AgendaItems;
using Agenda.Domain.DomainObjects.Meetings;
using Agenda.Domain.ValueObjects.Enums;
using Agenda.Service;
using Agenda.Service.Constants;
using Agenda.Utilities.Logging;
using Agenda.Utilities.Models.Whos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Agenda.Web.Controllers
{
    /// <summary>
    /// Agenda Controller.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    public class AgendaController : ControllerBase
    {
        private readonly ILogger<AgendaController> logger;
        private readonly IAgendaService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgendaController"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="service">Agenda Service.</param>
        /// <param name="isTestMode">Is Test Mode.</param>
        public AgendaController(
            ILogger<AgendaController> logger,
            IAgendaService service,
            bool isTestMode = false)
            : base(typeof(AgendaController), isTestMode)
        {
            this.logger = logger;
            this.service = service;
        }

        /// <summary>
        /// SOMETHING.
        /// </summary>
        /// <param name="meetingId">Meeting Id.</param>
        /// <returns>Action Result.</returns>
        public async Task<IActionResult> Index(Guid meetingId)
        {
            IWho who = this.Who();

            this.logger.ReportEntry(
                who,
                new { MeetingId = meetingId });

            IMeeting meeting = await this.service.GetMeetingByIdAsync(
                who,
                meetingId).ConfigureAwait(false);

            IAgendaItem agendaItem = await this.service.GetAgendaItemsByMeetingIdAsTreeAsync(
                who,
                meetingId).ConfigureAwait(false);

            if (agendaItem == null)
            {
                agendaItem = await this.service.CreateSkeletonAgendaAsync(
                    who,
                    AuditEvent.ViewAgenda,
                    meetingId,
                    SkeletonAgendaType.BasicContinuation).ConfigureAwait(false);
            }

            _ = meeting;
            _ = agendaItem;

            throw new NotImplementedException();
            ////return this.View();
        }
    }
}