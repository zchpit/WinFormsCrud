﻿using System.ComponentModel.DataAnnotations;
using WinFormsCrud.Dto;

namespace WinFormsCrud.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleDto UserRole { get; set; } = RoleDto.User;
        public bool IsActive { get; set; }

        public List<UserCase> UserCases { get; set; } = new List<UserCase>();
    }
}
