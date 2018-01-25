using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Library.AzureStorage;
using Library.DataContexts;
using Library.Models;
using Library.Validation;
using Library.ViewModels;

namespace Library.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private BooksDb dbBooks = new BooksDb();
        private PicturesDb dbPictures = new PicturesDb();
        private AuthorDb dbAuthor = new AuthorDb();

        ImageServiceTest imageService = new ImageServiceTest();

        public ActionResult Index()
        {
            return View(dbBooks.Books.ToList());
        }

        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return RedirectToAction("Index");
            }
            BookAndAuthorName bookAndAuthorName = new BookAndAuthorName();
            bookAndAuthorName.Book = dbBooks.Books.Find(id);
            if (bookAndAuthorName.Book == null)
            {
                return HttpNotFound();
            }
            Author tempAuthor = dbAuthor.Authors.Find(bookAndAuthorName.Book.AuthorId);
            if (tempAuthor == null)
            {
                return HttpNotFound();
            }
            bookAndAuthorName.AuthorName = String.Format("{0}, {1}", tempAuthor.FirstName, tempAuthor.LastName);

            return View(bookAndAuthorName);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BookAndAuthorName bookAndAuthor, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                var pictureName = await imageService.UploadImageAsync(picture);

                bookAndAuthor.Book.PictureName = pictureName;
                bookAndAuthor.Book.ISBN = bookAndAuthor.Book.ISBN.Replace("-", "").Trim();
                
                bookAndAuthor.Book.AuthorId = dbAuthor.Authors.AsEnumerable()
                    .Where(x => string.Format("{0} {1}",x.FirstName, x.LastName ) == bookAndAuthor.AuthorName)
                    .Single().AuthorId;

                dbBooks.Books.Add(bookAndAuthor.Book);

                dbBooks.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public JsonResult SearchAuthor(string queryString)
        {
            queryString = queryString.ToLower();
            var authorNames = dbAuthor.Authors
                .Where(a => a.FirstName.ToLower().Contains(queryString) ||
                a.LastName.Contains(queryString))
                .Select(n => new { n.FirstName, n.LastName })
                .Take(int.Parse(ConfigurationManager.AppSettings["NrOfSuggestions"])).ToList();
            return Json(authorNames, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Edit(int? id)
        {
            BookAndAuthorName bookAndAuthor = new BookAndAuthorName();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = dbBooks.Books.Find(id);
            Author tempAuthor = dbAuthor.Authors.Find(book.AuthorId);
            bookAndAuthor.Book = book;
            bookAndAuthor.AuthorName = tempAuthor.FirstName + " " + tempAuthor.LastName;

            return View(bookAndAuthor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BookAndAuthorName bookAndAuthorName, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                Book originalBook = dbBooks.Books.AsNoTracking().FirstOrDefault(b => b.ISBN == bookAndAuthorName.Book.ISBN);
                bookAndAuthorName.Book.Id = originalBook.Id;

                if (picture != null)
                {
                    bookAndAuthorName.Book.PictureName = await imageService.UploadImageAsync(picture, originalBook.PictureName);
                }
                if (bookAndAuthorName.Book.PictureName == null)
                {
                    bookAndAuthorName.Book.PictureName = originalBook.PictureName;
                }
                bookAndAuthorName.Book.AuthorId = dbAuthor.Authors.AsEnumerable()
                    .Where(x => string.Format("{0} {1}", x.FirstName, x.LastName) == bookAndAuthorName.AuthorName)
                    .Single().AuthorId;

                dbBooks.Entry(bookAndAuthorName.Book).State = EntityState.Modified;
                dbBooks.SaveChanges();

                return RedirectToAction("Index");
            }
            //TODO add error message if possible
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = dbBooks.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = dbBooks.Books.Find(id);
            dbBooks.Books.Remove(book);
            dbBooks.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var displayName = Path.GetFileName(file.FileName);
                var fileExtension = Path.GetExtension(displayName);
                var fileName = string.Format("{0}{1}", Guid.NewGuid(), fileExtension);
                var path = Path.Combine(
                    Server.MapPath(ConfigurationManager.AppSettings["PhotoUploadPath"]),
                    fileName);
                file.SaveAs(path);
                return Json(new { DisplayName = displayName, Path = path });
            }
            return Json(null);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbBooks.Dispose();
                dbPictures.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
