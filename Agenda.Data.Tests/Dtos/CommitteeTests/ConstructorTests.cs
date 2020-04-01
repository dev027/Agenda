// <copyright file="ConstructorTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Data.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Data.Tests.Dtos.CommitteeTests
{
    /// <summary>
    /// Test constructor for <see cref="CommitteeDto" />.
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
            CommitteeDto dto = new CommitteeDto();

            // ASSERT
            Assert.AreEqual(Guid.Empty, dto.Id);
            Assert.AreEqual(Guid.Empty, dto.OrganisationId);
            Assert.AreEqual(null, dto.Name);
            Assert.AreEqual(null, dto.Description);
        }

        /// <summary>
        /// Tests the basic constructor.
        /// </summary>
        [TestMethod]
        public void Test_Basic_Constructor()
        {
            // ARRANGE
            Guid paramId = Guid.NewGuid();
            Guid paramOrganisationId = Guid.NewGuid();
            const string paramName = "TSC";
            const string paramDescription = "Tournament Sub-Committee";

            // ACT
            CommitteeDto dto = new CommitteeDto(
                id: paramId,
                organisationId: paramOrganisationId,
                name: paramName,
                description: paramDescription);

            // ASSERT
            Assert.AreEqual(paramId, dto.Id);
            Assert.AreEqual(paramOrganisationId, dto.OrganisationId);
            Assert.AreEqual(paramName, dto.Name);
            Assert.AreEqual(paramDescription, dto.Description);
        }
    }
}