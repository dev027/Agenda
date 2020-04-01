// <copyright file="OrganisationDto.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Agenda.Data.DbContexts;
using Agenda.Domain.DomainObjects.Organisations;

namespace Agenda.Data.Dtos
{
    /// <summary>
    /// Organiser DTO.
    /// </summary>
    [Table(nameof(DataContext.Organisations))]
    public class OrganisationDto
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
        public OrganisationDto(
            Guid id,
            string code,
            string name)
        {
            this.Id = id;
            this.Code = code;
            this.Name = name;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the Organisation Id.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the Organisation Code.
        /// </summary>
        [Required]
        public string Code { get; private set; } = null!;

        /// <summary>
        /// Gets the Organisation Name.
        /// </summary>
        [Required]
        public string Name { get; private set; } = null!;

        #endregion Properties

        #region Public Properties

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
                name: organisation.Name);
        }

        /// <summary>
        /// Converts instance to domain object.
        /// </summary>
        /// <returns>Organiser.</returns>
        public IOrganisation ToDomain()
        {
            return new Organisation(
                this.Id,
                this.Code,
                this.Name);
        }

        #endregion
    }
}