using BusinessSolutionsLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessSolutionsLayer.Services
{
    public interface IUsersService
    {
        bool IdentUser(User user);

        User GetById(Guid id);

        void UpdateUser(User user);

        User AddUser(User user);
    }
}
