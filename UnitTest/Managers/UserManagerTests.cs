using BusinessLogic.Users;
using Contracts.Constants;
using Contracts.GlobalExeptionHandler.Exceptions;
using Contracts.V1.SharedModels;
using DataAccess.SQL.Entities;
using DataAccess.SQL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Principal;

namespace UnitTest.Managers
{
    public class UserManagerTests
    {
        private readonly UsersManager _manager;
        private readonly Mock<UserManager<UserEntity>> _mockUserManager = new Mock<UserManager<UserEntity>>(Mock.Of<IUserStore<UserEntity>>(),
            null, null, null, null, null, null, null, null!);
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor = new();
        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new();

        public UserManagerTests()
        {
            SetUpMocks();
            _manager = GetManager();
        }

        [Fact]
        public async Task UserManager_GetAllUser_Return_ListOfUsers()
        {
            //Arrange
            var filter = GenerateSampleModels.GetUsetListFilter() ;

            //Act
            var response = await _manager.GetAllUsers(filter);

            //Assert
            Assert.NotEmpty(response.Items);
            Assert.Equal(2, response.TotalResults);
            Helper.AssertionUserResource(response.Items.ToArray()[0], "testUser@unitTetst.com", "testUser");
            Helper.AssertionUserResource(response.Items.ToArray()[1], "testUser1@unitTetst.com", "testUser1");
        }

        [Fact]
        public async Task UserManager_GetUserById_Return_Users()
        {
            //Arrange
            string userId = "testId";

            //Act
            var response = await _manager.GetUserById(userId);

            //Assert
            Assert.NotNull(response);
            Helper.AssertionUserResource(response, "testUser@unitTetst.com", "testUser");
        }

        [Fact]
        public async Task UserManager_GetUserById_Throws_ItemNotFoundException()
        {
            //Arrange
            string userId = "testId";
            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<UserEntity?>(null)!);

            //Act
            var error = await Assert.ThrowsAsync<ItemNotFoundException>(async () =>
            {
                await GetManager().GetUserById(userId);
            });

            //Assert
            Assert.NotNull(error);
            Assert.Equal(ValidationMessages.UserWithIdDoesNotExist(userId), error.Errors["id"].FirstOrDefault());
        }

        [Fact]
        public async Task UserManager_GetUserByEmail_Return_Users()
        {
            //Arrange
            string email = "test@testEmail.com";

            //Act
            var response = await _manager.GetUserByEmail(email);

            //Assert
            Assert.NotNull(response);
            Helper.AssertionUserResource(response, "testUser@unitTetst.com", "testUser");
        }

        [Fact]
        public async Task UserManager_GetUserByEmail_Throws_ItemNotFoundException()
        {
            //Arrange
            string email = "test@testEmail.com";
            _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<UserEntity?>(null)!);
            //Act
            var error = await Assert.ThrowsAsync<ItemNotFoundException>(async () =>
            {
                await GetManager().GetUserByEmail(email);
            });

            //Assert
            Assert.NotNull(error);
            Assert.Equal(ValidationMessages.UserWithEmailDoesNotExist(email),
                error.Errors["email"].FirstOrDefault());
        }

        #region Helper
        private void SetUpMocks()
        {
            _mockUnitOfWork.Setup(x => x.UsersRepository
            .GetItemsAsync(
                It.IsAny<IEnumerable<Expression<Func<UserEntity, bool>>>>(),
                It.IsAny<IEnumerable<Expression<Func<UserEntity, object>>>>(),
                It.IsAny<BaseFilter>()))
                .Returns(Task.FromResult(GenerateSampleModels.GenerateUserEntities()));

            _mockUnitOfWork.Setup(x => x.UsersRepository
            .CountAsync(It.IsAny<IEnumerable<Expression<Func<UserEntity, bool>>>>()))
                .Returns(Task.FromResult(2));
            var identity = new GenericIdentity("id", "test-user");
            var contextUser = new ClaimsPrincipal(identity); //add claims as needed

            var context = new DefaultHttpContext()
            {
                User = contextUser,
            };

            _mockHttpContextAccessor.Setup(x => x.HttpContext)
                .Returns(context);

            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(GenerateSampleModels.GenerateUserEntities().FirstOrDefault()!));

            _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
               .Returns(Task.FromResult(GenerateSampleModels.GenerateUserEntities().FirstOrDefault()!));
        }

        private UsersManager GetManager() =>
            new(_mockUserManager.Object,
                _mockUnitOfWork.Object,
                _mockHttpContextAccessor.Object);
        #endregion
    }
}
