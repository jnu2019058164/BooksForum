using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using asp_test.Models;
using System.Data.SqlClient;

namespace asp_test.Controllers
{
    public class HomeController : Controller
    {
        private TestASPEntities db = new TestASPEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {

            ViewBag.Message = "Your contact page.";
            
            return View();
        }

        public ActionResult Books()
        {
            ViewBag.Message = "This is books.";
            return View();
        }

        public ActionResult Books_download(string BookId)
        {
            Books bks = db.Books.Find(BookId);
            if (bks != null) {               
                ViewData["Categories"] = bks.Categories;
                ViewData["Year"] = bks.Year;
                ViewData["Edition"] = bks.Edition;
                ViewData["Publisher"] = bks.Publisher;
                ViewData["Language"] = bks.Language;
                ViewData["Pages"] = bks.Pages;
                ViewData["ISBN10"] = bks.ISBN_10;
                ViewData["ISBN13"] = bks.ISBN_13;
                ViewData["File"] = "/statics/Books/" + bks.File;
                ViewData["BookName"] = bks.BookName;
                ViewData["Cover"] = "/statics/Cover/" + bks.Cover;
                ViewData["Author"] = bks.Author;
                return View();
            }
            else
            {
                return View();
            }           
        }
    }
}