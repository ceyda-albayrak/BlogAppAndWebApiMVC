using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogMvcApp.Models;

namespace BlogMvcApp.Controllers
{
    public class BlogsController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: Blogs
        public ActionResult Index()
        {
            var blogs = db.Blogs.Include(b => b.Category);
            return View(blogs.ToList());
        }

        public ActionResult List(int? id, string q)
        {
            var query = from p in db.Blogs
                        select new { BlogID=p.BlogID,Title=p.Title,Description=p.Description,DateofAdded=p.DateofAdded,Home=p.Home,Confirmation=p.Confirmation,Contentt=p.Contentt,CategoryID=p.CategoryID };
            var blogs = query.ToList().Select(i => new Blog
            {
                BlogID = i.BlogID,
                Title=i.Title,
                Description=i.Description,
                DateofAdded=i.DateofAdded,
                Home=i.Home,
                Confirmation=i.Confirmation,
                Contentt=i.Contentt,
                CategoryID=i.CategoryID
               
            }).AsQueryable() ;

            if (!String.IsNullOrEmpty(q))
            {
                blogs = blogs.Where(s => s.Title.Contains(q) || s.Description.Contains(q));
            }
            if (id!=null)
            {
                blogs = blogs.Where(i => i.CategoryID == id);
            }

                
            return View(blogs);
        }

        // GET: Blogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: Blogs/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Blogs/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BlogID,Title,Description,Contentt,Confirmation,Home,CategoryID")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                blog.DateofAdded = DateTime.Now;
                db.Blogs.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", blog.CategoryID);
            return View(blog);
        }

        // GET: Blogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", blog.CategoryID);
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BlogID,Title,Description,Contentt,Confirmation,Home,CategoryID")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                var entity = db.Blogs.Find(blog.BlogID);
                if (entity != null)
                {
                    entity.Title = blog.Title;
                    entity.Description = blog.Description;
                    entity.Contentt = blog.Contentt;
                    entity.Confirmation = blog.Confirmation;
                    entity.Home = blog.Home;
                    entity.CategoryID = blog.CategoryID;
                    db.SaveChanges();
                    TempData["Blog"] = entity;
                    return RedirectToAction("Index");
                };

                
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", blog.CategoryID);
            return View(blog);
        }

        public PartialViewResult BlogList()
        {
            return PartialView(db.Blogs.ToList());
        }


        // GET: Blogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Blogs.Find(id);
            db.Blogs.Remove(blog);
            db.SaveChanges();
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
    }
}
