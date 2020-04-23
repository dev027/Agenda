// <copyright file="ConstructorTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.DomainObjects.Meetings;
using Agenda.Domain.DomainObjects.Organisations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Domain.Tests.DomainObjects.Committees.CommitteeWithMeetingsTests
{
    /// <summary>
    /// Test the constructor for <see cref="CommitteeWithMeetings"/>.
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
            IOrganisation paramOrganisation = new Organisation(
                id: Guid.NewGuid(),
                code: "CBC",
                name: "County Bridge Club");
            const string paramName = "TSC";
            const string paramDescription = "Tournament Sub-Committee";

            ICommittee committee = new Committee(
                id: paramId,
                organisation: paramOrganisation,
                name: paramName,
                description: paramDescription);

            IList<IMeeting> meetings = new List<IMeeting>
            {
                new Meeting(
                    id: Guid.NewGuid(),
                    committee: committee,
                    meetingDateTime: DateTime.Now,
                    location: "Location")
            };

            // ACT
            ICommitteeWithMeetings committeeWithMeetings = new CommitteeWithMeetings(
                id: paramId,
                organisation: paramOrganisation,
                name: paramName,
                description: paramDescription,
                meetings: meetings);

            // ASSERT
            Assert.AreEqual(paramId, committeeWithMeetings.Id);
            Assert.AreSame(paramOrganisation, committeeWithMeetings.Organisation);
            Assert.AreEqual(paramName, committeeWithMeetings.Name);
            Assert.AreEqual(paramDescription, committeeWithMeetings.Description);
            Assert.AreSame(meetings, committeeWithMeetings.Meetings);
        }

        /// <summary>
        /// Tests the constructor meetings throws exception.
        /// </summary>
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Test_Constructor_Null_Organisation_Throws_Exception()
        {
            // ARRANGE
            Guid paramId = Guid.NewGuid();
            IOrganisation paramOrganisation = new Organisation(
                id: Guid.NewGuid(),
                code: "CBC",
                name: "County Bridge Club");
            const string paramName = "TSC";
            const string paramDescription = "Tournament Sub-Committee";

            // ACT
            _ = new CommitteeWithMeetings(
                id: paramId,
                organisation: paramOrganisation,
                name: paramName,
                description: paramDescription,
                meetings: null);

            // ASSERT
            Assert.Fail();
        }
    }
}