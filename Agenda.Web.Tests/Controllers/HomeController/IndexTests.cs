// <copyright file="IndexTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.DomainObjects.Locations;
using Agenda.Domain.DomainObjects.Meetings;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Domain.ValueObjects.SetupStatii;
using Agenda.Service;
using Agenda.Utilities.Models.Whos;
using Agenda.Web.Areas.Api.Models.Home;
using Agenda.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyHomeController = Agenda.Web.Controllers.HomeController;

namespace Agenda.Web.Tests.Controllers.HomeController
{
    /// <summary>
    /// SOMETHING.
    /// </summary>
    [TestClass]
    public class IndexTests
    {
        /// <summary>
        /// Test that when no meetings exists, it gets the SetUp status.
        /// </summary>
        /// <returns>Nothing.</returns>
        [TestMethod]
        public async Task NoMeetings()
        {
            // ARRANGE
            Mock<ILogger<MyHomeController>> loggerMock = CreateLoggerMock();

            IList<IMeeting> meetingList = new List<IMeeting>();
            ISetupStatus setupStatus = new SetupStatus(
                haveOrganisations: false,
                haveCommittees: false);

            Mock<IAgendaService> agendaServiceMock = CreateAgendaServiceMock(meetingList, setupStatus);

            Mock<IFeatureManager> featureManagerMock = CreateFeatureManagerMock(
                newReferenceSearch: true);

            using MyHomeController controller = CreateHomeController(
                loggerMock,
                agendaServiceMock,
                featureManagerMock);

            // ACT
            IActionResult actionResult = await controller.Index().ConfigureAwait(false);

            // ASSERT
            // Assert that this is a View Result
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)actionResult;

            // Assert that is has the correct view model.
            Assert.IsNotNull(viewResult.Model);
            Assert.IsInstanceOfType(viewResult.Model, typeof(IndexViewModel));

            // Verify service level methods called the correct number of times.
            agendaServiceMock.Verify(
                x =>
                    x.GetRecentMeetingsMostRecentFirstAsync(
                        It.IsAny<IWho>(),
                        It.IsAny<TimeSpan?>(),
                        It.IsAny<int?>()),
                Times.Once());

            agendaServiceMock.Verify(
                x =>
                x.GetSetupStatusAsync(
                    It.IsAny<IWho>()),
                Times.Once);
        }

        /// <summary>
        /// Test that when Meetings exists, it does not get the SetUp status.
        /// </summary>
        /// <returns>Nothing.</returns>
        [TestMethod]
        public async Task HaveMeetings()
        {
            // ARRANGE
            Mock<ILogger<MyHomeController>> loggerMock = CreateLoggerMock();

            IOrganisation organisation = new Organisation(
                id: Guid.NewGuid(),
                code: "BRADGATE",
                name: "Bradgate Bridge Club",
                bgColour: "000000");

            ILocation location = new Location(
                id: Guid.NewGuid(),
                organisation: organisation,
                name: "Location",
                address: "Address",
                what3Words: "one.two.three",
                latitude: 50,
                longitude: -1);

            IList<IMeeting> meetingList = new List<IMeeting>
            {
                new Meeting(
                    id: Guid.NewGuid(),
                    committee: new Committee(
                        id: Guid.NewGuid(),
                        organisation: organisation,
                        name: "Executive",
                        description: "Executive Committee"),
                    location: location,
                    meetingDateTime: DateTime.Today.AddHours(19).AddMinutes(30))
            };

            Mock<IAgendaService> agendaServiceMock = CreateAgendaServiceMock(
                meetingList: meetingList,
                setupStatus: null);

            Mock<IFeatureManager> featureManagerMocker = CreateFeatureManagerMock(
                newReferenceSearch: true);

            using MyHomeController controller = CreateHomeController(
                loggerMock,
                agendaServiceMock,
                featureManagerMocker);

            // ACT
            IActionResult actionResult = await controller.Index().ConfigureAwait(false);

            // ASSERT
            // Assert that this is a View Result
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)actionResult;

            // Assert that is has the correct view model.
            Assert.IsNotNull(viewResult.Model);
            Assert.IsInstanceOfType(viewResult.Model, typeof(IndexViewModel));

            // Verify service level methods called the correct number of times.
            agendaServiceMock.Verify(
                x =>
                    x.GetRecentMeetingsMostRecentFirstAsync(
                        It.IsAny<IWho>(),
                        It.IsAny<TimeSpan?>(),
                        It.IsAny<int?>()),
                Times.Once());

            agendaServiceMock.Verify(
                x =>
                x.GetSetupStatusAsync(
                    It.IsAny<IWho>()),
                Times.Never);
        }

        #region Private Methods

        private static Mock<ILogger<MyHomeController>> CreateLoggerMock()
        {
            return new Mock<ILogger<MyHomeController>>(MockBehavior.Loose);
        }

        private static Mock<IAgendaService> CreateAgendaServiceMock(
            IList<IMeeting> meetingList,
            ISetupStatus setupStatus)
        {
            Mock<IAgendaService> serviceMock = new Mock<IAgendaService>(MockBehavior.Strict);

            serviceMock.Setup(x =>
                    x.GetRecentMeetingsMostRecentFirstAsync(
                        It.IsAny<IWho>(),
                        It.IsAny<TimeSpan?>(),
                        It.IsAny<int?>()))
                .ReturnsAsync(meetingList);

            serviceMock.Setup(x =>
                    x.GetSetupStatusAsync(
                        It.IsAny<IWho>()))
                .ReturnsAsync(setupStatus);
            return serviceMock;
        }

        private static Mock<IFeatureManager> CreateFeatureManagerMock(
            bool newReferenceSearch)
        {
            Mock<IFeatureManager> featureManagerMock = new Mock<IFeatureManager>(MockBehavior.Strict);

            featureManagerMock.Setup(x =>
                    x.IsEnabledAsync(nameof(FeatureFlag.NewReferenceSearch)))
                .ReturnsAsync(newReferenceSearch);

            return featureManagerMock;
        }

        private static MyHomeController CreateHomeController(
            Mock<ILogger<MyHomeController>> loggerMock,
            Mock<IAgendaService> agendaServiceMock,
            Mock<IFeatureManager> featureManagerMock)
        {
            return new MyHomeController(
                logger: loggerMock.Object,
                service: agendaServiceMock.Object,
                featureManager: featureManagerMock.Object,
                isTestMode: true);
        }

        #endregion Private Methods
    }
}