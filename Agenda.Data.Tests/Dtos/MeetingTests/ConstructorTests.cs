// <copyright file="ConstructorTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Data.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Data.Tests.Dtos.MeetingTests
{
    /// <summary>
    /// Test constructor for <see cref="MeetingDto" />.
    /// </summary>
    [TestClass]
    public class ConstructorTests
    {
        /// <summary>
        /// Tests the default constructor.
        /// </summary>
        [TestMethod]
        public void Test_Default_Constructor()
        {
            // ACT
            MeetingDto dto = new MeetingDto();

            // ASSERT
            Assert.AreEqual(Guid.Empty, dto.Id);
            Assert.AreEqual(Guid.Empty, dto.CommitteeId);
            Assert.AreEqual(DateTime.MinValue, dto.MeetingDateTime);
            Assert.AreEqual(Guid.Empty, dto.LocationId);
        }

        /// <summary>
        /// Tests the basic constructor.
        /// </summary>
        [TestMethod]
        public void Test_Basic_Constructor()
        {
            // ARRANGE
            Guid paramId = Guid.NewGuid();
            Guid paramCommitteeId = Guid.NewGuid();
            Guid paramLocationId = Guid.NewGuid();
            DateTime paramMeetingDateTime = new DateTime(2019, 12, 30, 19, 15, 00);

            // ACT
            MeetingDto dto = new MeetingDto(
                 id: paramId,
                 committeeId: paramCommitteeId,
                 locationId: paramLocationId,
                 meetingDateTime: paramMeetingDateTime);

            // ASSERT
            Assert.AreEqual(paramId, dto.Id);
            Assert.AreEqual(paramCommitteeId, dto.CommitteeId);
            Assert.AreEqual(paramLocationId, dto.LocationId);
            Assert.AreEqual(paramMeetingDateTime, dto.MeetingDateTime);
        }
    }
}