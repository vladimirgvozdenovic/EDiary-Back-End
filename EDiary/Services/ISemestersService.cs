using EDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDiary.Services
{
    public interface ISemestersService
    {
        IEnumerable<Semester> GetSemesters();
        Semester GetSemesterById(SemesterEnum id);
        Semester PostSemester(Semester semester);
        bool PutSemester(SemesterEnum id, Semester semester);
        Semester DeleteSemester(SemesterEnum id);
    }
}
