using Microsoft.AspNetCore.Identity;

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
