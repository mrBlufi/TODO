using BusinessSolutionsLayer.Models;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BusinessSolutionsLayer
{
    public interface IUserRepository
    {
        void Add(User user);
        IEnumerable<User> Get(Expression<Func<UserData, bool>> func);
        void Update(User user);
    }
}
