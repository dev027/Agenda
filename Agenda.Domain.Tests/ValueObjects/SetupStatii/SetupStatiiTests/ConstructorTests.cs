// <copyright file="ConstructorTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Domain.ValueObjects.SetupStatii;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Domain.Tests.ValueObjects.SetupStatii.SetupStatiiTests
{
    /// <summary>
    /// Test the constructor for <see cref="SetupStatii"/>.
    /// </summary>
    [TestClass]
    public class ConstructorTests
    {
        /// <summary>
        /// Tests the basic constructor with valid values.
        /// </summary>
        /// <param name="paramHaveOrganisations">if set to <c>true</c> [have organisations].</param>
        /// <param name="paramHaveCommittees">if set to <c>true</c> [have committees].</param>
        [TestMethod]
        [DataRow(true, true, DisplayName = "Organisations and Committees")]
        [DataRow(true, false, DisplayName = "Organisations but no Committees")]
        [DataRow(false, false, DisplayName = "No Organisations or Committees")]
        public void Test_Basic_Constructor_With_Valid_Values(
            bool paramHaveOrganisations,
            bool paramHaveCommittees)
        {
            // ACT
            ISetupStatus setupStatus = new SetupStatus(
                haveOrganisations: paramHaveOrganisations,
                haveCommittees: paramHaveCommittees);

            // ASSERT
            Assert.IsNotNull(setupStatus);
            Assert.AreEqual(paramHaveOrganisations, setupStatus.HaveOrganisations);
            Assert.AreEqual(paramHaveCommittees, setupStatus.HaveCommittees);
        }

        /// <summary>
        /// Tests the basic constructor with invalid values throw exception.
        /// </summary>
        /// <param name="paramHaveOrganisations">if set to <c>true</c> [have organisations].</param>
        /// <param name="paramHaveCommittees">if set to <c>true</c> [have committees].</param>
        [TestMethod]
        [DataRow(false, true, DisplayName = "Committees without Organisations")]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Basic_Constructor_With_Invalid_Values_Throws_Exception(
            bool paramHaveOrganisations,
            bool paramHaveCommittees)
        {
            // ACT
            _ = new SetupStatus(
                haveOrganisations: paramHaveOrganisations,
                haveCommittees: paramHaveCommittees);

            // ASSERT
            Assert.Fail();
        }
    }
}