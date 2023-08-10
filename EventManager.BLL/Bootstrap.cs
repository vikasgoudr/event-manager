using EventManager.BLL.Contracts;
using EventManager.BLL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventManager.BLL;

public class Bootstrap
{
    public static void Configure(IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<IEventService, EventService>();
        services.AddTransient<IQuestionService, QuestionService>();
        services.AddTransient<IAnswerService, AnswerService>();
        services.AddTransient<IPaymentTierService,PaymentTierService>();
        services.AddTransient<ITicketService, TicketService>();
        DAL.Bootstrap.Configure(services, configuration);
    }
}