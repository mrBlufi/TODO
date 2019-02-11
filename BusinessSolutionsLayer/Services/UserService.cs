using AutoMapper;
using BusinessSolutionsLayer.Models;
using BusinessSolutionsLayer.Repository;
using DataAccessLayer.Models;
using System;
using System.Linq;

namespace BusinessSolutionsLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserData> userRepository;

        private readonly IRepository<RoleData> roleRepository;

        private readonly ICurrentUser currentUser;

        private readonly ICrytpoService crytpoService;

        private readonly IMapper mapper;

        public UserService(IRepository<UserData> userRepository, IRepository<RoleData> roleRepository, ICurrentUser currentUser, ICrytpoService crytpoService, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.currentUser = currentUser;
            this.crytpoService = crytpoService;
            this.mapper = mapper;
        }

        public void AddUser(User user)
        {
            var userData = new UserData()
            {
                Login = user.Login,
                Email = user.Email,
                Role = roleRepository.Get(x => x.Id == (int)RoleId.User).First(),
                Salt = crytpoService.GenerateSalt(150)
            };

            userData.Hash = crytpoService.GetHash(user.Password + userData.Salt);

            userRepository.Add(userData);
        }

        public void Authorize(Guid userId)
        {
            if(currentUser?.CurrentUser.Id != userId)
            {
                currentUser.SetUser(userRepository.Get(x => x.Id == userId).First());
            }
        }

        public User Get(string login)
        {
            return mapper.Map<User>(userRepository.Get(x => x.Login == login || x.Email == login).First());

        }

        public User Get(Guid id)
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
