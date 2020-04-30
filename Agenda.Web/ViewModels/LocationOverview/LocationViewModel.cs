// <copyright file="LocationViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using Agenda.Domain.DomainObjects.Locations;

namespace Agenda.Web.ViewModels.LocationOverview
{
    /// <summary>
    /// Location view model.
    /// </summary>
    public class LocationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationViewModel"/> class.
        /// </summary>
        /// <param name="locationId">Location Id.</param>
        /// <param name="name">Location Name.</param>
        /// <param name="address">Address.</param>
        public LocationViewModel(
            Guid locationId,
            string name,
            string address)
        {
            this.LocationId = locationId;
            this.Name = name;
            this.Address = address;
        }

        /// <summary>
        /// Gets the view action button text.
        /// </summary>
        [Display(Name="View")]
        public string ViewActionButtonText { get; } = null;

        /// <summary>
        /// Gets the Location Id.
        /// </summary>
        public Guid LocationId { get; }

        /// <summary>
        /// Gets the Location NAme.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the Address.
        /// </summary>
        public string Address { get; }

        /// <summary>
        /// Creates the Location view model.
        /// </summary>
        /// <param name="location">Location.</param>
        /// <returns>Location view model.</returns>
        public static LocationViewModel Create(ILocation location)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            return new LocationViewModel(
                locationId: location.Id,
                name: location.Name,
                address: location.Address);
        }
    }
}