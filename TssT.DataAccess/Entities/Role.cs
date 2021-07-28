using Microsoft.AspNetCore.Identity;
using System;

namespace TssT.DataAccess.Entities
{
    public class Role : IdentityRole
    {
        public Role()
        {

        }

        public Role(string roleName):base(roleName)
        {

        }
    }
}
