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

        public bool CreateReply(Post post, Comment comment, ReplyCreate model)
        {
            var entity =
                new Reply()
                {
                    Author = _userId,
                    Comment = comment,
                    ParentCommentId = comment.Id,
                    Post = post,
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
