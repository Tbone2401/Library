using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Models;

namespace Library.DataContexts
{
    public class BooksDb : DbContext
    {
        public BooksDb() : 
            base("DefaultConnection")
        {
            
        }
        public DbSet<Book> Books { get; set; }
    }
}
