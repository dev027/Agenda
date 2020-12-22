// <copyright file="ToDtoTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Data.Dtos;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.DomainObjects.Meetings;
using Agenda.Domain.DomainObjects.Organisations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Data.Tests.Dtos.MeetingTests
{
    /// <summary>
    /// Test <see cref="MeetingDto.ToDto"/>.
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
                name: "County Bridge Club",
                bgColour: "000000");
            IMeeting meeting = new Meeting(
                id: Guid.NewGuid(),
                committee: new Committee(
                    id: Guid.NewGuid(),
                    organisation: organisation,
                    name: "TSC",
                    description: "Tournament Sub-Committee"),
                meetingDateTime: new DateTime(2019, 12, 30, 19, 15, 00));

            // ACT
            MeetingDto meetingDto = MeetingDto.ToDto(meeting);

            // ASSERT
            Assert.AreEqual(meeting.Id, meetingDto.Id);
            Assert.AreEqual(meeting.Committee.Id, meetingDto.CommitteeId);
            Assert.AreEqual(meeting.MeetingDateTime, meetingDto.MeetingDateTime);
        }

        /// <summary>
        /// Tests passing null Committee throws exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_Passing_Null_Committee_Throws_Exception()
        {
            // ACT
            _ = MeetingDto.ToDto(null!);
        }
    }
}