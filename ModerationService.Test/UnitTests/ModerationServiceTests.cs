using ModerationService.Test.MockServices;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ModerationService.Services;

namespace ModerationService.Test.UnitTests
{
    [TestFixture]
    public class ModerationServiceTests
    {
        [Test]
        public void ApproveRequestSuccess()
        {
            //Arrange
            MockModerationDAL mockModerationDAL = new MockModerationDAL();
            Guid guid = Guid.NewGuid();

            var request = new Models.Request { RequestId = guid, RequestType = Models.RequestType.Upgrade_To_Chef, UserId = guid };

            mockModerationDAL.requests.Add(request);

            Services.ModerationService moderationService = new Services.ModerationService(mockModerationDAL);

            //Act
            var result = moderationService.ApproveRequest(request);

            //Assert
            Assert.AreEqual(0, mockModerationDAL.requests.Count);
        }

        [Test]
        public void GetRequestsSuccess()
        {
            //Arrange
            MockModerationDAL mockModerationDAL = new MockModerationDAL();
            Guid guid = Guid.NewGuid();

            var request = new Models.Request { RequestId = guid, RequestType = Models.RequestType.Upgrade_To_Chef, UserId = guid };

            mockModerationDAL.requests.Add(request);

            Services.ModerationService moderationService = new Services.ModerationService(mockModerationDAL);

            //Act
            var result = moderationService.GetRequests();

            //Assert
            Assert.AreEqual(result.Count, 1);
        }

        [Test]
        public void GetReportsSuccess()
        {
            //Arrange
            MockModerationDAL mockModerationDAL = new MockModerationDAL();
            Guid guid = Guid.NewGuid();

            var report = new Models.Report { PostId = guid, ReporterId = guid, ReportId = guid };

            mockModerationDAL.reports.Add(report);

            Services.ModerationService moderationService = new Services.ModerationService(mockModerationDAL);

            //Act
            var result = moderationService.GetReports();

            //Assert
            Assert.AreEqual(result.Count, 1);
        }

        [Test]
        public void GetMyReportsSuccess()
        {
            //Arrange
            MockModerationDAL mockModerationDAL = new MockModerationDAL();
            Guid guid = Guid.NewGuid();

            var report = new Models.Report { PostId = guid, ReporterId = guid, ReportId = guid };

            mockModerationDAL.reports.Add(report);

            Services.ModerationService moderationService = new Services.ModerationService(mockModerationDAL);

            //Act
            var result = moderationService.GetMyReports(guid);

            //Assert
            Assert.AreEqual(result.Count, 1);
        }

        [Test]
        public void PostReportSuccess()
        {
            //Arrange
            MockModerationDAL mockModerationDAL = new MockModerationDAL();
            Guid guid = Guid.NewGuid();

            var report = new Models.Report { PostId = guid, ReporterId = guid, ReportId = guid };

            Services.ModerationService moderationService = new Services.ModerationService(mockModerationDAL);

            //Act
            var result = moderationService.PostReport(report);

            //Assert
            Assert.AreEqual(mockModerationDAL.reports.Count, 1);
        }

        [Test]
        public void PostRequestSuccess()
        {
            //Arrange
            MockModerationDAL mockModerationDAL = new MockModerationDAL();
            Guid guid = Guid.NewGuid();

            var request = new Models.Request { RequestId = guid, RequestType = Models.RequestType.Upgrade_To_Chef, UserId = guid };

            Services.ModerationService moderationService = new Services.ModerationService(mockModerationDAL);

            //Act
            var result = moderationService.PostRequest(request);

            //Assert
            Assert.AreEqual(mockModerationDAL.requests.Count, 1);
        }

        [Test]
        public void DeclineRequestSuccess()
        {
            //Arrange
            MockModerationDAL mockModerationDAL = new MockModerationDAL();
            Guid guid = Guid.NewGuid();

            var request = new Models.Request { RequestId = guid, RequestType = Models.RequestType.Upgrade_To_Chef, UserId = guid };

            mockModerationDAL.requests.Add(request);

            Services.ModerationService moderationService = new Services.ModerationService(mockModerationDAL);

            //Act
            var result = moderationService.DeclineRequest(request);

            //Assert
            Assert.AreEqual(mockModerationDAL.requests.Count, 0);
        }

        [Test]
        public void RemoveReportSuccess()
        {
            //Arrange
            MockModerationDAL mockModerationDAL = new MockModerationDAL();
            Guid guid = Guid.NewGuid();

            var report = new Models.Report { PostId = guid, ReporterId = guid, ReportId = guid };

            mockModerationDAL.reports.Add(report);

            Services.ModerationService moderationService = new Services.ModerationService(mockModerationDAL);

            //Act
            var result = moderationService.RemoveReport(report);

            //Assert
            Assert.AreEqual(mockModerationDAL.reports.Count, 0);
        }
    }
}
