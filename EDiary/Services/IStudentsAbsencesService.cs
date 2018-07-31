using EDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDiary.Services
{
    public interface IStudentsAbsencesService
    {
        IEnumerable<StudentsAbsence> GetStudentsAbsences();
        StudentsAbsence GetStudentsAbsenceById(int id);
        StudentsAbsence PostStudentsAbsence(StudentsAbsence studentsAbsence);
        bool PutStudentsAbsence(int id, StudentsAbsence studentsAbsence);
        StudentsAbsence DeleteStudentsAbsence(int id);
        IEnumerable<StudentsAbsence> GetStudentsAbsencesByStudent(string id);
        IEnumerable<StudentsAbsence> GetStudentsAbsencesByLecture(int id);
        IEnumerable<StudentsAbsence> GetStudentsAbsencesByDate(DateTime date);        
    }
}
