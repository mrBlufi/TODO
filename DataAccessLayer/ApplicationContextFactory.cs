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
            return new ApplicationContext();
        }
    }
}
