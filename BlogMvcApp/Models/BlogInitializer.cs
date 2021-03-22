using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogMvcApp.Models
{
    public class BlogInitializer:DropCreateDatabaseIfModelChanges<BlogContext>
    {
        protected override void Seed(BlogContext context)
        {
            List<Category> categories = new List<Category>()
            {
                new Category()
                {
                    CategoryName="ksjdfhsdf"
                }
            };
            foreach(var item in categories)
            {
                context.Categories.Add(item);
            }

            context.SaveChanges();

            List<Blog> blogs = new List<Blog>()
            {
                new Blog()
                {
                    Title="dkljfsf", Confirmation=true,Contentt="kdjsfsdf",Description="ldskjfhsd",DateofAdded=DateTime.Now
                }
            };
            foreach ( var item in blogs)
            {
                context.Blogs.Add(item);
            }
            context.SaveChanges();

            base.Seed(context);
        }
    }
}