using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EDiary.Models
{
    public class Lecture
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [StringLength(500)]
        public String Topic { get; set; }

        [Required]
        [ForeignKey("Lesson")]
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }

        [Required]
        [ForeignKey("TeacherTeachSubjectToSchoolClass")]
        public int TeacherTeachSubjectToSchoolClassId { get; set; }
        public virtual TeacherTeachSubjectToSchoolClass TeacherTeachSubjectToSchoolClass { get; set; }

        [JsonIgnore]
        public virtual ICollection<StudentsAbsence> Absence { get; set; }

        public Lecture()
        {
            Absence = new List<StudentsAbsence>();
        }
    }
}