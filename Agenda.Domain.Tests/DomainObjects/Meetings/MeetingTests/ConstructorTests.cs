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
            Guid paramId = Guid.NewGuid();
            ICommittee paramCommittee = new Committee(
                id: Guid.NewGuid(),
                organisation: new Organisation(
                    id: Guid.NewGuid(),
                    code: "CBC",
                    name: "County Bridge Club"),
                name: "TSC",
                description: "Tournament Sub-Committee");
            DateTime paramMeetingDateTime = new DateTime(2019, 12, 30, 19, 15, 00);
            const string paramLocation = "County Bridge Club - Committee Room";

            // ACT
            IMeeting meeting = new Meeting(
                id: paramId,
                committee: paramCommittee,
                meetingDateTime: paramMeetingDateTime,
                location: paramLocation);

            // ASSERT
            Assert.AreEqual(paramId, meeting.Id);
            Assert.AreSame(paramCommittee, meeting.Committee);
            Assert.AreEqual(paramMeetingDateTime, meeting.MeetingDateTime);
            Assert.AreEqual(paramLocation, meeting.Location);
        }

        /// <summary>
        /// Tests the constructor null committee throws exception.
        /// </summary>
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Test_Constructor_Null_Committee_Throws_Exception()
        {
            // ARRANGE
            Guid paramId = Guid.NewGuid();
            DateTime paramMeetingDateTime = new DateTime(2019, 12, 30, 19, 15, 00);
            const string paramLocation = "County Bridge Club - Committee Room";

            // ACT
            _ = new Meeting(
                id: paramId,
                committee: null,
                meetingDateTime: paramMeetingDateTime,
                location: paramLocation);

            // ASSERT
            Assert.Fail();
        }

        /// <summary>
        /// Tests the constructor null location does not throw exception.
        /// </summary>
        [TestMethod]
        public void Test_Constructor_Null_Location_Does_Not_Throw_Exception()
        {
            // ARRANGE
            Guid paramId = Guid.NewGuid();
            ICommittee paramCommittee = new Committee(
                id: Guid.NewGuid(),
                organisation: new Organisation(
                    id: Guid.NewGuid(),
                    code: "CBC",
                    name: "County Bridge Club"),
                name: "TSC",
                description: "Tournament Sub-Committee");
            DateTime paramMeetingDateTime = new DateTime(2019, 12, 30, 19, 15, 00);

            // ACT
            IMeeting meeting = new Meeting(
                id: paramId,
                committee: paramCommittee,
                meetingDateTime: paramMeetingDateTime,
                location: null);

            // ASSERT
            Assert.AreEqual(paramId, meeting.Id);
            Assert.AreSame(paramCommittee, meeting.Committee);
            Assert.AreEqual(paramMeetingDateTime, meeting.MeetingDateTime);
            Assert.IsNull(meeting.Location);
        }
    }
}