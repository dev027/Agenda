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
            IMeeting meeting = new Meeting(
                Guid.NewGuid(),
                new Committee(
                id: Guid.NewGuid(),
                organisation: new Organisation(
                    id: Guid.NewGuid(),
                    code: "CBC",
                    name: "County Bridge Club"),
                name: "TSC",
                description: "Tournament Sub-Committee"),
                new DateTime(2019, 12, 30, 19, 15, 00),
                "County Bridge Club - Committee Room");

            // ACT
            MeetingDto meetingDto = MeetingDto.ToDto(meeting);

            // ASSERT
            Assert.AreEqual(meeting.Id, meetingDto.Id);
            Assert.AreEqual(meeting.Committee.Id, meetingDto.CommitteeId);
            Assert.AreEqual(meeting.MeetingDateTime, meetingDto.MeetingDateTime);
            Assert.AreEqual(meeting.Location, meetingDto.Location);
        }

        /// <summary>
        /// Tests passing null Committee throws exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_Passing_Null_Committee_Throws_Exception()
        {
            // ACT
            _ = MeetingDto.ToDto(null);
        }
    }
}