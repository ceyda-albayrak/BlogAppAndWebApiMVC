using BlogMvcApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogMvcApp.Controllers
{
    public class HomeController : Controller
    {
        private BlogContext db = new BlogContext();
        public ActionResult Index()
        {
           
            return View(db.Blogs.ToList());
        }

        
    }
}