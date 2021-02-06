using System.ComponentModel.DataAnnotations;

namespace PostComment.Models
{
    public class CommentCreate
    {
        [Required]
        [MaxLength(8000)]
        public string Text { get; set; }

        [Required]
        public int PostId { get; set; }
    }
}
