using PostComment.Data;
using PostComment.WebAPI.Models;
using System;

namespace PostComment.Models
{
    public class ReplyService
    {
        private readonly Guid _userId;

        public ReplyService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateReply(PostMessage post, CommentMessage comment, ReplyCreate model)
        {
            var entity =
                new Reply()
                {
                    Author = _userId,
                    Comment = new Comment()
                    {
                        Id = comment.Id,
                        Author = comment.Author,
                        Post = comment.Post,
                        PostId = comment.PostId,
                        ReplyChain = comment.ReplyChain,
                        Text = comment.Text
                    },
                    ParentCommentId = comment.Id,
                    Post = new Post()
                    {
                        Author = post.Author,
                        Comment = post.Comment,
                        Id = post.Id,
                        Text = post.Text,
                        Title = post.Title
                    },
                    PostId = post.Id,
                    Text = model.Text
                };

            using(var ctx = new ApplicationDbContext())
            {
                comment.ReplyChain.Add(entity);
                ctx.Comments.Add(entity);

                return ctx.SaveChanges() == 2;
            }
        }
    }
}
