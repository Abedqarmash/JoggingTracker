using Contracts.V1.User.Filters;
using DataAccess.SQL.Entities;

namespace UnitTest
{
    public static class GenerateSampleModels
    {
        #region entities
        public static List<JoggingEntity> GenerateJoggingEntities() =>
            new List<JoggingEntity>()
            {
                new()
                {
                    Id = 1,
                    UserId = Helper.GenerateRandomString(5),
                    Time = new TimeSpan(1,15,0),
                    Date = new DateTime(2024,2,3),
                    Distance = 10,
                },
                new()
                {
                    Id = 2,
                    UserId = Helper.GenerateRandomString(5),
                    Time = new TimeSpan(2,0,0),
                    Date = new DateTime(2024,2,4),
                    Distance = 7,
                },
                new()
                {
                    Id = 3,
                    UserId = Helper.GenerateRandomString(5),
                    Time = new TimeSpan(1,0,0),
                    Date = new DateTime(2024,2,7),
                    Distance = 7,
                },
                new()
                {
                    Id = 4,
                    UserId = Helper.GenerateRandomString(5),
                    Time = new TimeSpan(1,30,0),
                    Date = new DateTime(2024,2,9),
                    Distance = 9,
                }
            };

        public static List<UserEntity> GenerateUserEntities() =>
            new List<UserEntity>()
            {
                new()
                {
                    Id = Helper.GenerateRandomString(5),
                    CreatedBy = "test-user",
                    CreatedOn = DateTime.Now,
                    Email = "testUser@unitTetst.com",
                    UserName = "testUser"
                },
                new()
                {
                    Id = Helper.GenerateRandomString(5),
                    CreatedBy = "test-user",
                    CreatedOn = DateTime.Now,
                    Email = "testUser1@unitTetst.com",
                    UserName = "testUser1"
                },
            };
        #endregion

        #region filters
        public static UserListFilter GetUsetListFilter() => new()
        {
            Emails = new List<string>
                {
                    "testUser@UnitTest.com",
                    "testUser1@UnitTest.com",
                },
            Skip = 1,
            Take = 2,
        };
        #endregion
    }
}
