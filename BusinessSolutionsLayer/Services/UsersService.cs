using AutoMapper;
using BusinessSolutionsLayer.Models;
using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BusinessSolutionsLayer.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository userRepository;

        private readonly ICrytpoService crytpoService;

        public UsersService(IUserRepository userRepository, ICrytpoService crytpoService)
        {
            this.userRepository = userRepository;
            this.crytpoService = crytpoService;
        }

        public User AddUser(User user)
        {
            user.Role = RoleId.User;
            user.Salt = crytpoService.GenerateSalt(150);
            user.Hash = crytpoService.GetHash(user.Password + user.Salt);
            userRepository.Add(user);

            return user;
        }

        public string GetSignature(string login)
        {
            var user = userRepository.Get(x => x.Login.Equals(login) || x.Email.Equals(login))
                .FirstOrDefault();

            return crytpoService.GetHash(user.Login + user.Salt);
        }

        public bool IdentUser(User user)
        {
            user = userRepository.Get(x => x.Login.Equals(user.Login) || x.Email.Equals(user.Email))
                .FirstOrDefault();

            if (user != null && crytpoService.GetHash(user.Password + user.Salt) == user.Hash)
            {
                return true;
            }

            return false;
        }

        public void UpdateUser(User user)
        {
            try
            {
                userRepository.Update(user);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
