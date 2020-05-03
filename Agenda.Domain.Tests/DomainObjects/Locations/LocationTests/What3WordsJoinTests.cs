// <copyright file="What3WordsJoinTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Domain.DomainObjects.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Domain.Tests.DomainObjects.Locations.LocationTests
{
    /// <summary>
    /// Tests for <see cref="Location.What3WordsJoin"/>() methods.
    /// </summary>
    [TestClass]
    public class What3WordsJoinTests
    {
        /// <summary>
        /// Tests the with valid values.
        /// </summary>
        [TestMethod]
        public void Test_With_Valid_Values()
        {
            // ARRANGE
            const string paramWhat3WordsPart1 = "deputy";
            const string paramWhat3WordsPart2 = "grass";
            const string paramWhat3WordsPart3 = "chief";

            // ACT
            string what3Words = Location.What3WordsJoin(
                paramWhat3WordsPart1,
                paramWhat3WordsPart2,
                paramWhat3WordsPart3);

            // ASSERT
            Assert.AreEqual("deputy.grass.chief", what3Words);
        }

        /// <summary>
        /// Tests that uppercase values are converted to lowercase.
        /// </summary>
        [TestMethod]
        public void Test_That_Uppercase_Values_Are_Converted_To_Lowercase()
        {
            // ARRANGE
            const string paramWhat3WordsPart1 = "DEPUTY";
            const string paramWhat3WordsPart2 = "GRASS";
            const string paramWhat3WordsPart3 = "CHIEF";

            // ACT
            string what3Words = Location.What3WordsJoin(
                paramWhat3WordsPart1,
                paramWhat3WordsPart2,
                paramWhat3WordsPart3);

            // ASSERT
            Assert.AreEqual("deputy.grass.chief", what3Words);
        }

        /// <summary>
        /// Tests the that when part1 is null it throws an exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_That_When_Part1_Is_Null_Throws_Exception()
        {
            // ARRANGE
            const string paramWhat3WordsPart2 = "grass";
            const string paramWhat3WordsPart3 = "chief";

            // ACT
            _ = Location.What3WordsJoin(
                null,
                paramWhat3WordsPart2,
                paramWhat3WordsPart3);
        }

        /// <summary>
        /// Tests the that when part2 is null throws exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_That_When_Part2_Is_Null_Throws_Exception()
        {
            // ARRANGE
            const string paramWhat3WordsPart1 = "deputy";
            const string paramWhat3WordsPart3 = "chief";

            // ACT
            _ = Location.What3WordsJoin(
                paramWhat3WordsPart1,
                null,
                paramWhat3WordsPart3);
        }

        /// <summary>
        /// Tests the that when part3 is null throws exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_That_When_Part3_Is_Null_Throws_Exception()
        {
            // ARRANGE
            const string paramWhat3WordsPart1 = "deputy";
            const string paramWhat3WordsPart2 = "grass";

            // ACT
            _ = Location.What3WordsJoin(
                paramWhat3WordsPart1,
                paramWhat3WordsPart2,
                null);
        }
    }
}