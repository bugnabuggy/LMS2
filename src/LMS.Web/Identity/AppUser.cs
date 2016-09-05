using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LMS.Web.Identity
{
    public class AppUser : IdentityUser
    {
        public Guid LmsUserId { get; set; }
    }
}
