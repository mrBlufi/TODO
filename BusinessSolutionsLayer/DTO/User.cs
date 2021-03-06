﻿using Newtonsoft.Json;
using System;

namespace BusinessSolutionsLayer.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public RoleId Role { get; set; }

        public string Password { get; set; }
    }
}
