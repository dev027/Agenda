// <copyright file="ToDtoTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Data.Dtos;
using Agenda.Domain.DomainObjects.Organisations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Data.Tests.Dtos.OrganisationTests
{
    /// <summary>
    /// Test <see cref="OrganisationDto.ToDto"/>.
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
            IOrganisation organisation = new Organisation(
                id: Guid.NewGuid(),
                code: "CBC",
                name: "County Bridge Club");

            // ACT
            OrganisationDto organisationDto = OrganisationDto.ToDto(organisation);

            // ASSERT
            Assert.AreEqual(organisation.Id, organisationDto.Id);
            Assert.AreEqual(organisation.Code, organisationDto.Code);
            Assert.AreEqual(organisation.Name, organisationDto.Name);
        }

        /// <summary>
        /// Tests passing null Organisation throws exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_Passing_Null_Organisation_Throws_Exception()
        {
            // ACT
            _ = OrganisationDto.ToDto(null!);
        }
    }
}