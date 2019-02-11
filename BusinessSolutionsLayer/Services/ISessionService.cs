using BusinessSolutionsLayer.Models;
using System;

namespace BusinessSolutionsLayer.Services
{
    public interface ISessionService
    {
        Session Create(User user, DateTime experationTime);

        Session Get(Guid id);

        bool IsExpired(Session session);

        void Expire(Session session);
    }
}
