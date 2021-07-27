using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TssT.API.Contracts
{
    public class UserCredential
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
