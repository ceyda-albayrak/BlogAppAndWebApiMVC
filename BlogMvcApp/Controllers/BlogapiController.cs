using BlogMvcApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace BlogMvcApp.Controllers
{

    public class BlogapiController : ApiController
    {
        private BlogContext db = new BlogContext();

        [HttpGet]
        public IEnumerable<Blog> GetBlogs()
        {
            return db.Blogs.ToList();
        }

        [HttpGet]
        public Blog GetBlogs(int id)
        {
            return db.Blogs.Find(id);
        }

        [HttpPost]
        public HttpResponseMessage Add(Blog model)
        {
            try
            {
                db.Blogs.Add(model);
                db.SaveChanges();
                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.Created);
                return message;
            }
            catch (Exception ex)
            {
                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return message;
            }
        }

        [HttpPut]
        public HttpResponseMessage Update(int id, Blog model)
        {
            try
            {
                if (id == model.BlogID)
                {
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
                    return message;
                }
                else
                {
                    HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.NotModified);
                    return message;
                }

            }

            catch (Exception ex)
            {
                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return message;
            }

        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {

            Blog model = db.Blogs.Find(id);
            if (model != null)
            {
                db.Blogs.Remove(model);
                db.SaveChanges();
                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
                return message;
            }
            else
            {
                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return message;
            }


        }
    }
}
