// <copyright file="ConstructorTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Data.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Data.Tests.Dtos.OrganisationTests
{
    /// <summary>
    /// Test constructor for <see cref="OrganisationDto" />.
    /// </summary>
    [TestClass]
    public class ConstructorTests
    {
        /// <summary>
        /// Tests the default constructor.
        /// </summary>
        [TestMethod]
        public void Test_Default_Constructor()
        {
            // ACT
            OrganisationDto organisationDto = new OrganisationDto();

            // ASSERT
            Assert.AreEqual(Guid.Empty, organisationDto.Id);
            Assert.AreEqual(null, organisationDto.Code);
            Assert.AreEqual(null, organisationDto.Name);
        }

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

            // ACT
            OrganisationDto dto = new OrganisationDto(
                id: paramId,
                code: paramCode,
                name: paramName);

            // ASSERT
            Assert.AreEqual(paramId, dto.Id);
            Assert.AreEqual(paramCode, dto.Code);
            Assert.AreEqual(paramName, dto.Name);
        }
    }
}