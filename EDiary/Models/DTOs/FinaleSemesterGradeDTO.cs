using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EDiary.Models.DTOs
{
    public class FinaleSemesterGradeDTO
    {
        public int GradeId { get; set; }
        [Required]
        public MarkEnum Mark { get; set; }
        [Required]
        public int TeacherTeachSubjectToSchoolClassToStudentAtSemesterId { get; set; }
    }
}