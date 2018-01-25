using System.Web;

namespace Library.Models
{
    public class UploadableBook : IUploadableBook
    {
        public Book Book { get; set; }
        public HttpPostedFileBase Photo { get; set; }
    }
}