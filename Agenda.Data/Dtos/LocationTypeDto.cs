// <copyright file="LocationTypeDto.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using Agenda.Data.Attributes;
using Agenda.Domain.DomainObjects.LocationTypes;

namespace Agenda.Data.Dtos
{
    /// <summary>
    /// Location Type DTO.
    /// </summary>
    public class LocationTypeDto
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
    }
}