using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EDiary.Models
{
    public class TeacherTeachSubjectToSchoolClass
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("TeacherTeachSubject")]
        public int TeacherTeachSubjectId { get; set; }
        public virtual TeacherTeachSubject TeacherTeachSubject { get; set; }

        [Required]
        [ForeignKey("SchoolClass")]
        public string SchoolClassId { get; set; }
        public virtual SchoolClass SchoolClass { get; set; }

        /*[ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }*/

        [JsonIgnore]
        public virtual ICollection<Lecture> Lectures { get; set; }

        
        public virtual ICollection<TeacherTeachSubjectToSchoolClassToStudent> TeacherTeachSubjectToSchoolClassToStudents { get; set; }

        public TeacherTeachSubjectToSchoolClass()
        {
            Lectures = new List<Lecture>();
            TeacherTeachSubjectToSchoolClassToStudents = new List<TeacherTeachSubjectToSchoolClassToStudent>();
        }
    }
}