using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EDiary.Models
{
    public class TeacherTeachSubjectToSchoolClassToStudent
    {
        //[ForeignKey("FinaleGrade")]
        public int Id { get; set; }

        //[ForeignKey("FinaleGrade")]
        //public int FinaleGradeId { get; set; }
        //[Column("FinaleGrade")]
        public virtual FinaleGrade FinaleGrade { get; set; }

        [Required]
        [ForeignKey("TeacherTeachSubjectToSchoolClass")]
        public int TeacherTeachSubjectToSchoolClassId { get; set; }
        public virtual TeacherTeachSubjectToSchoolClass TeacherTeachSubjectToSchoolClass { get; set; }

        [Required]
        [ForeignKey("Student")]
        public string StudentId { get; set; }
        public virtual Student Student { get; set; }

        
        public virtual ICollection<TeacherTeachSubjectToSchoolClassToStudentAtSemester> TeacherTeachSubjectToSchoolClassToStudentAtSemesters { get; set; }

        public TeacherTeachSubjectToSchoolClassToStudent()
        {
            TeacherTeachSubjectToSchoolClassToStudentAtSemesters = new List<TeacherTeachSubjectToSchoolClassToStudentAtSemester>();
        }

    }
}