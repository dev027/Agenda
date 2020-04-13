// <copyright file="CommitteeController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Service;
using Agenda.Utilities.Models.Whos;
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
        public Task<IActionResult> Index(Guid id)
        {
            throw new NotImplementedException();

            ////IWho who = this.Who(nameof(this.Index));

            ////this.Entry(this.logger);

            ////IOrganisationWithCommittees organisation = await this.service
            ////    .GetOrganisationByIdWithCommitteesAsync(
            ////        who: who,
            ////        organisationId: id)
            ////    .ConfigureAwait(false);

            ////IndexViewModel model = IndexViewModel.Create(organisation);

            ////return this.ExitView(this.logger, this.View(model));
        }

        /// <summary>
        /// Adds this instance.
        /// </summary>
        /// <returns>View.</returns>
        public IActionResult Add()
        {
            IWho who = this.Who(nameof(this.Add));

            this.Entry(this.logger);

            throw new NotImplementedException();
        }
    }
}