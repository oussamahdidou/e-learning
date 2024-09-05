using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.generique;
using api.helpers;
using api.Model;
namespace api.interfaces
{
    public interface IInstitutionStudentRepository
    {
        Task Add(InstitutionStudent institutionStudent);   
    }
}