using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Library.DataContexts;
using Library.Models;
using Library.ViewModels;

namespace Library.Controllers
{
    public class AuthorsController : Controller
    {
        private AuthorDb Authordb = new AuthorDb();
        private BooksDb BooksDb = new BooksDb();

        // GET: Authors
        public ActionResult Index()
        {
            return View(Authordb.Authors.ToList());
        }

        // GET: Authors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuthorAndBooksViewModel authAndBooks = CreateAuthAndBooksVM(id);
            if (authAndBooks == null)
            {
                return HttpNotFound();
            }
            return View(authAndBooks);
        }

        private AuthorAndBooksViewModel CreateAuthAndBooksVM(int? id)
        {

            Author author = Authordb.Authors.Find(id);
            List<Book> books = FindBooks(author.AuthorId);
            AuthorAndBooksViewModel vm = new AuthorAndBooksViewModel();
            vm.AuthorOfBooks = author;
            vm.BooksOfAuthor = books;
            return vm;
        }

        private List<Book> FindBooks(int authorAuthorId)
        {
            return (from b in BooksDb.Books
                where b.AuthorId == authorAuthorId
                select b).ToList();
        }

        // GET: Authors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,BirthDay")] Author author)
        {
            if (ModelState.IsValid)
            {
                Authordb.Authors.Add(author);
                Authordb.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(author);
        }

        // GET: Authors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = Authordb.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AuthorId,FirstName,LastName,BirthDay")] Author author)
        {
            if (ModelState.IsValid)
            {
                Authordb.Entry(author).State = EntityState.Modified;
                Authordb.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = Authordb.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Author author = Authordb.Authors.Find(id);
            Authordb.Authors.Remove(author);
            Authordb.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Authordb.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
