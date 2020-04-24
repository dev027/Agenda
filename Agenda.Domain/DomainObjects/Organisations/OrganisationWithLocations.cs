// <copyright file="OrganisationWithLocations.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Agenda.Domain.DomainObjects.Locations;

namespace Agenda.Domain.DomainObjects.Organisations
{
    /// <summary>
    /// Organisation with Committees.
    /// </summary>
    /// <seealso cref="Organisation" />
    /// <seealso cref="IOrganisationWithLocations" />
    public class OrganisationWithLocations : Organisation, IOrganisationWithLocations
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrganisationWithLocations"/> class.
        /// </summary>
        /// <param name="id">Organisation Id.</param>
        /// <param name="code">Organisation Code.</param>
        /// <param name="name">Organisation Name.</param>
        /// <param name="locations">Locations.</param>
        public OrganisationWithLocations(
            Guid id,
            string code,
            string name,
            IList<ILocation> locations)
            : base(id, code, name)
        {
            this.Locations = locations ?? throw new ArgumentNullException(nameof(locations));
        }

        /// <summary>
        /// Gets the Committees.
        /// </summary>
        public IList<ILocation> Locations { get; }
    }
}