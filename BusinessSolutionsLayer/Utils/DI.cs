using BusinessSolutionsLayer.Repository;
using BusinessSolutionsLayer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessSolutionsLayer
{
    public static class DI
    {
        public static void Inject(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<ICrytpoService, CrytpoService>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<ITaskService, TaskService>();
            services.AddTransient<IFileService>(f => new FileService(configuration.GetValue<string>("TempFolder")));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
