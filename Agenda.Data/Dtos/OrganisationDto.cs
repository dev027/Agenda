﻿// <copyright file="OrganisationDto.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using Agenda.Data.Attributes;
using Agenda.Data.DbContexts;
using Agenda.Data.Resources;
using Agenda.Domain.DomainObjects.Organisations;

namespace Agenda.Data.Dtos
{
    /// <summary>
    /// Organiser DTO.
    /// </summary>
    [Table(nameof(DataContext.Organisations))]
    public class OrganisationDto : BaseDto
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganisationDto"/> class.
        /// </summary>
        public OrganisationDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganisationDto"/> class.
        /// </summary>
        /// <param name="id">Organisation Id.</param>
        /// <param name="code">Organisation Code.</param>
        /// <param name="name">Organisation Name.</param>
        /// <param name="bgColour">Background Colour.</param>
        public OrganisationDto(
            Guid id,
            string code,
            string name,
            string bgColour)
        {
            this.Id = id;
            this.Code = code;
            this.Name = name;
            this.BgColour = bgColour;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the Organisation Id.
        /// </summary>
        [AuditIgnore]
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the Organisation Code.
        /// </summary>
        [Required]
        [MaxLength(DomainMetadata.Code.MaxLength)]
        public string Code { get; private set; } = null!;

        /// <summary>
        /// Gets the Organisation Name.
        /// </summary>
        [Required]
        [MaxLength(DomainMetadata.Name.MaxLength)]
        public string Name { get; private set; } = null!;

        /// <summary>
        /// Gets the Background Colour.
        /// </summary>
        [Required]
        [MaxLength(DomainMetadata.Name.MaxLength)]
        [Description("Background Colour")]
        public string BgColour { get; private set; } = null!;

        #endregion Properties

        #region Child Properties

        /// <summary>
        /// Gets the Committees.
        /// </summary>
        public IList<CommitteeDto> Committees { get; private set; } = null!;

        #endregion Child Properties

        #region Public Methods

        /// <summary>
        /// Converts domain object to DTO.
        /// </summary>
        /// <param name="organisation">Organisation.</param>
        /// <returns>Organisation DTO.</returns>
        public static OrganisationDto ToDto(IOrganisation organisation)
        {
            if (organisation == null)
            {
                throw new ArgumentNullException(nameof(organisation));
            }

            return new OrganisationDto(
                id: organisation.Id,
                code: organisation.Code,
                name: organisation.Name,
                bgColour: organisation.BgColour);
        }

        /// <summary>
        /// Converts instance to domain object.
        /// </summary>
        /// <returns>Organisation.</returns>
        public IOrganisation ToDomain()
        {
            return new Organisation(
                id: this.Id,
                code: this.Code,
                name: this.Name,
                bgColour: this.BgColour);
        }

        /// <summary>
        /// Converts to domain object with committees.
        /// </summary>
        /// <returns>Organisation with Committees.</returns>
        public IOrganisationWithCommittees ToDomainWithCommittees()
        {
            if (this.Committees == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ExceptionResource.CannotConvertTo___If___IsNull,
                        nameof(IOrganisationWithCommittees),
                        nameof(this.Committees)));
            }

            return new OrganisationWithCommittees(
                id: this.Id,
                code: this.Code,
                name: this.Name,
                bgColour: this.BgColour,
                committees: this.Committees.Select(c => c.ToDomain()).ToList());
        }

        #endregion Public Methods
    }
}