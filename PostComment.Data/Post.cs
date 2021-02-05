using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostComment.Data
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }
        public virtual List<Comment> Comment { get; set; }

        [Required]
        public Guid Author { get; set; }

        public Post(int id, string text, Guid _userId)
        {
            Comment = new List<Comment>();

        }
    }
}
