using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Models;

namespace BusinessSolutionsLayer.Services
{
    public class CurrentUserService : ICurrentUser
    {
        public UserData CurrentUser { get; private set; }

        public void SetUser(UserData user)
        {
            CurrentUser = user;
        }
    }
}
