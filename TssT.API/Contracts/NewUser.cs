using System;
using Microsoft.AspNetCore.Identity;

namespace TssT.API.Contracts
{
    public class NewUser
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}