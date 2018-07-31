using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EDiary.Models
{
    public class StudentsAbsence
    {
        public int Id { get; set; }
        public bool Justified { get; set; }

        [Required]
        [ForeignKey("Student")]
        public string StudentId { get; set; }
        public Student Student { get; set; }

        [Required]
        [ForeignKey("Lecture")]
        public int LectureId { get; set; }
        public Lecture Lecture { get; set; }

        public StudentsAbsence()
        {

        }
    }
}