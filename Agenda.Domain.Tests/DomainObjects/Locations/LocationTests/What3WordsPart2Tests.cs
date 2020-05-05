// <copyright file="What3WordsPart2Tests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Domain.DomainObjects.Locations;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Domain.Tests.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Domain.Tests.DomainObjects.Locations.LocationTests
{
    /// <summary>
    /// Tests for <see cref="Location.What3WordsParts"/>() method.
    /// </summary>
    [TestClass]
    public class What3WordsPart2Tests
    {
        /// <summary>
        /// Tests the method with various values.
        /// </summary>
        /// <param name="what3WordsAddress">What3 words address to test.</param>
        /// <param name="expectedPart1">Expected part1.</param>
        /// <param name="expectedPart2">Expected part2.</param>
        /// <param name="expectedPart3">Expected part3.</param>
        [TestMethod]
        [DataRow("deputy.chief.grass", "deputy", "chief", "grass", DisplayName = "Valid values")]
        [DataRow("deputy", "deputy", "", "", DisplayName = "One part")]
        [DataRow("deputy.chief", "deputy", "chief", "", DisplayName = "Two parts")]
        [DataRow("deputy.chief.grass.something", "deputy", "chief", "grass", DisplayName = "Four parts")]
        [DataRow(null, "", "", "", DisplayName = "Null")]
        [DataRow("", "", "", "", DisplayName = "Empty string")]
        public void Test_With_Various_Values(
            string what3WordsAddress,
            string expectedPart1,
            string expectedPart2,
            string expectedPart3)
        {
            // ARRANGE
            ILocation location = new Location(
                id: Guid.NewGuid(),
                organisation: new Organisation(
                    id: Guid.NewGuid(),
                    code: "Code",
                    name: "Name",
                    bgColour: "000000"),
                name: "Location",
                address: "Address",
                what3Words: "one.two.three",
                latitude: 50,
                longitude: -1);

            location.SetPrivatePropertyValue(
                nameof(ILocation.What3Words),
                what3WordsAddress);

            Assert.AreEqual(what3WordsAddress, location.What3Words, "Initial value not set");

            // ACT
            string[] what3WordsParts = location.What3WordsParts();

            // ASSERT
            Assert.IsNotNull(what3WordsParts);
            Assert.AreEqual(3, what3WordsParts.Length);
            Assert.AreEqual(what3WordsParts[0], expectedPart1);
            Assert.AreEqual(what3WordsParts[1], expectedPart2);
            Assert.AreEqual(what3WordsParts[2], expectedPart3);
        }
    }
}