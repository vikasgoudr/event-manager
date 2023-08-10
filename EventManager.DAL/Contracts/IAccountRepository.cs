using EventManager.DAL.Entities;
using EventManager.Models.DTO;
using EventManager.Models.Models;
using EventManager.Util.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.DAL.Contracts;
/// <summary>
/// This interface is used to perform database operations on AppUser Model
/// </summary>
public interface IAccountRepository
{
    /// <summary>
    /// Method to Register New User
    /// </summary>
    /// <param name="user">User Object</param>
    /// <returns>Boolean indicating Registered or not</returns>
    public Task<bool> RegisterUser(RegisterUser user);
    /// <summary>
    /// Registers a new user in the system using Google account credentials
    /// </summary>
    /// <param name="user">User Object</param>
    /// <returns>Boolean indicating Registered or not</returns>
    public Task<bool> RegisterUsingGoogle(RegistrationUsingGoogle user);
    /// <summary>
    /// Checks valid user or not by using user object
    /// </summary>
    /// <param name="user">User Object</param>
    /// <returns>Boolean indicating login successful or not</returns>
    public Task<bool> Login(LoginUser user);
    /// <summary>
    /// Generates a token for valid user using his/her login credentials
    /// </summary>
    /// <param name="user">Login Object</param>
    /// <returns>Provides a token in string format</returns>
    public Task<string> GenerateTokenString(LoginUser user);
    /// <summary>
    /// Method used to get list of all users with the role Organiser
    /// </summary>
    /// <param name="pagerSettings">PagerSettings used to paginate the results</param>
    /// <returns>List of users with pagination</returns>
    public Task<PagedList<AppUser>> GetAllOrganisers(PagerSettings pagerSettings);
    /// <summary>
    /// Method used to change status of organiser approval status
    /// </summary>
    /// <param name="data">OrganiserApprovalStatus model</param>
    /// <returns>Boolean indicating status changed successfully or not</returns>
    public Task<bool> ChangeOrganiserApprovalStatus(OrganiserApprovalStatus data);
    /// <summary>
    /// Method used to get event organiser with specified id
    /// </summary>
    /// <param name="id">Organiser id</param>
    /// <returns>organiser deatils provided</returns>
    public Task<AppUser> GetOrganiser(int id);
    /// <summary>
    /// Method used to get list of all users with the role user
    /// </summary>
    /// <param name="pagerSettings">PagerSettings used to paginate the results</param>
    /// <returns>List of users with pagination</returns>
    public Task<PagedList<AppUser>> GetAllUsers(PagerSettings pagerSettings);
    /// <summary>
    /// Method used to check email is already exists
    /// </summary>
    /// <param name="email">Checks using email of the individual</param>
    /// <returns>Boolean indicating email exists or not</returns>
    public Task<bool> EmailAlreadyExists(string email);
    /// <summary>
    /// Method used to get complete profile details of the user
    /// </summary>
    /// <param name="id">user id</param>
    /// <returns>Returns user details by using UserProfileDTO</returns>
    public Task<UserProfileDTO> GetUserProfile(int id);
    /// <summary>
    /// Method used to change password of the user
    /// </summary>
    /// <param name="changePassword">ChangePassword Model</param>
    /// <returns>Returns boolean indicating password successfully changed or not</returns>
    public Task<bool> ChangePassword(ChangePassword changePassword);
    /// <summary>
    /// Method used to update the profile details of the user
    /// </summary>
    /// <param name="user">Profile is used to update the details</param>
    /// <returns>Returns user details by using UserProfileDTO</returns>
    public Task<UserProfileDTO> UpdateProfile(UserProfileDTO user);
}
