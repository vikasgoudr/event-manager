using EventManager.DAL.Context;
using EventManager.DAL.Contracts;
using EventManager.DAL.Entities;
using EventManager.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventManager.DAL
{
    public class Bootstrap
    {
        public static void Configure(IServiceCollection services, ConfigurationManager configuration)
        {

            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<IAnswerRepository, AnswerRepository>();
            services.AddTransient<IPaymentTierRepository,PaymentTierRepository>();
            services.AddTransient<ITicketRepository,TicketRepository>();
            services.AddDbContext<EventManagerDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetSection("ConnectionStrings:DefaultConnection").Value);
            });
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Lockout.MaxFailedAccessAttempts = Convert.ToInt32(configuration.GetSection("AuthSettings:MaxFailedAccessAttempts").Value);
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(Convert.ToDouble(configuration.GetSection("AuthSettings:DefaultLockoutTimeSpanInMinutes").Value));
                options.Password.RequireDigit = Convert.ToBoolean(configuration.GetSection("PasswordSettings:RequireDigit").Value);
                options.Password.RequiredLength = Convert.ToInt32(configuration.GetSection("PasswordSettings:RequiredLength").Value);
                options.Password.RequireLowercase = Convert.ToBoolean(configuration.GetSection("PasswordSettings:RequireLowercase").Value);
                options.Password.RequireUppercase = Convert.ToBoolean(configuration.GetSection("PasswordSettings:RequireUppercase").Value);
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<EventManagerDbContext>()
            .AddRoles<AppRole>()
            .AddDefaultTokenProviders();
           
        }
    }
}