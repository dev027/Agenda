// <copyright file="Location.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Globalization;
using System.Linq;
using Agenda.Domain.DomainObjects.Organisations;

namespace Agenda.Domain.DomainObjects.Locations
{
    /// <summary>
    /// Location.
    /// </summary>
    /// <seealso cref="ILocation" />
    public class Location : ILocation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="id">Location Id.</param>
        /// <param name="organisation">Organisation.</param>
        /// <param name="name">Location Name.</param>
        /// <param name="address">Address.</param>
        /// <param name="what3Words">What3Words Address.</param>
        /// <param name="latitude">Latitude.</param>
        /// <param name="longitude">Longitude.</param>
        public Location(
            Guid id,
            IOrganisation organisation,
            string name,
            string address,
            string what3Words,
            double latitude,
            double longitude)
        {
            this.Id = id;
            this.Organisation = organisation ?? throw new ArgumentNullException(nameof(organisation));
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Address = address ?? throw new ArgumentNullException(nameof(address));
            this.What3Words = what3Words ?? throw new ArgumentNullException(nameof(what3Words));
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        /// <inheritdoc/>
        public Guid Id { get; }

        /// <inheritdoc/>
        public IOrganisation Organisation { get; }

        /// <inheritdoc/>
        public string Name { get; }

        /// <inheritdoc/>
        public string Address { get; }

        /// <inheritdoc/>
        public string What3Words { get; }

        /// <inheritdoc/>
        public double Latitude { get; }

        /// <inheritdoc/>
        public double Longitude { get; }

        /// <summary>
        /// Joins the three parts of a What3Words address.
        /// </summary>
        /// <param name="what3WordsPart1">What3Words Address Part 1.</param>
        /// <param name="what3WordsPart2">What3Words Address Part 2.</param>
        /// <param name="what3WordsPart3">What3Words Address Part 3.</param>
        /// <returns>What3Words address.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Globalization",
            "CA1308:Normalize strings to uppercase",
            Justification = "What3Words needs to be lowercase")]
        public static string What3WordsJoin(
            string what3WordsPart1,
            string what3WordsPart2,
            string what3WordsPart3)
        {
            return string.Join(
                    DomainMetadata.What3Words.Separator,
                    what3WordsPart1,
                    what3WordsPart2,
                    what3WordsPart3)
                .ToLower(CultureInfo.InvariantCulture);
        }

        /// <inheritdoc />
        public string[] What3WordsParts()
        {
            return this.What3Words.Split(
                DomainMetadata.What3Words.Separator
                    .ToCharArray()
                    .First());
        }
    }
}