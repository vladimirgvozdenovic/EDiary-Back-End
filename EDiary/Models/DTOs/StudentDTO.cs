using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EDiary.Models.DTOs
{
    public class StudentDTO
    {
        public string UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string ParentId { get; set; }
        [Required]
        [RegularExpression(@"^[1-8]\-[1-9]\-(19\d{2}|2\d{3})$", ErrorMessage = "Format is not valid. Required format: 8-3-2018")]
        public string SchoolClassId { get; set; }
        public SchoolClass SchoolClass { get; set; }
        public ICollection<TeacherTeachSubjectToSchoolClassToStudent> TeacherTeachSubjectToSchoolClassToStudents { get; set; }
        public ICollection<StudentsAbsence> StudentsAbsences { get; set; }
    }
}