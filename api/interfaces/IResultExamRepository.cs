using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.generique;
using api.Model;

namespace api.interfaces
{
    public interface IResultExamRepository
    {
        Task<Result<ResultExam>> AddResult(AppUser user , int controleId , string filePath);
        Task<Result<ResultExam>> RemoveResult(AppUser user , int controleId);
        Task<Result<ResultExam>> GetResultExamById(AppUser user, int controleId);       
    }
} 