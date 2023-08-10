using EventManager.DAL.Context;
using EventManager.DAL.Contracts;
using EventManager.DAL.Entities;
using EventManager.Models.DTO;
using EventManager.Models.Enums;
using EventManager.Models.Models;
using EventManager.Util.ExtensionMethods;
using EventManager.Util.Extensions;
using EventManager.Util.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventManager.DAL.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _config;
    private readonly EventManagerDbContext _dbContext;
    private readonly RoleManager<AppRole> _roleManager;
    public AccountRepository(UserManager<AppUser> userManager, IConfiguration config, EventManagerDbContext context, RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _config = config;
        _dbContext = context;
        _roleManager = roleManager;
    }
    public async Task<bool> RegisterUser(RegisterUser user)
    {
        var identityUser = new AppUser
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Email = user.Email,
            Age = user.Age,
            PhoneNumber = user.PhoneNumber,
            Gender = user.Gender,
            ApprovalStatus=3
        };
        var result = await _userManager.CreateAsync(identityUser, user.Password);
        if (result.Succeeded)
        {
            var enumKey = ((RoleEnum)user.Role).GetDisplayName();
            var role = await _roleManager.FindByNameAsync(enumKey);
            var userByEmail = await _userManager.FindByEmailAsync(identityUser.Email);
            var userRole = await _userManager.AddToRoleAsync(userByEmail, role.Name);

        }
        return (result.Succeeded);
    }

    public async Task<bool> Login(LoginUser user)
    {
        var identityUser = await _userManager.FindByEmailAsync(user.Email);
        if (identityUser is null || identityUser.IsDeleted==true)
        {
            return false;
        }

        return await _userManager.CheckPasswordAsync(identityUser, user.Password);
    }

    public async Task<string> GenerateTokenString(LoginUser user)
    {
        var UserData = _dbContext.Users.FirstOrDefault(x => x.Email == user.Email);
        var RoleData = _dbContext.UserRoles.FirstOrDefault(x => x.UserId == UserData.Id);

        var claims = new List<Claim>
            {
                new Claim("UserId",UserData.Id+""),
                new Claim("Email",user.Email.ToString()),
                new Claim("Role",RoleData.RoleId.ToString()),
                new Claim("Name",UserData.UserName.ToString()),
                new Claim ("Age",UserData.Age.ToString()),
                new Claim("Gender",UserData.Gender.ToString()),
                new Claim("ApprovalStatus",UserData.ApprovalStatus.ToString())
            };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value!));

        SigningCredentials signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

        var securityToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(20),
            issuer: _config.GetSection("Jwt:Issuer").Value,
            audience: _config.GetSection("Jwt:Audience").Value,
            signingCredentials: signingCred);

        string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return tokenString;
    }
    public async Task<PagedList<AppUser>> GetAllOrganisers(PagerSettings pagerSettings)
    {
        var organizers = await _dbContext.UserRoles.Where(x => x.RoleId == (int)RoleEnum.EventOrganizer).Select(x => x.UserId).ToListAsync();
        var users = await _dbContext.Users.Where(x => !x.IsDeleted && organizers.Any(y => y == x.Id)).ToPagedListAsync(pagerSettings);
        return users;
    }

    public async Task<bool> ChangeOrganiserApprovalStatus(OrganiserApprovalStatus data)
    {
        var user = _userManager.FindByIdAsync(data.Id.ToString());
        user.Result.ApprovalStatus = data.Status;
        var result = await _userManager.UpdateAsync(user.Result);
        if (result.Succeeded)
        {
            return true;
        }
        return false;
    }

    public async Task<AppUser> GetOrganiser(int id)
    {
        //return _userManager.FindByIdAsync(id.ToString());
        var organisers = _userManager.GetUsersInRoleAsync("Organiser").Result;
        var user = organisers.FirstOrDefault(x => x.Id == id);
        if(user == null)
        {
            return null;
        }
        return user;
    }

    public async Task<PagedList<AppUser>> GetAllUsers(PagerSettings pagerSettings)
    {
        var users = await _dbContext.UserRoles.Where(x => x.RoleId == (int)RoleEnum.User).Select(x=>x.UserId).ToListAsync();
        var data = await _dbContext.Users.Where(x => !x.IsDeleted && users.Any(y => y == x.Id)).ToPagedListAsync(pagerSettings);
        return data;
    }

    public async Task<bool> EmailAlreadyExists(string email)
    {
        if (email != null)
        {
            var user = await _dbContext.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            Console.WriteLine(user);
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public async Task<UserProfileDTO> GetUserProfile(int id)
    {
        var result = new UserProfileDTO();
        var user = _dbContext.Users.Where(x=>x.Id==id).FirstOrDefault();
        if(user != null)
        {
            result.Id=user.Id;
            result.FirstName=user.FirstName;
            result.LastName=user.LastName;
            result.Gender=user.Gender;
            result.DisplayPicture=user.DisplayPicture;
            result.Age=user.Age;
            result.UserName=user.UserName;
            result.PhoneNumber = user.PhoneNumber;
            result.Email = user.Email;
            var userRole = await _dbContext.UserRoles.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();
            if (userRole != null)
            {
                result.Role = userRole.RoleId;
            }
            //var address = _dbContext.Addresses.Where(x => x.Id == user.AddressId).FirstOrDefaultAsync();
            //if(address!= null)
            //{
            //    result.Address = address as AddressDTO;
            //}
            return result;
        }
        return null;
    }

    public async Task<bool> RegisterUsingGoogle(RegistrationUsingGoogle user)
    {
        var identityUser = new AppUser
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Email = user.Email,
            Age = user.Age,
            PhoneNumber = user.PhoneNumber,
            Gender = user.Gender,
            ApprovalStatus = 3,
            DisplayPicture=user.DisplayPicture
        };
        var result = await _userManager.CreateAsync(identityUser, user.Password);
        foreach (var item in result.Errors)
        {
            Console.WriteLine(item.Code + " "+item.Description);
        }
        if (result.Succeeded)
        {
            var enumKey = ((RoleEnum)user.Role).GetDisplayName();
            var role = await _roleManager.FindByNameAsync(enumKey);
            var userByEmail = await _userManager.FindByEmailAsync(identityUser.Email);
            var userRole = await _userManager.AddToRoleAsync(userByEmail, role.Name);

        }
        return (result.Succeeded);
    }

    public async Task<bool> ChangePassword(ChangePassword changePassword)
    {

        var user = await _dbContext.Users.Where(x => x.Id == changePassword.Id && x.Email == changePassword.Email).FirstOrDefaultAsync();
        if (user != null)
        {
            //user.PasswordHash = changePassword.NewPassword;
            //_dbContext.SaveChanges();
            await _userManager.ChangePasswordAsync(user, changePassword.CurrentPassword, changePassword.NewPassword);
            return true;
        }
        return false;
    }

    public async Task<UserProfileDTO> UpdateProfile(UserProfileDTO userProfile)
    {
        var user = await _dbContext.Users.Where(x => x.Id == userProfile.Id).FirstOrDefaultAsync();
        if (user != null)
        {
            user.FirstName=userProfile.FirstName;
            user.LastName=userProfile.LastName;
            user.Gender=userProfile.Gender;
            user.UserName=userProfile.UserName;
            user.PhoneNumber=userProfile.PhoneNumber;
            user.Age=userProfile.Age;
            user.DisplayPicture = userProfile.DisplayPicture;
            //user.Address = userProfile.Address;
            _dbContext.SaveChanges();
            return userProfile;
        }
        return null;
    }
}
