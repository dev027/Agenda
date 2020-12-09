// <copyright file="ConstructorTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.DomainObjects.Organisations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Domain.Tests.DomainObjects.Committees.CommitteeTests
{
    /// <summary>
    /// Test the constructor for <see cref="Committee"/>.
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
            const string paramName = "TSC";
            const string paramDescription = "Tournament Sub-Committee";

            // ACT
            ICommittee committee = new Committee(
                id: paramId,
                organisation: paramOrganisation,
                name: paramName,
                description: paramDescription);

            // ASSERT
            Assert.AreEqual(paramId, committee.Id);
            Assert.AreSame(paramOrganisation, committee.Organisation);
            Assert.AreEqual(paramName, committee.Name);
            Assert.AreEqual(paramDescription, committee.Description);
        }

        /// <summary>
        /// Tests the constructor empty name throws exception.
        /// </summary>
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Test_Constructor_Empty_Name_Throws_Exception()
        {
            // ARRANGE
            Guid paramId = Guid.NewGuid();
            IOrganisation paramOrganisation = new Organisation(
                id: Guid.NewGuid(),
                code: "CBC",
                name: "County Bridge Club",
                bgColour: "000000");
            const string paramDescription = "Tournament Sub-Committee";

            // ACT
            _ = new Committee(
                id: paramId,
                organisation: paramOrganisation,
                name: string.Empty,
                description: paramDescription);
        }

        /// <summary>
        /// Tests the constructor empty description throws exception.
        /// </summary>
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Test_Constructor_Empty_Description_Throws_Exception()
        {
            // ARRANGE
            Guid paramId = Guid.NewGuid();
            IOrganisation paramOrganisation = new Organisation(
                id: Guid.NewGuid(),
                code: "CBC",
                name: "County Bridge Club",
                bgColour: "000000");
            const string paramName = "TSC";

            // ACT
            _ = new Committee(
                id: paramId,
                organisation: paramOrganisation,
                name: paramName,
                description: string.Empty);
        }
    }
}