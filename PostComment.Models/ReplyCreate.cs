using System.ComponentModel.DataAnnotations;

namespace PostComment.Models
{
    public class ReplyCreate : CommentCreate
    {
        [Required]
        public int ParentCommentId { get; set; }
    }
}
