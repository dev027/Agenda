// <copyright file="ToDomainTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Data.Dtos;
using Agenda.Domain.DomainObjects.Meetings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Data.Tests.Dtos.MeetingTests
{
    /// <summary>
    /// Test <see cref="CommitteeDto.ToDomain"/>.
    /// </summary>
    [TestClass]
    public class ToDomainTests
    {
        /// <summary>
        /// Tests passing valid values.
        /// </summary>
        [TestMethod]
        public void Test_Passing_Valid_Values()
        {
            // ARRANGE
            OrganisationDto organisationDto = new OrganisationDto(
                id: Guid.NewGuid(),
                code: "CBC",
                name: "County Bridge Club");
            CommitteeDto committeeDto = new CommitteeDto(
                id: Guid.NewGuid(),
                organisationId: organisationDto.Id,
                name: "TSC",
                description: "Tournament Sub-Committee",
                organisation: organisationDto);
            MeetingDto meetingDto = new MeetingDto(
                Guid.NewGuid(),
                committeeId: committeeDto.Id,
                meetingDateTime: new DateTime(2019, 12, 30, 19, 15, 00),
                location: "County Bridge Club - Committee Room",
                committee: committeeDto);

            // ACT
            IMeeting meeting = meetingDto.ToDomain();

            // ASSERT
            Assert.IsNotNull(meeting);
            Assert.AreEqual(meetingDto.Id, meeting.Id);
            Assert.IsNotNull(meeting.Committee);
            Assert.AreEqual(meetingDto.CommitteeId, meeting.Committee.Id);
            Assert.AreEqual(meetingDto.MeetingDateTime, meeting.MeetingDateTime);
            Assert.AreEqual(meetingDto.Location, meeting.Location);
        }

        /// <summary>
        /// Tests the with null committee throws exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Test_WithNull_Committee_Throws_Exception()
        {
            // ARRANGE
            MeetingDto meetingDto = new MeetingDto(
                Guid.NewGuid(),
                committeeId: Guid.NewGuid(),
                meetingDateTime: new DateTime(2019, 12, 30, 19, 15, 00),
                location: "County Bridge Club - Committee Room",
                committee: null!);

            // ACT
            _ = meetingDto.ToDomain();
        }
    }
}