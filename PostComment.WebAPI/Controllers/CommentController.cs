using Microsoft.AspNet.Identity;
using PostComment.Models;
using System;
using System.Web.Http;

namespace PostComment.WebAPI.Controllers
{
    public class CommentController : ApiController
    {
        private CommentService CreateCommentService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var commentService = new CommentService(userId);
            return commentService;
        }

        private PostService CreatePostService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var postService = new PostService(userId);
            return postService;
        }

        [Authorize]
        // POST api/Comment
        public IHttpActionResult Post(CommentCreate comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var postService = CreatePostService();
            var commentService = CreateCommentService();

            var post = postService.GetPostById(comment.PostId);
            if (!commentService.CreateComment(post, comment))
                return InternalServerError();

            return Ok();
        }

        [Authorize]
        // GET all api/Comment
        public IHttpActionResult Get()
        {
            var commentService = CreateCommentService();
            var comments = commentService.GetComments();
            return Ok(comments);
        }

        [Authorize]
        // GET by id api/Comment/:CommentId
        public IHttpActionResult Get(int id)
        {
            var commentService = CreateCommentService();
            var comment = commentService.GetCommentById(id);
            return Ok(comment);
        }

        [Authorize]
        // GET comment replies api/Comment/:CommentId/:replies
        [Route("api/Comment/{id}/{replies}")]
        [HttpGet]
        public IHttpActionResult GetCommentReplies([FromUri] int id, [FromUri] bool replies)
        {
            if (!replies)
                return Get(id);

            var commentService = CreateCommentService();
            var replyChain = commentService.GetReplies(id);
            return Ok(replyChain);
        }
    }
}