using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EDiary.Models
{
    public class Subject
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public SchoolYearEnum SchoolYear { get; set; }
        public int WeeklyLectureQuantity { get; set; }       

        /*[JsonIgnore]
        public virtual ICollection<TeacherTeachSubjectToSchoolClass> TeacherTeachSubjectToSchoolClasses { get; set; }*/

        [JsonIgnore]
        public virtual ICollection<TeacherTeachSubject> TeacherTeachSubjects { get; set; } 

        
        public virtual ICollection<Lesson> Lessons { get; set; }

        public Subject()
        {
            //TeacherTeachSubjectToSchoolClasses = new List<TeacherTeachSubjectToSchoolClass>();
            TeacherTeachSubjects = new List<TeacherTeachSubject>();
            Lessons = new List<Lesson>();
        }
    }
}