using BusinessLogic.Users.Mapper;
using Contracts.Constants;
using Contracts.GlobalExeptionHandler.Exceptions;
using Contracts.V1.SharedModels;
using Contracts.V1.User.Filters;
using Contracts.V1.User.Models;
using Contracts.V1.User.Resources;
using DataAccess.SQL.Entities;
using DataAccess.SQL.Enums;
using DataAccess.SQL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace BusinessLogic.Users
{
    public interface IUsersManager
    {
        public Task<ResourceCollection<UserResource>> GetAllUsers(UserListFilter? filter);
        public Task<UserResource> GetUserByEmail(string email);
        public Task<AuthenticationResource> LogIn(LoginModel model);
        public Task<UserResource> RegisterUser(RegisterModel model);
        public Task<UserResource> UpdateUser(string email, RegisterModel model);
        public Task<IEnumerable<string>> GrantPermission(string email, IEnumerable<RoleType> roles);
        public Task<IEnumerable<string>> RevokePermission(string email, IEnumerable<RoleType> roles);
        public Task DeleteUser(string email);
    }

    public class UsersManager: IUsersManager
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string UserName;

        public UsersManager(
            UserManager<UserEntity> userManager,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            UserName = _httpContextAccessor.HttpContext.User?.FindFirst("userName")?.Value ?? string.Empty;

        }

        public async Task<ResourceCollection<UserResource>> GetAllUsers(UserListFilter? filter)
        {
            var users = (await _unitOfWork.UsersRepository.GetItemsAsync(GetExpressions(filter), null, filter))
                .Select( (item) => item.ToResource((_userManager.GetRolesAsync(item).Result)))
                .ToList();


            return new ResourceCollection<UserResource>(users,
                await _unitOfWork.UsersRepository.CountAsync(GetExpressions(filter)));
        }

        public async Task<UserResource> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                throw new ItemNotFoundException(nameof(email), ValidationMessages.UserWithEmailDoesNotExist(email));
            var roles = await _userManager.GetRolesAsync(user);

            return user.ToResource(roles);
        }

        public async Task<AuthenticationResource> LogIn(LoginModel model)
        {
            var userEntity = await _userManager.FindByEmailAsync(model.Email);

            if(userEntity is null)
            {
                throw new ItemNotFoundException(nameof(model.Email),
                    ValidationMessages.UserWithEmailDoesNotExist(model.Email));
            }

            bool validatePassword = await _userManager.CheckPasswordAsync(userEntity, model.Password);

            if(!validatePassword)
            {
                throw new InvalidModelException(nameof(model.Password), ValidationMessages.InCorrectPasswordsMessage);
            }

            var roles = await _userManager.GetRolesAsync(userEntity);
            var (token, expireAt) = GenerateJwtToken(userEntity, roles);
            return new()
            {
                Token = token,
                ExpireAt = expireAt
            };
        }

        public async Task<UserResource> RegisterUser(RegisterModel model)
        {
            if ((await _userManager.FindByEmailAsync(model.Email)) is not null ||
                (await _userManager.FindByNameAsync(model.UserName)) is not null)
                throw new InvalidModelException(nameof(model),
                    ValidationMessages.UserWithEmailOrUserNameAlreadyExist(model.Email, model.UserName));


           var creationResult =  await _userManager.CreateAsync(model.ToEntity()
               .MapEntityTrackableInformation(UserName, DateTimeOffset.Now), model.Password);

           if(creationResult.Errors.Any())
            {
                throw new InvalidModelException(nameof(model), creationResult.Errors.FirstOrDefault()!.Description);
            }

            var userEntity = (await _userManager.FindByEmailAsync(model.Email));

            return userEntity.ToResource();
        }

        public async Task<UserResource> UpdateUser(string email, RegisterModel model)
        {
            var updateModel = await _userManager.FindByEmailAsync(email);
            if (updateModel is null)
                throw new InvalidModelException(nameof(email),
                    ValidationMessages.UserWithEmailDoesNotExist(email));


            updateModel.UserName = model.UserName;
            updateModel.Email = model.Email;

            var updatingResult = await _userManager.UpdateAsync(updateModel
                .MapEntityTrackableInformation(
                updateModel.CreatedBy,
                updateModel.CreatedOn,
                UserName));

            if (updatingResult.Errors.Any())
            {
                throw new InvalidModelException(nameof(model), updatingResult.Errors.FirstOrDefault()!.Description);
            }

            updatingResult = await _userManager.RemovePasswordAsync(updateModel);
            if (updatingResult.Errors.Any())
            {
                throw new InvalidModelException(nameof(model), updatingResult.Errors.FirstOrDefault()!.Description);
            }

            updatingResult = await _userManager.AddPasswordAsync(updateModel, model.Password);

            if (updatingResult.Errors.Any())
            {
                throw new InvalidModelException(nameof(model), updatingResult.Errors.FirstOrDefault()!.Description);
            }

            var userEntity = (await _userManager.FindByEmailAsync(model.Email));

            return userEntity.ToResource(await _userManager.GetRolesAsync(userEntity));
        }

        public async Task<IEnumerable<string>> GrantPermission(string email, IEnumerable<RoleType> roles)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                throw new ItemNotFoundException(nameof(email),
                    ValidationMessages.UserWithEmailDoesNotExist(email));

            ValidateUserPermissions(roles);

            foreach (var roleName in roles)
            {
                await _userManager.AddToRoleAsync(user, roleName.GetDescription());
            }

            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IEnumerable<string>> RevokePermission(string email, IEnumerable<RoleType> roles)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                throw new ItemNotFoundException(nameof(email),
                    ValidationMessages.UserWithEmailDoesNotExist(email));

            ValidateUserPermissions(roles);

            foreach (var roleName in roles)
            {
                await _userManager.RemoveFromRoleAsync(user, roleName.GetDescription());
            }

            return await _userManager.GetRolesAsync(user);
        }

        public async Task DeleteUser(string email)
        {
            var userEntity = await _userManager.FindByEmailAsync(email);

            if (userEntity is null)
            {
                throw new InvalidModelException(nameof(email),
                    ValidationMessages.UserWithEmailDoesNotExist(email));
            }

            await _userManager.DeleteAsync(userEntity);
        }

        private void ValidateUserPermissions(IEnumerable<RoleType> roles)
        {
            if (roles.Any(x => x == RoleType.Admin) &&
                            !_httpContextAccessor.HttpContext.User.IsInRole(RoleType.Admin.GetDescription()))
                throw new ForbiddenException("user", ValidationMessages.UserDoesNotHaveEnoughPermission);
        }

        private static (string, DateTimeOffset) GenerateJwtToken(UserEntity user, IEnumerable<string> roles)
        {
            var jwtHander = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes("nBPwRGs83PJPdaqvLeK55WyZE596Sttt");

            var claims = new List<Claim>
            {
                new Claim("id", user.Id),
                    new Claim("userName", user.UserName),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tolekDescriper = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtHander.CreateToken(tolekDescriper);
            return (jwtHander.WriteToken(token), token.ValidTo);
        }

        private static List<Expression<Func<UserEntity, bool>>> GetExpressions(UserListFilter? filter)
        {
            List<Expression<Func<UserEntity, bool>>> experssions = new();
            if (filter is null) return experssions;

            if (filter.Emails.Any())
            {
                experssions.Add(item =>  filter.Emails.Any(x => x == item.Email));
            }

            return experssions;
        }
    }
}
