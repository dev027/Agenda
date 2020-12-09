// <copyright file="Location.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Agenda.Domain.Constants;
using Agenda.Domain.DomainObjects.LocationTypes;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Domain.ValidationAttributes;

namespace Agenda.Domain.DomainObjects.Locations
{
    /// <summary>
    /// Location.
    /// </summary>
    /// <seealso cref="ILocation" />
    public class Location : BaseDomainModel, ILocation
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="id">Location Id.</param>
        /// <param name="organisation">Organisation.</param>
        /// <param name="locationType">Location Type.</param>
        /// <param name="name">Location Name.</param>
        /// <param name="address">Address.</param>
        /// <param name="what3Words">What3Words Address.</param>
        /// <param name="latitude">Latitude.</param>
        /// <param name="longitude">Longitude.</param>
        public Location(
            Guid id,
            IOrganisation organisation,
            ILocationType locationType,
            string name,
            string address,
            string what3Words,
            double? latitude,
            double? longitude)
        {
            this.Id = id;
            this.Organisation = organisation;
            this.LocationType = locationType;
            this.Name = name;
            this.Address = address;
            this.What3Words = what3Words;
            this.Latitude = latitude;
            this.Longitude = longitude;

            Validate(this, this.AdditionalValidation);
        }

        #endregion Constructors

        #region Public Properties

        /// <inheritdoc/>
        [ValidId]
        public Guid Id { get; }

        /// <inheritdoc/>
        public IOrganisation Organisation { get; }

        /// <inheritdoc />
        public ILocationType LocationType { get; }

        /// <inheritdoc/>
        [Required]
        [StringLength(
            maximumLength: DomainMetadata.Name.MaxLength,
            MinimumLength = DomainMetadata.Name.MinLength)]
        public string Name { get; }

        /// <inheritdoc/>
        [StringLength(
            maximumLength: DomainMetadata.Name.MaxLength,
            MinimumLength = DomainMetadata.Name.MinLength)]
        public string Address { get; }

        /// <inheritdoc/>
        [StringLength(
            maximumLength: DomainMetadata.What3Words.MaxLength,
            MinimumLength = DomainMetadata.What3Words.MinLength)]
        public string What3Words { get; private set; }

        /// <inheritdoc/>
        public double? Latitude { get; }

        /// <inheritdoc/>
        public double? Longitude { get; }

        #endregion Public Properties

        #region Public Methods

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
            if (what3WordsPart1 == null)
            {
                throw new ArgumentNullException(nameof(what3WordsPart1));
            }

            if (what3WordsPart2 == null)
            {
                throw new ArgumentNullException(nameof(what3WordsPart2));
            }

            if (what3WordsPart3 == null)
            {
                throw new ArgumentNullException(nameof(what3WordsPart3));
            }

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
            string[] what3WordsParts = this.What3Words.Split(
                DomainMetadata.What3Words.Separator
                    .ToCharArray()
                    .First());

            if (what3WordsParts.Length == 3)
            {
                return what3WordsParts;
            }

            if (what3WordsParts.Length > 3)
            {
                return what3WordsParts.Take(3).ToArray();
            }

            return what3WordsParts
                .AsEnumerable()
                .Append(string.Empty)
                .Append(string.Empty)
                .Append(string.Empty)
                .Take(3)
                .ToArray();
        }

        private IEnumerable<ValidationResult> AdditionalValidation()
        {
            return this.ValidateWhat3Words()
                .Concat(this.ValidateLatLong());
        }

        private IEnumerable<ValidationResult> ValidateWhat3Words()
        {
            if (this.LocationType.Code != LocationTypeCodes.RealWorld)
            {
                yield break;
            }

            string[] parts = this.What3WordsParts();

            for (int i = 0; i < 3; i++)
            {
                if (parts[i].Length >= DomainMetadata.What3Words.Part.MinLength &&
                    parts[i].Length <= DomainMetadata.What3Words.Part.MaxLength)
                {
                    continue;
                }

                yield return new ValidationResult(
                    $"{nameof(this.What3Words)} part {i} " +
                    "must be between " +
                    DomainMetadata.What3Words.Part.MinLength +
                    " and " +
                    DomainMetadata.What3Words.Part.MaxLength);
            }
        }

        private IEnumerable<ValidationResult> ValidateLatLong()
        {
            if (this.LocationType.Code != LocationTypeCodes.RealWorld)
            {
                yield break;
            }

            if (this.Latitude == null)
            {
                yield return new ValidationResult($"{nameof(this.Latitude)} is required");
            }
            else
            {
                if (this.Latitude <= DomainMetadata.Latitude.MinValue &&
                    this.Latitude >= DomainMetadata.Latitude.MaxValue)
                {
                    yield return new ValidationResult(
                        $"{nameof(this.Latitude)} " +
                        "must be between " +
                        DomainMetadata.Latitude.MinValue +
                        " and " +
                        DomainMetadata.Latitude.MaxValue);
                }
            }

            if (this.Longitude == null)
            {
                yield return new ValidationResult($"{nameof(this.Longitude)} is required");
            }
            else
            {
                if (this.Longitude <= DomainMetadata.Longitude.MinValue &&
                    this.Longitude >= DomainMetadata.Longitude.MaxValue)
                {
                    yield return new ValidationResult(
                        $"{nameof(this.Longitude)} " +
                        "must be between " +
                        DomainMetadata.Longitude.MinValue +
                        " and " +
                        DomainMetadata.Longitude.MaxValue);
                }
            }
        }

        #endregion Public Methods
    }
}