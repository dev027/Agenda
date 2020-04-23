// <copyright file="ConstructorTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Data.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Data.Tests.Dtos.LocationTests
{
    /// <summary>
    /// Test constructor for <see cref="CommitteeDto" />.
    /// </summary>
    [TestClass]
    public class ConstructorTests
    {
        /// <summary>
        /// Tests the default constructor.
        /// </summary>
        [TestMethod]
        public void Test_Default_Constructor()
        {
            // ACT
            LocationDto dto = new LocationDto();

            // ASSERT
            Assert.AreEqual(Guid.Empty, dto.Id);
            Assert.AreEqual(Guid.Empty, dto.OrganisationId);
            Assert.AreEqual(null, dto.Name);
            Assert.AreEqual(null, dto.Address);
            Assert.AreEqual(null, dto.What3Words);
            Assert.AreEqual(0, dto.Latitude);
            Assert.AreEqual(0, dto.Longitude);
        }

        /// <summary>
        /// Tests the basic constructor.
        /// </summary>
        [TestMethod]
        public void Test_Basic_Constructor()
        {
            // ARRANGE
            Guid paramId = Guid.NewGuid();
            Guid paramOrganisationId = Guid.NewGuid();
            const string paramName = "County Bridge Club";
            const string paramAddress = "St. Oswald's Road, New Parks";
            const string paramWhat3Words = "voice.crash.fleet";
            const double paramLatitude = 52.643583;
            const double paramLongitude = -1.181126;

            // ACT
            LocationDto dto = new LocationDto(
                id: paramId,
                organisationId: paramOrganisationId,
                name: paramName,
                address: paramAddress,
                what3Words: paramWhat3Words,
                latitude: paramLatitude,
                longitude: paramLongitude);

            // ASSERT
            Assert.AreEqual(paramId, dto.Id);
            Assert.AreEqual(paramOrganisationId, dto.OrganisationId);
            Assert.AreEqual(paramName, dto.Name);
            Assert.AreEqual(paramAddress, dto.Address);
            Assert.AreEqual(paramWhat3Words, dto.What3Words);
            Assert.AreEqual(paramLatitude, dto.Latitude);
            Assert.AreEqual(paramLongitude, dto.Longitude);
        }
    }
}