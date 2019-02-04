﻿using AutoMapper;
using DataAccessLayer.Models;
using System;

namespace BusinessSolutionsLayer.Models
{
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }

    public enum RoleId
    {
        Admin = 1,
        User
    }
}