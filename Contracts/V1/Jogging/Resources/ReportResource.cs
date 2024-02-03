namespace Contracts.V1.Jogging.Resources
{
    public class ReportResource
    {
        public double AverageSpeed { get; set; }

        public double AverageDistance { get; set; }

        public DateTimeOffset WeekStartDate { get; set; }
    }
}
