using PostComment.Data;
using System;
using System.Collections.Generic;

namespace PostComment.Models
{
    public class CommentMessage
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Guid Author { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public virtual ICollection<Reply> ReplyChain { get; set; }

        public CommentMessage()
        {
            ReplyChain = new List<Reply>();
        }

        public CommentMessage(Comment comment)
        {
            Id = comment.Id;
            Author = comment.Author;
            Text = comment.Text;
            PostId = comment.PostId;
            Post = comment.Post;
            ReplyChain = comment.ReplyChain;
        }
    }
}
