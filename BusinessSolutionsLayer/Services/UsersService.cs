using AutoMapper;
using BusinessSolutionsLayer.Models;
using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BusinessSolutionsLayer.Repository;
using DataAccessLayer.Models;

namespace BusinessSolutionsLayer.Services
{
    public class UsersService : IUsersService
    {
        private readonly IRepository<UserData> userRepository;

        private readonly IRepository<RoleData> roleRepository;

        private readonly ICrytpoService crytpoService;

        private readonly IMapper mapper;

        public UsersService(IRepository<UserData> userRepository, IRepository<RoleData> roleRepository, ICrytpoService crytpoService, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.crytpoService = crytpoService;
            this.mapper = mapper;
        }

        public User AddUser(User user)
        {
            var userData = new UserData()
            {
                Login = user.Login,
                Email = user.Email,
                Role = roleRepository.Get(x => x.Id == (int)RoleId.User).First(),
                Salt = crytpoService.GenerateSalt(150)
            };

            userData.Hash = crytpoService.GetHash(user.Password + userData.Salt);
            
            return mapper.Map<User>(userRepository.Add(userData));
        }

        public User GetById(Guid id)
        {
            return mapper.Map<User>(userRepository.Get(x => x.Id == id).First());
        }

        public bool IdentUser(User user)
        {
            var userData = userRepository.Get(x => x.Login.Equals(user.Login, StringComparison.InvariantCultureIgnoreCase)
            || x.Email.Equals(user.Email, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            if (userData != null && crytpoService.GetHash(user.Password + userData.Salt) == userData.Hash)
            {
                user.Id = userData.Id;
                return true;
            }

            return false;
        }

        public void UpdateUser(User user)
        {
            userRepository.Update(mapper.Map<UserData>(user));
        }
    }
}
