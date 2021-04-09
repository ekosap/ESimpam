using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSimpam.Repo
{
    public static class RegisterServices
    {
        public static IServiceCollection Register(this IServiceCollection services)
        {
            //service singleton
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            //service scope
            services.AddScoped<ILoginRepo, LoginRepo>();
            //service transient
            services.AddTransient<IRoleRepo, RoleRepo>();
            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<IScreenRepo, ScreenRepo>();
            services.AddTransient<IRoleScreenAccessRepo, RoleScreenAccessRepo>();
            services.AddTransient<IResidentRepo, ResidentRepo>();
            return services;
        }
    }
}
