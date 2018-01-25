using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Library.Models;

namespace Library.DataContexts
{
    public class AuthorDb : DbContext
    {
        public AuthorDb() : 
            base("DefaultConnection")
        {

        }
        public DbSet<Author> Authors { get; set; }
    }
}