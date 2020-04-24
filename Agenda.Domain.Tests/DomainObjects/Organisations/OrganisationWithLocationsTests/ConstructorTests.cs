// <copyright file="ConstructorTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Agenda.Domain.DomainObjects.Locations;
using Agenda.Domain.DomainObjects.Organisations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Domain.Tests.DomainObjects.Organisations.OrganisationWithLocationsTests
{
    /// <summary>
    /// Test the constructor for <see cref="OrganisationWithLocations"/>.
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
            const string paramCode = "CBC";
            const string paramName = "County Bridge Club";
            IOrganisation organisation = new Organisation(
                id: paramId,
                code: paramCode,
                name: paramName);

            IList<ILocation> paramLocations = new List<ILocation>
            {
                new Location(
                    id: Guid.NewGuid(),
                    organisation: organisation,
                    name: "County Bridge Club",
                    address: "St. Oswald's Road, New Parks",
                    what3Words: "voice.crash.fleet",
                    latitude: 52.643583,
                    longitude: -1.181126)
            };

            // ACT
            IOrganisationWithLocations organisationWithLocations = new OrganisationWithLocations(
                id: paramId,
                code: paramCode,
                name: paramName,
                locations: paramLocations);

            // ASSERT
            Assert.AreEqual(paramId, organisationWithLocations.Id);
            Assert.AreEqual(paramCode, organisationWithLocations.Code);
            Assert.AreEqual(paramName, organisationWithLocations.Name);
            Assert.AreSame(paramLocations, organisationWithLocations.Locations);
        }

        /// <summary>
        /// Tests the constructor null locations throws exception.
        /// </summary>
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Test_Constructor_Null_Locations_Throws_Exception()
        {
            // ARRANGE
            Guid paramId = Guid.NewGuid();
            const string paramCode = "CBC";
            const string paramName = "County Bridge Club";

            // ACT
            _ = new OrganisationWithLocations(
                id: paramId,
                code: paramCode,
                name: paramName,
                locations: null);

            // ASSERT
            Assert.Fail();
        }
    }
}