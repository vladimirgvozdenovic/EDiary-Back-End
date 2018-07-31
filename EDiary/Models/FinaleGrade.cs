using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EDiary.Models
{
    [Table("FinaleGrades")]
    public class FinaleGrade : Grade
    {
        //public int Id { get; set; }

        //[ForeignKey("TeacherTeachSubjectToSchoolClassToStudent")]
        //public int TeacherTeachSubjectToSchoolClassToStudentId { get; set; }
        //[Required]
        //[Column("TeacherTeachSubjectToSchoolClassToStudent")]
        [Required]
        public virtual TeacherTeachSubjectToSchoolClassToStudent TeacherTeachSubjectToSchoolClassToStudent { get; set; }

        public FinaleGrade() : base()
        {

        }

    }
}