// <copyright file="ToDtoTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Data.Dtos;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.DomainObjects.Organisations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Data.Tests.Dtos.CommitteeTests
{
    /// <summary>
    /// Test <see cref="CommitteeDto.ToDto"/>.
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
            ICommittee committee = new Committee(
                id: Guid.NewGuid(),
                organisation: new Organisation(
                    id: Guid.NewGuid(),
                    code: "CBC",
                    name: "County Bridge Club",
                    bgColour: "000000"),
                name: "TSC",
                description: "Tournament Sub-Committee");

            // ACT
            CommitteeDto committeeDto = CommitteeDto.ToDto(committee);

            // ASSERT
            Assert.AreEqual(committee.Id, committeeDto.Id);
            Assert.AreEqual(committee.Organisation.Id, committeeDto.OrganisationId);
            Assert.AreEqual(committee.Name, committeeDto.Name);
            Assert.AreEqual(committee.Description, committeeDto.Description);
        }

        /// <summary>
        /// Tests passing null Committee throws exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_Passing_Null_Committee_Throws_Exception()
        {
            // ACT
            _ = CommitteeDto.ToDto(null!);
        }
    }
}