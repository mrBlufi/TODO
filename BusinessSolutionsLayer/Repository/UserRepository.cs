using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BusinessSolutionsLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessSolutionsLayer
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper mapper;

        public UserRepository(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public void Add(User user)
        {
            using (var context = new ApplicationContext())
            {
                context.Entry(mapper.Map<UserData>(user)).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public IEnumerable<User> Get(Expression<Func<UserData, bool>> func)
        {
            using (var context = new ApplicationContext())
            {
                return mapper.Map<IEnumerable<User>>(context.Users.AsNoTracking().Where(func));
            }
        }

        public void Update(User user)
        {
            using (var context = new ApplicationContext())
            {
                context.Attach(mapper.Map<UserData>(user)).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
