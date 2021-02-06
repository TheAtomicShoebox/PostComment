using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostComment.Data
{
    public class Reply : Comment
    {
        [Required]
        [ForeignKey(nameof(Comment))]
        public int ParentCommentId { get; set; }

        public virtual Comment Comment { get; set; }
        
        public Reply() : base()
        {

        }
    }
}
