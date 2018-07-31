using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EDiary.Models
{
    public class TeacherTeachSubjectToSchoolClassToStudentAtSemester
    {
        //[ForeignKey("FinaleSemesterGrade")]
        public int Id { get; set; }

        //[ForeignKey("FinaleSemesterGrade")]
        //public int FinaleSemesterGradeId { get; set; }
        //[Column("FinaleSemesterGrade")]
        public virtual FinaleSemesterGrade FinaleSemesterGrade { get; set; }

        [Required]
        [ForeignKey("TeacherTeachSubjectToSchoolClassToStudent")]
        public int TeacherTeachSubjectToSchoolClassToStudentId { get; set; }
        public virtual TeacherTeachSubjectToSchoolClassToStudent TeacherTeachSubjectToSchoolClassToStudent { get; set; }

        [Required]
        [ForeignKey("Semester")]
        public SemesterEnum SemesterName { get; set; }
        public virtual Semester Semester { get; set; }

        
        public virtual ICollection<SemesterGrade> SemesterGrades { get; set; }

        public TeacherTeachSubjectToSchoolClassToStudentAtSemester()
        {
            SemesterGrades = new List<SemesterGrade>();
        }
    }
}