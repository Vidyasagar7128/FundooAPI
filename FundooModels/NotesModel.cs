using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FundooModels
{
    public class NotesModel
    {
        [Key]
        public long NotesId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Reminder { get; set; }
        [DefaultValue("white")]
        public string Theme { get; set; }
        [DefaultValue("false")]
        public bool Archive { get; set; }
        [DefaultValue("false")]
        public bool Pin { get; set; }
    }
}
