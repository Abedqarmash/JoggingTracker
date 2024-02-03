using Contracts.V1.Jogging.Resources;
using Contracts.V1.User.Resources;
using DataAccess.SQL.Entities;
using System.Text;

namespace UnitTest
{
    public static class Helper
    {
        private static Random random = new Random();

        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder stringBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                stringBuilder.Append(chars[index]);
            }

            return stringBuilder.ToString();
        }

        public static void AssertionReport(ReportResource report,
            double exprectedAverageSpeed,
            double expectedAverageDistance,
            DateTimeOffset expectedWeekSartDate)
        {
            Assert.Equal(exprectedAverageSpeed, report.AverageSpeed);
            Assert.Equal(expectedAverageDistance, report.AverageDistance);
            Assert.Equal(expectedWeekSartDate, report.WeekStartDate);
        }

        public static void AssertionUserResource(UserResource user,
            string exprectedEmail,
            string exprectedUserName)
        {
            Assert.Equal(exprectedEmail, user.Email);
            Assert.Equal(exprectedUserName, user.UserName);
        }
    }
}
