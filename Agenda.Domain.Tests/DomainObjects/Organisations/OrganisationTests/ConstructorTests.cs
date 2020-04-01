// <copyright file="ConstructorTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Domain.DomainObjects.Organisations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Domain.Tests.DomainObjects.Organisations.OrganisationTests
{
    /// <summary>
    /// Test the constructor for <see cref="Organisation"/>.
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

            // ACT
            IOrganisation organisation = new Organisation(
                id: paramId,
                code: paramCode,
                name: paramName);

            // ASSERT
            Assert.AreEqual(paramId, organisation.Id);
            Assert.AreEqual(paramCode, organisation.Code);
            Assert.AreEqual(paramName, organisation.Name);
        }

        /// <summary>
        /// Tests the constructor null code throws exception.
        /// </summary>
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Test_Constructor_Null_Code_Throws_Exception()
        {
            // ARRANGE
            Guid paramId = Guid.NewGuid();
            const string paramName = "County Bridge Club";

            // ACT
            _ = new Organisation(
                id: paramId,
                code: null,
                name: paramName);

            // ASSERT
            Assert.Fail();
        }

        /// <summary>
        /// Tests the constructor null name throws exception.
        /// </summary>
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Test_Constructor_Null_Name_Throws_Exception()
        {
            // ARRANGE
            Guid paramId = Guid.NewGuid();
            const string paramCode = "CBC";

            // ACT
            _ = new Organisation(
                id: paramId,
                code: paramCode,
                name: null);

            // ASSERT
            Assert.Fail();
        }
    }
}