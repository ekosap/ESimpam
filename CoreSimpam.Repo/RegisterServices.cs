using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSimpam.Repo
{
    public static class RegisterServices
    {
        public static IServiceCollection Register(this IServiceCollection services)
        {
            services.AddTransient<IRoleRepo, RoleRepo>();
            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<IScreenRepo, ScreenRepo>();
            return services;
        }
    }
}
