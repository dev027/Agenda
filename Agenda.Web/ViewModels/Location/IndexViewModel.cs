// <copyright file="IndexViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using Agenda.Domain.DomainObjects.Locations;

namespace Agenda.Web.ViewModels.Location
{
    /// <summary>
    /// Index view model.
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexViewModel"/> class.
        /// </summary>
        /// <param name="locationId">Location Id.</param>
        /// <param name="organisationName">Organisation Name.</param>
        /// <param name="name">Location Name.</param>
        /// <param name="address">Address.</param>
        /// <param name="what3Words">What3Words Address.</param>
        /// <param name="latitude">Latitude.</param>
        /// <param name="longitude">Longitude.</param>
        public IndexViewModel(
            Guid locationId,
            string organisationName,
            string name,
            string address,
            string what3Words,
            double latitude,
            double longitude)
        {
            this.LocationId = locationId;
            this.OrganisationName = organisationName;
            this.Name = name;
            this.Address = address;
            this.What3Words = what3Words;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        /// <summary>
        /// Gets the Page Title.
        /// </summary>
        [Display(Name="Edit Location")]
        public string PageTitle { get; } = null;

        /// <summary>
        /// Gets the Card Title.
        /// </summary>
        [Display(Name = "Location")]
        public string CardTitle { get; } = null;

        /// <summary>
        /// Gets the Location Id.
        /// </summary>
        public Guid LocationId { get; }

        /// <summary>
        /// Gets the Organisation Name.
        /// </summary>
        [Display(Name = "Organisation")]
        public string OrganisationName { get; }

        /// <summary>
        /// Gets the Location Name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the Address.
        /// </summary>
        public string Address { get; }

        /// <summary>
        /// Gets the What3Words Address.
        /// </summary>
        public string What3Words { get; }

        /// <summary>
        /// Gets the Latitude.
        /// </summary>
        public double Latitude { get; }

        /// <summary>
        /// Gets the Longitude.
        /// </summary>
        public double Longitude { get; }

        /// <summary>
        /// Creates Index view model.
        /// </summary>
        /// <param name="location">Location.</param>
        /// <returns>Index view model.</returns>
        public static IndexViewModel Create(ILocation location)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            return new IndexViewModel(
                location.Id,
                location.Organisation.Name,
                location.Name,
                location.Address,
                location.What3Words,
                location.Latitude,
                location.Longitude);
        }
    }
}