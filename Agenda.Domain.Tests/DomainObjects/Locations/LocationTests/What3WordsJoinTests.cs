// <copyright file="What3WordsJoinTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

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
    }
}