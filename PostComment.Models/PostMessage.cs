using PostComment.Data;
using System;
using System.Collections.Generic;

namespace PostComment.Models
{
    public class PostMessage
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public virtual List<Comment> Comment { get; set; }
        public Guid Author { get; set; }

        public PostMessage()
        {

        }

        public PostMessage(Post post)
        {
            Id = post.Id;
            Title = post.Title;
            Text = post.Text;
            Comment = post.Comment;
            Author = post.Author;
        }
    }
}
