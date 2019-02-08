using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public class ApplicationContextFactory : IApplicationContextFactory
    {
        public ApplicationContext GetContext()
        {
            var context = new ApplicationContext();
            context.ChangeTracker.AutoDetectChangesEnabled = false;
            context.ChangeTracker.LazyLoadingEnabled = false;
            return context;
        }
    }
}
