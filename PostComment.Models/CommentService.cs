using PostComment.Data;
using PostComment.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PostComment.Models
{
    public class CommentService
    {
        private readonly Guid _userId;

        public CommentService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateComment(PostMessage post, CommentCreate model)
        {
            var entity =
                new Comment()
                {
                    Text = model.Text,
                    Author = _userId,
                    Post = new Post()
                    {
                        Author = post.Author,
                        Id = post.Id,
                        Comment = post.Comment,
                        Text = post.Text,
                        Title = post.Title
                    },
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

        public CommentMessage GetCommentById(int id)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Comments
                        .Single(e => e.Author == _userId && e.Id == id);
                return new CommentMessage
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
