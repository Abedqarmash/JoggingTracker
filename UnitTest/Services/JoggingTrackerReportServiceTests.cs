using BusinessLogic.JoggingTracker;

namespace UnitTest.Services
{
    public class JoggingTrackerReportServiceTests
    {
        private readonly JoggingTrackerReportService _reportingService;
        public JoggingTrackerReportServiceTests()
        {
            _reportingService = new();
        }

        [Fact]
        public void JoggingTrackerReportService_GenerateReport()
        {
            //Arrange
            var model = GenerateSampleModels.GenerateJoggingEntities();

            //Act
            var result = _reportingService.GenerateWeeklyReports(model).ToArray();

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Length);
            Helper.AssertionReport(result[0], 8, 10, new DateTime(2024, 1, 28));
            Helper.AssertionReport(result[1], 5.5, 7.667, new DateTime(2024, 2, 4));
        }
    }
}
