using BusinessSolutionsLayer.Models;
using System;

namespace BusinessSolutionsLayer.Services
{
    public interface IUserService
    {
        bool IdentUser(User user);

        User Get(Guid id);

        User Get(string login);

        void UpdateUser(User user);

        void AddUser(User user);
    }
}
