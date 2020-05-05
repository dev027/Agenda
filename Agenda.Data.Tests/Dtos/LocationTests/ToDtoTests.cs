// <copyright file="ToDtoTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Data.Dtos;
using Agenda.Domain.DomainObjects.Locations;
using Agenda.Domain.DomainObjects.Organisations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Data.Tests.Dtos.LocationTests
{
    /// <summary>
    /// Test <see cref="LocationDto.ToDto"/>.
    /// </summary>
    [TestClass]
    public class ToDtoTests
    {
        /// <summary>
        /// Tests method with valid values.
        /// </summary>
        [TestMethod]
        public void Test_Passing_Valid_Values()
        {
            // ARRANGE
            ILocation location = new Location(
                    id: Guid.NewGuid(),
                    organisation: new Organisation(
                        id: Guid.NewGuid(),
                        code: "CBC",
                        name: "County Bridge Club",
                        bgColour: "000000"),
                    name: "County Bridge Club",
                    address: "St. Oswald's Road, New Parks",
                    what3Words: "voice.crash.fleet",
                    latitude: 52.643583,
                    longitude: -1.181126);

            // ACT
            LocationDto locationDto = LocationDto.ToDto(location);

            // ASSERT
            Assert.AreEqual(location.Id, locationDto.Id);
            Assert.AreEqual(location.Organisation.Id, locationDto.OrganisationId);
            Assert.AreEqual(location.Name, locationDto.Name);
            Assert.AreEqual(location.Address, locationDto.Address);
            Assert.AreEqual(location.What3Words, locationDto.What3Words);
            Assert.AreEqual(location.Latitude, locationDto.Latitude);
            Assert.AreEqual(location.Longitude, locationDto.Longitude);
        }

        /// <summary>
        /// Tests passing null Location throws exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_Passing_Null_Committee_Throws_Exception()
        {
            // ACT
            _ = LocationDto.ToDto(null!);
        }
    }
}