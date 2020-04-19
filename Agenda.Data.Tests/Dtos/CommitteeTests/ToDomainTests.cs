// <copyright file="ToDomainTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Data.Dtos;
using Agenda.Domain.DomainObjects.Committees;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Data.Tests.Dtos.CommitteeTests
{
    /// <summary>
    /// Test <see cref="CommitteeDto.ToDomain"/>.
    /// </summary>
    [TestClass]
    public class ToDomainTests
    {
        /// <summary>
        /// Tests passing valid values.
        /// </summary>
        [TestMethod]
        public void Test_Passing_Valid_Values()
        {
            // ARRANGE
            OrganisationDto organisationDto = new OrganisationDto(
                id: Guid.NewGuid(),
                code: "CBC",
                name: "County Bridge Club");
            CommitteeDto committeeDto = new CommitteeDto(
                id: Guid.NewGuid(),
                organisationId: organisationDto.Id,
                name: "TSC",
                description: "Tournament Sub-Committee",
                organisation: organisationDto);

            // ACT
            ICommittee committee = committeeDto.ToDomain();

            // ASSERT
            Assert.IsNotNull(committee);
            Assert.AreEqual(committeeDto.Id, committee.Id);
            Assert.IsNotNull(committee.Organisation);
            Assert.AreEqual(committeeDto.OrganisationId, committee.Organisation.Id);
            Assert.AreEqual(committeeDto.Name, committee.Name);
            Assert.AreEqual(committeeDto.Description, committee.Description);
        }

        /// <summary>
        /// Tests the with null organisation throws exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Test_WithNull_Organisation_Throws_Exception()
        {
            // ARRANGE
            CommitteeDto committeeDto = new CommitteeDto(
                id: Guid.NewGuid(),
                organisationId: Guid.NewGuid(),
                name: "TSC",
                description: "Tournament Sub-Committee",
                organisation: null!);

            // ACT
            _ = committeeDto.ToDomain();
        }
    }
}