using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostComment.Data
{
    class Reply : Comment
    {
    }

    public Reply(int id, string text, Guid _userID)
    {

    }
}
