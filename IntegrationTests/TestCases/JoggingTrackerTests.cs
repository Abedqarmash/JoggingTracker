using Contracts.V1.Jogging.Models;
using Contracts.V1.Jogging.Resources;
using Contracts.V1.User.Models;
using Contracts.V1.User.Resources;
using IntegrationTests.Attributes;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace IntegrationTests.TestCases
{
    [TestCaseOrderer(
    ordererTypeName: "IntegrationTests.Orderers.PriorityOrderer",
    ordererAssemblyName: "IntegrationTests")]
    public class JoggingTrackerTests : IClassFixture<WebApplicationFactory<API.Program>>
    {
        private readonly HttpClient _client;
        private static UserResource _userResource = default!;
        private static string Token = default!;
        public JoggingTrackerTests(WebApplicationFactory<API.Program> factory) 
        {
            _client = factory.CreateClient();
            if(!string.IsNullOrEmpty(Token))
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
            }
        }

        [Fact, TestPriority(1)]
        public async Task AuthenticationController_AuthenticateToAdminUser_200Ok()
        {
            // Arrange
            var model = new LoginModel
            {
                Email = "userAdmin@admin.com",
                Password = "AdminPass@123"
            };

            //Act
            var response = await _client.PostAsync("v1/api/authentication/login", model.ToJsonContent());
            var result = response.Deserialize<AuthenticationResource>();
            Token = result.Token!; // Replace with a valid JWT token

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(2)]
        public async Task UsersController_CreateNewUser_Return200Ok()
        {

            //Arrange
            string username = Helper.GenerateRandomString(5);
            var model = new RegisterModel
            {
                UserName = username,
                Email =$"{username}@gmail.com",
                Password="TestPass@123",
                ConfirmPassword="TestPass@123"
            };

            //Act
            var response = await _client.PostAsync("v1/api/users",model.ToJsonContent());

            _userResource = response.Deserialize<UserResource>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(model.UserName, _userResource.UserName);
            Assert.Equal(model.Email, _userResource.Email);

            var login = new LoginModel
            {
                Email = model.Email,
                Password = model.Password,
            };

            response = await _client.PostAsync("v1/api/authentication/login", login.ToJsonContent());
            Token = (response.Deserialize<AuthenticationResource>()).Token!;
        }

        [Theory]
        [MemberData(nameof(GetJoggingModels))]
        [TestPriority(3)]
        public async Task JoggingTrackerController_CreateRecords_Return200Ok(JoggingModel model)
        {
            //Act
            var response = await _client.PostAsync("v1/api/records", model.ToJsonContent());

            var result = response.Deserialize<JoggingTrackerResource>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(model.Date, result.Date);
            Assert.Equal(model.Distance, result.Distance);
            Assert.Equal(model.Time, result.Time);
            Assert.Equal(_userResource.Id, result.UserId);
        }

        [Fact, TestPriority(4)]
        public async Task JoggingTrackerController_GenerateReport_Return200Ok()
        {
            //Act
            var response = await _client.GetAsync("v1/api/records/reports");

            var result = (response.Deserialize<List<ReportResource>>()).ToArray();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Helper.AssertionReport(result[0], 8, 10, new DateTime(2024, 1, 28));
            Helper.AssertionReport(result[1], 5.5, 7.667, new DateTime(2024, 2, 4));

            var login = new LoginModel
            {
                Email = "userAdmin@admin.com",
                Password = "AdminPass@123"
            };

            response = await _client.PostAsync("v1/api/authentication/login", login.ToJsonContent());
            Token = (response.Deserialize<AuthenticationResource>()).Token!;
        }

        [Fact, TestPriority(5)]
        public async Task UsersController_DeleteUser_Return200Ok()
        {
            //Act
            var response = await _client.DeleteAsync($"v1/api/users/{_userResource.Email}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        #region Helper
        public static IEnumerable<object?[]> GetJoggingModels() => new List<object?[]>
        {
            new object[]
            {
                new JoggingModel
                {
                    Time = new TimeSpan(1,15,0),
                    Date = new DateTime(2024,2,3),
                    Distance = 10,
                    Location = "Park"
                }
            },
            new object[]
            {
                new JoggingModel
                {
                    Time = new TimeSpan(2,0,0),
                    Date = new DateTime(2024,2,4),
                    Distance = 7,
                    Location = "City"
                }
            },
            new object[]
            {
                new JoggingModel
                {
                    Time = new TimeSpan(1,0,0),
                    Date = new DateTime(2024,2,7),
                    Distance = 7,
                    Location = "Manara"
                }
            },
            new object[]
            {
                new JoggingModel
                {
                    Time = new TimeSpan(1,30,0),
                    Date = new DateTime(2024,2,9),
                    Distance = 9,
                    Location = "AlTira"
                }
            },
        };

        #endregion
    }
}
