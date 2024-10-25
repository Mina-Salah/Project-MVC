using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO.DAL.Entity
{
    public class AplicationUser : IdentityUser
    {
        public bool IsActive { get; set; }  
    }
}
