using Contracts.V1.Jogging.Resources;
using DataAccess.SQL.Entities;

namespace BusinessLogic.JoggingTracker
{
    public interface IJoggingTrackerReportService
    {
        public List<ReportResource> GenerateWeeklyReports(List<JoggingEntity> model);
    }

    public class JoggingTrackerReportService : IJoggingTrackerReportService
    {
        public List<ReportResource> GenerateWeeklyReports(List<JoggingEntity> model)
        {
            return model
                .GroupBy(entry => GetStartOfWeek(entry.Date))
                .Select(group => new ReportResource
                {
                    AverageSpeed = Math.Round(group.Average(entry => entry.Distance / entry.Time.TotalHours), 3),
                    AverageDistance = Math.Round(group.Average(entry => entry.Distance), 3),
                    WeekStartDate = group.Key
                })
                .OrderBy(x => x.WeekStartDate)
                .ToList();
        }

        private DateTime GetStartOfWeek(DateTimeOffset date)
        {
            // Adjust the date to the start of the week (Sunday)
            int daysToSubtract = (int)date.DayOfWeek;
            return date.Date.AddDays(-daysToSubtract);
        }
    }
}
