using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModels
{
    public class CreateLabelModel
    {
        [Key]
        public long Id { get; set; }
        public string LabelName { get; set; }
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual SignUpModel User { get; set; }
    }
}
