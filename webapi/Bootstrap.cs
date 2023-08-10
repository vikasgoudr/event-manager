using EventManager.BLL.Contracts;
using EventManager.BLL.Services;
using EventManager.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace EventManager.API
{
    public class Bootstrap
    {
        public static void Configure(IServiceCollection services, ConfigurationManager configuration)
        {
            BLL.Bootstrap.Configure(services, configuration);
        }
    }
}
