using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessSolutionsLayer.Services
{
    public interface ICurrentUser
    {
        UserData CurrentUser { get; }

        void SetUser(UserData user);
    }
}
