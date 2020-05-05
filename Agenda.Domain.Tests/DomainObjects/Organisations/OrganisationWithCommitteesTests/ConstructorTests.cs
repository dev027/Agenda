// <copyright file="ConstructorTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.DomainObjects.Organisations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Domain.Tests.DomainObjects.Organisations.OrganisationWithCommitteesTests
{
    /// <summary>
    /// Test the constructor for <see cref="OrganisationWithCommittees"/>.
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
            const string paramBgColour = "000000";
            IOrganisation organisation = new Organisation(
                id: paramId,
                code: paramCode,
                name: paramName,
                bgColour: paramBgColour);

            IList<ICommittee> paramCommittees = new List<ICommittee>
            {
                new Committee(
                    Guid.NewGuid(),
                    organisation,
                    "TSC",
                    "Tournament Sub-Committee")
            };

            // ACT
            IOrganisationWithCommittees organisationWithCommittees = new OrganisationWithCommittees(
                id: paramId,
                code: paramCode,
                name: paramName,
                bgColour: paramBgColour,
                committees: paramCommittees);

            // ASSERT
            Assert.AreEqual(paramId, organisationWithCommittees.Id);
            Assert.AreEqual(paramCode, organisationWithCommittees.Code);
            Assert.AreEqual(paramName, organisationWithCommittees.Name);
            Assert.AreEqual(paramBgColour, organisationWithCommittees.BgColour);
            Assert.AreSame(paramCommittees, organisationWithCommittees.Committees);
        }

        /// <summary>
        /// Tests the constructor null committees throws exception.
        /// </summary>
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Test_Constructor_Null_Committees_Throws_Exception()
        {
            // ARRANGE
            Guid paramId = Guid.NewGuid();
            const string paramCode = "CBC";
            const string paramName = "County Bridge Club";
            const string paramBgColour = "000000";

            // ACT
            _ = new OrganisationWithCommittees(
                id: paramId,
                code: paramCode,
                name: paramName,
                bgColour: paramBgColour,
                committees: null);
        }
    }
}