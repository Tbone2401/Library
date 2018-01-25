using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Models;

namespace Library.DataContexts
{
    public class PicturesDb : DbContext
    {
        public PicturesDb() : 
            base("DefaultConnection")
        {
            
        }
        public DbSet<Picture> Pictures { get; set; }
    }
}
