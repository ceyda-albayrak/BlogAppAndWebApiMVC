using BlogMvcApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;


namespace BlogMvcApp.Controllers
{
    public class CategoryapiController : ApiController
    {
        private BlogContext db = new BlogContext();

        [HttpGet]
        public IEnumerable<Category> GetCategories()
        {
            return db.Categories.ToList();
        }
        
        [HttpGet]
        public Category GetCategory(int id)
        {
            return db.Categories.Find(id);
        }
        
        [HttpPost]
        public HttpResponseMessage Add(Category model)
        {
            try
            {
                db.Categories.Add(model);
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
        public HttpResponseMessage Update(int id, Category model)
        {
            try
            {
                if(id==model.CategoryID)
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
            catch(Exception ex)
            {
                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return message;
            }
        }
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            Category model = db.Categories.Find(id);
            if(model != null)
            {
                db.Categories.Remove(model);
                db.SaveChanges();
                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
                return message;
            }
            else
            {
                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.NotFound);
                return message;

            }
        }


    }

}
