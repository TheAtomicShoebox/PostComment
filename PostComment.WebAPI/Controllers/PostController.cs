using Microsoft.AspNet.Identity;
using PostComment.Models;
using System;
using System.Web.Http;

namespace PostComment.WebAPI.Controllers
{
    public class PostController : ApiController
    {
        private PostService CreatePostService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var postService = new PostService(userId);
            return postService;
        }
        
        // Get api/Post
        [Authorize]
        public IHttpActionResult Get()
        {
            PostService postService = CreatePostService();
            var posts = postService.GetPosts();
            return Ok(posts);
        }

        [Authorize]
        // Get api/Post/:PostId
        public IHttpActionResult Get(int id)
        {
            PostService postService = CreatePostService();
            var post = postService.GetPostById(id);
            return Ok(post);
        }

        [Authorize]
        // POST api/Post
        public IHttpActionResult Post(PostCreate post)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePostService();

            if (!service.CreatePost(post))
                return InternalServerError();

            return Ok();
        }
    }
}