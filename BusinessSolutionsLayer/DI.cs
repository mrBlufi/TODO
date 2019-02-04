using BusinessSolutionsLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessSolutionsLayer
{
    public static class DI
    {
        public static void Inject(IServiceCollection services)
        {
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<ICrytpoService, CrytpoService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITaskRepository, TaskRepository>();
        }
    }
}
