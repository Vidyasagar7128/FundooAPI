using System;
using System.Collections.Generic;
using System.Text;

namespace FundooModels
{
    public class NoteShareModel
    {
        public long NoteId { get; set; }
        public long UserId { get; set; }
        public string Email { get; set; }
    }
}
