﻿using server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace server.Dto
{
    public class SaveUserDto : UserDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }
    }

    public class UserDto
    {
        public Guid Id { set; get; }
        public int Dni { set; get; }
        public string UserName { set; get; }
        public string PhoneNumber { set; get; }
        public string Token { get; set; }
    }

    public class ModifyUserDto
    {
        public int Dni { set; get; }
        public string UserName { set; get; }
        public string PhoneNumber { set; get; }
        public List<RoleWhenModifyUser> RolesUser { set; get; }
    }

    public class MofidyUserCommingFromClientDto : ModifyUserDto
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
    }

    public class createUserDto : ModifyUserDto
    {
        public string Password { get; set; }
    }

    public class UserAuthenticationDto
    {
        public string Token { get; set; }
    }
}
