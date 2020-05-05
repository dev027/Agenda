// <copyright file="ConstructorTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.DomainObjects.Locations;
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

            ILocation paramLocation = new Location(
                id: Guid.NewGuid(),
                organisation: organisation,
                name: "Location",
                address: "Address",
                what3Words: "one.two.three",
                latitude: 50,
                longitude: -1);

            DateTime paramMeetingDateTime = new DateTime(2019, 12, 30, 19, 15, 00);

            // ACT
            IMeeting meeting = new Meeting(
                id: paramId,
                committee: paramCommittee,
                location: paramLocation,
                meetingDateTime: paramMeetingDateTime);

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
            IOrganisation organisation = new Organisation(
                id: Guid.NewGuid(),
                code: "CBC",
                name: "County Bridge Club",
                bgColour: "000000");

            Guid paramId = Guid.NewGuid();

            ILocation paramLocation = new Location(
                id: Guid.NewGuid(),
                organisation: organisation,
                name: "Location",
                address: "Address",
                what3Words: "one.two.three",
                latitude: 50,
                longitude: -1);

            DateTime paramMeetingDateTime = new DateTime(2019, 12, 30, 19, 15, 00);

            // ACT
            _ = new Meeting(
                id: paramId,
                committee: null,
                location: paramLocation,
                meetingDateTime: paramMeetingDateTime);
        }

        /// <summary>
        /// Tests the constructor null location does throw exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_Constructor_Null_Location_Does_Throw_Exception()
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
            _ = new Meeting(
                id: paramId,
                committee: paramCommittee,
                location: null,
                meetingDateTime: paramMeetingDateTime);
        }
    }
}