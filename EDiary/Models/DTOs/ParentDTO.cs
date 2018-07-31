using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EDiary.Models.DTOs
{
    public class ParentDTO
    {
        public string UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [RegularExpression(@"^([+]381|00381|0)6[0-9](\s?|-|\/)([0-9]{6,7})$", ErrorMessage = "Phone number format is not valid.")]
        public string PhoneNumber { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}