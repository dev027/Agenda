// <copyright file="ToDomainWithCommitteesTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Agenda.Data.Dtos;
using Agenda.Data.Tests.TestUtilities;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.DomainObjects.Organisations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Data.Tests.Dtos.OrganisationTests
{
    /// <summary>
    /// Tests <see cref="OrganisationDto.ToDomainWithCommittees"/>.
    /// </summary>
    [TestClass]
    public class ToDomainWithCommitteesTests
    {
        /// <summary>
        /// Tests with valid list of committees.
        /// </summary>
        [TestMethod]
        public void Test_With_Valid_List_Of_Committees()
        {
            // ARRANGE
            Guid paramId = Guid.NewGuid();
            const string paramCode = "CBC";
            const string paramName = "County Bridge Club";
            const string paramBgColour = "000000";

            OrganisationDto organisationDto = new OrganisationDto(
                id: paramId,
                code: paramCode,
                name: paramName,
                bgColour: paramBgColour);

            IList<CommitteeDto> paramCommittees = new List<CommitteeDto>
            {
                new CommitteeDto(
                    id: Guid.NewGuid(),
                    organisationId: organisationDto.Id,
                    name: "TSC",
                    description: "Tournament Sub-Committee",
                    organisation: organisationDto)
            };

            organisationDto.SetPrivatePropertyValue(
                propName: nameof(OrganisationDto.Committees),
                value: paramCommittees);

            // ACT
            IOrganisationWithCommittees organisationWithCommittees =
                organisationDto.ToDomainWithCommittees();

            // ASSERT
            Assert.AreEqual(paramId, organisationWithCommittees.Id);
            Assert.AreEqual(paramCode, organisationWithCommittees.Code);
            Assert.AreEqual(paramName, organisationWithCommittees.Name);
            Assert.AreEqual(paramBgColour, organisationWithCommittees.BgColour);
            Assert.IsNotNull(organisationWithCommittees.Committees);
            Assert.AreEqual(1, organisationWithCommittees.Committees.Count);

            ICommittee committee = organisationWithCommittees.Committees[0];
            Assert.IsNotNull(committee);
            Assert.IsNotNull(committee.Organisation);
            Assert.AreEqual(paramId, committee.Organisation.Id);
        }

        /// <summary>
        /// Tests that null list of committees throws exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Test_That_Null_List_Of_Committees_Throws_Exception()
        {
            // ARRANGE
            Guid paramId = Guid.NewGuid();
            const string paramCode = "CBC";
            const string paramName = "County Bridge Club";
            const string paramBgColour = "000000";

            OrganisationDto organisationDto = new OrganisationDto(
                id: paramId,
                code: paramCode,
                name: paramName,
                bgColour: paramBgColour);

            // ACT
            _ = organisationDto.ToDomainWithCommittees();
        }
    }
}