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
    public class TutorialsController : Controller
    {
        private TestASPEntities db = new TestASPEntities();

        // GET: Tutorials
        public async Task<ActionResult> Index()
        {
            return View(await db.Tutorials.ToListAsync());
        }

        // GET: Tutorials/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutorials tutorials = await db.Tutorials.FindAsync(id);
            if (tutorials == null)
            {
                return HttpNotFound();
            }
            return View(tutorials);
        }

        // GET: Tutorials/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tutorials/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TId,Link,About")] Tutorials tutorials)
        {
            if (ModelState.IsValid)
            {
                db.Tutorials.Add(tutorials);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tutorials);
        }

        // GET: Tutorials/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutorials tutorials = await db.Tutorials.FindAsync(id);
            if (tutorials == null)
            {
                return HttpNotFound();
            }
            return View(tutorials);
        }

        // POST: Tutorials/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TId,Link,About")] Tutorials tutorials)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tutorials).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tutorials);
        }

        // GET: Tutorials/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutorials tutorials = await db.Tutorials.FindAsync(id);
            if (tutorials == null)
            {
                return HttpNotFound();
            }
            return View(tutorials);
        }

        // POST: Tutorials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Tutorials tutorials = await db.Tutorials.FindAsync(id);
            db.Tutorials.Remove(tutorials);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> TList()
        {
            return View(await db.Tutorials.ToListAsync());
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
