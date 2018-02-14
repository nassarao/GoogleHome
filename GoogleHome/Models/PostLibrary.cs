using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace GoogleHome.Models
{
    public class PostLibrary
    {
        [XmlArray("Posts")]
        public List<Post> Posts { get; set; }
        public PostLibrary()
        {

        }
    }

    public class Post
    {
        public string Date { get; set; }

        public string Message { get; set; }
    }
}