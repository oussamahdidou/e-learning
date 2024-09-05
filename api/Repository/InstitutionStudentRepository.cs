using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.extensions;
using api.generique;
using api.helpers;
using api.interfaces;
using api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
namespace api.Repository
{
    public class InstitutionStudentRepository : IInstitutionStudentRepository
    {
        private readonly apiDbContext _context;
        public InstitutionStudentRepository(apiDbContext context)
        {
            _context = context;
        }
        public async Task Add(InstitutionStudent institutionStudent)
        {
            _context.institutionStudents.Add(institutionStudent);
            await _context.SaveChangesAsync();
        }
    }
}