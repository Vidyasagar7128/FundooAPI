using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModels
{
    public class LabelModel
    {
        [Key]
        public long LabelId { get; set; }
        public string Name { get; set; }
        public long? NoteId { get; set; }
        [ForeignKey("NoteId")]
        public virtual NotesModel Note { get; set; }
        public long? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual SignUpModel User { get; set; }
    }
}
