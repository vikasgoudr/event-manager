using EventManager.BLL.Contracts;
using EventManager.Models.DTO;
using EventManager.Models.Models;
using EventManager.Util.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IAccountService BLservice;
    public UserController(IAccountService _service)
    {
        BLservice = _service;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUser user)
    {
        if (await BLservice.RegisterUser(user))
        {
            return Ok("Successfuly done");
        }
        return BadRequest();
    }

    [HttpPost("register-using-google")]
    public async Task<IActionResult> GoogleRegistration(RegistrationUsingGoogle user)
    {
        if (await BLservice.RegisterUsingGoogle(user))
        {
            return Ok("Successfuly done");
        }
        return BadRequest();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUser user)
    {
        if (!ModelState.IsValid)
        {

            return BadRequest();
        }
        if (await BLservice.Login(user))
        {
            Console.WriteLine("41 " + user.ToString());
            var tokenString = await BLservice.GenerateTokenString(user);
            return Ok(tokenString);
        }
        return BadRequest();
    }

    [HttpPost("all-organisers")]
    public async Task<IActionResult> GetAllOrganisers(PagerSettings pagerSettings)
    {
        var data = await BLservice.GetAllOrganisers(pagerSettings);
        if (data != null)
        {
            return Accepted(data);
        }
        return BadRequest();
    }


    [HttpPost("all-users")]
    public async Task<IActionResult> GetAllUsers(PagerSettings pagerSettings)
    {
        var data = await BLservice.GetAllUsers(pagerSettings);
        if (data != null)
        {
            return Accepted(data);
        }
        return BadRequest();
    }

    [HttpGet("get-organiser")]
    public async Task<IActionResult> GetOrganiser(int id)
    {
        var data = await BLservice.GetOrganiser(id);
        if (data != null)
        {
            return Accepted(data);
        }
        return BadRequest();
    }



    [HttpPut("change-approval-status")]
    public async Task<IActionResult> ChangeOrganiserApprovalStatus([FromBody] OrganiserApprovalStatus data)
    {
        var obj = await BLservice.ChangeOrganiserApprovalStatus(data);
        if(obj!=null)
        {
            return Accepted(obj);
        }
        return BadRequest();
    }
    [HttpPost("email-already-exists")]
    public async Task<IActionResult> EmailAlreadyExists([FromBody]string email)
    {
        var obj = await BLservice.EmailAlreadyExists(email);
        if (obj!=null)
        {
            return Accepted(obj);
        }
        return BadRequest();
    }
    [HttpPost("user-profile")]
    public async Task<IActionResult> GetProfileData([FromBody]int id)
    {
        var data = await BLservice.GetUserProfileData(id);
        if (data != null)
        {
            return Accepted(data);
        }
        return BadRequest();
    }

    [HttpPatch("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePassword changePassword)
    {
        var data = await BLservice.ChangePassword(changePassword);
        if (data!=null)
        {
            return Accepted(data);
        }
        return BadRequest();
    }

    [HttpPatch("update-profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UserProfileDTO profile)
    {
        var data = await BLservice.UpdateProfile(profile);
        if (data != null)
        {
            return Accepted(data);
        }
        return BadRequest();
    }
}
