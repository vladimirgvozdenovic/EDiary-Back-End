using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EDiary.Models
{
    public class SchoolClass
    {
        [Key]
        [RegularExpression(@"^[1-8]\-[1-9]\-(19\d{2}|2\d{3})$", ErrorMessage = "Format is not valid. Required format: 8-3-2018")]
        public string Id { get; set; }
        [Required]
        public SchoolYearEnum SchoolYear { get; set; }
        [Required]
        public int ClassNumber { get; set; }
        [Required]
        [RegularExpression(@"^(19\d{2}|2\d{3})$", ErrorMessage = "Year format is not valid.")]
        public int CalendarYear { get; set; }

        //[Required]
        //[ForeignKey("HeadTeacher")]
        //public int HeadTeacherId { get; set; }
        //[Required]
        //[Column("HeadTeacher")]
        [Required]
        public virtual Teacher HeadTeacher { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        [JsonIgnore]
        public virtual ICollection<TeacherTeachSubjectToSchoolClass> TeacherTeachSubjectToSchoolClasses { get; set; }

        public SchoolClass()
        {
            Students = new List<Student>();
            TeacherTeachSubjectToSchoolClasses = new List<TeacherTeachSubjectToSchoolClass>();
        }
    }
}