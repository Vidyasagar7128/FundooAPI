using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModels
{
    public class NotesModel
    {
        [Key]
        public long NoteId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Image { get; set; }
        public string Reminder { get; set; }
        [DefaultValue("white")]
        public string Theme { get; set; }
        [DefaultValue(0)]
        public int Status { get; set; }
        [DefaultValue("false")]
        public bool Pin { get; set; }
        public long? LabelId { get; set; }
        [ForeignKey("LabelId")]
        public virtual CreateLabelModel Label { get; set; }
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual SignUpModel User { get; set; }
    }
}
