using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FundooModels
{
    public class LabelNameModel
    {
        [Key]
        public long LabelId { get; set; }
        public string LabelName { get; set; }
    }
}
