using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLayer.Models
{
    public class UserData
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Salt { get; set; }

        public string Hash { get; set; }

        public RoleData Role { get; set; }
    }
}
