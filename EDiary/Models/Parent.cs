using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EDiary.Models
{
    //[Table("Parents")]
    public class Parent : User
    {
        //public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public override string Email { get; set; }
        [RegularExpression(@"^([+]381|00381|0)6[0-9](\s?|-|\/)([0-9]{6,7})$", ErrorMessage = "Phone number format is not valid.")]
        public override string PhoneNumber { get; set; }

        [JsonIgnore]
        public virtual ICollection<Student> Students { get; set; }

        public Parent() : base()
        {
            Students = new List<Student>();
        }
    }
}