using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EDiary.Models
{
    public class TeacherTeachSubject
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Teacher")]
        public string TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

        [Required]
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }   
        public virtual Subject Subject { get; set; }

        
        public virtual ICollection<TeacherTeachSubjectToSchoolClass> TeacherTeachSubjectToSchoolClasses { get; set; }

        public TeacherTeachSubject()
        {
            TeacherTeachSubjectToSchoolClasses = new List<TeacherTeachSubjectToSchoolClass>();
        }
    }
}