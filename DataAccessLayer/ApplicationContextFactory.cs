using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class ApplicationContextFactory : IApplicationContextFactory
    {
        private readonly DbContextOptions options;

        public ApplicationContextFactory(DbContextOptions options)
        {
            this.options = options;
        }

        public ApplicationContext GetContext()
        {
            var context = new ApplicationContext(options);
            return context;
        }
    }
}
