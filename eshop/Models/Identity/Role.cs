using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Identity
{
    public class Role : IdentityRole<int>
    {
        public Role(string name) : base(name)
        {

        }
    }
}
