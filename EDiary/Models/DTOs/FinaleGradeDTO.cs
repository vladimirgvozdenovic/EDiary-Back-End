using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EDiary.Models.DTOs
{
    public class FinaleGradeDTO
    {
        public int GradeId { get; set; }
        [Required]
        public MarkEnum Mark { get; set; }
        //public DateTime Date { get; set; }
        [Required]
        public int TeacherTeachSubjectToSchoolClassToStudentId { get; set; }
    }
}