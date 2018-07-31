using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EDiary.Models
{
    //[Table("Teachers")]
    public class Teacher : User
    {
        //public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public override string Email { get; set; }

        //[ForeignKey("HeadClass")]
        //public string HeadClassId { get; set; }
        //[Required]
        //[Column("HeadClass")]

        public virtual SchoolClass HeadClass { get;set;}

        [JsonIgnore]
        public virtual ICollection<TeacherTeachSubject> TeacherTeachSubjects { get; set; }

        public Teacher() : base()
        {
            TeacherTeachSubjects = new List<TeacherTeachSubject>();
        }
    }
}