using AutoMapper;
using EventManager.BLL.Contracts;
using EventManager.DAL.Contracts;
using EventManager.Models.DTO;
using EventManager.Models.Models;
using EventManager.Util.Models;

namespace EventManager.BLL.Services;

 

public class AccountService : IAccountService
{
    private readonly IAccountRepository authrep;
    private readonly IMapper _mapper;

    public AccountService(IAccountRepository _authrep, IMapper mapper)
    {
        authrep = _authrep;
        _mapper = mapper;
    }

   
   
    public async Task<bool> RegisterUser(RegisterUser user)
    {
        return await authrep.RegisterUser(user);
    }
     
    
    public async Task<bool> Login(LoginUser user)
    {
        return await authrep.Login(user);
    }
 
    public async Task<string> GenerateTokenString(LoginUser user)
    {
        return await authrep.GenerateTokenString(user);
    }


     
    public async Task<PagedList<OrganiserDTO>> GetAllOrganisers(PagerSettings pagerSettings)
    {
        var data =  await authrep.GetAllOrganisers(pagerSettings);
        if (data != null)
        {
            return new PagedList<OrganiserDTO>()
            {
                CurrentPage = data.CurrentPage,
                PageCount = data.PageCount,
                PageSize = data.PageSize,
                RowCount = data.RowCount,
                Data = _mapper.Map<List<OrganiserDTO>>(data.Data)
            };
        }
        return new PagedList<OrganiserDTO>()
        {
            CurrentPage = 1,
            PageCount = 1,
            PageSize = 1,
            RowCount = 0,
            Data = new List<OrganiserDTO>()
        };
    }

 

    public async Task<bool> ChangeOrganiserApprovalStatus(OrganiserApprovalStatus obj)
    {
        var data = await authrep.ChangeOrganiserApprovalStatus(obj);
        return data;
    }

 

    public async Task<OrganiserDTO> GetOrganiser(int id)
    {
        var data = await authrep.GetOrganiser(id);
        return _mapper.Map<OrganiserDTO>(data);
    }


 
   
    public async Task<PagedList<UserDTO>> GetAllUsers(PagerSettings pagerSettings)
    {
        var data = await authrep.GetAllUsers(pagerSettings);
        if (data != null)
        {
            return new PagedList<UserDTO>()
            {
                CurrentPage = data.CurrentPage,
                PageCount = data.PageCount,
                PageSize = data.PageSize,
                RowCount = data.RowCount,
                Data = _mapper.Map<List<UserDTO>>(data.Data)
            };
        }
        return new PagedList<UserDTO>()
        {
            CurrentPage = 1,
            PageCount = 1,
            PageSize = 1,
            RowCount = 0,
            Data = new List<UserDTO>()
        };
    }

     

    public Task<bool> EmailAlreadyExists(string email)
    {
        return authrep.EmailAlreadyExists(email);
    }

 
   
    public async Task<UserProfileDTO> GetUserProfileData(int id)
    {
        return await authrep.GetUserProfile(id);
        //var user = await authrep.GetUserProfile(id);
        //return _mapper.Map<UserProfileDTO>(user);
    }

 
    public async Task<bool> RegisterUsingGoogle(RegistrationUsingGoogle user)
    {
        return await authrep.RegisterUsingGoogle(user);
    }

 
    public async Task<bool> ChangePassword(ChangePassword password)
    {
        return await authrep.ChangePassword(password);
    }

 

    public async Task<UserProfileDTO> UpdateProfile(UserProfileDTO user)
    {
        return await authrep.UpdateProfile(user);
    }
}
