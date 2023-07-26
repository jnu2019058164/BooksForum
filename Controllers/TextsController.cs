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

namespace asp_test.Controllers
{
    public class TextsController : Controller
    {
        private TestASPEntities db = new TestASPEntities();

        // GET: Texts
        public async Task<ActionResult> Index()
        {
            return View(await db.Texts.ToListAsync());
        }

        // GET: Texts/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Texts text = await db.Texts.FindAsync(id);
            if (text == null)
            {
                return HttpNotFound();
            }
            return View(text);
        }

        // GET: Texts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Texts/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Editor,Date,Content")] Texts text)
        {
            if (ModelState.IsValid)
            {
                text.Id = DateTime.Now.ToString("yyyyMMddhhmmss");
                text.Date = DateTime.Now;
                db.Texts.Add(text);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(text);
        }

        // GET: Texts/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Texts text = await db.Texts.FindAsync(id);
            if (text == null)
            {
                return HttpNotFound();
            }
            return View(text);
        }

        // POST: Texts/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Editor,Date,Content")] Texts text)
        {
            if (ModelState.IsValid)
            {
                db.Entry(text).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("TextList");
            }
            return View(text);
        }

        // GET: Texts/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Texts text = await db.Texts.FindAsync(id);
            if (text == null)
            {
                return HttpNotFound();
            }
            return View(text);
        }

        // POST: Texts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Texts text = await db.Texts.FindAsync(id);
            db.Texts.Remove(text);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult TextPage(string TextId)
        {
            Texts text = db.Texts.Find(TextId);
            ViewData["id"] = text.Id;
            ViewData["Title"] = text.Title;
            ViewData["Editor"] = text.Editor;
            ViewData["Content"] = text.Content;
            ViewData["Date"] = text.Date;
            return View();
        }

        public async Task<ActionResult> TextList()
        {
            return View(await db.Texts.ToListAsync());
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
