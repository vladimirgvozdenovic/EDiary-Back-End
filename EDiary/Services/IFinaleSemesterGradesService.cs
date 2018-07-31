using EDiary.Models;
using EDiary.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDiary.Services
{
    public interface IFinaleSemesterGradesService
    {
        IEnumerable<FinaleSemesterGrade> GetFinaleSemesterGrades();
        FinaleSemesterGrade GetFinaleSemesterGradeById(int id);
        FinaleSemesterGrade PostFinaleSemesterGrade(FinaleSemesterGradeDTO finaleSemesterGradeDto);
        bool PutFinaleSemesterGrade(int id, FinaleSemesterGradeDTO finaleSemesterGradeDto);
        FinaleSemesterGrade DeleteFinaleSemesterGrade(int id);
        IEnumerable<FinaleSemesterGrade> GetFinaleSemesterGradesByDate(DateTime date);
    }
}
