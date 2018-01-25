using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Library.DataContexts
{

    public class IdentityDb : IdentityDbContext<ApplicationUser>
    {
        public IdentityDb()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static IdentityDb Create()
        {
            return new IdentityDb();
        }
    }
}