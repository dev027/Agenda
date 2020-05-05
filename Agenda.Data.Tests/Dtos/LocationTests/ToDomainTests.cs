// <copyright file="ToDomainTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Data.Dtos;
using Agenda.Domain.DomainObjects.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Data.Tests.Dtos.LocationTests
{
    /// <summary>
    /// Test <see cref="LocationDto.ToDomain"/>.
    /// </summary>
    [TestClass]
    public class ToDomainTests
    {
        /// <summary>
        /// Tests passing valid values.
        /// </summary>
        [TestMethod]
        public void Test_Passing_Valid_Values()
        {
            // ARRANGE
            OrganisationDto organisationDto = new OrganisationDto(
                id: Guid.NewGuid(),
                code: "CBC",
                name: "County Bridge Club",
                bgColour: "000000");
            LocationDto locationDto = new LocationDto(
                id: Guid.NewGuid(),
                organisationId: organisationDto.Id,
                name: "County Bridge Club",
                address: "St. Oswald's Road, New Parks",
                what3Words: "voice.crash.fleet",
                latitude: 52.643583,
                longitude: -1.181126,
                organisation: organisationDto);

            // ACT
            ILocation location = locationDto.ToDomain();

            // ASSERT
            Assert.IsNotNull(location);
            Assert.AreEqual(locationDto.Id, location.Id);
            Assert.IsNotNull(location.Organisation);
            Assert.AreEqual(locationDto.OrganisationId, location.Organisation.Id);
            Assert.AreEqual(locationDto.Name, location.Name);
            Assert.AreEqual(locationDto.Address, location.Address);
            Assert.AreEqual(locationDto.What3Words, location.What3Words);
            Assert.AreEqual(locationDto.Latitude, location.Latitude);
            Assert.AreEqual(locationDto.Longitude, location.Longitude);
        }

        /// <summary>
        /// Tests the with null organisation throws exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Test_WithNull_Organisation_Throws_Exception()
        {
            // ARRANGE
            LocationDto locationDto = new LocationDto(
                id: Guid.NewGuid(),
                organisationId: Guid.NewGuid(),
                name: "County Bridge Club",
                address: "St. Oswald's Road, New Parks",
                what3Words: "voice.crash.fleet",
                latitude: 52.643583,
                longitude: -1.181126,
                organisation: null!);

            // ACT
            _ = locationDto.ToDomain();
        }
    }
}