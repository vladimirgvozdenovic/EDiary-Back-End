using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EDiary.Models
{
    //[Table("Students")]
    public class Student : User
    {
        //public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public override string Email { get; set; }

        [ForeignKey("Parent")]
        public string ParentId { get; set; }
        public virtual Parent Parent { get; set; }

        [Required]
        [ForeignKey("SchoolClass")]
        public string SchoolClassId { get; set; }   
        public virtual SchoolClass SchoolClass { get; set; }

        [JsonIgnore]
        public virtual ICollection<TeacherTeachSubjectToSchoolClassToStudent> TeacherTeachSubjectToSchoolClassToStudents { get; set; }

        [JsonIgnore]
        public virtual ICollection<StudentsAbsence> StudentsAbsences { get; set; }

        public Student() : base()
        {
            TeacherTeachSubjectToSchoolClassToStudents = new List<TeacherTeachSubjectToSchoolClassToStudent>();
            StudentsAbsences = new List<StudentsAbsence>();
        }
    }
}