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
                name: "County Bridge Club",
                bgColour: "000000");
            const string paramName = "TSC";
            const string paramDescription = "Tournament Sub-Committee";

            ICommittee committee = new Committee(
                id: paramId,
                organisation: paramOrganisation,
                name: paramName,
                description: paramDescription);

            IList<IMeeting> paramMeetings = new List<IMeeting>
            {
                new Meeting(
                    id: Guid.NewGuid(),
                    committee: committee,
                    meetingDateTime: DateTime.Now)
            };

            // ACT
            ICommitteeWithMeetings committeeWithMeetings = new CommitteeWithMeetings(
                id: paramId,
                organisation: paramOrganisation,
                name: paramName,
                description: paramDescription,
                meetings: paramMeetings);

            // ASSERT
            Assert.AreEqual(paramId, committeeWithMeetings.Id);
            Assert.AreSame(paramOrganisation, committeeWithMeetings.Organisation);
            Assert.AreEqual(paramName, committeeWithMeetings.Name);
            Assert.AreEqual(paramDescription, committeeWithMeetings.Description);
            Assert.AreSame(paramMeetings, committeeWithMeetings.Meetings);
        }
    }
}