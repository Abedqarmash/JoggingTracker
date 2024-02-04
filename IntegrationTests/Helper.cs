using Contracts.V1.Jogging.Resources;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Text;

namespace IntegrationTests
{
    public static class Helper
    {
        private static Random random = new Random();

        public static void AssertionReport(ReportResource report,
            double exprectedAverageSpeed,
            double expectedAverageDistance,
            DateTimeOffset expectedWeekSartDate)
        {
            Assert.Equal(exprectedAverageSpeed, report.AverageSpeed);
            Assert.Equal(expectedAverageDistance, report.AverageDistance);
            Assert.Equal(expectedWeekSartDate, report.WeekStartDate);
        }

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

        [Description("Can be used only with minimal hosting model.")]
        public static StringContent ToJsonContent(this object content)
        {
            string content2 = JsonConvert.SerializeObject(content);
            StringContent stringContent = new StringContent(content2);
            stringContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            return stringContent;
        }

        public static T Deserialize<T>(this HttpResponseMessage response)
        {
            string result = response.Content.ReadAsStringAsync().Result;
            T val = (T)((typeof(T) == typeof(string)) ? ((object)(T)Convert.ChangeType(result, typeof(T))) : ((object)JsonConvert.DeserializeObject<T>(result)));
            return val;
        }
    }
}
