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
    public interface IParentsService
    {
        IEnumerable<ParentDTO> GetParents();
        ParentDTO GetParentById(string id);
        Task<IdentityResult> PostParent(ParentDTO parentDto);
        bool PutParent(string id, ParentDTO parentDto);
        Parent DeleteParent(string id);
        Parent PutStudent(string id, string StudentId);
        Parent GetParentByUsername(string username);
        IEnumerable<ParentDTO> GetParentsByName(string pattern);
        IEnumerable<Student> GetParentStudents(string id);
    }
}
