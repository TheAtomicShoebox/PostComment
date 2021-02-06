using PostComment.Data;
using PostComment.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostComment.Models
{
    public class CommentService
    {
        private readonly Guid _userId;

        public CommentService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateComment(Post post, CommentCreate model)
        {
            var entity =
                new Comment()
                {
                    Text = model.Text,
                    Author = _userId,
                    Post = post,
                    PostId = post.Id
                };
            using(var ctx = new ApplicationDbContext())
            {
                var postEntity =
                    ctx
                        .Posts
                        .Single(e => e.Author == _userId && e.Id == entity.PostId);

                postEntity.Comment.Add(entity);
                ctx.Comments.Add(entity);

                return ctx.SaveChanges() == 2;
            }
        }

        public IEnumerable<Comment> GetComments()
        {
            using(var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Comments
                        .Where(e => e.Author == _userId)
                        .Select(e => new Comment()
                        {
                            Id = e.Id,
                            Author = e.Author,
                            Post = e.Post,
                            PostId = e.PostId,
                            Text = e.Text,
                            ReplyChain = e.ReplyChain
                        });
                return query.ToArray();
            }
        }

        public Comment GetCommentById(int id)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Comments
                        .Single(e => e.Author == _userId && e.Id == id);
                return new Comment
                {
                    Id = entity.Id,
                    Author = entity.Author,
                    Post = entity.Post,
                    PostId = entity.PostId,
                    ReplyChain = entity.ReplyChain,
                    Text = entity.Text
                };
            }
        }

        public List<Reply> GetReplies(int id)
        {
            var comment = GetCommentById(id);
            return comment.ReplyChain;
        }
    }
}
