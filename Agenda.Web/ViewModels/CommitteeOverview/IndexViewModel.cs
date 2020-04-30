// <copyright file="IndexViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Agenda.Domain.DomainObjects.Organisations;

namespace Agenda.Web.ViewModels.CommitteeOverview
{
    /// <summary>
    /// Index view model.
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexViewModel"/> class.
        /// </summary>
        /// <param name="organisationId">Organisation Id.</param>
        /// <param name="organisationName">Organisation Name.</param>
        /// <param name="committeeViewModels">Committee view models.</param>
        public IndexViewModel(
            Guid organisationId,
            string organisationName,
            IList<CommitteeViewModel> committeeViewModels)
        {
            this.OrganisationId = organisationId;
            this.OrganisationName = organisationName;
            this.CommitteeViewModels = committeeViewModels;
        }

        /// <summary>
        /// Gets the Organisation Id.
        /// </summary>
        public Guid OrganisationId { get; }

        /// <summary>
        /// Gets the Organisation Name.
        /// </summary>
        [Display(Name = "Organisation")]
        public string OrganisationName { get; }

        /// <summary>
        /// Gets the Committee view models.
        /// </summary>
        public IList<CommitteeViewModel> CommitteeViewModels { get; }

        /// <summary>
        /// Creates the Index view model.
        /// </summary>
        /// <param name="organisation">The organisation.</param>
        /// <returns>Index view model.</returns>
        public static IndexViewModel Create(IOrganisationWithCommittees organisation)
        {
            if (organisation == null)
            {
                throw new ArgumentNullException(nameof(organisation));
            }

            return new IndexViewModel(
                organisationId: organisation.Id,
                organisationName: organisation.Name,
                committeeViewModels: organisation.Committees.Select(CommitteeViewModel.Create).ToList());
        }
    }
}