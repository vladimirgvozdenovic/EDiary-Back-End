using EDiary.Models;
using EDiary.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDiary.Services
{
    public interface ISemesterGradesService
    {
        IEnumerable<SemesterGrade> GetSemesterGrades();
        SemesterGrade GetSemesterGradeById(int id);
        SemesterGrade PostSemesterGrade(SemesterGradeDTO semesterGradeDto);
        bool PutSemesterGrade(int id, SemesterGradeDTO semesterGradeDto);
        SemesterGrade DeleteSemesterGrade(int id);
        IEnumerable<SemesterGrade> GetSemesterGradesByDate(DateTime date);
        IEnumerable<SemesterGrade> GetSemesterGradesByTeacherTeachSubjectToSchoolClassToStudentAtSemester(int id);
    }
}
