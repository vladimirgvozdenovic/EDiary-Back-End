using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EDiary.Models
{
    public class Semester
    {
        [Key]
        public SemesterEnum Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [JsonIgnore]
        public virtual ICollection<TeacherTeachSubjectToSchoolClassToStudentAtSemester> TeacherTeachSubjectToSchoolClassToStudentAtSemesters { get; set; }

        Semester()
        {
            TeacherTeachSubjectToSchoolClassToStudentAtSemesters = new List<TeacherTeachSubjectToSchoolClassToStudentAtSemester>();
        }

    }
}