using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FundooModels
{
    public class SignUpModel
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]{1,40}$",
         ErrorMessage = "Firstname is Invalid.")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]{1,40}$",
          ErrorMessage = "Lastname is Invalid.")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^[a-z]+[0-9]+[@]+[a-z]+[.]+[a-z]{3}$",
          ErrorMessage = "Email is Invalid.")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",
          ErrorMessage = "Password is Invalid.")]
        public string Password { get; set; }
    }
}
