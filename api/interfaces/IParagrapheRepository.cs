using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.generique;
using api.Model;

namespace api.interfaces
{
    public interface IParagrapheRepository
    {
        Task<Result<Paragraphe>> GetCourById(int id);
    }
}