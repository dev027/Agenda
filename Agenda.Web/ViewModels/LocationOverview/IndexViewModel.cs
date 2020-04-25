// <copyright file="IndexViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Web.Views.LocationOverview;

namespace Agenda.Web.ViewModels.LocationOverview
{
    /// <summary>
    /// Index View Model.
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexViewModel"/> class.
        /// </summary>
        /// <param name="organisationId">Organisation Id.</param>
        /// <param name="organisationName">Organisation Name.</param>
        /// <param name="locationViewModels">Location view models.</param>
        public IndexViewModel(
            Guid organisationId,
            string organisationName,
            IList<LocationViewModel> locationViewModels)
        {
            this.OrganisationId = organisationId;
            this.OrganisationName = organisationName;
            this.LocationViewModels = locationViewModels;
        }

        /// <summary>
        /// Gets the Page Title.
        /// </summary>
        [Display(Name = "Locations")]
        public string PageTitle { get; } = null;

        /// <summary>
        /// Gets the Action Title.
        /// </summary>
        [Display(Name = "Action")]
        public string ActionTitle { get; } = null;

        /// <summary>
        /// Gets the Location Name.
        /// </summary>
        [Display(Name = "Name")]
        public string LocationName { get; } = null;

        /// <summary>
        /// Gets the Location Address.
        /// </summary>
        [Display(Name = "Address")]
        public string LocationAddress { get; } = null;

        /// <summary>
        /// Gets the Organisation Id.
        /// </summary>
        public Guid OrganisationId { get; }

        /// <summary>
        /// Gets the Organisation Name.
        /// </summary>
        public string OrganisationName { get; }

        /// <summary>
        /// Gets the Location view models.
        /// </summary>
        public IList<LocationViewModel> LocationViewModels { get; }

        /// <summary>
        /// Creates the Index view model.
        /// </summary>
        /// <param name="organisation">Organisation with Locations.</param>
        /// <returns>Index view model.</returns>
        public static IndexViewModel Create(IOrganisationWithLocations organisation)
        {
            if (organisation == null)
            {
                throw new ArgumentNullException(nameof(organisation));
            }

            return new IndexViewModel(
                organisationId: organisation.Id,
                organisationName: organisation.Name,
                locationViewModels: organisation.Locations.Select(LocationViewModel.Create).ToList());
        }
    }
}