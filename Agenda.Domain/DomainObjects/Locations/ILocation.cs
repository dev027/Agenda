// <copyright file="ILocation.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Domain.DomainObjects.Organisations;

namespace Agenda.Domain.DomainObjects.Locations
{
    /// <summary>
    /// Organisation.
    /// </summary>
    public interface ILocation
    {
        /// <summary>
        /// Gets the Location Id.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the Organisation.
        /// </summary>
        IOrganisation Organisation { get; }

        /// <summary>
        /// Gets the Location Name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the Address.
        /// </summary>
        string Address { get; }

        /// <summary>
        /// Gets the What3Word address.
        /// </summary>
        string What3Words { get; }

        /// <summary>
        /// Gets the Latitude.
        /// </summary>
        double Latitude { get; }

        /// <summary>
        /// Gets the Longitude.
        /// </summary>
        double Longitude { get; }

        /// <summary>
        /// Split the What3Words address into parts.
        /// </summary>
        /// <returns>
        /// What3Word Address Parts.
        /// </returns>
        string[] What3WordsParts();
    }
}