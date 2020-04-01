// <copyright file="OrganisationDto.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Domain.DomainObjects.Organisations;

namespace Agenda.Web.Areas.Api.Models.Organisations.Out
{
    /// <summary>
    /// Organisation DTO.
    /// </summary>
    public class OrganisationDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrganisationDto"/> class.
        /// </summary>
        /// <param name="organisation">The organisation.</param>
        public OrganisationDto(IOrganisation organisation)
        {
            if (organisation == null)
            {
                throw new ArgumentNullException(nameof(organisation));
            }

            this.Id = organisation.Id;
            this.Code = organisation.Code;
            this.Name = organisation.Name;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; }

        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }
    }
}
