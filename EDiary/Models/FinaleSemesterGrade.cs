using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EDiary.Models
{
    [Table("FinaleSemesterGrades")]
    public class FinaleSemesterGrade : Grade
    {
        [Required]
        public virtual TeacherTeachSubjectToSchoolClassToStudentAtSemester TeacherTeachSubjectToSchoolClassToStudentAtSemester { get; set; }

        public FinaleSemesterGrade() : base()
        {

        }
    }
}