﻿using Auth.API.Enums;

namespace Auth.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserStatus Status { get; set; }
        public bool IsAdmin { get; set; }
    }
}
