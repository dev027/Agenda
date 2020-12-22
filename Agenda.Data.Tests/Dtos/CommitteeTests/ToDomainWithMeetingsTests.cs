// <copyright file="ToDomainWithMeetingsTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Agenda.Data.Dtos;
using Agenda.Data.Tests.TestUtilities;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.DomainObjects.Meetings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Data.Tests.Dtos.CommitteeTests
{
    /// <summary>
    /// Tests <see cref="CommitteeDto.ToDomainWithMeetings"/>.
    /// </summary>
    [TestClass]
    public class ToDomainWithMeetingsTests
    {
        /// <summary>
        /// Tests with valid list of Meetings.
        /// </summary>
        [TestMethod]
        public void Test_With_Valid_List_Of_Meetings()
        {
            // ARRANGE
            Guid paramId = Guid.NewGuid();
            const string paramName = "TSC";
            const string paramDescription = "Tournament Sub-Committee";

            OrganisationDto organisationDto = new OrganisationDto(
                id: Guid.NewGuid(),
                code: "CBC",
                name: "County Bridge Club",
                bgColour: "000000");

            CommitteeDto committeeDto = new CommitteeDto(
                id: paramId,
                organisationId: organisationDto.Id,
                name: paramName,
                description: paramDescription,
                organisation: organisationDto);

            IList<MeetingDto> paramMeetings = new List<MeetingDto>
            {
                new MeetingDto(
                    id: Guid.NewGuid(),
                    committeeId: committeeDto.Id,
                    meetingDateTime: DateTime.Now,
                    committee: committeeDto)
            };

            committeeDto.SetPrivatePropertyValue(
                propName: nameof(CommitteeDto.Meetings),
                value: paramMeetings);

            // ACT
            ICommitteeWithMeetings committeeWithMeetings =
                committeeDto.ToDomainWithMeetings();

            // ASSERT
            Assert.AreEqual(paramId, committeeWithMeetings.Id);

            Assert.AreEqual(paramName, committeeWithMeetings.Name);
            Assert.AreEqual(paramDescription, committeeWithMeetings.Description);

            Assert.IsNotNull(committeeWithMeetings.Organisation);
            Assert.AreEqual(organisationDto.Id, committeeWithMeetings.Organisation.Id);

            Assert.IsNotNull(committeeWithMeetings.Meetings);
            Assert.AreEqual(1, committeeWithMeetings.Meetings.Count);

            IMeeting meeting = committeeWithMeetings.Meetings[0];
            Assert.IsNotNull(meeting);
            Assert.IsNotNull(meeting.Committee);
            Assert.AreEqual(paramId, meeting.Committee.Id);

            Assert.IsNotNull(meeting.Committee.Organisation);
            Assert.AreEqual(organisationDto.Id, meeting.Committee.Organisation.Id);
        }

        /// <summary>
        /// Tests that null list of meetings throws exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Test_That_Null_List_Of_Meetings_Throws_Exception()
        {
            Guid paramId = Guid.NewGuid();
            const string paramName = "TSC";
            const string paramDescription = "Tournament Sub-Committee";

            OrganisationDto organisationDto = new OrganisationDto(
                id: Guid.NewGuid(),
                code: "CBC",
                name: "County Bridge Club",
                bgColour: "000000");

            CommitteeDto committeeDto = new CommitteeDto(
                id: paramId,
                organisationId: organisationDto.Id,
                name: paramName,
                description: paramDescription,
                organisation: organisationDto);

            // ACT
            _ = committeeDto.ToDomainWithMeetings();
        }

        /// <summary>
        /// Tests that null organisation throws exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Test_That_Null_Organisation_Throws_Exception()
        {
            // ARRANGE
            Guid paramId = Guid.NewGuid();
            const string paramName = "TSC";
            const string paramDescription = "Tournament Sub-Committee";

            OrganisationDto organisationDto = new OrganisationDto(
                id: Guid.NewGuid(),
                code: "CBC",
                name: "County Bridge Club",
                bgColour: "000000");

            CommitteeDto committeeDto = new CommitteeDto(
                id: paramId,
                organisationId: organisationDto.Id,
                name: paramName,
                description: paramDescription);

            IList<MeetingDto> paramMeetings = new List<MeetingDto>
            {
                new MeetingDto(
                    id: Guid.NewGuid(),
                    committeeId: committeeDto.Id,
                    meetingDateTime: DateTime.Now,
                    committee: committeeDto)
            };

            committeeDto.SetPrivatePropertyValue(
                propName: nameof(CommitteeDto.Meetings),
                value: paramMeetings);

            // ACT
            _ = committeeDto.ToDomainWithMeetings();
        }
    }
}