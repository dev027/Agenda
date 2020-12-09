// <copyright file="LocationType.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using Agenda.Domain.ValidationAttributes;

namespace Agenda.Domain.DomainObjects.LocationTypes
{
    /// <summary>
    /// Location Type.
    /// </summary>
    /// <seealso cref="ILocationType" />
    public class LocationType : BaseDomainModel, ILocationType
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationType"/> class.
        /// </summary>
        /// <param name="id">Location Type Id.</param>
        /// <param name="code">Location Type Code.</param>
        /// <param name="name">Location Type Name.</param>
        /// <param name="description">Location Type Description.</param>
        public LocationType(
            Guid id,
            string code,
            string name,
            string description)
        {
            this.Id = id;
            this.Code = code;
            this.Name = name;
            this.Description = description;

            Validate(this);
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets the Location Type Id.
        /// </summary>
        [ValidId]
        public Guid Id { get; }

        /// <summary>
        /// Gets the Location Type Code.
        /// </summary>
        [Required]
        [StringLength(
            maximumLength: DomainMetadata.Code.MaxLength,
            MinimumLength = DomainMetadata.Code.MinLength)]
        public string Code { get; }

        /// <summary>
        /// Gets the Location Type Name.
        /// </summary>
        [StringLength(
            maximumLength: DomainMetadata.Name.MaxLength,
            MinimumLength = DomainMetadata.Name.MinLength)]
        public string Name { get; }

        /// <summary>
        /// Gets the Location Type Description.
        /// </summary>
        [StringLength(
            maximumLength: DomainMetadata.Description.MaxLength,
            MinimumLength = DomainMetadata.Description.MinLength)]
        public string Description { get; }

        #endregion Public Properties
    }
}