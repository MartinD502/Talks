using Application.Dtos;
using Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Talks.Tests.Unit.Services
{
    [Trait("Category", "Unit")]
    public class SessionApprovalServiceTests
    {
        private readonly SessionApprovalService _sessionApprovalService;

        public SessionApprovalServiceTests()
        {
            _sessionApprovalService = new SessionApprovalService();
        }

        #region Sessions no Legacy should be approved.
        [Fact]
        public void ApproveSessions_WhenSessionHasNoLegacyTitleAndDescription_ReturnIsApproved()
        {
            var sessions = new List<SessionDto>
            {
                new SessionDto
                {
                    Title = "Building Modern Tech",
                    Description = "Learn how to build modern technologies with .NET",
                    IsApproved = false
                }
            };

            var result = _sessionApprovalService.ApproveSessions(sessions);
            Assert.True(result[0].IsApproved);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ApproveSessions_WhenSessionHasNoLegacyTitleOnly_ReturnIsApproved(string? description)
        {
            var sessions = new List<SessionDto>
            {
                new SessionDto
                {
                    Title = "Best Practices",
                    Description = description,
                    IsApproved = false
                }
            };

            var result = _sessionApprovalService.ApproveSessions(sessions);
            Assert.True(result[0].IsApproved);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ApproveSessions_WhenSessionHasNoLegacyDescriptionOnly_ReturnIsApproved(string? title)
        {
            var sessions = new List<SessionDto>
            {
                new SessionDto
                {
                    Title = title,
                    Description = "An introduction into best practices.",
                    IsApproved = false
                }
            };

            var result = _sessionApprovalService.ApproveSessions(sessions);
            Assert.True(result[0].IsApproved);
        }
        #endregion Sessions no Legacy should be approved.

        #region Sessions legacy should not be approved.
        [Theory]
        [InlineData("Cobol")]
        [InlineData("Commodore")]
        [InlineData("Punch Cards")]
        [InlineData("VBScript")]
        public void ApproveSessions_WhenSessionHasLegacyTitleOnly_ReturnIsNotApproved(string title)
        {
            var sessions = new List<SessionDto>
            {
                new SessionDto
                {
                    Title = title,
                    Description = "Modern practices",
                    IsApproved = false
                }
            };

            var result = _sessionApprovalService.ApproveSessions(sessions);
            Assert.False(result[0].IsApproved);
        }

        [Theory]
        [InlineData("Cobol")]
        [InlineData("Commodore")]
        [InlineData("Punch Cards")]
        [InlineData("VBScript")]
        public void ApproveSessions_WhenSessionHasLegacyDescriptionOnly_ReturnIsNotApproved(string description)
        {
            var sessions = new List<SessionDto>
            {
                new SessionDto
                {
                    Title = "Modern Development",
                    Description = $"Here {description} technology is used",
                    IsApproved = false
                }
            };

            var result = _sessionApprovalService.ApproveSessions(sessions);
            Assert.False(result[0].IsApproved);
        }
        #endregion Sessions legacy should not be approved.
    }
}
