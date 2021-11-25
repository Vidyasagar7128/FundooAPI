using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModels
{
    class CollaboratorModel
    {
        [Key]
        public long CollaborateId { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        [ForeignKey("UserId")]
        public long UserId { get; set; }
    }
}
