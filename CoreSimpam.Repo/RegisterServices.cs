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
            return services;
        }
    }
}
