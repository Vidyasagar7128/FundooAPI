// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaborationModel.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Gaikwad Vidyasagar"/>
// --------------------------------------------------------------------------------------------------------------------
namespace FundooModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    public class CollaboratorModel
    {
        [Key]
        public long CollaboratorId { get; set; }
        public string Email { get; set; }
        public long NoteId { get; set; }
        [ForeignKey("NoteId")]
        public virtual NotesModel Note { get; set; }
        public long SenderId { get; set; }
        public long? ReceiverId { get; set; }
        [ForeignKey("ReceiverId")]
        public virtual SignUpModel User { get; set; }
    }
}
