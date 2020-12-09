// <copyright file="EditViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using Agenda.Domain.DomainObjects.Locations;
using Agenda.Domain.DomainObjects.LocationTypes;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Web.Models;
using Agenda.Web.Models.ValidationAttributes;
using DomainMetadata = Agenda.Domain.DomainObjects.Locations.DomainMetadata;

namespace Agenda.Web.ViewModels.Location
{
    /// <summary>
    /// Add view model.
    /// </summary>
    public class EditViewModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EditViewModel"/> class.
        /// </summary>
        public EditViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditViewModel"/> class.
        /// </summary>
        /// <param name="formState">Form State.</param>
        /// <param name="locationId">Location Id.</param>
        /// <param name="organisationId">Organisation Id.</param>
        /// <param name="organisationName">Organisation Name.</param>
        /// <param name="name">Location Name.</param>
        /// <param name="address">Address.</param>
        /// <param name="what3WordsPart1">What3Words Address Part 1.</param>
        /// <param name="what3WordsPart2">What3Words Address Part 2.</param>
        /// <param name="what3WordsPart3">What3Words Address Part 3.</param>
        /// <param name="latitude">Latitude.</param>
        /// <param name="longitude">Longitude.</param>
        public EditViewModel(
            FormState formState,
            Guid locationId,
            Guid organisationId,
            string organisationName,
            string name,
            string address,
            string what3WordsPart1,
            string what3WordsPart2,
            string what3WordsPart3,
            double? latitude,
            double? longitude)
        {
            this.FormState = formState;
            this.LocationId = locationId;
            this.OrganisationId = organisationId;
            this.OrganisationName = organisationName;
            this.Name = name;
            this.Address = address;
            this.What3WordsPart1 = what3WordsPart1;
            this.What3WordsPart2 = what3WordsPart2;
            this.What3WordsPart3 = what3WordsPart3;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the Form State.
        /// </summary>
        public FormState FormState { get; set; }

        /// <summary>
        /// Gets or sets the Location Id.
        /// </summary>
        [Required]
        public Guid LocationId { get; set; }

         /// <summary>
        /// Gets or sets the Organisation Id.
        /// </summary>
        [Required]
        public Guid OrganisationId { get; set; }

        /// <summary>
        /// Gets or sets the Organisation Name.
        /// </summary>
        [Display(Name = "Organisation")]
        [Required]
        public string OrganisationName { get; set; }

        /// <summary>
        /// Gets or sets the Location Name.
        /// </summary>
        [Display(Name = "Name")]
        [Required]
        [StringLength(
            DomainMetadata.Name.MaxLength,
            MinimumLength = DomainMetadata.Name.MinLength)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Address.
        /// </summary>
        [Display(Name = "Address")]
        [Required]
        [StringLength(
            DomainMetadata.Address.MaxLength,
            MinimumLength = DomainMetadata.Address.MinLength)]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the What3Words Address Part 1.
        /// </summary>
        [Display(Name = "What3Words Address")]
        [ValidWhat3WordsPart]
        [StringLength(
            DomainMetadata.What3Words.Part.MaxLength,
            MinimumLength = DomainMetadata.What3Words.Part.MinLength)]
        public string What3WordsPart1 { get; set; }

        /// <summary>
        /// Gets or sets the What3Words Address Part 2.
        /// </summary>
        [Display(Name = "What3Words Address")]
        [ValidWhat3WordsPart]
        [StringLength(
            DomainMetadata.What3Words.Part.MaxLength,
            MinimumLength = DomainMetadata.What3Words.Part.MinLength)]
        public string What3WordsPart2 { get; set; }

        /// <summary>
        /// Gets or sets the What3Words Address Part 3.
        /// </summary>
        [Display(Name = "What3Words Address")]
        [ValidWhat3WordsPart]
        [StringLength(
            DomainMetadata.What3Words.Part.MaxLength,
            MinimumLength = DomainMetadata.What3Words.Part.MinLength)]
        public string What3WordsPart3 { get; set; }

        /// <summary>
        /// Gets or sets the Latitude.
        /// </summary>
        [Display(Name = "Latitude")]
        [Required]
        [Range(DomainMetadata.Latitude.MinValue, DomainMetadata.Latitude.MaxValue)]
        public double? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the Longitude.
        /// </summary>
        [Display(Name = "Longitude")]
        [Required]
        [Range(DomainMetadata.Longitude.MinValue, DomainMetadata.Longitude.MaxValue)]
        public double? Longitude { get; set; }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Creates the view model.
        /// </summary>
        /// <param name="location">Location.</param>
        /// <returns>Edit view model.</returns>
        public static EditViewModel Create(ILocation location)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            string[] what3WordsParts = location.What3WordsParts();

            return new EditViewModel(
                formState: FormState.Initial,
                locationId: location.Id,
                organisationId: location.Organisation.Id,
                organisationName: location.Organisation.Name,
                name: location.Name,
                address: location.Address,
                what3WordsPart1: what3WordsParts[0],
                what3WordsPart2: what3WordsParts[1],
                what3WordsPart3: what3WordsParts[2],
                latitude: location.Latitude,
                longitude: location.Longitude);
        }

        /// <summary>
        /// Converts instance to Location domain object.
        /// </summary>
        /// <param name="organisation">Organisation.</param>
        /// <param name="locationType">Location Type.</param>
        /// <returns>Committee domain object.</returns>
        public ILocation ToDomain(
            IOrganisation organisation,
            ILocationType locationType)
        {
            string what3Words = Domain.DomainObjects.Locations.Location.What3WordsJoin(
                this.What3WordsPart1,
                this.What3WordsPart2,
                this.What3WordsPart3);

            return new Domain.DomainObjects.Locations.Location(
                this.LocationId,
                organisation,
                locationType,
                this.Name,
                this.Address,
                what3Words,
                this.Latitude,
                this.Longitude);
        }

        #endregion Public Methods
    }
}