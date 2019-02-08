using BusinessSolutionsLayer.Repository;
using BusinessSolutionsLayer.Services;
using DataAccessLayer.Models;
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
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<ITaskService, TaskService>();
            services.AddTransient<IFileService>(f => new FileService(@"D:\Temp"));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
