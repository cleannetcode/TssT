﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TssT.Core.Models;

namespace TssT.Core.Interfaces
{
    public interface IUserService
    {
        public Task<User> GetByNameAndPassword(string userName, string userPassword);
    }
}
