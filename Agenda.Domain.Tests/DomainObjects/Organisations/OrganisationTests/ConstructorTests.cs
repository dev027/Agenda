﻿// <copyright file="ConstructorTests.cs" company="Do It Wright">
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
            const string paramBgColour = "000000";

            // ACT
            IOrganisation organisation = new Organisation(
                id: paramId,
                code: paramCode,
                name: paramName,
                bgColour: paramBgColour);

            // ASSERT
            Assert.AreEqual(paramId, organisation.Id);
            Assert.AreEqual(paramCode, organisation.Code);
            Assert.AreEqual(paramName, organisation.Name);
            Assert.AreEqual(paramBgColour, organisation.BgColour);
        }

        /// <summary>
        /// Tests the constructor empty code throws exception.
        /// </summary>
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Test_Constructor_Empty_Code_Throws_Exception()
        {
            // ARRANGE
            Guid paramId = Guid.NewGuid();
            const string paramName = "County Bridge Club";
            const string paramBgColour = "000000";

            // ACT
            _ = new Organisation(
                id: paramId,
                code: string.Empty,
                name: paramName,
                bgColour: paramBgColour);
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
            const string paramCode = "CBC";
            const string paramBgColour = "000000";

            // ACT
            _ = new Organisation(
                id: paramId,
                code: paramCode,
                name: string.Empty,
                bgColour: paramBgColour);
        }

        /// <summary>
        /// Tests the constructor empty background colour throws exception.
        /// </summary>
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Test_Constructor_Empty_Background_Colour_Throws_Exception()
        {
            // ARRANGE
            Guid paramId = Guid.NewGuid();
            const string paramCode = "CBC";
            const string paramName = "County Bridge Club";

            // ACT
            _ = new Organisation(
                id: paramId,
                code: paramCode,
                name: paramName,
                bgColour: string.Empty);
        }
    }
}