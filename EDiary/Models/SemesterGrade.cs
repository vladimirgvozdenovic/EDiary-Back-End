using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EDiary.Models
{
    [Table("SemesterGrades")]
    public class SemesterGrade : Grade
    {
        //public int Id { get; set; }

        //[ForeignKey("TeacherTeachSubjectToSchoolClassToStudentAtSemester")]
        //public int TeacherTeachSubjectToSchoolClassToStudentAtSemesterId { get; set; }
        //[Required]
        //[Column("TeacherTeachSubjectToSchoolClassToStudentAtSemester")]
        [Required]
        public virtual TeacherTeachSubjectToSchoolClassToStudentAtSemester TeacherTeachSubjectToSchoolClassToStudentAtSemester { get; set; }

        public SemesterGrade() : base()
        {

        }
    }
}