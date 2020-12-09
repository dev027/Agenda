// <copyright file="ToDomainTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Data.Dtos;
using Agenda.Domain.Constants;
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
                name: "County Bridge Club",
                bgColour: "000000");
            CommitteeDto committeeDto = new CommitteeDto(
                id: Guid.NewGuid(),
                organisationId: organisationDto.Id,
                name: "TSC",
                description: "Tournament Sub-Committee",
                organisation: organisationDto);
            LocationTypeDto locationTypeDto = new LocationTypeDto(
                id: Guid.NewGuid(),
                code: LocationTypeCodes.RealWorld,
                name: "Real World",
                description: "Description");
            LocationDto locationDto = new LocationDto(
                id: Guid.NewGuid(),
                organisationId: organisationDto.Id,
                locationTypeId: locationTypeDto.Id,
                name: "Location",
                address: "Address",
                what3Words: "one.two.three",
                latitude: 50,
                longitude: -1,
                organisation: organisationDto,
                locationType: locationTypeDto);
            MeetingDto meetingDto = new MeetingDto(
                Guid.NewGuid(),
                committeeId: committeeDto.Id,
                locationId: locationDto.Id,
                meetingDateTime: new DateTime(2019, 12, 30, 19, 15, 00),
                committee: committeeDto,
                location: locationDto);

            // ACT
            IMeeting meeting = meetingDto.ToDomain();

            // ASSERT
            Assert.IsNotNull(meeting);
            Assert.AreEqual(meetingDto.Id, meeting.Id);
            Assert.IsNotNull(meeting.Committee);
            Assert.AreEqual(meetingDto.CommitteeId, meeting.Committee.Id);
            Assert.IsNotNull(meeting.Location);
            Assert.AreEqual(meetingDto.LocationId, meeting.Location.Id);
            Assert.AreEqual(meetingDto.MeetingDateTime, meeting.MeetingDateTime);
        }

        /// <summary>
        /// Tests the with null committee throws exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Test_WithNull_Committee_Throws_Exception()
        {
            // ARRANGE
            OrganisationDto organisationDto = new OrganisationDto(
                id: Guid.NewGuid(),
                code: "CBC",
                name: "County Bridge Club",
                bgColour: "000000");
            CommitteeDto committeeDto = new CommitteeDto(
                id: Guid.NewGuid(),
                organisationId: organisationDto.Id,
                name: "TSC",
                description: "Tournament Sub-Committee",
                organisation: organisationDto);
            LocationTypeDto locationTypeDto = new LocationTypeDto(
                id: Guid.NewGuid(),
                code: LocationTypeCodes.RealWorld,
                name: "Real World",
                description: "Description");
            LocationDto locationDto = new LocationDto(
                id: Guid.NewGuid(),
                organisationId: organisationDto.Id,
                locationTypeId: locationTypeDto.Id,
                name: "Location",
                address: "Address",
                what3Words: "one.two.three",
                latitude: 50,
                longitude: -1);
            MeetingDto meetingDto = new MeetingDto(
                Guid.NewGuid(),
                committeeId: committeeDto.Id,
                locationId: locationDto.Id,
                meetingDateTime: new DateTime(2019, 12, 30, 19, 15, 00),
                committee: null!,
                location: locationDto);

            // ACT
            _ = meetingDto.ToDomain();
        }

        /// <summary>
        /// Tests the with null location throws exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Test_WithNull_Location_Throws_Exception()
        {
            // ARRANGE
            OrganisationDto organisationDto = new OrganisationDto(
                id: Guid.NewGuid(),
                code: "CBC",
                name: "County Bridge Club",
                bgColour: "000000");
            CommitteeDto committeeDto = new CommitteeDto(
                id: Guid.NewGuid(),
                organisationId: organisationDto.Id,
                name: "TSC",
                description: "Tournament Sub-Committee",
                organisation: organisationDto);
            LocationTypeDto locationTypeDto = new LocationTypeDto(
                id: Guid.NewGuid(),
                code: LocationTypeCodes.RealWorld,
                name: "Real World",
                description: "Description");
            LocationDto locationDto = new LocationDto(
                id: Guid.NewGuid(),
                organisationId: organisationDto.Id,
                locationTypeId: locationTypeDto.Id,
                name: "Location",
                address: "Address",
                what3Words: "one.two.three",
                latitude: 50,
                longitude: -1);
            MeetingDto meetingDto = new MeetingDto(
                Guid.NewGuid(),
                committeeId: committeeDto.Id,
                locationId: locationDto.Id,
                meetingDateTime: new DateTime(2019, 12, 30, 19, 15, 00),
                committee: committeeDto,
                location: null!);

            // ACT
            _ = meetingDto.ToDomain();
        }
    }
}