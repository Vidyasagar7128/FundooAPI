using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModels
{
    public class ImageModel
    {
        [Key]
        public long Id { get; set; }
        public string ImageUrl { get; set; }
        [ForeignKey("NoteId")]
        public long NoteId { get; set; }
        [ForeignKey("UserId")]
        public long UserId { get; set; }
    }
}
