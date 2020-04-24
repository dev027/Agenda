// <copyright file="IOrganisationWithLocations.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Agenda.Domain.DomainObjects.Locations;

namespace Agenda.Domain.DomainObjects.Organisations
{
    /// <summary>
    /// Organisation with Locations.
    /// </summary>
    /// <seealso cref="IOrganisation" />
    public interface IOrganisationWithLocations : IOrganisation
    {
        /// <summary>
        /// Gets the Locations.
        /// </summary>
        IList<ILocation> Locations { get; }
    }
}