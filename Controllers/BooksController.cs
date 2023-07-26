using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using asp_test.Models;
using System.IO;


namespace asp_test.Controllers
{
    public class BooksController : Controller
    {
        private TestASPEntities db = new TestASPEntities();

        // GET: Books
        public async Task<ActionResult> Index()
        {
            return View(await db.Books.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = await db.Books.FindAsync(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[Bind(Include = "BookId,Categories,Publisher,Language,Pages,ISBN_10,ISBN_13,File,Edition,Year,Cover,BookName,Author")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Book book)
        {
            Books books = new Books();
            DateTime dt = DateTime.Now;
            books.BookId = dt.ToString("yyyyMMddhhmmss");
            books.Categories = book.Categories;
            books.BookName = book.BookName;
            books.Year = book.Year;
            books.Edition = book.Edition;
            books.Pages = book.Pages;
            books.Publisher = book.Publisher;
            books.Language = book.Language;
            books.ISBN_10 = book.ISBN_10;
            books.ISBN_13 = book.ISBN_13;
            books.Author = book.Author;
            book.File = Request.Files["files"];
            book.Cover = Request.Files["Cover"];         
            books.File = books.BookId + Path.GetExtension(book.File.FileName);
            books.Cover = books.BookId + Path.GetExtension(book.Cover.FileName);
            book.File.SaveAs(Server.MapPath("~/statics/Books/" + books.File));
            book.Cover.SaveAs(Server.MapPath("~/statics/Cover/" + books.Cover));
            try
            {
                db.Books.Add(books);
                await db.SaveChangesAsync();
                return Redirect("/Home/Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Books/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = await db.Books.FindAsync(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        // POST: Books/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BookId,Categories,Publisher,Language,Pages,ISBN_10,ISBN_13,File,Edition,Year,Cover,BookName,Author")] Books books)
        {
            if (ModelState.IsValid)
            {
                db.Entry(books).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(books);
        }

        // GET: Books/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = await db.Books.FindAsync(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Books books = await db.Books.FindAsync(id);
            db.Books.Remove(books);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult SearchResult(string kw)
        {
            var info = db.Books.Where(model => model.BookName.Contains(kw) |
                                                model.Categories.Contains(kw) |
                                                model.Author.Contains(kw)|
                                                model.ISBN_10.Contains(kw)|
                                                model.ISBN_13.Contains(kw)|
                                                model.Publisher.Contains(kw)
                                                );
                                                
            ViewData["Result"] = kw;
            return View(info);
        }
    }
}
