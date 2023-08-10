using EventManager.Models.DTO;
using EventManager.Models.Models;
using EventManager.Util.Models;
using Microsoft.AspNetCore.Identity;

namespace EventManager.BLL.Contracts
{
    /// <summary>
    /// The IAccountService interface defines methods for managing user accounts and profiles.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="user">The RegisterUser object containing user information.</param>
        /// <returns>True if the user is registered successfully, otherwise false.</returns>
        public Task<bool> RegisterUser(RegisterUser user);

        /// <summary>
        /// Registers a new user using Google authentication.
        /// </summary>
        /// <param name="user">The RegistrationUsingGoogle object containing user information from Google authentication.</param>
        /// <returns>True if the user is registered successfully, otherwise false.</returns>
        public Task<bool> RegisterUsingGoogle(RegistrationUsingGoogle user);

        /// <summary>
        /// Performs user login.
        /// </summary>
        /// <param name="user">The LoginUser object containing user login credentials.</param>
        /// <returns>True if the login is successful, otherwise false.</returns>
        public Task<bool> Login(LoginUser user);

        /// <summary>
        /// Generates a token string for the given user to be used for authentication.
        /// </summary>
        /// <param name="user">The LoginUser object for which the token is to be generated.</param>
        /// <returns>The generated token string.</returns>
        public Task<string> GenerateTokenString(LoginUser user);

        /// <summary>
        /// Retrieves a paged list of organizers.
        /// </summary>
        /// <param name="pagerSettings">PagerSettings object for pagination.</param>
        /// <returns>A paged list of OrganiserDTO objects.</returns>
        public Task<PagedList<OrganiserDTO>> GetAllOrganisers(PagerSettings pagerSettings);

        /// <summary>
        /// Changes the approval status of an organizer.
        /// </summary>
        /// <param name="data">The OrganiserApprovalStatus object containing the organizer ID and approval status.</param>
        /// <returns>True if the approval status is changed successfully, otherwise false.</returns>
        public Task<bool> ChangeOrganiserApprovalStatus(OrganiserApprovalStatus data);

        /// <summary>
        /// Retrieves an organizer's details by ID.
        /// </summary>
        /// <param name="id">The ID of the organizer to retrieve.</param>
        /// <returns>The OrganiserDTO object representing the organizer's details.</returns>
        public Task<OrganiserDTO> GetOrganiser(int id);

        /// <summary>
        /// Retrieves a paged list of users.
        /// </summary>
        /// <param name="pagerSettings">PagerSettings object for pagination.</param>
        /// <returns>A paged list of UserDTO objects.</returns>
        public Task<PagedList<UserDTO>> GetAllUsers(PagerSettings pagerSettings);

        /// <summary>
        /// Checks if an email address already exists in the system.
        /// </summary>
        /// <param name="email">The email address to check.</param>
        /// <returns>True if the email address already exists, otherwise false.</returns>
        public Task<bool> EmailAlreadyExists(string email);

        /// <summary>
        /// Retrieves the user profile data by ID.
        /// </summary>
        /// <param name="id">The ID of the user for whom the profile data is to be retrieved.</param>
        /// <returns>The UserProfileDTO object representing the user's profile data.</returns>
        public Task<UserProfileDTO> GetUserProfileData(int id);

        /// <summary>
        /// Changes the user's password.
        /// </summary>
        /// <param name="password">The ChangePassword object containing the user's ID and new password.</param>
        /// <returns>True if the password is changed successfully, otherwise false.</returns>
        public Task<bool> ChangePassword(ChangePassword password);

        /// <summary>
        /// Updates the user's profile data.
        /// </summary>
        /// <param name="user">The UserProfileDTO object containing the updated user profile data.</param>
        /// <returns>The updated UserProfileDTO object.</returns>
        public Task<UserProfileDTO> UpdateProfile(UserProfileDTO user);
    }
}
