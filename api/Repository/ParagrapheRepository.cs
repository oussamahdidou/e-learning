using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.generique;
using api.interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ParagrapheRepository : IParagrapheRepository
    {
         private readonly apiDbContext apiDbContext;
        public ParagrapheRepository(apiDbContext apiDbContext)
        {
            this.apiDbContext = apiDbContext;
        }


        public async Task<Result<Paragraphe>> GetCourById(int id)
        {
            Paragraphe? paragraphe = await apiDbContext.paragraphes.FirstOrDefaultAsync(x => x.Id == id);

            if(paragraphe == null)
            {
                return Result<Paragraphe>.Failure("Cour Not Found");
            }

            return Result<Paragraphe>.Success(paragraphe);
        }
    }
}