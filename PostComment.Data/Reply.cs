using System;

namespace PostComment.Data
{
    public class Reply : Comment
    {
        public Reply(int id, string text, Guid _userId) : base(id, text, _userId)
        {

        }
    }
}
