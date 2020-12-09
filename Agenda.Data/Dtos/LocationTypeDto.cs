// <copyright file="LocationTypeDto.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Agenda.Data.Attributes;
using Agenda.Domain.DomainObjects.LocationTypes;

namespace Agenda.Data.Dtos
{
    /// <summary>
    /// Location Type DTO.
    /// </summary>
    public class LocationTypeDto : BaseDto
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationTypeDto"/> class.
        /// </summary>
        public LocationTypeDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationTypeDto"/> class.
        /// </summary>
        /// <param name="id">Location Type Id.</param>
        /// <param name="code">Location Type Code.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        public LocationTypeDto(
            Guid id,
            string code,
            string name,
            string description)
        {
            this.Id = id;
            this.Code = code;
            this.Name = name;
            this.Description = description;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the Location Type Id.
        /// </summary>
        [AuditIgnore]
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the Location Type Code.
        /// </summary>
        [Required]
        [MaxLength(DomainMetadata.Code.MaxLength)]
        public string Code { get; private set; } = null!;

        /// <summary>
        /// Gets the Location Type Name.
        /// </summary>
        [Required]
        [MaxLength(DomainMetadata.Name.MaxLength)]
        public string Name { get; private set; } = null!;

        /// <summary>
        /// Gets the Location Type Description.
        /// </summary>
        [Required]
        [MaxLength(DomainMetadata.Description.MaxLength)]
        public string Description { get; private set; } = null!;

        #endregion Properties

        #region Child Properties

        /// <summary>
        /// Gets the locations.
        /// </summary>
        public IList<LocationDto> Locations { get; private set; } = null!;

        #endregion Child Properties

        #region Public Methods

        /// <summary>
        /// Converts domain object to DTO.
        /// </summary>
        /// <param name="locationType">Location Type.</param>
        /// <returns>Location Type DTO.</returns>
        public static LocationTypeDto ToDto(ILocationType locationType)
        {
            if (locationType == null)
            {
                throw new ArgumentNullException(nameof(locationType));
            }

            return new LocationTypeDto(
                id: locationType.Id,
                code: locationType.Code,
                name: locationType.Name,
                description: locationType.Description);
        }

        /// <summary>
        /// Converts instance to domain object.
        /// </summary>
        /// <returns>Location Type.</returns>
        public ILocationType ToDomain()
        {
            return new LocationType(
                id: this.Id,
                code: this.Code,
                name: this.Name,
                description: this.Description);
        }

        #endregion Public Methods
    }
}