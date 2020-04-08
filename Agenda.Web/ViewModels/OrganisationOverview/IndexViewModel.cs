// <copyright file="IndexViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Web.ViewModels.Organisation;

namespace Agenda.Web.ViewModels.OrganisationOverview
{
    /// <summary>
    /// organisation Overview Index view model.
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexViewModel"/> class.
        /// </summary>
        /// <param name="organisationViewModels">List of Organisation view models.</param>
        public IndexViewModel(
            IList<OrganisationViewModel> organisationViewModels)
        {
            this.PageTitle = null;
            this.ActionTitle = null;
            this.OrganisationCodeTitle = null;
            this.OrganisationNameTitle = null;

            this.OrganisationViewModels = organisationViewModels;
        }

        /// <summary>
        /// Gets the page title.
        /// </summary>
        [Display(Name = "Organisations")]
        public string PageTitle { get; }

        /// <summary>
        /// Gets the action title.
        /// </summary>
        [Display(Name = "Action")]
        public string ActionTitle { get; }

        /// <summary>
        /// Gets the organisation code title.
        /// </summary>
        [Display(Name = "Code")]
        public string OrganisationCodeTitle { get; }

        /// <summary>
        /// Gets the organisation name title.
        /// </summary>
        [Display(Name = "Name")]
        public string OrganisationNameTitle { get; }

        /// <summary>
        /// Gets the Organisation view models.
        /// </summary>
        public IList<OrganisationViewModel> OrganisationViewModels { get; }

        /// <summary>
        /// Creates the view model.
        /// </summary>
        /// <param name="organisations">The organisations.</param>
        /// <returns>View model.</returns>
        public static IndexViewModel Create(
            IList<IOrganisation> organisations)
        {
            return new IndexViewModel(
                organisations.Select(OrganisationViewModel.Create).ToList());
        }
    }
}