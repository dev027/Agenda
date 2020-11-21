// <copyright file="LocationDto.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

#nullable enable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Agenda.Data.Attributes;
using Agenda.Data.DbContexts;
using Agenda.Data.Resources;
using Agenda.Domain.DomainObjects.Locations;
using DomainMetadata = Agenda.Domain.DomainObjects.Locations.DomainMetadata;

namespace Agenda.Data.Dtos
{
    /// <summary>
    /// Location DTO.
    /// </summary>
    [Table(nameof(DataContext.Locations))]
    public class LocationDto : BaseDto
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationDto"/> class.
        /// </summary>
        public LocationDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationDto"/> class.
        /// </summary>
        /// <param name="id">Location Id.</param>
        /// <param name="organisationId">Organisation Id.</param>
        /// <param name="name">Location Name.</param>
        /// <param name="address">Address.</param>
        /// <param name="what3Words">What3Words Address.</param>
        /// <param name="latitude">Latitude.</param>
        /// <param name="longitude">Longitude.</param>
        public LocationDto(
            Guid id,
            Guid organisationId,
            string name,
            string address,
            string what3Words,
            double latitude,
            double longitude)
        {
            this.Id = id;
            this.OrganisationId = organisationId;
            this.Name = name;
            this.Address = address;
            this.What3Words = what3Words;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationDto"/> class.
        /// </summary>
        /// <param name="id">Location Id.</param>
        /// <param name="organisationId">Organisation Id.</param>
        /// <param name="name">Location Name.</param>
        /// <param name="address">Address.</param>
        /// <param name="what3Words">What3Words Address.</param>
        /// <param name="latitude">Latitude.</param>
        /// <param name="longitude">Longitude.</param>
        /// <param name="organisation">Organisation.</param>
        public LocationDto(
            Guid id,
            Guid organisationId,
            string name,
            string address,
            string what3Words,
            double latitude,
            double longitude,
            OrganisationDto organisation)
        {
            this.Id = id;
            this.OrganisationId = organisationId;
            this.Name = name;
            this.Address = address;
            this.What3Words = what3Words;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Organisation = organisation;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the Location Id.
        /// </summary>
        [AuditIgnore]
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the Organisation Id.
        /// </summary>
        public Guid OrganisationId { get; private set; }

        /// <summary>
        /// Gets the Location Name.
        /// </summary>
        [Required]
        [MaxLength(DomainMetadata.Name.MaxLength)]
        public string Name { get; private set; } = null!;

        /// <summary>
        /// Gets the Address.
        /// </summary>
        [Required]
        [MaxLength(DomainMetadata.Address.MaxLength)]
        public string Address { get; private set; } = null!;

        /// <summary>
        /// Gets the What3Word Address.
        /// </summary>
        [Required]
        [MaxLength(DomainMetadata.What3Words.MaxLength)]
        public string What3Words { get; private set; } = null!;

        /// <summary>
        /// Gets the Latitude.
        /// </summary>
        [Required]
        [Range(DomainMetadata.Latitude.MinValue, DomainMetadata.Latitude.MaxValue)]
        public double Latitude { get; private set; }

        /// <summary>
        /// Gets the Longitude.
        /// </summary>
        [Required]
        [Range(DomainMetadata.Longitude.MinValue, DomainMetadata.Longitude.MaxValue)]
        public double Longitude { get; private set; }

        #endregion Properties

        #region Parent Properties

        /// <summary>
        /// Gets the Organisation.
        /// </summary>
        [ForeignKey(nameof(OrganisationId))]
        public OrganisationDto? Organisation { get; private set; } = null!;

        #endregion Parent Properties

        #region Public Properties

        /// <summary>
        /// Converts domain object to DTO.
        /// </summary>
        /// <param name="location">Location.</param>
        /// <returns>Committee DTO.</returns>
        public static LocationDto ToDto(ILocation location)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            return new LocationDto(
                location.Id,
                location.Organisation.Id,
                location.Name,
                location.Address,
                location.What3Words,
                location.Latitude,
                location.Longitude);
        }

        /// <summary>
        /// Converts instance to domain object.
        /// </summary>
        /// <returns>Location.</returns>
        public ILocation ToDomain()
        {
            if (this.Organisation == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ExceptionResource.CannotConvertTo___If___IsNull,
                        nameof(ILocation),
                        nameof(this.Organisation)));
            }

            return new Location(
                id: this.Id,
                organisation: this.Organisation.ToDomain(),
                name: this.Name,
                address: this.Address,
                what3Words: this.What3Words,
                latitude: this.Latitude,
                longitude: this.Longitude);
        }

        #endregion Public Properties
    }
}