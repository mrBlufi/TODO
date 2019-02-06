using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BusinessSolutionsLayer.Models;
using BusinessSolutionsLayer.Repository;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessSolutionsLayer.Services
{
    public class SessionService : ISessionService
    {
        private readonly IRepository<SessionData> repository;

        private readonly IMapper mapper;

        public SessionService(IRepository<SessionData> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public Session Create(User user, DateTime experationTime)
        {
            var session = new Session()
            {
                Id = Guid.NewGuid(),
                User = user,
                ExperationTime = experationTime,
                CreatingTime = DateTime.Now
            };

            repository.Add(mapper.Map<SessionData>(session));

            return session;
        }

        public void Expire(Session session)
        {
            var sessionData = repository.Get(x => x.Id == session.Id && x.User.Id == session.User.Id && x.ExperationTime > DateTime.Now)
                .FirstOrDefault();

            sessionData.ExperationTime = session.ExperationTime = DateTime.Now;

            repository.Update(sessionData);
        }

        public Session Get(Guid id)
        {
            return mapper.Map<Session>(repository.Get(x => x.Id == id, i => i.User.Role).FirstOrDefault());
        }

        public bool IsExpired(Session session)
        {
            return !repository.Get(x => x.Id == session.Id && x.User.Id == session.User.Id && x.ExperationTime > DateTime.Now).Any();
        }
    }
}
