using System;
using System.Collections.Generic;

namespace TabloidCLI.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime PublishDateTime { get; set; }
        public Author Author { get; set; }
        public Blog Blog { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();

        public override string ToString()
        {
            return $"{Title} ({PublishDateTime.ToShortDateString()}) - {Url}";
        }
    }
}
