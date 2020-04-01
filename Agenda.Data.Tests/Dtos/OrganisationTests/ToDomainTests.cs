// <copyright file="ToDomainTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Data.Dtos;
using Agenda.Domain.DomainObjects.Organisations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Data.Tests.Dtos.OrganisationTests
{
    /// <summary>
    /// Test <see cref="OrganisationDto.ToDomain"/>.
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

            // ACT
            IOrganisation organisation = organisationDto.ToDomain();

            // ASSERT
            Assert.IsNotNull(organisation);
            Assert.AreEqual(organisationDto.Id, organisation.Id);
            Assert.AreEqual(organisationDto.Code, organisation.Code);
            Assert.AreEqual(organisationDto.Name, organisation.Name);
        }
    }
}