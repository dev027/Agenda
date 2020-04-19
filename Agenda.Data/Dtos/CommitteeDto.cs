// <copyright file="CommitteeDto.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using Agenda.Data.DbContexts;
using Agenda.Data.Resources;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.DomainObjects.Organisations;
using DomainMetadata=Agenda.Domain.DomainObjects.Committees.DomainMetadata;

namespace Agenda.Data.Dtos
{
    /// <summary>
    /// Organiser DTO.
    /// </summary>
    [Table(nameof(DataContext.Committees))]
    public class CommitteeDto
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CommitteeDto"/> class.
        /// </summary>
        public CommitteeDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommitteeDto"/> class.
        /// </summary>
        /// <param name="id">Committee Id.</param>
        /// <param name="organisationId">Organisation Id.</param>
        /// <param name="name">Committee Name.</param>
        /// <param name="description">Committee Description.</param>
        public CommitteeDto(
            Guid id,
            Guid organisationId,
            string name,
            string description)
        {
            this.Id = id;
            this.OrganisationId = organisationId;
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommitteeDto"/> class.
        /// </summary>
        /// <param name="id">Committee Id.</param>
        /// <param name="organisationId">Organisation Id.</param>
        /// <param name="name">Committee Name.</param>
        /// <param name="description">Committee Description.</param>
        /// <param name="organisation">Organisation.</param>
        public CommitteeDto(
            Guid id,
            Guid organisationId,
            string name,
            string description,
            OrganisationDto organisation)
        {
            this.Id = id;
            this.OrganisationId = organisationId;
            this.Name = name;
            this.Description = description;
            this.Organisation = organisation;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the Committee Id.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the Organisation Id.
        /// </summary>
        public Guid OrganisationId { get; private set; }

        /// <summary>
        /// Gets the Committee Name.
        /// </summary>
        [Required]
        [MaxLength(DomainMetadata.Name.MaxLength)]
        public string Name { get; private set; } = null!;

        /// <summary>
        /// Gets the Committee Description.
        /// </summary>
        [Required]
        [MaxLength(DomainMetadata.Description.MaxLength)]
        public string Description { get; private set; } = null!;

        #endregion Properties

        #region Parent Properties

        /// <summary>
        /// Gets the Organisation.
        /// </summary>
        [ForeignKey(nameof(OrganisationId))]
        public OrganisationDto? Organisation { get; private set; } = null!;

        #endregion Parent Properties

        #region Child Properties

        /// <summary>
        /// Gets the Meetings.
        /// </summary>
        public IList<MeetingDto> Meetings { get; private set; } = null!;

        #endregion Child Properties

        #region Public Properties

        /// <summary>
        /// Converts domain object to DTO.
        /// </summary>
        /// <param name="committee">Committee.</param>
        /// <returns>Committee DTO.</returns>
        public static CommitteeDto ToDto(ICommittee committee)
        {
            if (committee == null)
            {
                throw new ArgumentNullException(nameof(committee));
            }

            return new CommitteeDto(
                id: committee.Id,
                organisationId: committee.Organisation.Id,
                name: committee.Name,
                description: committee.Description);
        }

        /// <summary>
        /// Converts instance to domain object.
        /// </summary>
        /// <returns>Committee.</returns>
        public ICommittee ToDomain()
        {
            if (this.Organisation == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ExceptionResource.CannotConvertTo___If___IsNull,
                        nameof(IOrganisation),
                        nameof(this.Organisation)));
            }

            return new Committee(
                id: this.Id,
                organisation: this.Organisation.ToDomain(),
                name: this.Name,
                description: this.Description);
        }

        /// <summary>
        /// Converts instance to domain object with meetings.
        /// </summary>
        /// <returns>Committee with Meetings.</returns>
        public ICommitteeWithMeetings ToDomainWithMeetings()
        {
            if (this.Organisation == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ExceptionResource.CannotConvertTo___If___IsNull,
                        nameof(ICommittee),
                        nameof(this.Organisation)));
            }

            if (this.Meetings == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ExceptionResource.CannotConvertTo___If___IsNull,
                        nameof(ICommittee),
                        nameof(this.Meetings)));
            }

            return new CommitteeWithMeetings(
                id: this.Id,
                organisation: this.Organisation.ToDomain(),
                name: this.Name,
                description: this.Description,
                meetings: this.Meetings.Select(m => m.ToDomain()).ToList());
        }

        #endregion Public Properties
    }
}