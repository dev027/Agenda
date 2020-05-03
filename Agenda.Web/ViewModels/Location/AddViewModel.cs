// <copyright file="AddViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Agenda.Domain.DomainObjects.Locations;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Web.Models;
using Agenda.Web.Models.ValidationAttributes;
using Agenda.Web.Resources;
using DomainMetadata = Agenda.Domain.DomainObjects.Locations.DomainMetadata;

namespace Agenda.Web.ViewModels.Location
{
    /// <summary>
    /// Add view model.
    /// </summary>
    public class AddViewModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AddViewModel"/> class.
        /// </summary>
        public AddViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddViewModel"/> class.
        /// </summary>
        /// <param name="formState">Form State.</param>
        /// <param name="organisationId">Organisation Id.</param>
        /// <param name="organisationName">Organisation Name.</param>
        /// <param name="name">Location Name.</param>
        /// <param name="address">Address.</param>
        /// <param name="what3WordsPart1">What3Words Address Part 1.</param>
        /// <param name="what3WordsPart2">What3Words Address Part 2.</param>
        /// <param name="what3WordsPart3">What3Words Address Part 3.</param>
        /// <param name="latitude">Latitude.</param>
        /// <param name="longitude">Longitude.</param>
        public AddViewModel(
            FormState formState,
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

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Form State.
        /// </summary>
        public FormState FormState { get; set; }

        /// <summary>
        /// Gets or sets the Organisation Id.
        /// </summary>
        [Required]
        public Guid OrganisationId { get; set; }

        /// <summary>
        /// Gets or sets the Organisation Name.
        /// </summary>
        [Display(Name = "Organisation")]
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
        [ValidWhat3WordsPart(Name="What3Words Address Part 1")]
        [StringLength(
            DomainMetadata.What3Words.Part.MaxLength,
            MinimumLength = DomainMetadata.What3Words.Part.MinLength)]
        public string What3WordsPart1 { get; set; }

        /// <summary>
        /// Gets or sets the What3Words Address Part 2.
        /// </summary>
        [Display(Name = "What3Words Address")]
        [ValidWhat3WordsPart(Name = "What3Words Address Part 2")]
        [StringLength(
            DomainMetadata.What3Words.Part.MaxLength,
            MinimumLength = DomainMetadata.What3Words.Part.MinLength)]
        public string What3WordsPart2 { get; set; }

        /// <summary>
        /// Gets or sets the What3Words Address Part 3.
        /// </summary>
        [Display(Name = "What3Words Address")]
        [ValidWhat3WordsPart(Name = "What3Words Address Part 3")]
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
        public double? Longitude { get; set;  }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates Add view model.
        /// </summary>
        /// <param name="organisation">Organisation.</param>
        /// <returns>Add view model.</returns>
        public static AddViewModel Create(IOrganisation organisation)
        {
            if (organisation == null)
            {
                throw new ArgumentNullException(nameof(organisation));
            }

            return new AddViewModel(
                formState: FormState.Initial,
                organisationId: organisation.Id,
                organisationName: organisation.Name,
                name: string.Empty,
                address: string.Empty,
                what3WordsPart1: string.Empty,
                what3WordsPart2: string.Empty,
                what3WordsPart3: string.Empty,
                latitude: null,
                longitude: null);
        }

        /// <summary>
        /// Converts view model to Location domain object.
        /// </summary>
        /// <param name="organisation">Organisation.</param>
        /// <returns>Location domain object.</returns>
        public ILocation ToDomain(IOrganisation organisation)
        {
            if (this.Latitude == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ExceptionResource.CannotConvertTo___When___IsNull,
                        nameof(ILocation),
                        nameof(this.Latitude)));
            }

            if (this.Longitude == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ExceptionResource.CannotConvertTo___When___IsNull,
                        nameof(ILocation),
                        nameof(this.Longitude)));
            }

            string what3Words = Domain.DomainObjects.Locations.Location.What3WordsJoin(
                this.What3WordsPart1,
                this.What3WordsPart2,
                this.What3WordsPart3);

            return new Domain.DomainObjects.Locations.Location(
                id: Guid.NewGuid(),
                organisation: organisation,
                name: this.Name,
                address: this.Address,
                what3Words: what3Words,
                latitude: this.Latitude.Value,
                longitude: this.Longitude.Value);
        }

        #endregion
    }
}
