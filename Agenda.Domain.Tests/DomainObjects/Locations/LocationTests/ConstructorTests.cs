// <copyright file="ConstructorTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Domain.DomainObjects.Locations;
using Agenda.Domain.DomainObjects.Organisations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Domain.Tests.DomainObjects.Locations.LocationTests
{
    /// <summary>
    /// Test the constructor for <see cref="Location"/>.
    /// </summary>
    [TestClass]
    public class ConstructorTests
    {
        /// <summary>
        /// Tests the basic constructor.
        /// </summary>
        [TestMethod]
        public void Test_Basic_Constructor()
        {
            // ARRANGE
            Guid paramId = Guid.NewGuid();
            IOrganisation paramOrganisation = new Organisation(
                id: Guid.NewGuid(),
                code: "CBC",
                name: "County Bridge Club",
                bgColour: "000000");
            const string paramName = "County Bridge Club";
            const string paramAddress = "St. Oswald's Road, New Parks";
            const string paramWhat3Words = "voice.crash.fleet";
            const double paramLatitude = 52.643583;
            const double paramLongitude = -1.181126;

            // ACT
            ILocation location = new Location(
                id: paramId,
                organisation: paramOrganisation,
                name: paramName,
                address: paramAddress,
                what3Words: paramWhat3Words,
                latitude: paramLatitude,
                longitude: paramLongitude);

            // ASSERT
            Assert.AreEqual(paramId, location.Id);
            Assert.AreSame(paramOrganisation, location.Organisation);
            Assert.AreEqual(paramName, location.Name);
            Assert.AreEqual(paramAddress, location.Address);
            Assert.AreEqual(paramWhat3Words, location.What3Words);
            Assert.AreEqual(paramLatitude, location.Latitude);
            Assert.AreEqual(paramLongitude, location.Longitude);
        }

        /// <summary>
        /// Tests the constructor null organisation throws exception.
        /// </summary>
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Test_Constructor_Null_Organisation_Throws_Exception()
        {
            // ARRANGE
            Guid paramId = Guid.NewGuid();
            const string paramName = "County Bridge Club";
            const string paramAddress = "St. Oswald's Road, New Parks";
            const string paramWhat3Words = "voice.crash.fleet";
            const double paramLatitude = 52.643583;
            const double paramLongitude = -1.181126;

            // ACT
            _ = new Location(
                id: paramId,
                organisation: null,
                name: paramName,
                address: paramAddress,
                what3Words: paramWhat3Words,
                latitude: paramLatitude,
                longitude: paramLongitude);
        }

        /// <summary>
        /// Tests the constructor null name throws exception.
        /// </summary>
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Test_Constructor_Null_Name_Throws_Exception()
        {
            // ARRANGE
            Guid paramId = Guid.NewGuid();
            IOrganisation paramOrganisation = new Organisation(
                id: Guid.NewGuid(),
                code: "CBC",
                name: "County Bridge Club",
                bgColour: "000000");
            const string paramAddress = "St. Oswald's Road, New Parks";
            const string paramWhat3Words = "voice.crash.fleet";
            const double paramLatitude = 52.643583;
            const double paramLongitude = -1.181126;

            // ACT
            _ = new Location(
                id: paramId,
                organisation: paramOrganisation,
                name: null,
                address: paramAddress,
                what3Words: paramWhat3Words,
                latitude: paramLatitude,
                longitude: paramLongitude);
        }

        /// <summary>
        /// Tests the constructor null address throws exception.
        /// </summary>
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Test_Constructor_Null_Address_Throws_Exception()
        {
            // ARRANGE
            Guid paramId = Guid.NewGuid();
            IOrganisation paramOrganisation = new Organisation(
                id: Guid.NewGuid(),
                code: "CBC",
                name: "County Bridge Club",
                bgColour: "000000");
            const string paramName = "County Bridge Club";
            const string paramWhat3Words = "voice.crash.fleet";
            const double paramLatitude = 52.643583;
            const double paramLongitude = -1.181126;

            // ACT
            _ = new Location(
                id: paramId,
                organisation: paramOrganisation,
                name: paramName,
                address: null,
                what3Words: paramWhat3Words,
                latitude: paramLatitude,
                longitude: paramLongitude);
        }

        /// <summary>
        /// Tests the constructor null What3Words address throws exception.
        /// </summary>
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Test_Constructor_Null_What3Words_Address_Throws_Exception()
        {
            // ARRANGE
            Guid paramId = Guid.NewGuid();
            IOrganisation paramOrganisation = new Organisation(
                id: Guid.NewGuid(),
                code: "CBC",
                name: "County Bridge Club",
                bgColour: "000000");
            const string paramName = "County Bridge Club";
            const string paramAddress = "St. Oswald's Road, New Parks";
            const double paramLatitude = 52.643583;
            const double paramLongitude = -1.181126;

            // ACT
            _ = new Location(
                id: paramId,
                organisation: paramOrganisation,
                name: paramName,
                address: paramAddress,
                what3Words: null,
                latitude: paramLatitude,
                longitude: paramLongitude);
        }
    }
}