// <copyright file="ConstructorTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.DomainObjects.Meetings;
using Agenda.Domain.DomainObjects.Organisations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Domain.Tests.DomainObjects.Meetings.MeetingTests
{
    /// <summary>
    /// Test the constructor for <see cref="Meeting"/>.
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
            IOrganisation organisation = new Organisation(
                id: Guid.NewGuid(),
                code: "CBC",
                name: "County Bridge Club",
                bgColour: "000000");

            Guid paramId = Guid.NewGuid();
            ICommittee paramCommittee = new Committee(
                id: Guid.NewGuid(),
                organisation: organisation,
                name: "TSC",
                description: "Tournament Sub-Committee");

            DateTime paramMeetingDateTime = new DateTime(2019, 12, 30, 19, 15, 00);

            // ACT
            IMeeting meeting = new Meeting(
                id: paramId,
                committee: paramCommittee,
                meetingDateTime: paramMeetingDateTime);

            // ASSERT
            Assert.AreEqual(paramId, meeting.Id);
            Assert.AreSame(paramCommittee, meeting.Committee);
            Assert.AreEqual(paramMeetingDateTime, meeting.MeetingDateTime);
        }
    }
}