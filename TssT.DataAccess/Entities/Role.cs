using Microsoft.AspNetCore.Identity;
using System;

namespace TssT.DataAccess.Entities
{
    public class Role : IdentityRole<string>
    {
        public Role()
        {

        }

        public Role(string roleName):base(roleName)
        {

        }
    }
}
