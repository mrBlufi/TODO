using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessSolutionsLayer.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Salt { get; set; }

        public string Hash { get; set; }

        public RoleId Role { get; set; }

        public string Password { get; set; }
    }
}
