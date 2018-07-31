using EDiary.Models;
using EDiary.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDiary.Services
{
    public interface IFinaleGradesService
    {
        IEnumerable<FinaleGrade> GetFinaleGrades();
        FinaleGrade GetFinaleGradeById(int id);
        FinaleGrade PostFinaleGrade(FinaleGradeDTO finaleGradeDto);
        bool PutFinaleGrade(int id, FinaleGradeDTO finaleGradeDto);
        FinaleGrade DeleteFinaleGrade(int id);
        IEnumerable<FinaleGrade> GetFinaleGradesByDate(DateTime date);
    }
}
