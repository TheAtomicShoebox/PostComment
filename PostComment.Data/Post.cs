﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
      
        public virtual ICollection<Comment> Comment { get; set; }

        [Required]
        public Guid Author { get; set; }

        public Post()
        {
            Comment = new List<Comment>();
        }
    }
}
