using EDiary.Models;
using EDiary.Models.DTOs;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDiary.Services
{
    interface IUserService
    {
        Task<IdentityResult> RegisterTeacher(TeacherDTO teacherDto);
        Task<IdentityResult> RegisterStudent(StudentDTO studentDto);
        Task<IdentityResult> RegisterParent(ParentDTO parentDto);
        Task<IdentityResult> RegisterAdmin(AdminUser admin);
    }
}
