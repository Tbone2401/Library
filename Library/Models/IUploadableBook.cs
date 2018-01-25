using System.Web;

namespace Library.Models
{
    public interface IUploadableBook
    {
        Book Book { get; set; }
        HttpPostedFileBase Photo { get; set; }
    }
}