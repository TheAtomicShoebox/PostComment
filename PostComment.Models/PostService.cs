using PostComment.Data;
using PostComment.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PostComment.Models
{
    public class PostService
    {
        private readonly Guid _userId;

        public PostService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreatePost(PostCreate model)
        {
            var entity =
                new Post()
                {
                    Title = model.Title,
                    Text = model.Text,
                    Author = _userId,
                    Comment = new List<Comment>()
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Posts.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<PostMessage> GetPosts()
        {
            using(var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Posts
                        .Where(e => e.Author == _userId)
                        .Select(e => new PostMessage()
                        {
                            Title = e.Title,
                            Text = e.Text,
                            Author = e.Author,
                            Comment = e.Comment,
                            Id = e.Id
                        });
                return query.ToArray();
            }
        }

        public PostMessage GetPostById(int id)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Posts
                        .Single(e => e.Author == _userId && e.Id == id);
                return new PostMessage
                {
                    Id = entity.Id,
                    Author = entity.Author,
                    Comment = entity.Comment,
                    Text = entity.Text,
                    Title = entity.Title
                };
            }
        }
    }
}
