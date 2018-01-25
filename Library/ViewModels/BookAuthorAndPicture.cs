using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Library.Models;

namespace Library.ViewModels
{
    public class BookAuthorAndPicture
    {
        public Book Book { get; set; }
        public string AuthorName { get; set; }

        public HttpPostedFileBase Picture { get; set; }
    }
}
