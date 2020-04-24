// <copyright file="ToDomainWithLocationsTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Agenda.Data.Dtos;
using Agenda.Data.Tests.TestUtilities;
using Agenda.Domain.DomainObjects.Locations;
using Agenda.Domain.DomainObjects.Organisations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Data.Tests.Dtos.OrganisationTests
{
    /// <summary>
    /// Tests <see cref="OrganisationDto.ToDomainWithLocations"/>.
    /// </summary>
    [TestClass]
    public class ToDomainWithLocationsTests
    {
        /// <summary>
        /// Tests with valid list of locations.
        /// </summary>
        [TestMethod]
        public void Test_With_Valid_List_Of_Locations()
        {
            // ARRANGE
            Guid paramId = Guid.NewGuid();
            const string paramCode = "CBC";
            const string paramName = "County Bridge Club";

            OrganisationDto organisationDto = new OrganisationDto(
                id: paramId,
                code: paramCode,
                name: paramName);

            IList<LocationDto> paramLocations = new List<LocationDto>
            {
                new LocationDto(
                    id: Guid.NewGuid(),
                    organisationId: organisationDto.Id,
                    name: "County Bridge Club",
                    address: "St. Oswald's Road, New Parks",
                    what3Words: "voice.crash.fleet",
                    latitude: 52.643583,
                    longitude: -1.181126,
                    organisation: organisationDto)
            };

            organisationDto.SetPrivatePropertyValue(
                propName: nameof(OrganisationDto.Locations),
                value: paramLocations);

            // ACT
            IOrganisationWithLocations organisationWithLocations =
                organisationDto.ToDomainWithLocations();

            // ASSERT
            Assert.AreEqual(paramId, organisationWithLocations.Id);
            Assert.AreEqual(paramCode, organisationWithLocations.Code);
            Assert.AreEqual(paramName, organisationWithLocations.Name);
            Assert.IsNotNull(organisationWithLocations.Locations);
            Assert.AreEqual(1, organisationWithLocations.Locations.Count);

            ILocation location = organisationWithLocations.Locations[0];
            Assert.IsNotNull(location);
            Assert.IsNotNull(location.Organisation);
            Assert.AreEqual(paramId, location.Organisation.Id);
        }

        /// <summary>
        /// Tests that null list of locations throws exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Test_That_Null_List_Of_Locations_Throws_Exception()
        {
            // ARRANGE
            Guid paramId = Guid.NewGuid();
            const string paramCode = "CBC";
            const string paramName = "County Bridge Club";

            OrganisationDto organisationDto = new OrganisationDto(
                id: paramId,
                code: paramCode,
                name: paramName);

            // ACT
            _ = organisationDto.ToDomainWithLocations();
        }
    }
}