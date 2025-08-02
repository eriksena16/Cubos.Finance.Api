using Cubos.Finance.Application;
using Cubos.Finance.Data;
using Cubos.Finance.External;


namespace Cubos.Finance.Api
{
    public static class FinanceServiceLocator
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            #region Services
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IComplianceFacade, ComplianceFacade>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPeopleService, PeopleService>();
            services.AddScoped<IBankAccountService, BankAccountService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<CompliceAuthHandler>();

            #endregion

            services.AddScoped<IPeopleRepository, PeopleRepository>();
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
        }
    }
}
