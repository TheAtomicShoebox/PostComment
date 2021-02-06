using Microsoft.AspNet.Identity;
using PostComment.Models;
using System;
using System.Web.Http;

namespace PostComment.WebAPI.Controllers
{
    public class ReplyController : ApiController
    {
        private ReplyService CreateReplyService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var replyService = new ReplyService(userId);
            return replyService;
        }

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

        // POST api/Reply
        public IHttpActionResult Post(ReplyCreate reply)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var postService = CreatePostService();
            var commentService = CreateCommentService();
            var replyService = CreateReplyService();

            var post = postService.GetPostById(reply.PostId);
            var comment = commentService.GetCommentById(reply.ParentCommentId);
            if (!replyService.CreateReply(post, comment, reply))
                return InternalServerError();

            return Ok();
        }
    }
}