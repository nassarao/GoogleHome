using GoogleHome.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace GoogleHome.Controllers
{
    public class LuluController : Controller
    {
        // GET: Lulu
        public ActionResult Index()
        {
            LuluVM vm = new LuluVM()
            {
                PostLibrary = getPosts()
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult addPost(LuluVM vm)
        {
            Post newPost = new Post()
            {
                Date = DateTime.Now.ToShortDateString(),
                Message = vm.NewPost 
            };
            
            XDocument doc = XDocument.Load(Server.MapPath("~/App_Data/LuluPosts.xml"));
            XElement library = doc.Element("PostLibrary");
            XElement posts = library.Element("Posts");
            XElement post = new XElement("Post");
            post.Add(new XElement("Date", newPost.Date));
            post.Add(new XElement("Message",newPost.Message ));
            posts.Add(post);
            doc.Save(Server.MapPath("~/App_Data/LuluPosts.xml"));

            return RedirectToAction("index");
        }

        private PostLibrary getPosts()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PostLibrary));
            using (StreamReader reader = new StreamReader(Server.MapPath("~/App_Data/LuluPosts.xml")))
            {
                PostLibrary posts = (PostLibrary)serializer.Deserialize(reader);
                return posts;
            }

        }
    }
}